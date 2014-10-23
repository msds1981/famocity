using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Web.Security;
namespace FamoCity
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            if (Session["passport"] != null)
            {
                FamoPassport pass = (FamoPassport)Session["passport"];
                pass.Logout();
                Session["passport"] = pass;
                Session["logout"] = "true";
                if (Session["target"] != null)
                    Response.Redirect(Session["target"].ToString());
                else
                    Response.Redirect("/main");
            }
            else
                Response.Redirect("/login");
        }
    }
}