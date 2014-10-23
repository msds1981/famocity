using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class admCharGroupShow : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/Chargrps";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Chargrps";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");


            if (!IsPostBack)
            {

                bindgvCharGroup();

            }


        }

        private void bindgvCharGroup()
        {
            FamChrGroup fgp = new FamChrGroup(Passport);
            gvCharGroup.DataSource = fgp.GetChrGroups();
            gvCharGroup.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Chargrp");
        }
       
    }
}