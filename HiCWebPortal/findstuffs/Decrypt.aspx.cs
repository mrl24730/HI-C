using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kitchen;
using WebPortal.DB;

namespace WebPortal.findstuffs
{
    public partial class Decrypt : System.Web.UI.Page
    {
        public string decryptedText = "No request...";
        public string encryptText = "";

        public string plainText = "";
        public string encryptedText = "";

        private uint ip = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!String.IsNullOrEmpty(Request["encrypt"])){
                encryptText = Request["encrypt"];

                uint.TryParse(encryptText,out ip);

                if (ip == 0)
                {
                    decryptedText = CryptoHelper.decryptAES(encryptText, DBHelper.defaultSKey);
                }
                else
                {
                    decryptedText = AppHelper.Uint2IP(ip);
                }
                
            }

            if (!String.IsNullOrEmpty(Request["plain"]))
            {
                plainText = Request["plain"];
                encryptedText = CryptoHelper.encryptAES(plainText, DBHelper.defaultSKey, DBHelper.fixedIV);
                
            }
        }
    }
}
