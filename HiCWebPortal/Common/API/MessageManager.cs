using Kitchen;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using WebPortal.DB;
using WebPortal.Models;

namespace WebPortal.Common
{
    public class MessageManager
    {
        private static readonly MessageManager _instance = new MessageManager();
        private static readonly int expiredAfterMins = int.Parse(ConfigurationManager.AppSettings["ExpiredAfterMins"]);

        private MessageManager() { }

        public static MessageManager Instance { get { return _instance; } }

        public StatusCode AddMessage(Message message, ref string error_message)
        {
            try
            {
                DeleteExpiredMessaage();

                int rowInsert = MessageRepository.AddMessagae(message);

                //Emailer.sendMail(new string[] { "it@kitchen-digital.com" }, "it@kitchen-digital.com", "Hi-C Insert Success", rowInsert.ToString(), "Hi-C Server", "103.26.121.220");

                if (rowInsert == 0)
                {
                    return StatusCode.FailInsertMessage;
                }

                return StatusCode.Success;
            } catch (Exception ex)
            {
                error_message = ex.Message;
                //Emailer.sendMail(new string[] { "it@kitchen-digital.com" }, "it@kitchen-digital.com", "Hi-C Insert Fail", ex.ToString(), "Hi-C Server", "103.26.121.220");
                return StatusCode.Fail;
            }

        }

        public MessageAPIModel GetMessage(string passCode)
        {
            DeleteExpiredMessaage();

            Message returnedMsg;

            try
            {
                returnedMsg = MessageRepository.GetValidMessageByPassCode(passCode);
            } catch (Exception ex)
            {
                return null;
            }

            if (returnedMsg == null)
            {
                return null;
            }

            MessageAPIModel msgResult = new MessageAPIModel
            {
                MsgId = WebUtility.HtmlEncode(returnedMsg.MsgId),
                Sender = WebUtility.HtmlEncode(returnedMsg.Sender),
                Receiver = WebUtility.HtmlEncode(returnedMsg.Receiver)
            };

            return msgResult;
        }

        private void DeleteExpiredMessaage()
        {
            MessageRepository.DeleteExpiredMessaage(DateTime.UtcNow.AddMinutes(expiredAfterMins * -1));
        }
    }
}
