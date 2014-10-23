using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class admChrClothShow : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/Charcloths";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Charcloths";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");


            if (!IsPostBack)
            {

                bindgvCharClothes();

            }


        }
        private void bindgvCharClothes()
        {
            FamChrClothes fcc = new FamChrClothes(Passport);
            gvObject.DataSource = fcc.GetChrClothes();
            gvObject.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Charcloth");
        }
    }
}