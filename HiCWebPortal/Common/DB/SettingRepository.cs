using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebPortal.DB
{
    public static class SettingRepository
    {
        public static string GetSetting(string key)
        {
            string sqlSelect = @"SELECT value FROM tbl_setting WHERE id = @Key";


            using (IDbConnection conn = DBHelper.CreateConnection())
            {
                string value = conn.Query<string>(sqlSelect, new { Key = key}).FirstOrDefault();

                return value;
            }
        }
    }
}
