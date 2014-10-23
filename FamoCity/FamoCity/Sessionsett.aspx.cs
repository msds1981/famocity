using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FamoCity
{
    public partial class Sessionsett : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["hisham"] == null)
            {
                Session["hisham"] = "hisham";
            }

            if (Request.Cookies["userName"].Value == null)
            {
                Response.Cookies["userName"].Value = "hisham";
            }
        }
    }
}