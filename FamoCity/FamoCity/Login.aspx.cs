using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using System.Net;

namespace FamoCity
{
    public partial class Login1 : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {                       
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (Session["Email"] != null && Session["pass"] != null)
                {
                   // string pas = ClassMain.DecryptParameter(Session["pass"].ToString());
                   // string usr = Session["Email"].ToString();
                    if (Session["Email"] != "" && Session["pass"] != "")
                    {
                        txtEmail.Text = Session["Email"].ToString();
                        txtPassword.Text = ClassMain.DecryptParameter(Session["Pass"].ToString());
                        Login();
                        Session["Email"] = "";
                        Session["pass"] = "";
                    }
                }
            }
        }

        private void Login() {
            if (!ClassMain.Login(txtEmail.Text, txtPassword.Text))
            {
                Lblmsg.Text = "اسم المستخدم او كلمة المرور خاطئة";
                Lblmsg.Visible = true;
            }
            else
            {
                Lblmsg.Visible = false;
                Lblmsg.Text = "";
            }
        }

        protected void lnkForgetPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("/forgetpassword");
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //FamUser FU = new FamUser(Passport);
            //DataTable dt = FU.GetUserByEmail(txtEmail.Text);
            //if (dt.Rows.Count > 0)
            //    args.IsValid = false;
            //else
            //    args.IsValid = true;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}