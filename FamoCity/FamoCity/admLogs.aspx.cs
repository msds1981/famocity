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
    public partial class admLogs : System.Web.UI.Page
    {

        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/admLogs";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/admin/admLogs";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");
            if (!IsPostBack)
            {
               bindlogs();
            }
        }

        private void bindlogs()
        {
            FamLog fl = new FamLog(Passport);

            
            DataTable dt = fl.GetLogsOfUser(txtname.Text);

            gvLog.DataSource = dt; ;
            gvLog.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindlogs();
           

        }

        protected void gvLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLog.PageIndex = e.NewPageIndex;
            bindlogs();
        }
    }





}

