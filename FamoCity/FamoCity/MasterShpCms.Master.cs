using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class MasterShpCms : System.Web.UI.MasterPage
    {
        FamoPassport pass;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
                pass = (FamoPassport)Session["passport"];
            else
                Response.Redirect("/login");
        }

        protected void lnkMainPage_Click(object sender, EventArgs e)
        {
            FamShop f = new FamShop(pass, pass.ShopID);
            Response.Redirect("/Shop/" + f.WebName);
        }
    }
}