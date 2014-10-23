using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class admIndexShow : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/CharIndexs";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/CharIndexs";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (!IsPostBack)
            {
                BindIndexData();
            }


        }

        private void BindIndexData()
        {
            FamIndex fi = new FamIndex(Passport);
            gvIndex.DataSource = fi.GetIndexs();
            gvIndex.DataBind();

        }

        protected void gvIndex_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIndex.PageIndex = e.NewPageIndex;
            BindIndexData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin/admIndex");
        }      
    }
}