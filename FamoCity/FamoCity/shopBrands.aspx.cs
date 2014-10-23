using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class shopBrands : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/OwnerInfo";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/OwnerInfo";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Shop)
                Response.Redirect("/login");

            FamBrand b=new FamBrand(Passport);
            GridView1.DataSource = b.GetBrands();
            GridView1.DataBind();
        }
    }
}