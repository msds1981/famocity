using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Services
{
    public partial class Capatcha : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool CheckCaptcha() {
            bool b = false;
            if (txtimgcode.Text.ToLower() == Session["CaptchaImageText"].ToString().ToLower())
                return true;
            return b;
        }

    }
}