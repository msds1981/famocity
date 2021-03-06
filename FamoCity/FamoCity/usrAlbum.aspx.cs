﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class usrAlbum : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/usrMain.aspx";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/usrMain.aspx";
                Response.Redirect("/login");
            }
        }
    }
}