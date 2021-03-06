﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;

namespace FamoCity
{
    public partial class shopPass2 : System.Web.UI.Page
    {
        int Id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/ChangePassword";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/ChangePassword";
                Response.Redirect("/login");
            }

            FamShop s = new FamShop(Passport, Passport.ShopID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/login");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            FamUser FU = new FamUser(Passport, Passport.UserID);

            if (FU.Password == txtOldPassword.Text)
            {
                if (txtRepeatPassword.Text == txtNewPassword.Text)
                {
                    FU.Password = txtNewPassword.Text;
                    FU.Update();
                    ltrError.Text = " تم تغير كلمة المرور بنجاح";
                }
                else
                {
                    ltrError.Text = "<div class='error-left'></div><div class='error-inner'> لم يتم التعديل على كلمة المرور ... ليس هناك توافق في ادخال البيانات </div>";
                }
            }
            //else
            //    ltrError.Text = "<div class='error-left'></div><div class='error-inner'>كلمة المرور القديمة ليست صحيحة</div>";
        }

        protected void csvOldPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            FamUser fu = new FamUser(Passport, Passport.UserID);
            if (fu.ID > 0 && fu.Password == txtOldPassword.Text)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    }
}
