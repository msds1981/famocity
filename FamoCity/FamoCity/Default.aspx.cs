using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoCity.NopService;
using FamoLibrary;
using mUtilities;
namespace FamoCity
{
    public partial class Default : System.Web.UI.Page
    {
        FamoPassport Passport;
        private readonly HttpContextBase _httpContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            //this._httpContext = httpContext;
            if (Session["passport"] == null)
            {
                Passport = new FamoPassport(ClassMain.ConnectionString);
                Session["passport"] = Passport;
            }
            else
            {
                Passport = (FamoPassport)Session["passport"];
                ClassMain.RedirectPage(Passport);
            }

            /*if (!Passport.Logged) {
                Passport.LoginWithoutRecordLogs("msds1981@yahoo.com", "a");
                Session["passport"] = Passport;
            }*/

            if (!IsPostBack)
            {
                ClassMain.FillYears(ddlRegYear);
            }

            //hisham code Cookies login

            /*         string paths = FormsAuthentication.FormsCookiePath;
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
                     }*/
            //end hisham code cookies login



            /*
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
                            }
                        }
                        FillYears();

                        try
                        {
                            FamOption op = new FamOption(Passport);
                            int shpAgr = Convert.ToInt32(op.GetValue(FamoLibrary.FamoBlock.Const_Option_Pub_ShopAgree));
                            hdnAgree.Value = shpAgr.ToString();
                        }
                        catch (Exception ex) { hdnAgree.Value = "0"; }*/
        }

