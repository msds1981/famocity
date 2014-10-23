using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity.userControls
{
    public partial class ucShopLogo : System.Web.UI.UserControl
    {
        FamoPassport pass;
        protected void Page_Load(object sender, EventArgs e)
        {
            pass = (FamoPassport)Session["passport"];
            if (pass == null) return;
            if (pass.ConnectionString == "") 
            {
                pass.ConnectionString = ClassMain.ConnectionString;
                Session["passport"] = pass;
            }
            string name = "";
            if (Page.RouteData.Values["shopname"] != null)
                name = Page.RouteData.Values["shopname"].ToString();
            FamShop sh = ClassMain.GetShop(name);
            if (sh.ID > 0)
            {
                lblCompanyName.Text = sh.Name;
                imgLogo.ImageUrl = sh.File.Path;
            }
        }
    }
}