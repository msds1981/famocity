using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using mUtilities;
using System.Data;
using System.Web.Services;
using System.Text.RegularExpressions;
using FamoCity.NopService;
using System.Web.Security;

namespace FamoCity
{
    public partial class register : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            // this._httpContext = HttpContext;
            if (Session["passport"] == null)
            {
                Passport = new FamoPassport(ClassMain.ConnectionString);
                Session["passport"] = Passport;
            }
            else
                Passport = (FamoPassport)Session["passport"];

            /*if (!Passport.Logged) {
                Passport.LoginWithoutRecordLogs("msds1981@yahoo.com", "a");
                Session["passport"] = Passport;
            }*/

            if (!IsPostBack)
            {
                ClassMain.FillYears(ddlYear);
            }

            //hisham code Cookies login

            string paths = FormsAuthentication.FormsCookiePath;
            HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            try
            {
                FormsIdentity identity = HttpContext.Current.User.Identity as FormsIdentity;

                HttpContext currentContext = HttpContext.Current;
                if (HttpContext.Current.User != null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated || 1 == 1)
                    {
                        if (HttpContext.Current.User.Identity is FormsIdentity)
                        {
                            FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
                            FormsAuthenticationTicket ticket = id.Ticket;
                        }
                    }
                }
                // var formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
            }
            catch (Exception ex) { }
            //FormsAuthentication decrypt = new FormsAuthentication();
            //decrypt=.Decrypt(cookie.Value);
            if (cookie != null)
            {


                //FormsAuthentication decrypt = new FormsAuthentication();
                //decrypt=.Decrypt(cookie.Value);

                //esponse.Redirect("default.aspx");
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                string name = ticket.Name;

                int version = ticket.Version;

                string path = ticket.CookiePath;
                //FormsAuthentication.RedirectFromLoginPage(name, true);
                FamUser user = new FamUser(Passport);
                DataTable dt = new DataTable();
                dt = user.GetUserByEmail(name);
                EncryptionThread t = new EncryptionThread();
                Passport.Login(dt.Rows[0]["email"].ToString(), t.DecryptString(dt.Rows[0]["password"].ToString(), FamoBase.const_Encryption_Password));
                if (Passport.Logged)
                {
                    // ClassMain.RedirectPage(Passport);
                }
            }
            //end hisham code cookies login
        }

        protected void btnsaveUser_Click(object sender, EventArgs e)
        {
            //register new personal account
            if (!Methods.CheckEmailExist(txtRegUser.Text)) return;
            if (ddlYear.SelectedValue.ToString() == "" || ddlMonth.SelectedValue.ToString() == "" || ddlDay.SelectedValue.ToString() == "") return;
            if (ddlYear.SelectedValue.ToString() == "0" || ddlMonth.SelectedValue.ToString() == "0" || ddlDay.SelectedValue.ToString() == "0") return;

            ClassMain.RegisterNewUser(Passport, txtRegUser.Text, txtPassUser.Text, txtFirstName.Text,
                txtLastName.Text.Trim(), ddlYear.SelectedValue.ToString(),
                ddlMonth.SelectedValue.ToString(), ddlDay.SelectedValue.ToString(),
                (rdbMan.Checked ? "male" : "female"));

        }
    }
}