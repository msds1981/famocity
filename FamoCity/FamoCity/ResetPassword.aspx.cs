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
    public partial class ResetPassword : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnsend_Click(object sender, EventArgs e)
        {
            FamUser FU = new FamUser(Passport);
            DataTable dt = FU.GetUserByEmail(txtEmail.Text);
            if (dt.Rows.Count > 0)
            {
                FU.ID = Convert.ToInt32(dt.Rows[0]["user_id"]);
                if (SendEmail(FU)) {
                    lblmsg.Text = "تم ارسال كلمة السر الى الايميل" + FU.Email;
                }
            }
            else
            {
                lblmsg.Text = " الايميل ليس موجود  ";
            }
        }
        

        private bool SendEmail(FamUser user)
        {
            FamOption op = new FamOption(Passport);

            string html = GmailSender.ScreenScrapeHtml(op.GetValue(FamoBase.Const_Option_Pub_ResetPassword).ToString());
            html = html.Replace("|fullname|", user.FirstName + " " + user.LastName);
            html = html.Replace("|email|", user.Email);
            html = html.Replace("|password|", user.Password);
            GmailSender.SendMail("", "", "", "Reset password of Famo.com", html);

            return true;
        }

     
    }
}