using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebPortal.DB;
using Dapper;
using WebPortal.Common;

namespace WebPortal.DB
{
    public static class MessageRepository
    {
        public static Message GetValidMessageByPassCode(string passCode)
        {
            string sqlSelect = @"SELECT id, passCode, msgId, createdAtUTC, receivedAtUTC, deletedAtUTC, sender, receiver,
                                 ipAddress, isDeleted FROM tbl_message 
                                 WHERE passCode = @PassCode AND isDeleted = 0";
            

            using (IDbConnection conn = DBHelper.CreateConnection())
            {
                Message msg = conn.Query<Message>(sqlSelect, new { PassCode = passCode }).FirstOrDefault();

                if(msg != null && msg.IsFirstTimeRead())
                {
                    MarkReceived(conn, msg);
                }

                return msg;
            }
        }

        public static int AddMessagae(Message msgToAdd)
        {
            string sql = @"INSERT INTO tbl_message (passCode, msgId, createdAtUTC, sender, receiver, ipAddress, isDeleted)
                           SELECT @PassCode, @MsgId, @CreatedAtUTC, @Sender, @Receiver, @IpAddress, 0 
                           WHERE NOT EXISTS (SELECT passCode FROM tbl_message WHERE passCode = @PassCode AND isDeleted = 0)
                           SELECT @@IDENTITY AS id";

            using (IDbConnection conn = DBHelper.CreateConnection())
            {
                // return conn.Execute(sql, msgToAdd);
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    var affectedRows = conn.Execute(sql, msgToAdd, transaction: transaction);

                    transaction.Commit();

                    return affectedRows;
                }
            }

            return 0;
        }

        public static int DeleteExpiredMessaage(DateTime expiryAtUTC)
        {
            string sql = @"UPDATE tbl_message SET isDeleted = 1, deletedAtUTC = @Now WHERE receivedAtUTC <= @ExpiryAtUTC";

            using (IDbConnection conn = DBHelper.CreateConnection())
            {
                return conn.Execute(sql, new { ExpiryAtUTC = expiryAtUTC, Now = DateTime.UtcNow });
            }
        }

        private static bool MarkReceived(IDbConnection conn, Message msg)
        {
            string sql = @"UPDATE tbl_message SET receivedAtUTC = @Now WHERE id = @Id";

            int rowUpdated = conn.Execute(sql, new { Now = DateTime.UtcNow, Id = msg.Id });

            if (rowUpdated < 0) throw new InvalidOperationException();

            return true;
        }
    }
}
