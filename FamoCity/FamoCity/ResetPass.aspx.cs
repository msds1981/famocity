using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using mUtilities;

namespace FamoCity
{
    public partial class ResetPass : System.Web.UI.Page
    {

        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null)
            {
                Passport = new FamoPassport(ClassMain.ConnectionString);
                //Passport.LoginWithoutRecordLogs("msds1981@yahoo.com", "a");
                Session["passport"] = Passport;
            }
            else
                Passport = (FamoPassport)Session["passport"];

        }
        protected void btnsend_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
            {
                lblmsg.Text = "لإستعادة كلمة المرور يجب كتابة الإيميل";
                return;
            }
            if (!Capcha1.CheckCaptcha())
            {
                //label for the wrong captcha
                lblCap.Text = "الرجاء إعادة إدخال الأحرف";
                return;
            }

            FamUser FU = new FamUser(Passport);
            FU.FillUserByEmail(txtEmail.Text);
            if (FU.ID > 0)
            {
                lblCap.Text = "";
                if (SendEmail(FU))
                {
                   // lblmsg.Text = "تم ارسال كلمة السر الى الايميل" + FU.Email;
                    Session["msg"] = "تم ارسال رسالة الى البريد \n" + FU.Email + "\n للإستمرار في عملية إعادة كلمة المرور الرجاء قراءة التعليمات في الرسالة";
                    Response.Redirect("/message");
                }
            }
            else
            {
                lblmsg.Text = "الإيميل الذي تم ادخاله غير مسجل لدينا";
            }
        }


        private bool SendEmail(FamUser user)
        {
            string html = GmailSender.ScreenScrapeHtml(ClassMain.WebHosting + ClassMain.GetOptionValue(FamoBase.Const_Option_Pub_ResetPassword));
            html = html.Replace("|fullname|", user.FirstName + " " + user.LastName);
            html = html.Replace("|email|", user.Email);
            //html = html.Replace("|password|", ClassMain.WebHosting + "newpass.aspx?p=" + getParameter(user));
            //html = html.Replace("|password|", ClassMain.WebHosting + "NewPassword/" + getParameter(user));
            html = html.Replace("|password|", ClassMain.WebHosting + "newpass.aspx?d=" + getParameter(user));
            ClassMain.SendEmail("نموذج استعادة كلمة المرور", user.Email, html);
            return true;
        }

        private string getParameter(FamUser user)
        {
            EncryptionThread enc = new EncryptionThread();
            string t = DateTime.Now.Ticks.ToString();
            string Email = user.ID + "|" + user.Email + "|" + user.LanguageID + "|" + user.FullName + "|" + user.Gender.ToString() + "|" + t;
            return enc.EncryptString(Email, "abcsx").Replace("/", "$");
        }
    }
}