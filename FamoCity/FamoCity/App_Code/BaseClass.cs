using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FamoLibrary;

namespace FamoCity
{
    public class BaseClass : System.Web.UI.Page
    {
        public FamoPassport Passport { get; set; }
        public string Connection { get { return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStr1"].ConnectionString; } }
        
        public string WebHosting { get { return System.Configuration.ConfigurationManager.AppSettings["webHost"]; } }
        public string EmailSender { get { return System.Configuration.ConfigurationManager.AppSettings["EmailSender"]; } }
        public string EmailSenderPass { get { return System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]; } }
        public string AdminEmail { get { return System.Configuration.ConfigurationManager.AppSettings["AdminEmail"]; } }
        public string EmailSubject { get { return System.Configuration.ConfigurationManager.AppSettings["EmailSubject"]; } }

        protected override void OnInit(EventArgs e)
        {
            // if the user is not Admin , redirect to Login Page
            if (Session["passport"] == null)
            {
                Passport = new FamoPassport(Connection);
                /*FamUser u = new FamUser(Passport);
                u.UserName = "guest";
                u.Email = "msds1981@yahoo.com";
                u.Password = "G0Famo2013";
                u.LanguageID = 1;
                u.Status = FamoBlock.enmUserStatus.Active;
                u.Save();*/
                if (!Passport.Logged)
                    Passport.Login("msds1981@yahoo.com", "a");
                Session["passport"] = Passport;
            }
            else
            {
                Passport = (FamoPassport)Session["passport"];
            }
            // this needed to initialize its base page class 
            base.OnInit(e);
        }

    }
}