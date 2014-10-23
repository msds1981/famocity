using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class admSceneShow : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/Scenes";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Scenes";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");


            if (!IsPostBack)
            {

                bindgvScene();

            }


        }
        private void bindgvScene()
        {
            FamScene fs = new FamScene(Passport);
            gvScene.DataSource = fs.Getscens();
            gvScene.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Scene");
        }
    }
}