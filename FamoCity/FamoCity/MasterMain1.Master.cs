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
    public partial class MasterMain1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.Cookies["loginusersmart"] != null)
                {
                    try
                    {

                        if (!string.IsNullOrEmpty(Request.Cookies["loginpasswordsmart"].Value))
                        {
                            Session["Email"] = Request.Cookies["loginusersmart"].Value;
                            Session["Pass"] = Request.Cookies["loginpasswordsmart"].Value;
                            Response.Redirect("/login");
                        }
                    }
                    catch (Exception ed) { }
                }
            }
            catch (Exception ed) { }
        }

        protected void btnLogin2_Click(object sender, EventArgs e)
        {
            //Response.Redirect("login/" + txtEmail.Text + "/" + ClassMain.EncryptParameter(txtPass.Text));

            //1 - read email entered and password

            //2 - encrypt password

            //3 - keep the login data in session

            //4 - go to login page 

            //5 - in login page execute login function 
            if (keeplogin.Checked)
            {
                Response.Cookies["loginusersmart"].Value = txtEmail.Text;
                Response.Cookies["loginpasswordsmart"].Value = ClassMain.EncryptParameter(txtPass.Text);
            }
            else
            {
                Response.Cookies["loginusersmart"].Value = "";
                Response.Cookies["loginpasswordsmart"].Value = "";
            }
            Session["Email"] = txtEmail.Text;
            Session["Pass"] = ClassMain.EncryptParameter(txtPass.Text);
            Response.Redirect("/login");


        //    ClassMain.Login(txtEmail.Text, txtPass.Text);



           /* FamoPassport Passport;
            Passport = new FamoPassport(ClassMain.ConnectionString);

            if (Passport.Login(txtEmail.Text, txtPass.Text))
            {
                Session["passport"] = Passport;
                FamOption op = new FamOption(Passport);
                ClassMain.RedirectPage(Passport);
                /*  if (Passport.UserType == FamoBlock.enmUserType.User)
                  {
                      //تسجيل الدخول لمستخدم
                      string page = op.GetValue(FamoBlock.Const_Option_Usr_StepsRegister + Passport.UserID.ToString());
                      if (page != "")
                          Response.Redirect(page);
                      else
                          Response.Redirect("");//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$  يجب هنا وضع الصفحة الرئيسية للمستخدم بعد تصميمها
                  }
                  else if (Passport.UserType == FamoBlock.enmUserType.Shop){
                      //تسجيل الدخول لشركة
                      FamShop s = new FamShop(Passport, Passport.UserID);
                      DataTable dt = s.GetUserShops(Passport.UserID);
                      if (dt.Rows.Count > 0)
                      {
                          Passport.ShopID = Convert.ToInt32(dt.Rows[0]["shop_id"]);
                          Session["passport"] = Passport;
                      } 
                      Response.Redirect("shopInfo.aspx");

                  }
                  else if (Passport.UserType == FamoBlock.enmUserType.Managment) { 
                      //تسجيل الدخول لإدارة الموقع
                      Response.Redirect("admLang.aspx");//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$  يجب هنا وضع الصفحة الرئيسية للإدارة بعد تصميمها
                  }
              }
              else
                  Response.Redirect("Login.aspx");
            }*/
        }

    }
}