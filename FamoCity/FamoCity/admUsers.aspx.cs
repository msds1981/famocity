using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;

namespace FamoCity
{
    public partial class admUsers : System.Web.UI.Page
    {
        int id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/admUsers";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/admUsers";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {

                    bindGvUsers();

                }
            }
        }
        private void bindGvUsers()
        {

            FamUser fa = new FamUser(Passport);

            gvUser.DataSource = fa.GetUsers();
            gvUser.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/admUsers");

        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            bindGvUsers();
            {
            }


        }
    }
}