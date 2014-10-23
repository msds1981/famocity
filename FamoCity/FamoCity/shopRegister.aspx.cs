using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Transactions;
using System.Data;
using System.Text.RegularExpressions;

namespace FamoCity
{
    public partial class shopRegister : System.Web.UI.Page
    {
        FamoPassport Passport;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null)
            {

                Passport = new FamoPassport(ClassMain.ConnectionString);
                Passport.LoginWithoutRecordLogs("msds1981@yahoo.com", "a");
                Session["passport"] = Passport;
            }
            else
                Passport = (FamoPassport)Session["passport"];

            if (!IsPostBack)
            {
                FillYears();
                BindCountry();
                BindCompanyActivity();
                try
                {
                    FamOption op = new FamOption(Passport);
                    int shpAgr = Convert.ToInt32(op.GetValue(FamoLibrary.FamoBlock.Const_Option_Pub_ShopAgree));
                    hdnAgree.Value = shpAgr.ToString();
                }
                catch (Exception ex) { hdnAgree.Value = "0"; }
            }
        }

        private void BindCompanyActivity()
        {

            FamActivity Fa = new FamActivity(Passport);

            ckblActivity.DataSource = Fa.GetActivitiesByLanguage(Convert.ToInt32(ClassMain.GetOptionValue(FamoBlock.Const_Option_Pub_MainLanguage)), "name");
            ckblActivity.DataValueField = "actv_id";
            ckblActivity.DataTextField = "text";
            ckblActivity.DataBind();
        }

        private void BindCountry()
        {
            FamList FL = new FamList(Passport);
          
            ddlCountry.DataSource = FL.GetListsByName(FamoLibrary.FamoBlock.Const_List_Country, Convert.ToInt32(ClassMain.GetOptionValue(FamoBlock.Const_Option_Pub_MainLanguage))); ;
            ddlCountry.DataValueField = "list_id";
            ddlCountry.DataTextField = "text";
            ddlCountry.DataBind();
            ddlCountry.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlCountry.SelectedValue = "0";
        }

        protected void btnRegistar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;
            if (!Capatcha1.CheckCaptcha()) return;
            ClassMain.LoginGuest();
            String webname;
            FamOption op = new FamOption(Passport);
            if (op.GetValue(FamoBase.Const_Option_Pub_ShopRegister) != "1")
            {
                Session["msg"] = "تم ايقاف تسجيل الشركات";
                Response.Redirect("/message");
            }
            try
            {
                if (chkGreeting.Checked)
                {

                    FamUser FU = new FamUser(Passport);
                    FU.Email = txtEmail.Text;
                    FU.Password = txtPassword.Text;
                    if (radMale.Checked)
                        FU.Gender = FamoBlock.enmGender.Male;
                    else
                        FU.Gender = FamoBlock.enmGender.Female;
                    FU.FirstName = txtFirsName.Text;
                    FU.LastName = txtLastName.Text;
                    FU.Agreement.ID = Convert.ToInt32(hdnAgree.Value);
                    FU.Status = FamoBlock.enmUserStatus.Active;
                    FU.Type = FamoBlock.enmUserType.Shop;
                   

                    int user_id = FU.Save();

                    if (user_id > 0)
                    {
                        //حفظ بيانات الشركة
                        FamShop FS = new FamShop(Passport);
                        FS.Name = txtCompanyName.Text;
                        FS.WebName = txtWebName.Text;
                        FS.Owner.ID = user_id;
                        int shop_id = FS.Save();
                        webname = FS.WebName;



                        //حفظ بيانات الجوال
                        FamPhoneProfile FPF = new FamPhoneProfile(Passport);
                        FPF.Number = txtNumberPhone.Text;
                        FPF.Type = FamoBlock.enmPhoneType.Mobile;
                        FPF.ObjectCode = FamoBlock.enmObjectCode.Shops;
                        FPF.ObjectId = shop_id;
                        FPF.Save();

                        FPF = new FamPhoneProfile(Passport);
                        //حفظ بيانات الفاكس
                        FPF.Number = txtNumberFax.Text;
                        FPF.Type = FamoBlock.enmPhoneType.Fax;
                        FPF.ObjectCode = FamoBlock.enmObjectCode.Shops;
                        FPF.ObjectId = shop_id;
                        FPF.Save();

                        //العنوان
                        FamContactProfile FCP = new FamContactProfile(Passport);
                        FCP.Address = txtAddress.Text;
                        FCP.ObjectCode = FamoBlock.enmObjectCode.Shops;
                        FCP.ObjectId = shop_id;
                        FCP.Country.ID = Convert.ToInt32(ddlCountry.SelectedValue);
                        FCP.Save();

                        FamShopInterest FSI = new FamShopInterest(Passport);
                        foreach (ListItem abc in ckblActivity.Items)
                        {
                            if (abc.Selected)
                            {
                                FSI.Shop.ID = shop_id;
                                FSI.Activity.ID = Convert.ToInt32(abc.Value);
                                FSI.Save();
                            }
                        }
                        // If we reach here, no errors, so commit the transaction

                        if (FU.ID > 0)
                        {
                           // ClassMain.Login(FU.Email, FU.Password);
                            //FamUser use = new FamUser(Passport, FU.Email, FU.Password);
                            //if (use.ID > 0)
                            if(Passport.Login(FU.Email, FU.Password))
                            {
                                Session["passport"] = (FamoPassport)Passport;
                                Response.Redirect("/Shop/" + webname);
                            }
                            
                        }
                    }

                    else
                    {
                      


                    }
                 


                }
                else
                lblmsg.Text = "يرجى تحدد الاتفاقية  ";
            }
            catch (Exception ex)
            {
            }
          
        }
        private void FillYears()
        {
            ListItem itm = new ListItem("سنة", "0");
            ddlYear.Items.Add(itm);
            for (int i = 1950; i < 2000; i++)
            {
                itm = new ListItem(i.ToString(), i.ToString());
                ddlYear.Items.Add(itm);
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            FamUser FU = new FamUser(Passport);
            DataTable dt = FU.GetUserByEmail(txtEmail.Text);
            if (dt.Rows.Count > 0)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            int cnt = 0;

            for (int i = 0; i < ckblActivity.Items.Count; i++)
            {
                if (ckblActivity.Items[i].Selected)
                {
                    cnt++;
                }
            }
            args.IsValid = (cnt == 0) ? false : true;
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            FamShop shp = new FamShop(Passport);
            DataTable dt = shp.GetShop(txtWebName.Text);
            if (dt.Rows.Count > 0)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex rgx = new Regex(@"^[a-zA-Z0-9_-]{3,20}$");
            if (!rgx.IsMatch(txtWebName.Text))
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
}