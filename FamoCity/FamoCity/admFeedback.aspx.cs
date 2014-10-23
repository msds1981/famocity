using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.IO;
using System.Data;

namespace FamoCity
{
    public partial class admFeedback : System.Web.UI.Page
    {
      
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/feedback";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/admin/feedback";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");
            if (!IsPostBack)
            {
                bindgvfeedback();
            }
        }
        private void bindgvfeedback()
        {
            FamFeedback ffb = new FamFeedback(Passport);
            gvFeedBack.DataSource = ffb.GetFeeds();
            gvFeedBack.DataBind();
        }

       protected void gvFeedBack_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int i = 0; i++;
            if (e.CommandName == "cmddelete")
            {
                string dll = e.CommandArgument.ToString();
                FamFeedback fgd = new FamFeedback(Passport, int.Parse(dll));
                fgd.Delete();
               
            }
               bindgvfeedback();

           string filename = e.CommandArgument.ToString();
           if (filename!="")
           { 
           Response.ContentType = "application/octet-stream";
           Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename.Replace("~/",""));
           string aaa = Server.MapPath(filename);
           Response.TransmitFile(Server.MapPath( filename));
           Response.End();
         
           }
            bindgvfeedback();
         }
        
        }
    }
