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
    public partial class admOption : System.Web.UI.Page
    {
      
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/admObtion";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/admObtion";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            FamOption fo = new FamOption(Passport);

                if (!IsPostBack)
                {
                    ClassMain.FillLanguage(ddlLang);
                   
                    showDataOption();
                }

        }

        private void showDataOption()
        {
            FamOption fo = new FamOption(Passport);
       
            txtVideoInto.Text = fo.GetValue(FamoBlock.Const_Option_Pub_VideoIntro);
            txtInto.Text = fo.GetValue(FamoBlock.Const_Option_Pub_WebIntro);
            txtTitle.Text = fo.GetValue(FamoBlock.Const_Option_Pub_TitleIntro);
            TxtScene.Text = fo.GetValue(FamoBlock.Const_Option_Unity_ScenePath);
            txtTrigger.Text = fo.GetValue(FamoBlock.Const_Option_Unity_TriggerPath);
            txtObject.Text = fo.GetValue(FamoBlock.Const_Option_Unity_ObjectPath);
            txtChar.Text = fo.GetValue(FamoBlock.Const_Option_Unity_Character);
            txtgustuser.Text = fo.GetValue(FamoBlock.Const_Option_Pub_GuestLogin);
            txtgustpassword.Text = fo.GetValue(FamoBlock.Const_Option_Pub_GuestPassword);
            ddlLang.SelectedValue =fo.GetValue(FamoBlock.Const_Option_Pub_MainLanguage);


            string value = fo.GetValue(FamoLibrary.FamoBlock.Const_Option_Pub_UserRegister);
            if (value == "1") rdTrueCreateUser.Checked = true;
            else if (value == "0") rdFalseCreateUser.Checked = true;

            value = fo.GetValue(FamoLibrary.FamoBlock.Const_Option_Pub_ShopRegister);
            if(value =="1") rdTrueCreateCompaby.Checked=true;
            else if (value == "0") rdFalseCreateCompany.Checked = true;

            value = fo.GetValue(FamoLibrary.FamoBlock.Const_Option_Pub_LoginUsers);
            if (value == "1") rdTrueRegisrUser.Checked = true;
            else if (value == "0") rdFalseUserRegist.Checked = true;

            value = fo.GetValue(FamoLibrary.FamoBlock.Const_Option_Pub_LoginShops);
            if (value == "1") rdTrueRegisrCompany.Checked = true;
            else if (value == "0") rdFalseCompanyRegist.Checked = true;

             value = fo.GetValue(FamoLibrary.FamoBlock.Const_Option_Pub_LoginShops);
            if (value == "1") rdTrueRegisrCompany.Checked = true;
            else if (value == "0") rdFalseCompanyRegist.Checked = true;

          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FamOption fo = new FamOption(Passport);

            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_VideoIntro, txtVideoInto.Text);
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_WebIntro, txtInto.Text);
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_TitleIntro, txtTitle.Text);
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Unity_ScenePath, TxtScene.Text);
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Unity_TriggerPath, txtTrigger.Text);
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Unity_ObjectPath, txtObject.Text);
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Unity_Character, txtChar.Text);
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_GuestLogin, txtgustuser.Text);
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_GuestPassword, txtgustpassword.Text);
         
            
            fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_MainLanguage, ddlLang.SelectedValue);


            if (rdTrueCreateUser.Checked)

                fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_UserRegister, "1");

            else
            {
                if (rdFalseCreateUser.Checked)

                    fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_UserRegister, "0");
            }

            if (rdTrueCreateCompaby.Checked)

                fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_ShopRegister, "1");

            else
            {
                if (rdFalseCreateCompany.Checked)
                    fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_ShopRegister, "0");
            }
            if (rdTrueRegisrUser.Checked)

                fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_LoginUsers, "1");

            else
            {
                if (rdFalseUserRegist.Checked)
                    fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_LoginUsers, "0");
            }


            if (rdTrueRegisrCompany.Checked)

                fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_LoginShops, "1");

            else
            {
                if (rdFalseCompanyRegist.Checked)

                    fo.SetValue(FamoLibrary.FamoBlock.Const_Option_Pub_LoginShops, "0");
            }
            lblMsg.Text = "تم الحفظ بقاعدة البيانات بنجاح";

        }
      
        
        
        }
    }
