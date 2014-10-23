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
    public partial class shopBrands2 : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/Shop/edit/Brands";
                   Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/Shop/edit/Brands";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Shop)
                Response.Redirect("/login");

            FamBrand b = new FamBrand(Passport);
            gridView.DataSource = b.GetShopBrands(Passport.ShopID);
            gridView.DataBind();
        }

        [WebMethod]
        public static bool delete(int id)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamBrand b = new FamBrand(pass, id);
            return b.Delete();
        }
    }
}