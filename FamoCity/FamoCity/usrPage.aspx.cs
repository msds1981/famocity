using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using System.IO;
using System.Text;

namespace FamoCity
{
    public partial class usrPage : System.Web.UI.Page
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

            int userid = 0;
            if (Page.RouteData.Values["userid"] != null)
            {

            }
            else if (Request["uid"] != null)
            {
                userid = Convert.ToInt32(Request["uid"]);
            }

            //show data of user who is logged in case userid=0
            if (userid == 0) userid = Passport.UserID;
            //show images collection
           // ltrImages.Text = ClassMain.BuildUserImagesHeader(Passport, userid);
          //  ltrUserInfo.Text = ClassMain.BuildUserInfoPanel(Passport, userid);
        }

    }
}