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
    public partial class Default2 : System.Web.UI.Page
    { 
        FamoPassport Passport;
        private readonly HttpContextBase _httpContext;
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

            //hisham code Cookies login

            string paths = FormsAuthentication.FormsCookiePath;
            HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            try
            {
                FormsIdentity identity = HttpContext.Current.User.Identity as FormsIdentity;

                HttpContext currentContext = HttpContext.Current;
                if (HttpContext.Current.User != null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated || 1==1)
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
                Passport.Login(dt.Rows[0]["email"].ToString(),t.DecryptString( dt.Rows[0]["password"].ToString(),FamoBase.const_Encryption_Password));
                if (Passport.Logged)
                {
                   // ClassMain.RedirectPage(Passport);
                }
            }
            //end hisham code cookies login




            if (!IsPostBack)
            {
                 
                //read cookies
                if (Request.Cookies["c"] != null)
                {
                    if (Request.Cookies["c"].Value == "true")
                    {
                        chklogin.Checked = true;
                        if (Request.Cookies["e"] != null)
                            txtlogemail.Text = ClassMain.DecryptParameter(Request.Cookies["e"].Value);
                        if (Request.Cookies["u"] != null)
                            txtlogpassword.Text = ClassMain.DecryptParameter(Request.Cookies["u"].Value);
                        if (Request.Cookies["path"] != null)
                            Image1.ImageUrl = Request.Cookies["path"].Value;

                        if (Session["logout"] == null)
                        {
                            Login();
                            if (Request.Cookies["c"] != null)
                                Response.Cookies["path"].Value = ClassMain.getUserLogoPath(Passport, Passport.UserID);
                        }
                    }
                }

                if (Passport.Logged)
                {
                    ClassMain.RedirectPage(Passport);
                }
                /*
                if (Session["Email"] != null && Session["pass"] != null)
                {

                    if (Session["Email"] != "" && Session["pass"] != "")
                    {
                        txtlogemail.Text = Session["Email"].ToString();
                        txtlogpassword.Text = ClassMain.DecryptParameter(Session["Pass"].ToString());
                        Login();
                        Session["Email"] = "";
                        Session["pass"] = "";
                    }
                }*/
            }
            FillYears();

            try
            {
                FamOption op = new FamOption(Passport);
                int shpAgr = Convert.ToInt32(op.GetValue(FamoLibrary.FamoBlock.Const_Option_Pub_ShopAgree));
                hdnAgree.Value = shpAgr.ToString();
            }
            catch (Exception ex) { hdnAgree.Value = "0"; }
        }

        private void Login()
        {

            if(Passport.Login(txtlogemail.Text, txtlogpassword.Text))
                 LoginSetting();
            if (!ClassMain.Login(txtlogemail.Text, txtlogpassword.Text))
            {
                lblmsg.Text = "اسم المستخدم او كلمة المرور خاطئة";
                lblmsg.Attributes["style"] = "display:block;color:red;";
            }
            else
            {

              
                lblmsg.Attributes["style"] = "display:none;";
                lblmsg.Text = "";
            }
        }




        //hisham cookies code



        private void LoginSetting()
        {

            var now = DateTime.UtcNow.ToLocalTime();

            FormsAuthenticationTicket tkt;
            HttpCookie cookie;

            tkt = new FormsAuthenticationTicket(
               1 /*version*/,
               txtlogemail.Text,
               now,
               now.AddMinutes(30),
               chklogin.Checked,
               txtlogemail.Text,
               FormsAuthentication.FormsCookiePath);

           
            //tkt = new FormsAuthenticationTicket(1, txtlogemail.Text, DateTime.Now, DateTime.Now.AddMinutes(30), chklogin.Checked, "your custom data");



            string cookiestr;
            cookiestr = FormsAuthentication.Encrypt(tkt);
            cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            cookie.HttpOnly = true;
            if (tkt.IsPersistent)
                cookie.Expires = tkt.Expiration;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Secure = FormsAuthentication.RequireSSL;
            
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            //_httpContext.Response.Cookies.Add(ck);
            


            Response.Cookies.Add(cookie);

            //string strRedirect;
            //strRedirect = Request["ReturnUrl"];
            //if (strRedirect == null)
            //    strRedirect = "default.aspx";
            //Response.Redirect(strRedirect, true);
            //}
            //else
            //    Response.Redirect("logon.aspx", true);
        }


        //hisham cookies code


        protected void btnenter_Click(object sender, EventArgs e)
        {
             
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (chklogin.Checked == true)
            {
                Response.Cookies["c"].Value = "true";
                string email = ClassMain.EncryptParameter(txtlogemail.Text);//encryptkey
                string password = ClassMain.EncryptParameter(txtlogpassword.Text);//encryptkey
                Response.Cookies["e"].Value = email;
                Response.Cookies["u"].Value = password;
               

            }
            else
            { 
                Response.Cookies["c"].Value = "false";
                Response.Cookies["e"].Value = "";
                Response.Cookies["u"].Value = "";
            }
            Login(); 

        }

        //protected void btnReEmail_Click(object sender, EventArgs e)
        //{
        //    FamUser FU = new FamUser(Passport);
        //    FU.FillUserByEmail(txtReEmail.Text);
        //    if (FU.ID > 0)
        //    {

        //        if (SendEmail(FU))
        //        {
        //            // lblmsg.Text = "تم ارسال كلمة السر الى الايميل" + FU.Email;
        //            Session["msg"] = "تم ارسال رسالة الى البريد \n" + FU.Email + "\n للإستمرار في عملية إعادة كلمة المرور الرجاء قراءة التعليمات في الرسالة";
        //            Response.Redirect("/message");
        //        }
        //    }
        //    //else
        //    //{
        //    //    lblEeEmail.Text = "الإيميل الذي تم ادخاله غير مسجل لدينا";
        //    //}
        //}


        //private bool SendEmail(FamUser user)
        //{
        //    string html = GmailSender.ScreenScrapeHtml(ClassMain.WebHosting + ClassMain.GetOptionValue(FamoBase.Const_Option_Pub_ResetPassword));
        //    html = html.Replace("|fullname|", user.FirstName + " " + user.LastName);
        //    html = html.Replace("|email|", user.Email);
        //    //html = html.Replace("|password|", ClassMain.WebHosting + "newpass.aspx?p=" + getParameter(user));
        //    //html = html.Replace("|password|", ClassMain.WebHosting + "NewPassword/" + getParameter(user));
        //    html = html.Replace("|password|", ClassMain.WebHosting + "newpass.aspx?d=" + getParameter(user));
        //    ClassMain.SendEmail("نموذج استعادة كلمة المرور", user.Email, html);
        //    return true;
        //}

        //private string getParameter(FamUser user)
        //{
        //    EncryptionThread enc = new EncryptionThread();
        //    string t = DateTime.Now.Ticks.ToString();
        //    string Email = user.ID + "|" + user.Email + "|" + user.LanguageID + "|" + user.FullName + "|" + user.Gender.ToString() + "|" + t;
        //    return enc.EncryptString(Email, "abcsx").Replace("/", "$");
        //}

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

        protected void btnsaveUser_Click(object sender, EventArgs e)
        {
            //register new personal account

            if (!IsValid)
                return;

            //validations
            if (!Methods.IsEmailValid(txtRegUser.Text.Trim()) || txtRegUser.Text.Trim() == "") return;
            if (txtPassUser.Text != txtRePassUser.Text) return;

            FamOption op = new FamOption(Passport);
            if (op.GetValue(FamoBase.Const_Option_Pub_UserRegister) != "1")
            {
                Session["msg"] = "تم ايقاف تسجيل المستخدمين";
                Response.Redirect("/message");
            }
            try
            {
                ClassMain.LoginGuest();
                if (chkGreeting.Checked)
                {
                    FamUser FU = new FamUser(Passport);
                    FU.Email = txtRegUser.Text;
                    FU.Password = txtPassUser.Text;
                    FU.BirthDate = ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
                    if (rdbMan.Checked)
                        FU.Gender = FamoBlock.enmGender.Male;
                    else
                        FU.Gender = FamoBlock.enmGender.Female;

                    FU.Agreement.ID = Convert.ToInt32(chkGreeting.Checked);
                    FU.Group.ID = 1;
                    int l = Convert.ToInt32(ClassMain.GetOptionValue(FamoBlock.Const_Option_Pub_MainLanguage));
                    FU.LanguageID = l;
                    FU.Status = FamoBlock.enmUserStatus.Active;
                    FU.Type = FamoBlock.enmUserType.User;

                    int x = FU.Save();
                    if (x > 0)
                    {
                        FU.UserName = x.ToString();
                        FU.Update();

                        RegisterNewCustomer(FU, ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "-" + ddlDay.SelectedValue);

                        

                        //create random images at first time
                        ClassMain.BuildRandomImagesAFT(Passport, FamoBlock.enmUserType.User, x);

                        if (Passport.Login(FU.Email, FU.Password))
                        {
                            Session["passport"] = Passport;
                            //في تسجيل الدخول للمره الثانية بدون إكمال التسجيل
                            SetStep("/person/" + Passport.UserName.Trim() + "/setting");//حفظ الخطوة الثانية للتسجيل 
                            //Response.Redirect("~/usrmain.aspx?uid=" + x + "&pg=6");
                            ClassMain.RedirectPage(Passport);
                        }
                    }
                    else
                    {
                        //lblmesg.Text = "لم يتم التسجيل";
                    }

                }
                //else
                //lblmesg.Text = "يرجى الاتفاقية على التسجيل ";

            }

            catch (Exception ex)
            {

            }

        }


        
        private void SetStep(string pageStep)
        {
            FamOption op = new FamOption(Passport);
            op.SetValue(FamoBlock.Const_Option_Usr_StepsRegister + Passport.UserID.ToString(), pageStep);
        }

        protected void CustomValidatorcheck_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!chkGreeting.Checked)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void SelectValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (!chkGreeting.Checked)
                args.IsValid = false;
            else
                args.IsValid = true;
            //args.IsValid = chkGreeting.Checked;
        }

        protected void CsrShop_ServerValidate(object source, ServerValidateEventArgs args)
        {
            FamUser FU = new FamUser(Passport);
            DataTable dt = FU.GetUserByEmail(txtEmailShop.Text);
            if (dt.Rows.Count > 0)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void btnEnterShop_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

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
                if (ChkBoxShop.Checked)
                {

                    FamUser FU = new FamUser(Passport);
                    FU.Email = txtEmailShop.Text;
                    FU.Password = TxtPassShop.Text;
                    FU.Agreement.ID = Convert.ToInt32(hdnAgree.Value);
                    FU.Status = FamoBlock.enmUserStatus.Active;
                    FU.Type = FamoBlock.enmUserType.Shop;
                    int user_id = FU.Save();

                    if (user_id > 0)
                    {
                        /*
                        //حفظ بيانات الشركة
                        FamShop FS = new FamShop(Passport);
                        FS.Owner.ID = user_id;
                        //FS.WebName = FS.ID.ToString();
                        FS.Name = TxtNameShop.Text;
                        int shop_id = FS.Save();
                        FS.WebName = Convert.ToString(shop_id);
                        webname = FS.WebName.ToString();
                        */

                        if (FU.ID > 0)
                        {
                            //create new customer in shop
                            int c = RegisterNewCustomer(FU,"");
                            //create new vendor
                            CreateNewVendor(c, TxtNameShop.Text, FU.Email);

                            if (Passport.Login(FU.Email, FU.Password))
                            {
                                Session["passport"] = (FamoPassport)Passport;
                                //Response.Redirect("/Shop/" + webname);
                                NopService.NopService cl = new NopService.NopService();
                                //cl.getSlugName(ClassMain.serviceUser, ClassMain.servicePass,

                                EncryptionService s = new EncryptionService(ClassMain.EncryptKey);
                                string k = s.EncryptText(FU.Password);

                                //Response.Redirect("");
                            }
                        }
                    }
                    else
                    {

                    }
                }
                //else
                //    lblmsgshop.Text = "يرجى تحدد الاتفاقية  ";
            }
            catch (Exception ex)
            {
            }
        }

        private int RegisterNewCustomer(FamUser user,string BirthDate) {
            //create new customer in famocity.com/shop via webservice
            int customerId = 0;
            try
            {
                //create new customer in famocity.com/shop
                NopService.NopService client = new NopService.NopService();
                //1981-04-24
                bool resb;
                client.RegisterNewCustomer(ClassMain.serviceUser, ClassMain.servicePass, "", user.Email, user.Password, "", ""
                    , BirthDate
                    , (user.Gender == FamoBlock.enmGender.Female ? "F" : "M"), out customerId, out resb);
            }
            catch (Exception ex) { }

            if (customerId > 0)
            {
                user.CustomerId = customerId;
                user.Update();
            }
            return customerId;
        }

        private int CreateNewVendor(int customerId, string VendorName, string email)
        {
            NopService.NopService client = new NopService.NopService();
            bool a; int b;
            client.RegisterNewVendor(ClassMain.serviceUser, ClassMain.servicePass, VendorName, email, customerId, true, out b, out a);
            return b;
        }

        protected bool checkDataEmail(object source, ServerValidateEventArgs args)
        {
            FamUser FU = new FamUser(Passport);
            DataTable dt = FU.GetUserByEmail(txtEmailShop.Text);
            if (dt.Rows.Count > 0)
                args.IsValid = false;
            else
                args.IsValid = true;
            return false;
        }

        protected void CsrShop_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            FamUser FU = new FamUser(Passport);
            DataTable dt = FU.GetUserByEmail(txtEmailShop.Text);
            if (dt.Rows.Count > 0)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
}