using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Web.Services;
using System.Data;
namespace FamoCity
{

    public partial class shopProducts : System.Web.UI.Page
    {
        int Id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/Products";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/Products";
                Response.Redirect("/login");
            }

            if (Passport.ShopID == 0)
                Response.Redirect("/login");



            Id = Passport.ShopID;
            ShowData();
        }

        private void ShowData()
        {
            FamProduct p = new FamProduct(Passport);
            GridView1.DataSource = p.GetShopProducts(Passport.ShopID);
            GridView1.DataBind();
        }

        [WebMethod]
        public static bool delete(int id)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamProduct p = new FamProduct(pass, id);
            return p.Delete();
        }
    }
}