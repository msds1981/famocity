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
    public partial class newPass : System.Web.UI.Page
    {
        FamoPassport Passport;
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
                Passport = (FamoPassport)Session["passport"];
            else 
                Passport = new FamoPassport(ClassMain.ConnectionString);

            if (!IsPostBack)
            {
                string code = "";
                string[] ar = { "" };
                string data = "";
                if (Page.RouteData.Values["data"] != null)
                    data = Page.RouteData.Values["data"].ToString();

                if (Request["d"] != null)
                    data = Request["d"].ToString();

                if (data != null)
                {
                    code = data.Replace("$", "/"); //Request["p"].ToString();
                    ar = Decode(code.Trim().Replace(" ", "+"));
                }

                if (ar.Length == 6)
                {
                    FamUser u = new FamUser(Passport, Convert.ToInt32(ar[0]));
                    if (u.ID > 0)
                    {
                        id = u.ID;
                        hdn.Value = id.ToString();
                    }
                    else
                    {
                        Response.Redirect("/login");
                    }
                }
                else
                {
                    Response.Redirect("/login");
                }
            }
        }


        private string[] Decode(string str)
        {
            EncryptionThread dec = new EncryptionThread();
            string d = dec.DecryptString(str, "abcsx");
            string[] arr = d.Split(new Char[] { '|' });

            return arr;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtRepeatPassword.Text) return;
            if (hdn.Value == null) return;

            FamUser fu = new FamUser(Passport, Convert.ToInt32(hdn.Value));
            if (fu.ID > 0)
            {
                fu.Password = ClassMain.EncryptParameter(txtNewPassword.Text);
                if (fu.Update())
                {
                    Session["msg"] = "تم تغيير كلمة المرور للمستخدم " + fu.Email;
                    Response.Redirect("/message");
                }
            }
        }
    }
}