        protected void btnRigister_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            if (HFTypeButton.Value == "login")
            {
                Login();
            }
            else if (HFTypeButton.Value == "register")
            {
                //register new personal account
                if (!Methods.CheckEmailExist(txtRegEmail.Text)) { ShowErrorMessage("هذا الايميل مسجل لدينا مسبقاً الرجاء ادخال ايميل مختلف ."); return; }
                if ((ddlRegDay.SelectedValue.ToString() == "" || ddlRegMonth.SelectedValue.ToString() == "" || ddlRegYear.SelectedValue.ToString() == "") ||
                    ddlRegDay.SelectedValue.ToString() == "0" || ddlRegMonth.SelectedValue.ToString() == "0" || ddlRegYear.SelectedValue.ToString() == "0")
                {
                    ShowErrorMessage("الرجاء التأكد من ادخال تاريخ الميلاد بشكل صحيح");
                    return;
                }

                int re = ClassMain.RegisterNewUser(Passport, txtRegEmail.Text, txtRegPassword.Text, txtRegFirstName.Text,
                    txtRegLastName.Text.Trim(), ddlRegYear.SelectedValue.ToString(),
                    ddlRegMonth.SelectedValue.ToString(), ddlRegDay.SelectedValue.ToString(),
                    sex.Value.ToString());

                if (re == 0) ShowErrorMessage("حدث خطأ اثناء تسجيل المستخدم الرجاء المحاوله مره اخرى .");
            }
            else
            {
                //forget password
                RetrivePassword(txtRegEmail.Text.Trim());
            }

        }

        private void Login()
        {
            if (ClassMain.Login(txtRegEmail.Text, txtRegPassword.Text))
            {
                SaveCookies();
            }
            else
            {
                //Response.Redirect("/relogin");
                ShowErrorMessage("حدث خطأ اثناء التسجيل الرجاء المحاوله مره اخرى .");
                //lblmsg.Text = "اسم المستخدم او كلمة المرور خاطئة";
                //lblmsg.Attributes["style"] = "display:block;color:red;";
            }
        }

        private bool RetrivePassword(string email)
        {
            if (!Methods.CheckEmailExist(email)) return false;
            FamUser fu = new FamUser(Passport);
            fu.FillUserByEmail(email);
            if (SendEmail(fu))
            {
                // lblmsg.Text = "تم ارسال كلمة السر الى الايميل" + FU.Email;
                Session["msg"] = "تم ارسال رسالة الى البريد \n" + fu.Email + "\n للإستمرار في عملية إعادة كلمة المرور الرجاء قراءة التعليمات في الرسالة";
                Response.Redirect("/message");
            }
            return true;
        }
        private bool SendEmail(FamUser user)
        {
            string html = GmailSender.ScreenScrapeHtml(ClassMain.WebHosting + ClassMain.GetOptionValue(FamoBase.Const_Option_Pub_ResetPassword));
            html = html.Replace("|fullname|", user.FirstName + " " + user.LastName);
            html = html.Replace("|email|", user.Email);
            //html = html.Replace("|password|", ClassMain.WebHosting + "newpass.aspx?p=" + getParameter(user));
            html = html.Replace("|password|", ClassMain.WebHosting + "NewPassword/" + getParameter(user));
            //html = html.Replace("|password|", ClassMain.WebHosting + "newpass.aspx?d=" + getParameter(user));
            ClassMain.SendEmail("نموذج استعادة كلمة المرور", user.Email, html);
            return true;
        }
        private string getParameter(FamUser user)
        {
            EncryptionThread enc = new EncryptionThread();
            string t = DateTime.Now.Ticks.ToString();
            string Email = user.ID + "|" + user.Email + "|" + user.LanguageID + "|" + user.FullName + "|" + user.Gender.ToString() + "|" + t;
            return enc.EncryptString(Email, "HS6YYE98KKCXHMSKSJC9S9945").Replace("/", "$");
        }
        private void SaveCookies()
        {

            var now = DateTime.UtcNow.ToLocalTime();

            FormsAuthenticationTicket tkt;
            HttpCookie cookie;

            tkt = new FormsAuthenticationTicket(
               1 /*version*/,
               txtRegEmail.Text,
               now,
               now.AddMinutes(30),
               chkRemember.Checked,
               txtRegEmail.Text,
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

        private void SetStep(string pageStep)
        {
            FamOption op = new FamOption(Passport);
            op.SetValue(FamoBlock.Const_Option_Usr_StepsRegister + Passport.UserID.ToString(), pageStep);
        }

        private void ShowErrorMessage(string message)
        {
            string tag = "<div class='box-body'><div class='alert alert-danger alert-dismissable'><button type='button' class='close'>×</button>|message|</div></div>";
            ltrMessage.Text = tag.Replace("|message|", message);
        }

        /*hessah
         * external Authentication(fasebook-tweeter-googleplue)
         * 
         * */
        //ajax
        [WebMethod]
        public static void external_Authentication(FamUser FUparameters, FamExternalAuthentication Extparameters)
        {

            FamoPassport Passport;
            Page c = new Page();
            if (c.Session["passport"] == null)
            {
                Passport = new FamoPassport(ClassMain.ConnectionString);
                c.Session["passport"] = Passport;
            }
            else
                Passport = (FamoPassport)c.Session["passport"];

            FamUser FU = new FamUser(Passport);
            //return user frome user tabel
            DataTable dtFU = FU.GetUserByEmail(FUparameters.Email);
            FamExternalAuthentication ext = new FamExternalAuthentication(Passport);
            //يرجع له الكوستمر الموجود في جدول external
            DataTable dtExt = ext.GetAuthenticationByEmail(Extparameters.Email, Extparameters.ProviderSystemName);
            int FUuserId = 0;
            int ExtuserId = 0;
            if (dtFU != null && dtExt != null)
            {
                //if user exite two tabel
                if (dtFU.Rows.Count > 0 && dtExt.Rows.Count > 0)
                {
                    //read all row in tabel
                    //foreach (DataRow rowFU in dtFU.Rows)
                    //{ FUuserId = Convert.ToInt32(rowFU["user_id"]); }

                    FUuserId = Convert.ToInt32(dtFU.Rows[0]["user_id"]);

                    //foreach (DataRow rowExt in dtFU.Rows)
                    //{
                    //    ExtuserId = Convert.ToInt32(rowExt["user_id"]);
                    //}
                    ExtuserId = Convert.ToInt32(dtExt.Rows[0]["user_id"]);

                    //if user id same in second tabel 
                    if (FUuserId.Equals(ExtuserId) && ExtuserId != 0)
                    {
                        FamUser u = new FamUser(Passport, FUuserId);
                        string password = u.Password;
                        bool login = ClassMain.Login(FUparameters.Email, password);

                    }
                }
                //if account not exit in user tabel 
                else if (dtFU.Rows.Count > 0 && dtExt.Rows.Count == 0)
                {
                    FamUser Fusere = FU.GetCustomerUserDataAsObject(FUuserId);
                    //store in External tabel
                    Extparameters.User = Fusere;
                    int extid = Extparameters.Save();
                    if (extid > 0)
                    {
                        FamUser u = new FamUser(Passport, FUuserId);
                        string password = u.Password;
                        bool login = ClassMain.Login(FUparameters.Email, password);
                    }


                }
                //if account not exit in user and External tabel 
                    #region Register user
                    //generte random passowrde
                    var randomPassword = GenerateRandomDigitCode(20);
                    Default reg = new Default();
                    if (!Methods.CheckEmailExist(FUparameters.Email)) { reg.ShowErrorMessage("هذا الايميل مسجل لدينا مسبقاً الرجاء ادخال ايميل مختلف ."); return; }
                    string birthday;
                    /* if ((ddlRegDay.SelectedValue.ToString() == "" || ddlRegMonth.SelectedValue.ToString() == "" || ddlRegYear.SelectedValue.ToString() == "") ||
                         ddlRegDay.SelectedValue.ToString() == "0" || ddlRegMonth.SelectedValue.ToString() == "0" || ddlRegYear.SelectedValue.ToString() == "0")
                     {
                         ShowErrorMessage("الرجاء التأكد من ادخال تاريخ الميلاد بشكل صحيح");
                         return; }*/
                    //register new user account in user & external tabel
                    int re = ExTernalRegisterNewUser(Passport, FUparameters.Email, randomPassword, FUparameters.FirstName.Trim(),
                        FUparameters.LastName.Trim(), "0000", "00", "00", FUparameters.Gender.ToString(), Extparameters);

                    if (re == 0) reg.ShowErrorMessage("حدث خطأ اثناء تسجيل المستخدم الرجاء المحاوله مره اخرى .");

                    #endregion
               
            }
        }
        //facebook_Authentication
        [WebMethod]
        public static void Authentication_Prammeter(string email, string firstName, string lastName, string gendor, string birthday, string user_id, string ccess_token, string provider_SystemName)
        {
            FamoPassport Passport;
            Page c = new Page();
            Default defultpage = new Default();
            if (c.Session["passport"] == null)
            {
                Passport = new FamoPassport(ClassMain.ConnectionString);
                c.Session["passport"] = Passport;
            }
            else
                Passport = (FamoPassport)c.Session["passport"];

            FamUser FU = new FamUser(Passport);
            FamExternalAuthentication Extparameters = new FamExternalAuthentication(Passport)
            {
                ExternalIdentifier = user_id,
                OAuthToken = ccess_token,
                OAuthAccessToken = user_id,
                ProviderSystemName = provider_SystemName,
                Email = email
            };
            FamUser FUparameters = new FamUser(Passport)
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            //FU.BirthDate = BirthDay + "/" + BirthMonth + "/" + BirthYear;
            if (gendor == "male")
                FUparameters.Gender = FamoBlock.enmGender.Male;
            else
                FUparameters.Gender = FamoBlock.enmGender.Female;


            external_Authentication(FUparameters, Extparameters);
        }
        public static int ExTernalRegisterNewUser(FamoPassport pass, string email, string password, string FirstName, string LastName, string BirthYear, string BirthMonth, string BirthDay, string gender, FamExternalAuthentication ext)
        {
            email = email.Trim();
            FirstName = FirstName.Trim();
            LastName = LastName.Trim();

            //validations
            if (!Methods.IsEmailValid(email) || email == "") return 0;

            FamOption op = new FamOption(pass);
            if (op.GetValue(FamoBase.Const_Option_Pub_UserRegister) != "1")
            {
                HttpContext.Current.Session["msg"] = "تم ايقاف تسجيل المستخدمين";
                HttpContext.Current.Response.Redirect("/message");
            }
            try
            {
                ClassMain.LoginGuest();
                // if (chkGreeting.Checked)
                // {
                FamUser FU = new FamUser(pass);
                FU.Email = email;
                FU.Password = password;
                FU.BirthDate = BirthDay + "/" + BirthMonth + "/" + BirthYear;
                if (gender == "male")
                    FU.Gender = FamoBlock.enmGender.Male;
                else
                    FU.Gender = FamoBlock.enmGender.Female;


                //FU.Agreement.ID = Convert.ToInt32(chkGreeting.Checked);
                FU.Group.ID = 1;
                int l = Convert.ToInt32(ClassMain.GetOptionValue(FamoBlock.Const_Option_Pub_MainLanguage));
                FU.LanguageID = l;
                FU.Status = FamoBlock.enmUserStatus.Active;
                FU.Type = FamoBlock.enmUserType.User;
                FU.FirstName = FirstName;
                FU.LastName = LastName;

                int uid = FU.Save();
                if (uid > 0)
                {
                    // FU.UserName = x.ToString();
                    string fullname = FirstName + (LastName == "" ? "" : "-" + LastName);
                    fullname = fullname.Replace(" ", "");
                    if (Methods.CheckUserNameExist(fullname))
                        FU.UserName = fullname;
                    else
                        FU.UserName = fullname + FU.ID;

                    FU.Update();
                    //store in External tabel
                    ext.User = FU;
                    int extid = ext.Save();
                    if (extid > 0)
                    {
                    }
                    //register customer in famo shop website
                    try
                    {
                        //  RegisterNewCustomer(FU);
                    }
                    catch (Exception ex)
                    {
                        //في حالة حدوث خطأ في تسجيل الكستمر في الشوب فإنه يتم الغاء عملية التسجيل تماماً
                        //any error occured while saving customer, delete the new created user
                        FU.DeleteFinally();
                        return 0;
                    }


                    //create random images at first time
                    ClassMain.BuildRandomImagesAFT(pass, FamoBlock.enmUserType.User, uid);

                    if (pass.Login(FU.Email, FU.Password))
                    {
                        HttpContext.Current.Session["passport"] = pass;
                        //في تسجيل الدخول للمره الثانية بدون إكمال التسجيل
                        //SetStep("/person/" + Passport.UserName.Trim() + "/setting");//حفظ الخطوة الثانية للتسجيل 
                        //Response.Redirect("~/usrmain.aspx?uid=" + x + "&pg=6");
                        ClassMain.RedirectPage(pass);
                    }

                    return uid;
                }
                else
                {
                    //lblmesg.Text = "لم يتم التسجيل";
                }
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

    }

}