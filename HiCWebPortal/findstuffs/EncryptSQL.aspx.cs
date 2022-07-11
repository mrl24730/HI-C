using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kitchen;

namespace WebPortal.findStuffs
{
    public partial class EncryptSQL : System.Web.UI.Page
    {

        public string en_sql;
        public string sql;
        public string r;
        public string result;
        public string fixedIV = "f1fa*c0k5";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            sql = Request["sql"];
            sql = HttpUtility.HtmlDecode(sql);
            r = Request["r"];
            result = Request["result"];
            

            if (sql != null && sql!="" && r!="" && r != null)
            {
                en_sql = CryptoHelper.encryptAES(sql, r, fixedIV);
            }

            if (result != null && result!="")
            {
                result = CryptoHelper.decryptAES(result, r);
            }
            
        }
    }
}
