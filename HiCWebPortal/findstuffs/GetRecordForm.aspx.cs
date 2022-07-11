using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kitchen;

namespace WebPortal.findstuffs
{
    public partial class GetRecordForm : System.Web.UI.Page
    {
        public int r;
        public int currentTS;
        public string hash;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Random seed = new Random((int)DateTime.Now.Ticks);
            r = seed.Next(11111111,999999999);
            currentTS = AppHelper.secondSince();
            hash = CryptoHelper.HexStr(CryptoHelper.ComputeHash("kitchen*88" + r + currentTS + "kitchen", "md5"), true);
        }
    }
}
