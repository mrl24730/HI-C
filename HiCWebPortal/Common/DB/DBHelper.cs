using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebPortal.DB
{
    public static class DBHelper
    {
        #region Fields
        private static readonly string envir = (string)ConfigurationManager.AppSettings["Environment"];
        public static readonly string constr = (string)ConfigurationManager.ConnectionStrings["dB" + envir].ConnectionString;
        public static string defaultSKey = "ke8Sle93";
        public static string fixedIV = "k*Cee8$sjk32!g";

        #endregion


        public static IDbConnection CreateConnection()
        {
            return new SqlConnection(constr);
        }
    }
}
