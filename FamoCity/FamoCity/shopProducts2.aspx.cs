using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Web.Services;

namespace FamoCity
{
    public partial class shopProducts2 : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/Shop/edit/Prods";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/Shop/edit/Prods";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Shop)
                Response.Redirect("/login");

            FamProduct p = new FamProduct(Passport);
            gridView.DataSource = p.GetShopProducts(Passport.ShopID);
            gridView.DataBind();
        }
    }
}