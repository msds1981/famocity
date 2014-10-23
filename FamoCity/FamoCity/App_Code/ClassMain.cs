using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using mUtilities;
using System.Data;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Net;
using System.Web.Security;
using FamoCity.NopService;
using FamoCity.PersonalService;

namespace FamoCity
{
    public class ClassMain
    {
        #region Constants
        public const string DefaultsFolder = "/images/defaults";
        public const string respath = "FamoCity.App_GlobalResources.";
        public const string respublic = "FamoCity.App_GlobalResources.publicresource";
        public const string DefaultMalePhoto = DefaultsFolder + "/famo_male.png";
        public const string DefaultFemalePhoto = DefaultsFolder + "/famo_female.png";
        public const string DefaultPhoto1 = DefaultsFolder + "/image-not-found.png";
        public const string DefaultPhoto2 = DefaultsFolder + "/image-not-found2.png";//for activity
        public const string DefaultPhoto3 = DefaultsFolder + "/shop-not-found-image.png";//for products
        public const string DefaultPhoto4 = DefaultsFolder + "/image-not-found4.png";//for albums
        public const string DefaultShopPhoto = DefaultsFolder + "/company.png";

        public const int MaxRandomImages = 11;//عدد المجلدات التي تحتوي على الصور العشوائيه الافتراضيه

        const string imgadd = "/newfiles/images/friends-icons/6.png";//add request
        //        const string imgadd1 = "/newfiles/images/friends-icons/add_1.png";//add request
        //        const string imgadd2 = "/newfiles/images/friends-icons/add_2.png";
        const string imgcancel = "/newfiles/images/friends-icons/3.png";//cancel request,not allow request
        //        const string imgcancel1 = "/newfiles/images/friends-icons/Delete-Request_1.png";//cancel request,not allow request
        //        const string imgcancel2 = "/newfiles/images/friends-icons/Delete-Request_2.png";
        const string imgunfr = "/newfiles/images/friends-icons/1.png";//unfriend
        //        const string imgunfr1 = "/newfiles/images/friends-icons/Un-friend_1.png";//unfriend
        //        const string imgunfr2 = "/newfiles/images/friends-icons/Un-friend_2.png";
        const string imgallow = "/newfiles/images/friends-icons/4.png";//allow request
        //        const string imgallow1 = "/newfiles/images/friends-icons/Confirm_1.png";//allow request
        //        const string imgallow2 = "/newfiles/images/friends-icons/Confirm_2.png";
        const string imgchat = "/newfiles/images/friends-icons/5.png";
        //        const string imgchat1 = "/newfiles/images/friends-icons/chat_1.png";
        //        const string imgchat2 = "/newfiles/images/friends-icons/chat_2.png";
        #endregion

        #region Properties
        public static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["ConnectionStr1"].ConnectionString; } }
        public static string WebHosting { get { return ConfigurationManager.AppSettings["webHost"]; } }
        public static string WebHostingShop { get { return ConfigurationManager.AppSettings["webHostShop"]; } }
        public static string EmailSender { get { return ConfigurationManager.AppSettings["EmailSender"]; } }
        public static string EmailSenderPass { get { return ConfigurationManager.AppSettings["EmailPassword"]; } }
        public static string EmailSubject { get { return ConfigurationManager.AppSettings["EmailSubject"]; } }
        public static int MaximumFileSize { get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxFileSize"]); } }
        public static string ImageExtensions { get { return ConfigurationManager.AppSettings["ImgExtension"]; } }
        public static string FolderPath { get { return ConfigurationManager.AppSettings["FolderPath"]; } }

        public static string serviceUser { get { return ConfigurationManager.AppSettings["shopServiceAdmin"]; } }
        public static string servicePass { get { return ConfigurationManager.AppSettings["shopServiceAdminPass"]; } }
        public static string EncryptKey { get { return ConfigurationManager.AppSettings["encryptkey"]; } }
        #endregion

        #region Pages
        public static FamoPassport GetPassport()
        {
            if (HttpContext.Current.Session["passport"] != null)
                return (FamoPassport)HttpContext.Current.Session["passport"];
            else
            {
                FamoPassport Passport = new FamoPassport(ClassMain.ConnectionString);
                return Passport;
            }
        }
        public static void SignIn(FamUser user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                user.Email,
                now,
                now.Add(FormsAuthentication.Timeout),
                createPersistentCookie,
                user.Email,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static FamUser ReadTickets()
        {
            var cookieName = FormsAuthentication.FormsCookieName;
            var authCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (authCookie != null)
            {
                // This could throw an exception if it fails the decryption process. Check MachineKeys for consistency.  
                var authenticationTicket = FormsAuthentication.Decrypt(authCookie.Value);

                // Retrieve information from the ticket  
                var username = authenticationTicket.Name;
                var userData = authenticationTicket.UserData;
                FamUser u = new FamUser(GetPassport());
                u.FillUserByEmail(username);
                return u;
            }
            return null;
        }
        public static bool Login(string email, string password)
        {
            /*
            //Local IP Address
            string hostName = Dns.GetHostName();
            IPHostEntry localIp = Dns.GetHostEntry(hostName);

            string loc = localIp.AddressList[2].ToString();


            // Remote IP Address
            string remoteIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (String.IsNullOrEmpty(remoteIp))
            {
                remoteIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }*/

            FamoPassport Passport = new FamoPassport(ClassMain.ConnectionString, HttpContext.Current.Request.UserHostAddress);
            FamOption op = new FamOption(Passport);
            FamUser use = new FamUser(Passport, email, password);
            if (use.ID > 0)
            {
                if (op.GetValue(FamoBase.Const_Option_Pub_LoginUsers) != "1" && use.Type == FamoBase.enmUserType.User)
                {
                    HttpContext.Current.Session["msg"] = "تم ايقاف تسجيل الدخول للمستخدمين";
                    HttpContext.Current.Response.Redirect("/message");
                }
                else if (op.GetValue(FamoBase.Const_Option_Pub_LoginShops) != "1" && use.Type == FamoBase.enmUserType.Shop)
                {
                    HttpContext.Current.Session["msg"] = "تم ايقاف تسجيل الدخول للشركات";
                    HttpContext.Current.Response.Redirect("/message");
                }


                if (Passport.Login(email, password))
                {
                    HttpContext.Current.Session["passport"] = Passport;
                    //if user doesn't have customerId
                    //في حالة عدم وجود هذا المستخدم في موقع المتاجر فإنه سيتم انشاءه 
                    if (Passport.Customer == 0)
                    {
                        RegisterNewCustomer(new FamUser(Passport, Passport.UserID));
                    }
                    //SignIn(use, false);
                    RedirectPage(Passport);
                }
                else
                    return false;
            }
            else
                return false;
            return true;
        }
        public static FamoPassport LoginGuest()
        {
            FamoPassport pas = GetPassport();
            FamUser u = new FamUser(pas);
            pas.LoginUserWithoutRecordLogs(GetOptionValue(FamoBlock.Const_Option_Pub_GuestLogin), GetOptionValue(FamoBlock.Const_Option_Pub_GuestPassword));
            return pas;
        }
        public static string GetOptionValue(string Key)
        {
            FamOption o = new FamOption(GetPassport());
            return o.GetValue(Key);
        }




        // #region encryptionlink
        public static string GetEncryption(string email, string passo, string path)
        {
            EncryptionService en = new EncryptionService(EncryptKey);
            //string pass = en.EncryptText("em=m.saeed@fameway.com&ps=m123&pg=/customer/info");
            string pass = en.EncryptText("" + email + "&" + passo + "&" + path);
            return pass;
        }

        //#endregion
        public static void RedirectPage(FamoPassport Passport)
        {
            if (Passport.UserType == FamoBlock.enmUserType.User || Passport.UserType == FamoBlock.enmUserType.Shop)
            {
                string u = Passport.UserName.Trim() == "" ? Passport.UserID.ToString() : Passport.UserName.Trim();
                HttpContext.Current.Session["passport"] = Passport;

                string red = GetEncryption(Passport.Email, Passport.Password, "/person/" + u + "/arena");
               //HttpContext.Current.Response.Redirect("~/shop/login/route?em=" + red);

                HttpContext.Current.Response.Redirect("/person/" + u + "/arena");
                /*
                //تسجيل الدخول لمستخدم
                string page = GetOptionValue(FamoBlock.Const_Option_Usr_StepsRegister + Passport.UserID.ToString());
                if (page != "")
                    HttpContext.Current.Response.Redirect(page);
                else//+ Passport.UserID.ToString()
                {
                    string u = Passport.UserName.Trim() == "" ? Passport.UserID.ToString() : Passport.UserName.Trim();
                    HttpContext.Current.Response.Redirect("/person/" + u + "/arena");//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$  يجب هنا وضع الصفحة الرئيسية للمستخدم بعد تصميمها
                }*/
            }
            else if (Passport.UserType == FamoBlock.enmUserType.Shop)
            {
                //تسجيل الدخول لشركة

                FamUser usr = new FamUser(Passport, Passport.UserID);


                string red = GetEncryption(Passport.Email, usr.Password, "http://www.famocity.com/shop/customer/info");
                HttpContext.Current.Response.Redirect("http://www.famocity.com/shop/login/route?em=" + red);




                //FamShop s = new FamShop(Passport);
                //s.FillShop(Passport.UserID);
                ////DataTable dt = s.GetUserShops(Passport.UserID);
                //if (s.ID > 0)
                //{
                //    Passport.ShopID = s.ID;//Convert.ToInt32(dt.Rows[0]["shop_id"]);
                //    HttpContext.Current.Session["passport"] = Passport;
                //    HttpContext.Current.Response.Redirect("/Shop/" + s.WebName);
                //}
                //else
                //{
                //    Passport.Logout();
                //    HttpContext.Current.Session["passport"] = Passport;
                //    HttpContext.Current.Session["msg"] = "هذا المتجر غير موجود";
                //    HttpContext.Current.Response.Redirect("/message");
                //}
            }
            else if (Passport.UserType == FamoBlock.enmUserType.Managment)
            {
                //تسجيل الدخول لإدارة الموقع
                HttpContext.Current.Response.Redirect("/admin/main");//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$  يجب هنا وضع الصفحة الرئيسية للإدارة بعد تصميمها
            }
        }

        public static string GetVideoYoutubeEmbed(string key, int width, int height)
        {
            StringUtility u = new StringUtility();
            string value = ClassMain.GetOptionValue(key);
            string str = "";
            if (value.Trim() != "")
            {
                str = "<iframe class='video' width='" + width.ToString() + "' height='" + height.ToString() + "' src='http://www.youtube.com/embed/";
                str += u.getParamValue(value, "v") + "?feature=player_detailpage' frameborder='0' allowfullscreen></iframe>";
            }
            return str;
        }

        public static void FillLanguage(DropDownList ddl, string SelectedElement = "")
        {

            FamLanguage Fm = new FamLanguage(GetPassport());

            ddl.DataSource = Fm.GetLanguages();
            ddl.DataValueField = "lang_id";
            ddl.DataTextField = "name";
            ddl.DataBind();
            if (SelectedElement == "")
            {
                ddl.Items.Add(new ListItem("- اختر من القائمة -", "0"));
                ddl.SelectedValue = "0";
            }
        }
        public static void FillLanguageselected(DropDownList ddl)
        {

            FamLanguage Fm = new FamLanguage(GetPassport());

            ddl.DataSource = Fm.GetLanguages();
            ddl.DataValueField = "lang_id";
            ddl.DataTextField = "name";
            ddl.DataBind();

        }
        public static string getParamValue(string url, string param)
        {
            //get parameter value from URL
            string value = "";
            int mark = url.IndexOf("?");
            param = param + "=";
            string[] Params = url.Substring(mark + 1, url.Length - 1 - mark).Split(new Char[] { '&' });
            //&v=7ROs_1PIiVs
            int start = 0;
            foreach (string s in Params)
            {
                if (s.Contains(param))
                {
                    start = s.IndexOf("=");
                    value = s.Substring(start + 1, s.Length - start - 1);
                }
            }

            return value;
        }

        public static string saveNewVisit(FamoPassport pass, FamoBlock.enmObjectCode objcode, int objid, int userid)
        {
            FamShopVisits visits = new FamShopVisits(pass);
            visits.ObjectCode = objcode;
            visits.ObjectID = objid;
            visits.User.ID = userid;
            return visits.Save().ToString();

        }
        public static void RecordNewVisit(FamoPassport pass, int shopid)
        {
            string visited = "";
            //record visit to this shop only at first time
            if (HttpContext.Current.Session["Visited"] != null)
                visited = HttpContext.Current.Session["Visited"].ToString();

            if (!visited.Contains(shopid.ToString()))
            {
                saveNewVisit(pass, FamoBlock.enmObjectCode.Shops, shopid, pass.UserID);
                HttpContext.Current.Session["Visited"] = (visited + shopid.ToString() + ",");
            }
        }

        //انشاء البوم جديد لحفظ صور المقالات وحفظ رقمه في الخيارات
        public static int GetOrCreateTopicsAlbum(FamoPassport pass, int userid)
        {
            string key = FamoBlock.Const_Option_Usr_TopicAlbum + "_usr" + userid.ToString();
            string value = GetOptionValue(key);

            FamAlbum al = new FamAlbum(pass);
            al.Name = "مشاركات";
            al.User.ID = userid;
            al.Description = "الصور الخاصة بمشاركاتك";
            int albid = al.Save();
            FamOption o = new FamOption(pass);
            o.SetValue(key, albid.ToString());
            return albid;
        }

        public static int RegisterNewUser(FamoPassport pass, string email, string password, string FirstName, string LastName, string BirthYear, string BirthMonth, string BirthDay, string gender)
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

                    //register customer in famo shop website
                    try
                    {
                        RegisterNewCustomer(FU);
                    }
                    catch (Exception ex) { 
                        //في حالة حدوث خطأ في تسجيل الكستمر في الشوب فإنه يتم الغاء عملية التسجيل تماماً
                        //any error occured while saving customer, delete the new created user
                        FU.DeleteFinally();
                        return 0;
                    }
                    

                    //create random images at first time
                    BuildRandomImagesAFT(pass, FamoBlock.enmUserType.User, uid);

                    if (pass.Login(FU.Email, FU.Password))
                    {
                        HttpContext.Current.Session["passport"] = pass;
                        //في تسجيل الدخول للمره الثانية بدون إكمال التسجيل
                        //SetStep("/person/" + Passport.UserName.Trim() + "/setting");//حفظ الخطوة الثانية للتسجيل 
                        //Response.Redirect("~/usrmain.aspx?uid=" + x + "&pg=6");
                        RedirectPage(pass);
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

        public static int RegisterNewCustomer(FamUser user)
        {
            //create new customer in famocity.com/shop via webservice
            int customerId = 0;

            string[] b = user.BirthDate.Split('/');

            //create new customer in famocity.com/shop
            NopService.NopService client = new NopService.NopService();
            //1981-04-24
            bool resb;
            client.RegisterNewCustomer(ClassMain.serviceUser, ClassMain.servicePass, "", user.Email, user.Password, user.FirstName, user.LastName
                , b[2] + "-" + b[1] + "-" + b[0]
                , (user.Gender == FamoBlock.enmGender.Male ? "M" : "F"), out customerId, out resb);


            if (customerId > 0)
            {
                user.CustomerId = customerId;
                user.Update();
            }
            return customerId;
        }

        public static void FillYears(DropDownList ddl)
        {
            ListItem itm = new ListItem("سنة", "0");
            ddl.Items.Add(itm);
            for (int i = 1950; i < 2010; i++)
            {
                itm = new ListItem(i.ToString(), i.ToString());
                ddl.Items.Add(itm);
            }
        }

        #endregion

        #region Shop
        public static FamShop GetShop(string webname)
        {
            string shpName = webname;
            FamShop sh = new FamShop(GetPassport(), shpName);
            try
            {
                if (sh.ID == 0) sh = new FamShop(GetPassport(), Convert.ToInt32(shpName));
            }
            catch (Exception ex) { }
            return sh;
        }

        #endregion

        #region TreeView
        private static int nodesCount;
        private static int[] nodesID;
        private static int[] nodesParentID;
        private static string[] nodesText;
        public static void FillActivityTree(ref TreeView tree, int MasterID = 0, int Language_id = 0, bool NoodSelectable = false)//this function to Fill TreeView With Nodes
        {
            nodesCount = 0;
            nodesID = null;
            nodesParentID = null;
            nodesText = null;
            //() As String
            tree.Nodes.Clear();
            FamoPassport pass = GetPassport();
            FamActivity fa = new FamActivity(pass);


            DataTable DT = new DataTable();

            DT = fa.GetActivitiesByLanguage(Language_id, "name");

            if (DT.Rows.Count <= 0)
            {
                tree.Nodes.Clear();
                return;
            }

            //() As String
            try
            {
                foreach (DataRow drTree in DT.Rows)
                {
                    Array.Resize(ref nodesID, nodesCount + 1);
                    nodesID[nodesCount] = Convert.ToInt32(drTree["actv_id"]);
                    Array.Resize(ref nodesParentID, nodesCount + 1);
                    nodesParentID[nodesCount] = Convert.ToInt32(drTree["parent_id"]);
                    Array.Resize(ref nodesText, nodesCount + 1);
                    if (NoodSelectable)
                        nodesText[nodesCount] = "<a href='/admin/Activity/" + Convert.ToInt32(drTree["actv_id"]) + "'>" + Convert.ToString(drTree["text"]) + "</>";
                    else
                        nodesText[nodesCount] = Convert.ToString(drTree["text"]);
                    nodesCount += 1;
                }
                RecurTree(tree, null, MasterID);

            }
            catch (Exception exx) { }
        }
        private static void RecurTree(TreeView tree, TreeNode parentNode, int parentID)// this function call it self and create the nods
        {
            for (int i = 0; i <= nodesCount - 1; i++)
            {

                if (nodesParentID[i] == parentID)
                {
                    TreeNode tmpNode = new TreeNode(nodesText[i], nodesID[i].ToString());


                    tmpNode.SelectAction = TreeNodeSelectAction.None;

                    RecurTree(tree, tmpNode, nodesID[i]);
                    if (parentNode == null)
                    {
                        tree.Nodes.Add(tmpNode);
                    }
                    else
                    {
                        parentNode.ChildNodes.Add(tmpNode);

                    }
                }
            }
        }
        public static void FillActivityDropDownList(FamoPassport pass, ref DropDownList DropList, int MasterID = 0)
        {
            //string lng = GetOptionValue("");
            //DataTable dt = act.GetActivitiesByLanguage(1, "name", MasterID);
            string space = "";
        }
        private static void FillSubActivities(FamoPassport pass, int parentId)
        {
            FamActivity act = new FamActivity(pass);
            //هل هناك ابناء
            DataTable dt = act.GetActivitiesByLanguage(1, "name", parentId);
            foreach (DataRow dr in dt.Rows)
            {
                FillSubActivities(pass, Convert.ToInt32(dt.Rows[0][""]));
            }
        }
        #endregion

        #region Mail
        public static bool SendEmail(string Email, string HtmlContent)
        {
            return GmailSender.SendMail(EmailSender, EmailSenderPass, Email, EmailSubject, HtmlContent);
        }
        public static bool SendEmail(string Subject, string Email, string HtmlContent)
        {
            return GmailSender.SendMail(EmailSender, EmailSenderPass, Email, Subject, HtmlContent);
        }
        #endregion

        #region FilesUploads
        public static int UploadImage(FileUpload fu, FamoBase.enmObjectCode ObjCode, int ObjID)
        {
            string filePath = "";
            if (!fu.HasFile && fu.PostedFile.ContentLength > MaximumFileSize) return 0;
            filePath = ("~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
            string fileExtension = filePath.Substring(filePath.LastIndexOf(".") + 1, 3);
            fileExtension = fileExtension.ToLower();
            if (fileExtension.Contains(ImageExtensions))
                fu.SaveAs(HttpContext.Current.Server.MapPath(filePath));
            else
                return 0;

            return SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);
        }
        //ajax async file upload
        public static int UploadImage(AjaxControlToolkit.AsyncFileUpload fu, FamoBase.enmObjectCode ObjCode, int ObjID)
        {
            string filePath = "";
            if (!fu.HasFile && fu.PostedFile.ContentLength > MaximumFileSize) return 0;
            filePath = ("~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
            string fileExtension = filePath.Substring(filePath.LastIndexOf(".") + 1, 3);
            fileExtension = fileExtension.ToLower();
            //change the string to ImageExtentions variable
            if (ImageExtensions.Contains(fileExtension))
                fu.SaveAs(HttpContext.Current.Server.MapPath(filePath));
            else
                return 0;

            return SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);
        }
        // end of ajax file upload

        public static int UploadFile(FileUpload fu, FamoBase.enmObjectCode ObjCode, int ObjID)
        {
            string filePath = "";
            if (!fu.HasFile && fu.PostedFile.ContentLength > MaximumFileSize) return 0;
            filePath = ("~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
            string fileExtension = filePath.Substring(filePath.LastIndexOf(".") + 1, 3);
            fileExtension = fileExtension.ToLower();
            if (fileExtension.Contains(ImageExtensions))
                fu.SaveAs(HttpContext.Current.Server.MapPath(filePath));
            else
                return 0;

            return SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);
        }
        public static int UploadFile(FileUpload fu, string targetPath, FamoBase.enmObjectCode ObjCode, int ObjID)
        {
            string filePath = "";
            if (!fu.HasFile && fu.PostedFile.ContentLength > MaximumFileSize) return 0;
            filePath = (targetPath + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
            string fileExtension = filePath.Substring(filePath.LastIndexOf(".") + 1, 3);
            fileExtension = fileExtension.ToLower();
            fu.SaveAs(HttpContext.Current.Server.MapPath(filePath));

            return SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Document);
        }

        public static bool UploadMultiFiles(FamoBase.enmObjectCode ObjCode, int ObjId)
        {

            string filePath = "";
            try
            {
                // Get the HttpFileCollection
                HttpFileCollection hfc = HttpContext.Current.Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        filePath = "~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(hpf.FileName);
                        //filePath = Server.MapPath("~/files_upload/") + DateTime.Now.Ticks + Path.GetExtension(hpf.FileName);
                        hpf.SaveAs(HttpContext.Current.Server.MapPath(filePath));
                        SaveFile(filePath, FamoBlock.enmObjectCode.Products, ObjId, FamoBlock.enmFileType.Photo);
                    }
                }
            }
            catch (Exception ex)
            { }
            return true;
        }
        public static int SaveFile(string FilePath, FamoBase.enmObjectCode ObjCode, int ObjId, FamoBlock.enmFileType FileType)
        {
            FamoPassport pass = GetPassport();
            // حفظ ملف file
            FamFile fi = new FamFile(pass);
            fi.UserID = pass.UserID;
            fi.AlbumID = 0;
            fi.ObjectCode = ObjCode;
            fi.Name = Path.GetFileName(FilePath);
            fi.FileType = FileType;
            fi.ObjectId = ObjId;
            fi.Path = FilePath;
            return fi.Save();

        }

        /* public static int[] UploadImage(Telerik.Web.UI.RadAsyncUpload fu, FamoBase.enmObjectCode ObjCode, int ObjID)
         {
             int[] fid = new int[fu.UploadedFiles.Count];
             for (int i = 0; i < fu.UploadedFiles.Count; i++)
             {
                 string filePath = "~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(fu.UploadedFiles[i].FileName);// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                 string physicpath = HttpContext.Current.Server.MapPath(filePath);
                 try
                 {
                     fu.UploadedFiles[i].SaveAs(physicpath);

                     fid[i] = SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);
                 }
                 catch (Exception ex) { fid[i] = 0; }
             }
             return fid;
         }*/
        /////Telerik save file //Salwa

        /*public static int[] UploadImageForShop(string ShopFolder, Telerik.Web.UI.RadAsyncUpload fu, FamoBase.enmObjectCode ObjCode, int ObjID)
        {
            //FamoFiles عند انشاء متجر يتم انشاء مجلد خاص به داخل مجلد 
            int[] fid = new int[fu.UploadedFiles.Count];
            for (int i = 0; i < fu.UploadedFiles.Count; i++)
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles"))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/FamoFiles");

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder);

                if (Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                {
                    string filePath = "~/FamoFiles/" + ShopFolder + "/" + fu.UploadedFiles[i].FileName;// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                    string physicpath = HttpContext.Current.Server.MapPath(filePath);
                    try
                    {
                        fu.UploadedFiles[i].SaveAs(physicpath);

                        fid[i] = SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);
                    }
                    catch (Exception ex) { fid[i] = 0; }
                }
            }
            return fid;
        }*/
        /////Telerik save file //hisham

        /*public static int[] UploadImage(string Folder, Telerik.Web.UI.RadAsyncUpload fu, FamoBase.enmObjectCode ObjCode, int ObjID)
        {
            int[] fid = new int[fu.UploadedFiles.Count];
            for (int i = 0; i < fu.UploadedFiles.Count; i++)
            {
                string filePath = "~/famofiles/" + Folder + "/" + DateTime.Now.Ticks + Path.GetExtension(fu.UploadedFiles[i].FileName);// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                string physicpath = HttpContext.Current.Server.MapPath(filePath);
                try
                {
                    fu.UploadedFiles[i].SaveAs(physicpath);

                    fid[i] = SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);
                }
                catch (Exception ex) { fid[i] = 0; }
            }
            return fid;
        }*/
        /////Telerik save file //Hisham

        /*public static int[] UploadUnityFiles(string ShopFolder, Telerik.Web.UI.RadAsyncUpload fu, FamoBase.enmObjectCode ObjCode, int ObjID)
        {
            int[] fid = new int[fu.UploadedFiles.Count];
            for (int i = 0; i < fu.UploadedFiles.Count; i++)
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles"))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/FamoFiles");

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder);

                if (Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                {
                    string filePath = "/FamoFiles/" + ShopFolder + "/" + fu.UploadedFiles[i].FileName;// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                    string physicpath = HttpContext.Current.Server.MapPath(filePath);
                    try
                    {
                        fu.UploadedFiles[i].SaveAs(physicpath);

                        fid[i] = SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);
                    }
                    catch (Exception ex) { fid[i] = 0; }
                }
            }
            return fid;
        }*/
        /////Telerik save file //hisham

        /* public static int[] UploadUnityFiles(string platform, FamoPassport passport, int trigger, Telerik.Web.UI.RadAsyncUpload fu, FamoBase.enmObjectCode ObjCode, int ObjID)
            {
                FamTrigger trgr = new FamTrigger(passport, trigger);
                FamScene scen = new FamScene(passport, trgr.Scene.ID);
                string ShopFolder = scen.folder.ToString() + "/" + trgr.folder.ToString() + "/" + platform;
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles"))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/FamoFiles");

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder);


                int[] fid = new int[fu.UploadedFiles.Count];
                //for (int i = 0; i < fu.UploadedFiles.Count; i++)
                //{


                List<string> str = new List<string>();
                FamObject famobj = new FamObject(passport);

                DataTable dt = new DataTable();
                dt = famobj.GetObjectsByPlatFolder(trigger, platform);

                for (int j = 0; j < fu.UploadedFiles.Count; j++)
                {
                    int found = 0;
                    int currobjid = 0;
                    int fileid = 0;
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr["code"].ToString() == fu.UploadedFiles[j].FileName)
                        {
                            found = 1;
                            currobjid = Convert.ToInt32(dr["ojct_id"]);
                            fileid = Convert.ToInt32(dr["file_id"]);
                            break;
                        }

                    }

                    if (found != 0)
                    {
                        FamObject curobj = new FamObject(passport, currobjid);
                        curobj.Version = curobj.Version + 1;
                        curobj.Update();

                        if (Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                        {
                            string filePath = "FamoFiles/" + ShopFolder + "/" + fu.UploadedFiles[j].FileName;// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                            string physicpath = HttpContext.Current.Server.MapPath(filePath);
                            FamFile fil = new FamFile(passport,fileid);
                            fil.Path = filePath;
                            fil.Update();
                            fu.UploadedFiles[j].SaveAs(physicpath);
                        }
                        //fuFile.UploadedFiles[i].
                    }
                    else
                    {
                        if (Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                        {
                            string filePath = "/FamoFiles/" + ShopFolder + "/" + fu.UploadedFiles[j].FileName;// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                            string physicpath = HttpContext.Current.Server.MapPath(filePath);

                            fu.UploadedFiles[j].SaveAs(physicpath);

                            fid[j] = SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);

                            FamFile objfiles = new FamFile(passport, fid[j]);
                            FamObject fobj = new FamObject(passport);
                            fobj.Trigger.ID = Convert.ToInt32(trigger);
                            fobj.Version = 0;
                            fobj.File.ID = fid[j];
                            fobj.ObjectCode = FamoBlock.enmObjectCode.Products;
                            fobj.ObjectId = 0;
                            fobj.Code = objfiles.Name;
                            fobj.plat_folder=platform;
                            int objid = fobj.Save();
                        }
                    }
                }
                return fid;
            }
            */
        public static int[] UploadUnityFilesByfixedpath(string path, string platform, FamoPassport passport, int trigger, FamoBase.enmObjectCode ObjCode, int ObjID)
        {
            FamTrigger trgr = new FamTrigger(passport, trigger);
            FamScene scen = new FamScene(passport, trgr.Scene.ID);
            string ShopFolder = scen.folder.ToString() + "/" + trgr.folder.ToString() + "/" + platform;
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles"))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/FamoFiles");

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder);
            DirectoryInfo files = new DirectoryInfo(path);
            FileInfo[] file = files.GetFiles("*.unity3d");
            //string[] file = Directory.GetFiles(path);
            int[] fid = new int[file.Length];
            //int[] fid = new int[fu.UploadedFiles.Count];
            //for (int i = 0; i < fu.UploadedFiles.Count; i++)
            //{


            List<string> str = new List<string>();
            FamObject famobj = new FamObject(passport);

            DataTable dt = new DataTable();
            dt = famobj.GetObjectsByPlatFolder(trigger, platform);


            for (int j = 0; j < file.Length; j++)
            {
                int found = 0;
                int currobjid = 0;
                int fileid = 0;
                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["code"].ToString() == file[j].Name)
                    {
                        found = 1;
                        currobjid = Convert.ToInt32(dr["ojct_id"]);
                        fileid = Convert.ToInt32(dr["file_id"]);
                        break;
                    }

                }

                if (found != 0)
                {
                    FamObject curobj = new FamObject(passport, currobjid);
                    curobj.Version = curobj.Version + 1;
                    curobj.plat_folder = platform;
                    curobj.Update();

                    if (Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                    {
                        string filePath = "FamoFiles/" + ShopFolder + "/" + file[j].Name;// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                        string physicpath = HttpContext.Current.Server.MapPath(filePath).Replace("\\", "/");
                        FamFile fil = new FamFile(passport, fileid);
                        fil.Path = filePath;

                        if (File.Exists(physicpath))
                            File.Copy(file[j].FullName.Replace("\\", "/"), physicpath, true);// DateTime.Now.Ticks + file[j].Name
                        else
                            File.Copy(file[j].FullName.Replace("\\", "/"), physicpath.Replace("\\", "/"));
                        fil.Update();
                        // fu.UploadedFiles[j].SaveAs(physicpath);
                    }
                    //fuFile.UploadedFiles[i].
                }
                else
                {
                    if (Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/FamoFiles/" + ShopFolder))
                    {
                        // string filePath = "/FamoFiles/" + ShopFolder + "/" + fu.UploadedFiles[j].FileName;// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                        string filePath = "/FamoFiles/" + ShopFolder + "/" + file[j].Name;// fu.TemporaryFolder + fu.UploadedFiles[0].FileName;
                        string physicpath = HttpContext.Current.Server.MapPath(filePath).Replace("\\", "/");

                        // fu.UploadedFiles[j].SaveAs(physicpath);
                        if (File.Exists(physicpath))
                            File.Copy(file[j].FullName.Replace("\\", "/"), physicpath, true);// DateTime.Now.Ticks + file[j].Name
                        // File.Replace(file[j].FullName.Replace("\\", "/"), physicpath, physicpath.Replace(file[j].Name, null));
                        else
                            File.Copy(file[j].FullName.Replace("\\", "/"), physicpath.Replace("\\", "/"));
                        // int filint = SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);
                        fid[j] = SaveFile(filePath, ObjCode, ObjID, FamoBlock.enmFileType.Photo);

                        FamFile objfiles = new FamFile(passport, fid[j]);
                        FamObject fobj = new FamObject(passport);
                        fobj.Trigger.ID = Convert.ToInt32(trigger);
                        fobj.Version = 0;
                        fobj.File.ID = fid[j];
                        // fobj.File.ID = filint;
                        fobj.ObjectCode = FamoBlock.enmObjectCode.Products;
                        fobj.ObjectId = 0;
                        fobj.Code = objfiles.Name;
                        fobj.plat_folder = platform;
                        int objid = fobj.Save();
                    }
                }
            }
            return fid;
        }

        #endregion

        #region Encryption
        public static string EncryptParameter(string Param)
        {
            EncryptionThread t = new EncryptionThread();
            return t.EncryptString(Param, FamoBase.const_Encryption_Param);
        }
        public static string DecryptParameter(string Param)
        {
            EncryptionThread t = new EncryptionThread();
            return t.DecryptString(Param, FamoBase.const_Encryption_Param);
        }
        #endregion

        #region products

        public static string buildProductCode(DataTable dt, FamoPassport pass)
        {
            string divs = "";
            int i = 1;

            foreach (DataRow dr in dt.Rows)
            {
                //needed data
                int prID = Convert.ToInt32(dr["prod_id"]);
                FamProduct p = new FamProduct(pass, prID);
                String imgPath = "";
                FamFile f = new FamFile(pass);
                DataTable dtf = f.GetFiles(FamoBlock.enmObjectCode.Products, prID);
                if (dtf.Rows.Count > 0)
                {
                    int fileID = Convert.ToInt32(dtf.Rows[0]["file_id"]);
                    FamFile file = new FamFile(pass, fileID);
                    imgPath = file.Path.Substring(1);
                }



                //building coded

                divs += "<div class='box'><div class='article'><div class='article_frame'><h3>";
                divs += dr["name"].ToString() + "</h3>";
                //Shop/{shopname}/product/{id}
                //shopMainProd.aspx?p=" + ClassMain.EncryptParameter(prID + "") + "'
                //divs += "</h3><a class='button1' id='b" + i + "' href='/shopMainProd.aspx?p=" + ClassMain.EncryptParameter(prID + "") + "'><img alt='Product image' class='thumbnail' src='" + imgPath.Replace("~", "") + "' /></a>";

                //new popup 
                //divs += "<a class='topopup' id='b" + i + "' href='javascript:void(0);' pid='" + i + "'><img alt='Product image' class='thumbnail' src='" + imgPath.Replace("~", "") + "' /></a>";
                //newest popup
                divs += "<a class='topopup' id='b" + i + "' href='javascript:void(0);' pid='" + prID + "'><img alt='Product image' class='thumbnail' src='" + imgPath.Replace("~", "") + "' /></a>";

                //old popup
                //divs += "<a class='button1' id='b" + i + "' href='/Shop/" + p.Shop.WebName + "/product/" + p.ID.ToString() + "'><img alt='Product image' class='thumbnail' src='" + imgPath.Replace("~", "") + "' /></a>";
                divs += "<h4><span><img src='/images/slashed_icons.png' /></span>";
                divs += p.Brand.Title;
                divs += "</h4><p>";
                divs += dr["description"].ToString();
                divs += "</p></div><ul><li class='sale'>";
                float newPrice = (float)Convert.ToDecimal(dr["new_price"].ToString());
                divs += newPrice + "ريال";
                divs += "</li><li class='offer'>";
                float discount = (float)Convert.ToDecimal(dr["discount"].ToString());
                divs += "خصم " + discount + "ريال";
                divs += "</li></ul></div></div>";
                i++;
            }

            return divs;
        }

        #endregion


        #region SocialProduct
        //created by Hisham
        public static string BuildTopicTagsProduct(FamoPassport pass, DataRow dr, int userid)
        {
            //بناء مقالة واحدة
            StringBuilder output = new StringBuilder();
            output.Append("<div id='tpcbox_" + pass.UserID + "' class='tpc' tpcid='" + pass.UserID + "' pguid='" + userid + "'>");
            output.Append(BuildHeadTopicTagsProduct(pass, dr));
            output.Append(BuildContentTopicTagsProduct(pass, dr));
            //  output.Append(BuildInfoTags(pass, topic));
            //   output.Append(BuildInputCommentTags(pass, topic));
            //output.Append("<input type='hidden' id='hdnbox_" + topic.ID + "' value='" + BuildTopicPopup(pass, topic, false) + "' />");
            //<input" />
            output.Append("</div>");
            return output.ToString();
        }
        private static string BuildHeadTopicTagsProduct(FamoPassport pass, DataRow dr)
        {
            StringBuilder output = new StringBuilder();
            //output.Append("<div class='tpc-head'><img src='/newfiles/images/head_menu_icon.png' /></div>");

            output.Append("<div class='tpc-head comp-green'>");
            /* if (pass.UserID == topic.User.ID)
             {
                 output.Append("<a href='javascript:void(0);' eevntt='lst_" + topic.ID + "' class='topic-option'><img src='/newfiles/images/head_menu_icon.png' /></a>" +
                               "<div id='lst_" + topic.ID + "' class='drpdwn_topic listdowntopic'>" +
                               "<div class='arrow_option-topic'></div>" +
                               "<ul>" +
                               "<li onclick=\"DeleteTopic(document.getElementById('tpcbox_" + topic.ID + "'));\">حذف</li>");
                 string NoCommText = topic.Nocomment ? "السماح بالتعليقات" : "منع التعليقات";
                 output.Append("<li id='nocomopt_" + topic.ID + "' onclick=\"CommentStatus(document.getElementById('tpcinfo" + topic.ID + "'));\">" + NoCommText + "</li>" +
                     //"<li><a href='#'>الاعدادات</a></li>" +
                     //"<li><a href='#'>الاصدقاء المقربون</a></li>" +
                     //"<li><a href='#'>المعارف</a></li>" +
                     //"<li><a href='#'>اقتراح الاصدقاء</a></li>" +
                     //"<li><a href='#'>الغاء الاصدقاء</a></li>" +
                               "</ul>" +
                               "</div>");
             }*/
            output.Append("</div>");//tpc-head
            return output.ToString();
        }

        private static string BuildContentTopicTagsProduct(FamoPassport pass, DataRow dr)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-cont'>");
            //top info
            output.Append(BuildTopInfoTagsProduct(pass, dr));

            //description
            string text = FilterText(dr["ShortDescription"].ToString());
            //string desc = topic.Description.Trim() == "" ? "" : "<p>" + topic.Description + "</p>";
            if (text.Trim() != "")
                output.Append("<div class='tpc-cont-desc'>" +   //onclickDisplay
                          "<p>" + text + "&nbsp;<a href='" + dr["UrlProduct"].ToString() + "' " + "" + " tpcid='" + dr["ProductId"].ToString() + "'>&nbsp;اقرأ المزيد</a></p>" +
                          "</div>");//tpc-cont-desc
            //media
            output.Append(BuildMediaTagsProduct(pass, dr));
            output.Append("</div>");//tpc-cont

            return output.ToString();
        }
        private static string BuildMediaTagsProduct(FamoPassport pass, DataRow dr)
        {

            StringBuilder output = new StringBuilder();
            int objcode = Convert.ToInt32(FamoBlock.enmObjectCode.Products);
            int objid = Convert.ToInt32(dr["ProductId"].ToString());
            //output.Append("<div class='tpc-cont-media'>" +
            //                "<a href='javascript:void(0);' onclick='DisplayTopic(" + topic.ID + ");' tpcid='" + topic.ID + "'>");
            //اذا كانت المقالة تحتوي على رابط 

            output.Append("<a href='" + dr["UrlProduct"].ToString() + "'><img src='" + dr["ProductImage"].ToString() + "' /></a>");
            //if (topic.Link.ToLower().Contains("youtube.com"))
            //{
            //    //video share from youtube
            //    //
            //    output.Append("<div class='youtube-play'>" +
            //                  "<img src='/newfiles/images/youtube.png'>" +
            //                  "</div>");
            //    output.Append("<img src='http" + "://img.youtube.com/vi/" + getParamValue(topic.Link, "v") + "/mqdefault.jpg" +
            //                  "' class='tpc-media-img1'>");
            //}
            //else
            //{
            //    //image share from user
            //    List<FamFile> f = topic.GetFilesList();
            //    if (f.Count > 0)
            //    {
            //        if (f.Count == 1)
            //            output.Append("<img src='/thumb.aspx?image=" + CheckIfImageExist(f[0].Path.Replace("~", ""), DefaultPhoto1) + "&size=300' class='tpc-media-img1 width300' />");
            //        else //if (f.Count == 2)
            //        {
            //            output.Append("<img src='/thumb.aspx?image=" + CheckIfImageExist(f[0].Path.Replace("~", ""), DefaultPhoto1) + "&size=165' class='tpc-media-img2' />");
            //            output.Append("<img src='/thumb.aspx?image=" + CheckIfImageExist(f[1].Path.Replace("~", ""), DefaultPhoto1) + "&size=165' class='tpc-media-img2' />");
            //        }
            //    }
            //}
            //output.Append("</a>");
            //output.Append("</div>");//tpc-cont-media
            return output.ToString();
        }

        private static string BuildInfoTagsProduct(FamoPassport pass, FamTopic topic)
        {
            if (!pass.Logged) return "";
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-info' id='tpcinfo" + topic.ID + "' tpcid='" + topic.ID + "'>");
            //topic's time
            output.Append(BuildTimeTopicTags(pass, topic));
            //count of comments
            output.Append(BuildCountOfComments(pass, topic));
            //count of likes & unlikes
            output.Append(BuildCountOfLikes(pass, topic));
            //count of republishes
            //output.Append(BuildCountOfPublish(pass, topic_id));
            output.Append("</div>");//tpc-info
            return output.ToString();
        }


        private static string BuildTopInfoTagsProduct(FamoPassport pass, DataRow dr, string suffix = "")
        {
            string onclickDisplay = "", onclickLoad = "", pageLink = "javascript:void(0);";
            int objcode = Convert.ToInt32(FamoBlock.enmObjectCode.Products);
            int objid = Convert.ToInt32(dr["ProductId"].ToString()); ;
            //EventLoadTopic : امكانية فتح نافذة البوب اب عند الضغط على العنصر
            //onclickDisplay = "onclick='DisplayTopic(" + topic.ID + ");'";
            //عدم تمكين النقر على صورتي في حالة الاطلاع على صفحتي
            onclickLoad = /*pass.UserID != topic.User.ID ? */" onclick='LoadPage(" + objid + "," + (int)PageName.Arena + ",0);'"/* : ""*/;
            //pageLink = "usrmain.aspx?uid=" + topic.User.ID + "&pg=" + (int)PageName.Arena + "";
            //معلومات صاحب المقالة

            string photo = "", username = "";
            try
            {
                photo = dr["VendorImage"].ToString();
                username = dr["VendorName"].ToString();

            }
            catch (Exception ex) { }

            string userlink = dr["UrlVendor"].ToString();
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-top-info" + suffix + "'>");
            //tpc-cont-photo
            output.Append("<div class='tpc-cont-photo" + suffix + "'>" +//\"uid=" + topic.User.ID + "\"
                           "<a href='" + pageLink/*userlink */+ "' " + onclickLoad + ">" + // objcode='" + objcode + "' objid='" + objid + "'
                           "<img src='" + photo + "' />" +
                           "</a>" +
                           "</div>");//tpc-cont-photo
            //tpc-cont-detail
            output.Append("<div class='tpc-cont-detail" + suffix + "'>" +
                          "<div class='arrow_right" + suffix.Replace("-", "_") + "'></div>");
            output.Append("<div class='tpc-cont-name" + suffix + "'>" +
                          "<label>اضاف</label>&nbsp;" +
                          "<a href='" + userlink + "' " + onclickLoad + ">" + username + "</a>" + // objcode='" + objcode + "' objid='" + objid + "'
                          "&nbsp;<label></label>" +
                          "</div>");//tpc-cont-name



            output.Append("</div>");//tpc-cont-detail


            output.Append("</div>");//tpc-top-info
            return output.ToString();
        }
        public static string BuildInputCommentTagsProduct(FamoPassport pass, FamTopic topic)
        {
            /*
            <div class="tpc-comm">
                <div class="tpc-comm-photo">
                    <img src="userphoto.jpg" />
                </div>
                <div class="tpc-comm-input">
                    <input type="text" id="txtNewComm_2" class="txtNewComm" onkeypress="return onKeyPressNewComment(event,document.getElementById('txtNewComm_2').value,2);" tpcid="2"/>
                </div>
            </div>

             */

            if (!pass.Logged) return "";
            StringBuilder output = new StringBuilder();
            if (topic.Nocomment) return "";//في حالة عدم السماح للتعليقات
            string textid = "txtNewComm_" + topic.ID;

            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();
            dt = serv.GetCustomerData(ClassMain.serviceUser, ClassMain.servicePass, pass.Customer, true).Tables[0];
            string uphoto = ClassMain.getUserLogoPath(pass, pass.UserID);
            try
            {
                uphoto = dt.Rows[0]["CustomerImage"].ToString();
            }
            catch (Exception ex) { }
            output.Append("<div class='tpc-comm'>" +
                          "<div class='tpc-comm-photo'>" +
                          "<img src='" + uphoto + "' />" +
                          "</div>");//tpc-comm-photo
            output.Append("<div class='tpc-comm-input'>" +
                          "<input type='text' id='" + textid + "' class='txtNewComm' onkeypress='return onKeyPressNewComment(event,document.getElementById(\"" + textid + "\")," + topic.ID + ");' tpcid='" + topic.ID + "'/>" +
                          "</div>");//tpc-comm-input
            output.Append("</div>");//tpc-comm

            return output.ToString();
        }

        #endregion

        #region Social
        public static string BuildTopicsArea(FamoPassport pass, FamUser user)
        {
            //بناء المحتوى الرئيسي للمقالات
            StringBuilder output = new StringBuilder();
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='topics' class='content-insider'>");

            output.Append("<div class='right_panel'>");
            output.Append("<div class='last-news-right-panel'>أخر الاخبار</div>");//for tablet


            //قراءة جميع مقالات المستخدم واصدقاءه
            FamTopic tpc = new FamTopic(pass);
            List<FamTopic> top;
            if (user.ID == pass.UserID)//في حالة الاطلاع على مشاركاتي
                top = tpc.GetNewestTopicsList(user.ID);
            else
                top = tpc.GetMyNewestTopicsList(user.ID);

            int rows = top.Count;
            DataTable dtProducts = new DataTable();

            //Read Products from commerce site 
            //يتم عرض المنتجات فقط في صفحتي
            if (pass.UserID == user.ID)
            {
                NopService.NopService servc = new NopService.NopService();
                try
                {
                    dtProducts = servc.GetFollowingProducts(serviceUser, servicePass, pass.Customer, true).Tables[0];

                    rows += dtProducts.Rows.Count;//Increase products rows count
                    dtProducts.DefaultView.Sort = "CreatedUtc desc";
                    dtProducts = dtProducts.DefaultView.ToTable();
                }
                catch (Exception ex) { }
            }

            //    <input class="hdnFirstTopId" type="hidden" value="0" />
            //    <input class="hdnLastTopId" type="hidden" value="0" />
            //output.Append("<input class='hdnFirstTopId' type='hidden' value='0' />");


            //merge topics with products and display them on Arena

            DateTime _Tdate, _Pdate;
            for (int i = 0; i < rows; i++)
            {
                //_Tdate=top[i].UserCreatedDate;
                //_Pdate=Convert.ToDateTime(dt.Rows[i]);
                //if (_Tdate > _Pdate)
                //{
                //    output.Append(BuildTopicTags(pass, top[i], user.ID));
                //    _cash = Convert.ToDateTime(dt.Rows[i]);
                //}
                //else
                //{ 

                //}
                if (i < top.Count)
                    output.Append(BuildTopicTags(pass, top[i], user.ID));

                if (pass.UserID == user.ID)
                    if (dtProducts != null)
                    {
                        if (i < dtProducts.Rows.Count)
                            output.Append(BuildTopicTagsProduct(pass, dtProducts.Rows[i], user.ID));
                    }
            }

            //foreach (DataRow dr in dt.Rows)
            //{
            //    output.Append(BuildTopicTagsp(pass, dr, user.ID));
            //}

            //foreach (FamTopic t in top)
            //    output.Append(BuildTopicTags(pass, t, user.ID));

            //BuildTopicTags()
            output.Append("</div>");//right_panel
            output.Append("</div>");//topics

            //see more topics
            if (top.Count == 10)
            {
                var lastid = top[9].ID.ToString();
                string onclk = "SeeMoreTopics(" + lastid + ")";
                output.Append("<div class='load_more_bar content-insider'><a href='javascript:void(0);' class='' onclick='" + onclk + "'> للمزيد </a></div>");
            }
            return output.ToString();
        }

        //build topic tags
        public static string BuildTopicTags(FamoPassport pass, FamTopic topic, int userid)
        {
            //بناء مقالة واحدة
            StringBuilder output = new StringBuilder();
            output.Append("<div id='tpcbox_" + topic.ID + "' class='tpc' tpcid='" + topic.ID + "' pguid='" + userid + "'>");
            output.Append(BuildHeadTopicTags(pass, topic));
            output.Append(BuildContentTopicTags(pass, topic));
            output.Append(BuildInfoTags(pass, topic));
            output.Append(BuildInputCommentTags(pass, topic));
            //output.Append("<input type='hidden' id='hdnbox_" + topic.ID + "' value='" + BuildTopicPopup(pass, topic, false) + "' />");
            //<input" />
            output.Append("</div>");
            return output.ToString();
        }

        //build Header ot topic
        private static string BuildHeadTopicTags(FamoPassport pass, FamTopic topic)
        {
            StringBuilder output = new StringBuilder();
            //output.Append("<div class='tpc-head'><img src='/newfiles/images/head_menu_icon.png' /></div>");

            output.Append("<div class='tpc-head'>");
            if (pass.UserID == topic.User.ID)
            {
                output.Append("<a href='javascript:void(0);' eevntt='lst_" + topic.ID + "' class='topic-option'><img src='/newfiles/images/head_menu_icon.png' /></a>" +
                              "<div id='lst_" + topic.ID + "' class='drpdwn_topic listdowntopic'>" +
                              "<div class='arrow_option-topic'></div>" +
                              "<ul>" +
                              "<li onclick=\"DeleteTopic(document.getElementById('tpcbox_" + topic.ID + "'));\">حذف</li>");
                string NoCommText = topic.Nocomment ? "السماح بالتعليقات" : "منع التعليقات";
                output.Append("<li id='nocomopt_" + topic.ID + "' onclick=\"CommentStatus(document.getElementById('tpcinfo" + topic.ID + "'));\">" + NoCommText + "</li>" +
                    //"<li><a href='#'>الاعدادات</a></li>" +
                    //"<li><a href='#'>الاصدقاء المقربون</a></li>" +
                    //"<li><a href='#'>المعارف</a></li>" +
                    //"<li><a href='#'>اقتراح الاصدقاء</a></li>" +
                    //"<li><a href='#'>الغاء الاصدقاء</a></li>" +
                              "</ul>" +
                              "</div>");
            }
            output.Append("</div>");//tpc-head
            return output.ToString();
        }

        //build content of topic
        private static string BuildContentTopicTags(FamoPassport pass, FamTopic topic)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-cont'>");
            //top info
            output.Append(BuildTopInfoTags(pass, topic));

            //description
            string text = FilterText(topic.Description);
            //string desc = topic.Description.Trim() == "" ? "" : "<p>" + topic.Description + "</p>";
            if (text.Trim() != "")
                output.Append("<div class='tpc-cont-desc'>" +   //onclickDisplay
                          "<p>" + text + "&nbsp;<a href='javascript:void(0);' " + "onclick='DisplayTopic(" + topic.ID + ");'" + " tpcid='" + topic.ID + "'>&nbsp;اقرأ المزيد</a></p>" +
                          "</div>");//tpc-cont-desc
            //media
            output.Append(BuildMediaTags(pass, topic));
            output.Append("</div>");//tpc-cont

            return output.ToString();
        }
        private static string BuildTopInfoTags(FamoPassport pass, FamTopic topic, string suffix = "")
        {
            string onclickDisplay = "", onclickLoad = "", pageLink = "javascript:void(0);";
            int objcode = Convert.ToInt32(topic.ObjectCode);
            int objid = topic.ObjectId;
            //EventLoadTopic : امكانية فتح نافذة البوب اب عند الضغط على العنصر
            onclickDisplay = "onclick='DisplayTopic(" + topic.ID + ");'";
            //عدم تمكين النقر على صورتي في حالة الاطلاع على صفحتي
            onclickLoad = /*pass.UserID != topic.User.ID ? */" onclick='LoadPage(" + topic.User.ID + "," + (int)PageName.Arena + ",0);'"/* : ""*/;
            //pageLink = "usrmain.aspx?uid=" + topic.User.ID + "&pg=" + (int)PageName.Arena + "";
            //معلومات صاحب المقالة
            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();
            try
            {
                dt = serv.GetCustomerData(serviceUser, servicePass, topic.User.CustomerId, true).Tables[0];
            }
            catch (Exception ex) { }
            string photo = getUserLogoPath(pass, topic.User.ID), username = topic.User.FullName;
            try
            {
                photo = dt.Rows[0]["CustomerImage"].ToString();
                username = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();

            }
            catch (Exception ex) { }

            string userlink = WebHosting + "person/" + topic.User.ID + "/Arena";
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-top-info" + suffix + "'>");
            //tpc-cont-photo
            output.Append("<div class='tpc-cont-photo" + suffix + "'>" +//\"uid=" + topic.User.ID + "\"
                           "<a href='" + pageLink /*userlink*/ + "' " + onclickLoad + ">" + // objcode='" + objcode + "' objid='" + objid + "'
                           "<img src='" + photo + "' />" +
                           "</a>" +
                           "</div>");//tpc-cont-photo
            //tpc-cont-detail
            output.Append("<div class='tpc-cont-detail" + suffix + "'>" +
                          "<div class='arrow_right" + suffix.Replace("-", "_") + "'></div>");
            output.Append("<div class='tpc-cont-name" + suffix + "'>" +
                          "<label>اضاف</label>&nbsp;" +
                          "<a href='" + userlink + "' " + onclickLoad + ">" + username + "</a>" + // objcode='" + objcode + "' objid='" + objid + "'
                          "&nbsp;<label>مشاركة جديدة</label>" +
                          "</div>");//tpc-cont-name



            output.Append("</div>");//tpc-cont-detail


            output.Append("</div>");//tpc-top-info
            return output.ToString();
        }
        private static string BuildMediaTags(FamoPassport pass, FamTopic topic)
        {

            StringBuilder output = new StringBuilder();
            int objcode = Convert.ToInt32(topic.TopicType);
            int objid = topic.ID;
            output.Append("<div class='tpc-cont-media'>" +
                            "<a href='javascript:void(0);' onclick='DisplayTopic(" + topic.ID + ");' tpcid='" + topic.ID + "'>");
            //اذا كانت المقالة تحتوي على رابط 
            if (topic.Link.ToLower().Contains("youtube.com"))
            {
                //video share from youtube
                //
                output.Append("<div class='youtube-play'>" +
                              "<img src='/newfiles/images/youtube.png'>" +
                              "</div>");
                output.Append("<img src='http" + "://img.youtube.com/vi/" + getParamValue(topic.Link, "v") + "/mqdefault.jpg" +
                              "' class='tpc-media-img1'>");
            }
            else
            {
                //image share from user
                List<FamFile> f = topic.GetFilesList();
                if (f.Count > 0)
                {
                    if (f.Count == 1)
                        output.Append("<img src='/thumb.aspx?image=" + CheckIfImageExist(f[0].Path.Replace("~", ""), DefaultPhoto1) + "&size=300' class='tpc-media-img1 width300' />");
                    else //if (f.Count == 2)
                    {
                        output.Append("<img src='/thumb.aspx?image=" + CheckIfImageExist(f[0].Path.Replace("~", ""), DefaultPhoto1) + "&size=165' class='tpc-media-img2' />");
                        output.Append("<img src='/thumb.aspx?image=" + CheckIfImageExist(f[1].Path.Replace("~", ""), DefaultPhoto1) + "&size=165' class='tpc-media-img2' />");
                    }
                }
            }
            output.Append("</a>");
            output.Append("</div>");//tpc-cont-media
            return output.ToString();
        }

        private static string getContentOfURL(FamTopic topic)
        {
            //اعادة محتوى مسار الرابط
            StringBuilder str = new StringBuilder();
            string filename = "";
            string link = topic.Link.ToLower();
            try
            {
                if (link.Contains("youtube.com") && link.Contains("watch?"))
                {
                    filename = getParamValue(link, "v");
                    str.Append("<iframe width='550' height='315' src='//www.youtube.com/embed/" + filename + "' frameborder='0' allowfullscreen></iframe>");
                }

                /*if (Link.Contains("youtube"))
                {

                    filename = getParamValue(Link, "v");
                    str.Append("<object" +
                        "<param name='movie' value='" + Link + "'>" +
                        "<param name='allowFullScreen' value='true'><param name='allowScriptAccess' value='always'><param name='wmode' value='transparent'></param>" +
                        "<embed src='http://www.youtube.com/v/" + filename + "?version=3&feature=player_detailpage&autoplay=1' type='application/x-shockwave-flash' allowfullscreen='true' allowScriptAccess='always' " +
                        "width='470' height='280'></object>");
                }
                else if (Link.Contains("vimeo"))
                {
                    //http://vimeo.com/1808434
                    int s = Link.IndexOf("com/");
                    filename = Link.Substring(s + 4, Link.Length - s - 4);
                    str += "<iframe src='http://player.vimeo.com/video/30637945?title=0&amp;byline=0&amp;portrait=0' width='470' height='280' frameborder='0' webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>";
                }
                else
                {
                    str += "<a href='" + Link + "' target='_blank'>" + Link + "</a>";
                }*/
            }
            catch (Exception ex)
            {
                str.Append("<a href='" + link + "' target='_blank'>" + link + "</a>");
            }
            return str.ToString();
        }

        //build tags of tpc-info
        private static string BuildInfoTags(FamoPassport pass, FamTopic topic)
        {
            if (!pass.Logged) return "";
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-info' id='tpcinfo" + topic.ID + "' tpcid='" + topic.ID + "'>");
            //topic's time
            output.Append(BuildTimeTopicTags(pass, topic));
            //count of comments
            output.Append(BuildCountOfComments(pass, topic));
            //count of likes & unlikes
            output.Append(BuildCountOfLikes(pass, topic));
            //count of republishes
            //output.Append(BuildCountOfPublish(pass, topic_id));
            output.Append("</div>");//tpc-info
            return output.ToString();
        }

        private static string BuildTimeTopicTags(FamoPassport pass, FamTopic topic)
        {
            /*
             <div class="tpc-info-time">
                            <label>
                                منذ 5 ساعات</label>
                        </div>
             */
            if (!pass.Logged) return "";
            StringBuilder output = new StringBuilder();
            string pretty = GetPrettyDate(topic.UserCreatedDate);
            output.Append("<div class='tpc-info-time'>" +
                          "<label title='" + topic.UserCreatedDate + "'>" + pretty + "</label>" +
                          "</div>");//tpc-info-time
            return output.ToString();
        }

        private static string BuildCountOfComments(FamoPassport pass, FamTopic topic, string suffix = "")
        {
            /*<div class="tpc-info-opt">
                <!-- Display Topic Details -->
                <a href="javascript:void(0);" onclick="DisplayTopic(2);" tpcid="2">
                    <img src="comment.jpg" />
                    <label class="comments_2" title="عدد التعليقات">
                        223</label>
                </a>
            </div>*/
            FamComment com = new FamComment(pass);
            //عدد التعليقات
            int count = com.GetNumbersOfComments(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic.ID);
            //بعض اسماء المعلقين
            string names = com.GetUserOfComment(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic.ID);
            StringBuilder output = new StringBuilder();
            //commcounts تم اضافتها من اجل تحديث عدد التعليقات لكل المقالات اثناء التصفح
            //tpcid new attribute added to label tag
            output.Append("<div class='tpc-info-opt cursor" + suffix + "' onclick='DisplayTopic(" + topic.ID + ");' tpcid='" + topic.ID + "'>" +
                           "<img src='/newfiles/images/comments_ico.png' />" +
                           "<label class='commcounts ' id='commentscount_" + topic.ID + "' tpcid='" + topic.ID + "' title='" + names + "'>" + count + "</label>" +
                           "</div>");
            return output.ToString();
        }
        private static string BuildCountOfLikes(FamoPassport pass, FamTopic topic, string suffix = "")
        {
            FamVote v = new FamVote(pass);
            //Like & unLike count
            int likecount = v.GetVoteUpCount(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic.ID);
            int unlikecount = v.GetVoteDownCount(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic.ID);
            //Like & unLike names
            string likenames = v.GetUserOfVoteUp(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic.ID);
            if (likenames.Trim() == "") likenames = "لايوجد";
            string unlikenames = v.GetUserOfVoteDown(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic.ID);
            if (unlikenames.Trim() == "") unlikenames = "لايوجد";

            //Like & unLike action
            DataTable dt = v.GetUserVotes(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic.ID, pass.UserID);

            //the ID of <a> for like and unlike event
            string LikeTageId = "AddLike_" + topic.ID;
            string UnLikeTageId = "AddUnLike_" + topic.ID;

            string likeOnClick = "onclick='AddLike(" + topic.ID + ");'";//document.getElementById(\"" + LikeTageId + "\"),
            string unlikeOnClick = "onclick='AddUnLike(" + topic.ID + ");'";//document.getElementById(\"" + UnLikeTageId + "\"),

            string LikeCursor = " cursor";
            string UnLikeCursor = " cursor";

            FamoBlock.enmVoteStatus status;
            if (dt.Rows.Count > 0)
            {
                //لا يمكن عمل 'اعجاب' و 'عدم اعجاب' في نفس الوقت 
                //اذا عمل 'اعجاب' لا يمكنه عمل 'اعجاب' مره اخرى مع امكانية عمل 'عدم اعجاب' والعكس صحيح
                status = (FamoBlock.enmVoteStatus)Convert.ToInt32(dt.Rows[0]["status"]);
                if (status == FamoBlock.enmVoteStatus.VoteUp)
                {
                    likeOnClick = "";
                    LikeCursor = "";
                }

                if (status == FamoBlock.enmVoteStatus.VoteDown)
                {
                    unlikeOnClick = "";
                    UnLikeCursor = "";
                }
            }

            StringBuilder output = new StringBuilder();
            /*<div class="tpc-info-opt">
                <!-- Add Like to topic -->
                <a href="javascript:void(0);" onclick="AddLike(2);" tpcid="2">
                    <img src="Like.jpg" />
                    <label class="likes_2" title="عدد الاعجاب">
                        44</label>
                </a>
            </div>*/


            //like tags
            output.Append("<div id='" + LikeTageId + "' class='tpc-info-opt" + suffix + LikeCursor + "' " + likeOnClick + " tpcid='" + topic.ID + "'>" +
                // "<a id='" + LikeTageId + "' href='javascript:void(0);' " + likeOnClick + " tpcid='" + topic.ID + "'>" +
                           "<img src='/newfiles/images/like-icon.png' />" +
                           "<label id='likescount_" + topic.ID + "' class='likesCount likes_" + topic.ID + "' title='" + likenames + "' tpcid='" + topic.ID + "'>" + likecount + "</label>" +
                //"</a>" +
                           "</div>");

            /*
             <div class="tpc-info-opt">
                <!-- Display Topic Details -->
                <a href="javascript:void(0);" onclick="UnLike(2);" tpcid="2">
                    <img src="UnLike.jpg" />
                    <label class="unlikes_2" title="عدد الغير معجبين">
                        44</label>
                </a>
            </div>
             */
            //unlike tags
            output.Append("<div id='" + UnLikeTageId + "' class='tpc-info-opt" + suffix + UnLikeCursor + "' " + unlikeOnClick + " tpcid='" + topic.ID + "'>" +
                //"<a id='" + UnLikeTageId + "' href='javascript:void(0);' " + unlikeOnClick + " tpcid='" + topic.ID + "'>" +
                           "<img src='/newfiles/images/unlike-icon.png' />" +
                           "<label id='unlikescount_" + topic.ID + "' class='unlikesCount unlikes_" + topic.ID + "' title='" + unlikenames + "' tpcid='" + topic.ID + "'>" + unlikecount + "</label>" +
                //"</a>" +
                           "</div>");

            return output.ToString();
        }
        private static string BuildCountOfPublish(FamoPassport pass, int topic_id, string suffix = "")
        {
            /*            
             <div class="tpc-info-opt">
                <a href="javascript:void(0);" onclick="RePublish(2);" tpcid="2">
                    <img src="shares.jpg" />
                    <label class="publishes_2" title="عدد المشاركات">
                        2</label>
                </a>
            </div>
            */
            FamTopic t = new FamTopic(pass);
            //عدد التعليقات
            //int count = t.GetTopics(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic_id);
            //بعض اسماء المعلقين
            //string names = t.GetUserOfComment(Convert.ToInt32(FamoBlock.enmObjectCode.Topics), topic_id);
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-info-opt" + suffix + "' onclick='RePublish(" + topic_id + ");' tpcid='" + topic_id + "'>" +
                //"<a href='javascript:void(0);' onclick='RePublish(" + topic_id + ");' tpcid='" + topic_id + "'>" +
                           "<img src='shares.jpg' />" +
                           "<label class='publishes_" + topic_id + "' title='" + 0 + "'>" + 0 + "</label>" +
                //"</a>" +
                           "</div>");
            return output.ToString();
        }
        //build topic view
        private static string BuildTopicView(FamoPassport pass, FamTopic topic)
        {


            /*
             <div class="tpc-view" tpcid="2">
                    <div class="tpc-head">
                    </div>
                    
                    <div class="tpc-cont-view">
                        <div class="tpc_photo_details_view">
                            <div class="tpc-cont-photo-view">
                                <a href="javascript:void(0);" onclick="LoadPage(11,99);" objcode="11" objid="99">
                                    
                                    <img src="/newfiles/images/test.jpg" />
                                </a>
                            </div>
                            <div class="tpc-cont-detail-view">
                                <div class="tpc-cont-name-view">
                                    <label>
                                        اضاف</label>
                                    <a href="javascript:void(0);" onclick="LoadPage(11,99);" objcode="11" objid="99">محمد
                                        سعيد</a><!-- Load User/shop Page -->
                                    <label>
                                        مشاركة جديدة</label>
                                </div>
                                <div class="tpc-cont-desc-view">
                                    <p>
                                        معجب بالامس <a href="javascript:void(0);" onclick="DisplayTopic(2);" tpcid="2">اقرأ المزيد</a><!-- Display Topic Details -->
                                    </p>
                                </div>
                            </div>
                        </div>
                        <!-- Start Media -->
                        <div class="tpc-cont-media-view">
                            <a href="javascript:void(0);" onclick="DisplayTopic(2);" tpcid="2">
                                <!-- Display Topic Details -->
                                <img src="/newfiles/images/test.jpg" class="tpc-media-img1" />
                            </a>
                        </div>
                        <div class="tpc-cont-desc-view2">
                            <p>
                                تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                                تفاصيل تفاصيل تفاصيل <a href="javascript:void(0);" onclick="DisplayTopic(2);" tpcid="2">اقرأ
                                    المزيد</a><!-- Display Topic Details -->
                            </p>
                            <p>
                                <div class="tpc-info-time-view">
                                    منذ 10 ساعات</div>
                                <div class="tpc-info-opt-view">
                                    <!-- Display Topic Details -->
                                    <a href="javascript:void(0);" onclick="DisplayTopic(2);" tpcid="2">
                                        <img src="comment.jpg" />
                                        <label class="comments_2" title="عدد التعليقات">
                                            223</label>
                                    </a>
                                </div>
                                <div class="tpc-info-opt-view">
                                    <!-- Add Like to topic -->
                                    <a href="javascript:void(0);" onclick="AddLike(2);" tpcid="2">
                                        <img src="Like.jpg" />
                                        <label class="likes_2" title="عدد الاعجاب">
                                            44</label>
                                    </a>
                                </div>
                                <div class="tpc-info-opt-view">
                                    <!-- Display Topic Details -->
                                    <a href="javascript:void(0);" onclick="UnLike(2);" tpcid="2">
                                        <img src="UnLike.jpg" />
                                        <label class="unlikes_2" title="عدد الغير معجبين">
                                            44</label>
                                    </a>
                                </div>
                                <div class="tpc-info-opt-view">
                                    <a href="javascript:void(0);" onclick="RePublish(2);" tpcid="2">
                                        <img src="shares.jpg" />
                                        <label class="publishes_2" title="عدد المشاركات">
                                            2</label>
                                    </a>
                                </div>
                            </p>
                        </div>
                        <!-- End Media -->
                    </div>
                    <!-- End Content -->
                    <!-- Start Comment -->
                    <div class="tpc-comm-view">
                        <div class="tpc-comm-photo-view">
                            <img src="userphoto.jpg" />
                        </div>
                        <div class="tpc-comm-input-view">
                            <input type="text" id="txtNewComm_2" class="txtNewComm" onkeypress="return onKeyPressNewComment(event,document.getElementById('txtNewComm_2'),2);"
                                tpcid="2" />
                        </div>
                    </div>
                    <!-- End Comment -->
                </div>
             */

            return "";
        }
        //build input comment
        public static string BuildInputCommentTags(FamoPassport pass, FamTopic topic)
        {
            /*
            <div class="tpc-comm">
                <div class="tpc-comm-photo">
                    <img src="userphoto.jpg" />
                </div>
                <div class="tpc-comm-input">
                    <input type="text" id="txtNewComm_2" class="txtNewComm" onkeypress="return onKeyPressNewComment(event,document.getElementById('txtNewComm_2').value,2);" tpcid="2"/>
                </div>
            </div>

             */

            if (!pass.Logged) return "";
            StringBuilder output = new StringBuilder();
            if (topic.Nocomment) return "";//في حالة عدم السماح للتعليقات
            string textid = "txtNewComm_" + topic.ID;

            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();
            try
            {
                dt = serv.GetCustomerData(ClassMain.serviceUser, ClassMain.servicePass, pass.Customer, true).Tables[0];
            }
            catch (Exception ex) { }
            string uphoto = ClassMain.getUserLogoPath(pass, pass.UserID);
            try
            {
                uphoto = dt.Rows[0]["CustomerImage"].ToString();
            }
            catch (Exception ex) { }
            output.Append("<div class='tpc-comm'>" +
                          "<div class='tpc-comm-photo'>" +
                          "<img src='" + uphoto + "' />" +
                          "</div>");//tpc-comm-photo
            output.Append("<div class='tpc-comm-input'>" +
                          "<input type='text' id='" + textid + "' class='txtNewComm' onkeypress='return onKeyPressNewComment(event,document.getElementById(\"" + textid + "\")," + topic.ID + ");' tpcid='" + topic.ID + "'/>" +
                          "</div>");//tpc-comm-input
            output.Append("</div>");//tpc-comm

            return output.ToString();
        }


        //************ popup topics ***************//
        public static string BuildTopicPopup(FamoPassport pass, FamTopic topic, bool ShowComments = true)
        {
            //show one topic in popup
            StringBuilder output = new StringBuilder();
            //output.Append("<div class='tpc-view'>");
            /*output.Append("<div class='tpc-view-head'>" +
                          "<a href='javascript:void(0);' onclick='ClosePopup(\"#pnlTopic\");'><img class='closepopup' src='/newfiles/images/close.gif' /></a>" +
                          "</div>");
             */
            //media
            output.Append("<div id='popup_left' class='tpc-view-photo-left'>");
            string lnk = topic.Link.ToLower();
            if (lnk.Contains("youtube.com") && lnk.Contains("watch?"))
            {
                //show video
                output.Append(getContentOfURL(topic));
            }
            //else if (topic.Link.ToLower().Contains("vemio.com"))
            //else if (other video website)
            else
            {
                List<FamFile> f = topic.GetFilesList();
                if (f.Count > 0)
                {
                    output.Append("<img src='" + CheckIfImageExist(f[0].Path.Replace("~", ""), DefaultPhoto1) + "' class='tpc-media-img-view' />");
                }
            }

            output.Append("</div>");//tpc-view-photo-left
            output.Append("<div id='content_1' class='tpc-view-right-panel'>");
            //tpc-top-info-view
            output.Append(BuildTopInfoPopup(pass, topic));
            //comments_view
            if (ShowComments) output.Append(BuildComments(pass, topic));
            output.Append("</div'>");//tpc-view-right-panel
            //output.Append("</div'>");//tpc-view
            return output.ToString();
            /*
        <div class="tpc-view">
            <div class="tpc-view-head">
                Head
            </div>
            <div class="tpc-view-photo-left">
                <a href="javascript:void(0);" onclick="DisplayTopic(2);" tpcid="2">
                    <!-- Display Topic Details -->
                    <img src="/newfiles/images/aa11.jpg" class="tpc-media-img-view" />
                </a>
            </div>
            <div id="content_1" class="tpc-view-right-panel">
                <div class="tpc-top-info-view">
                    <div class="tpc-cont-photo-view">
                        <a href="javascript:void(0);" onclick="LoadPage(11,99);" objcode="11" objid="99">
                            <!-- Load User/shop Page -->
                            <img src="/newfiles/images/img.jpg" />
                        </a>
                    </div>
                    <div class="tpc-cont-detail-view">
                        <div class="arrow_right_view">
                        </div>
                        <!-- ++ -->
                        <div class="tpc-cont-name-view">
                            <a href="javascript:void(0);" onclick="LoadPage(11,99);" objcode="11" objid="99">محمد
                                سعيد</a><!-- Load User/shop Page -->
                            <p>
                                <div class="tpc-info-time-view">
                                    منذ 10 ساعات</div>
                            </p>
                        </div>
                    </div>
                    <div class="tpc-cont-desc-view">
                        <p>
                            تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                            تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                            تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                            تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                            تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                            تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                            تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                            تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل
                            تفاصيل تفاصيل <a href="javascript:void(0);" onclick="DisplayTopic(2);" tpcid="2">اقرأ المزيد</a><!-- Display Topic Details -->
                        </p>
                        <!-- info -->
                    </div>
                </div><!-- tpc-top-info-view -->
                
                <!-- view comments -->
             
            </div>
        </div>

             */
        }

        private static string BuildTopInfoPopup(FamoPassport pass, FamTopic topic)
        {
            //description of topic for popup
            //معلومات صاحب المقالة
            int objcode = Convert.ToInt32(topic.TopicType);
            int objid = topic.ID;
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-top-info-view'>");
            //tpc-cont-photo


            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();
            dt = serv.GetCustomerData(serviceUser, servicePass, topic.User.CustomerId, true).Tables[0];
            string photo = getUserLogoPath(pass, topic.User.ID), username = topic.User.FullName;
            try
            {
                photo = dt.Rows[0]["CustomerImage"].ToString();
                username = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();

            }
            catch (Exception ex) { }
            output.Append("<div class='tpc-cont-photo-view'>" +
                           "<a href='javascript:void(0);' onclick='LoadPage(" + objid + "," + (int)PageName.Arena + ",0);' objcode='" + objcode + "' objid='" + objid + "'>" +
                           "<img src='" + photo + "' />" +
                           "</a>" +
                           "</div>");//tpc-cont-photo-view
            //tpc-cont-detail-view
            output.Append("<div class='tpc-cont-detail-view'>" +
                          "<div class='arrow_right_view'></div>");
            output.Append("<div class='tpc-cont-name-view'>" +
                          "<label>اضاف</label>&nbsp;" +
                          "<a href='javascript:void(0);' onclick='LoadPage(" + objid + "," + (int)PageName.Arena + ",0);' objcode='" + objcode + "' objid='" + objid + "'>" + username + "</a>" +
                          "&nbsp;<label>مشاركة جديدة</label>" +
                          "</div>");//tpc-cont-name-view
            output.Append("</div>");//tpc-cont-detail-view

            string text = FilterText(topic.Description.Trim());//decode the text from html tags
            string desc = text == "" ? "" : "<p>" + text + "</p>";
            output.Append("<div class='tpc-cont-desc-view'>" + desc);

            //tpc-info-view
            output.Append(BuildInfoTagsPopup(pass, topic));
            output.Append("</div>");//tpc-cont-desc-view
            output.Append("</div>");//tpc-top-info-view
            return output.ToString();
        }

        private static string BuildInfoTagsPopup(FamoPassport pass, FamTopic topic)
        {
            if (!pass.Logged) return "";
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-info-view'>");
            //count of comments
            output.Append(BuildCountOfComments(pass, topic, "-view"));
            //count of likes & unlikes
            output.Append(BuildCountOfLikes(pass, topic, "-view"));
            //count of republishes
            //output.Append(BuildCountOfPublish(pass, topic_id));
            output.Append("</div>");//tpc-info-view
            return output.ToString();
        }


        /*       private static string BuildComments(FamoPassport pass, FamTopic topic)
               {
                   StringBuilder output = new StringBuilder();
                   output.Append("<div class='comments_view'><div class='tpc-view-add-comment'><div class='tpc-view-add-input'><input type='text' tpcid='2' onkeypress='return onKeyPressNewComment(event,document.getElementById('txtNewComm_2'),2);'class='txtNewComm' id='txtNewComm_2'></div><div class='tpc-view-add-photo'><img src='/newfiles/images/img.jpg'></div></div><ul><!-- First Comment Start --><div class='tpc-comment-view-box'><li class=''><div class='tpc-view-comment-auther'>مصطفى محمد رحاب</div><div class='tpc-view-comment-topic'>" +
                                               "ياجدعان المووضوع ده فاجر اوى ولازم كلنا نتابعه لانه هيفرق معانا الفتره اللى جايةدي كتير فوق مانتو متخيلين مش عاوزين كسل بقا اللى يكرمكو</div><div class='tpc-view-comment-time'>منذ عشر ساعات</div></li><div class='tpc-comment-user-pic'><img src='/newfiles/images/img.jpg'></div></div><!-- Second Comment Start --><div class='tpc-comment-view-box'><li class=''><div class='tpc-view-comment-auther'>مصطفى محمد رحاب</div><div class='tpc-view-comment-topic'>ياجدعان المووضوع ده فاجر اوى ولازم كلنا نتابعه لانه هيفرق معانا الفتره اللى جايةدي كتير فوق مانتو متخيلين مش عاوزين كسل بقا اللى يكرمكو</div>" +
                                           "<div class='tpc-view-comment-time'>منذ عشر ساعات</div></li><div class='tpc-comment-user-pic'><img src='/newfiles/images/img.jpg'></div></div><!-- Third Comment Start --><div class='tpc-comment-view-box'><li class=''><div class='tpc-view-comment-auther'>مصطفى محمد رحاب</div><div class='tpc-view-comment-topic'>ياجدعان المووضوع ده فاجر اوى ولازم كلنا نتابعه لانه هيفرق معانا الفتره اللى جايةدي كتير فوق مانتو متخيلين مش عاوزين كسل بقا اللى يكرمكو</div><div class='tpc-view-comment-time'>منذ عشر ساعات</div></li><div class='tpc-comment-user-pic'><img src='/newfiles/images/img.jpg'></div></div></ul></div>");
                   return output.ToString();

                   /*
                    <div class="comments_view">
                               <div class="tpc-view-add-comment">
                                   <div class="tpc-view-add-input">
                                       <input type="text" tpcid="2" onkeypress="return onKeyPressNewComment(event,document.getElementById('txtNewComm_2'),2);"
                                           class="txtNewComm" id="txtNewComm_2">
                                   </div>
                                   <div class="tpc-view-add-photo">
                                       <img src="/newfiles/images/img.jpg"></div>
                               </div>
                               <ul>
                                   <!-- First Comment Start -->
                                   <div class="tpc-comment-view-box">
                                       <li class="">
                                           <div class="tpc-view-comment-auther">
                                               مصطفى محمد رحاب</div>
                                           <div class="tpc-view-comment-topic">
                                               ياجدعان المووضوع ده فاجر اوى ولازم كلنا نتابعه لانه هيفرق معانا الفتره اللى جاية
                                               دي كتير فوق مانتو متخيلين مش عاوزين كسل بقا اللى يكرمكو
                                           </div>
                                           <div class="tpc-view-comment-time">
                                               منذ عشر ساعات</div>
                                       </li>
                                       <div class="tpc-comment-user-pic">
                                           <img src="/newfiles/images/img.jpg">
                                       </div>
                                   </div>
                                   <!-- Second Comment Start -->
                                   <div class="tpc-comment-view-box">
                                       <li class="">
                                           <div class="tpc-view-comment-auther">
                                               مصطفى محمد رحاب</div>
                                           <div class="tpc-view-comment-topic">
                                               ياجدعان المووضوع ده فاجر اوى ولازم كلنا نتابعه لانه هيفرق معانا الفتره اللى جاية
                                               دي كتير فوق مانتو متخيلين مش عاوزين كسل بقا اللى يكرمكو
                                           </div>
                                           <div class="tpc-view-comment-time">
                                               منذ عشر ساعات</div>
                                       </li>
                                       <div class="tpc-comment-user-pic">
                                           <img src="/newfiles/images/img.jpg">
                                       </div>
                                   </div>
                                   <!-- Third Comment Start -->
                                   <div class="tpc-comment-view-box">
                                       <li class="">
                                           <div class="tpc-view-comment-auther">
                                               مصطفى محمد رحاب</div>
                                           <div class="tpc-view-comment-topic">
                                               ياجدعان المووضوع ده فاجر اوى ولازم كلنا نتابعه لانه هيفرق معانا الفتره اللى جاية
                                               دي كتير فوق مانتو متخيلين مش عاوزين كسل بقا اللى يكرمكو
                                           </div>
                                           <div class="tpc-view-comment-time">
                                               منذ عشر ساعات</div>
                                       </li>
                                       <div class="tpc-comment-user-pic">
                                           <img src="/newfiles/images/img.jpg">
                                       </div>
                                   </div>
                               </ul>
                           </div>
            

               }
           */
        public static string BuildComments(FamoPassport pass, FamTopic topic)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<div class='comments_view'>");
            //منع التعليقات
            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();
            dt = serv.GetCustomerData(serviceUser, servicePass, topic.User.CustomerId, true).Tables[0];
            string photo = getUserLogoPath(pass, topic.User.ID), username = topic.User.FullName;
            try
            {
                photo = dt.Rows[0]["CustomerImage"].ToString();
                username = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();

            }
            catch (Exception ex) { }
            if (!topic.Nocomment)
            {
                output.Append("<div class='tpc-view-add-comment'>" +
                              "<div class='tpc-view-add-input'>" +
                              "<input type='text' tpcid='" + topic.ID + "' onkeypress='return onKeyPressNewComment(event,document.getElementById(\"txtNewCommPopup_" + topic.ID + "\")," + topic.ID + ");'" +
                              "class='txtNewComm' id='txtNewCommPopup_" + topic.ID + "'>" +
                              "</div>" +
                              "<div class='tpc-view-add-photo'>" +
                              "<img src='" + photo + "'></div>" +
                              "</div>");//tpc-view-add-comment
            }
            output.Append("<ul id='comments-popup'>");


            FamComment comm = new FamComment(pass);
            List<FamComment> f = comm.GetLastCommentsList((int)FamoBlock.enmObjectCode.Topics, topic.ID);
            int i = 1;
            foreach (FamComment fm in f)
            {
                output.Append(BuildOneComment(pass, fm));
                if (i == 10) output.Append(BuildSeeMoreComments(fm));
                i++;
            }

            output.Append("</ul>");//comments-popup
            output.Append("</div>");//comments_view
            return output.ToString();
        }
        public static string BuildOneComment(FamoPassport pass, FamComment comment)
        {
            StringBuilder output = new StringBuilder();
            string pretty = GetPrettyDate(comment.UserCreatedDate);
            output.Append("<div id='commentbox_" + comment.ID + "' class='tpc-comment-view-box' comid='" + comment.ID + "' tpcid='" + comment.ObjectId + "'>" +
                           " <li class=''>" +
                               "<div class='tpc-view-comment-auther'>" + comment.User.FullName +
                               "</div>" +
                               " <div class='tpc-view-comment-topic'>" +
                                 FilterText(comment.Description) +
                              "  </div>" +
                              "  <div class='tpc-view-comment-time' title='" + comment.UserCreatedDate + "'>" +
                                  pretty + " </div> " +
                           "  </li>" +
                          "<div class='tpc-comment-user-pic'>" +
                                 "<img src='" + getUserLogoPath(comment.User) + " '>" +
                          "  </div>" +
                       " </div>");//tpc-comment-view-box

            return output.ToString();
        }
        public static string BuildSeeMoreComments(FamComment comm)
        {
            string divid = "seemore_" + comm.ID;
            StringBuilder output = new StringBuilder();
            output.Append("<div class='tpc-comment-view-box'>" +
                          "<div id='" + divid + "' class='see_more_button' ><a href='javascript:void(0);' onclick='LoadMoreComments(document.getElementById(\"" + divid + "\")," + comm.ObjectId + ")'>قراءة المزيد</a></div>" +
                          "</div>");
            return output.ToString();
        }
        private static string FilterText(string text)
        {
            return text.Replace("<", "&lt;").Replace("'", "&#39;");
        }





        #endregion

        #region User Pages
        public enum PageName
        {
            Arena = 1,
            Photos = 2,
            Friends = 3,
            Shops = 4,
            FamoCityNews = 5,
            Setting = 6
        }
        public static string BuildUserPageWithoutContent(FamoPassport pass, FamoBlock.enmObjectCode usertype, int objid, PageName Page, int tab)
        {
            //بناء صفحه المستخدم
            if (objid == 0) return "";
            StringBuilder output = new StringBuilder();
            FamUser user = new FamUser(pass, objid);
            output.Append(BuildRandomPhotos(pass, objid));//الصور العشوائية
            output.Append(BuildStatisticsBar(pass, user));//شريط الاحصائيات
            return output.ToString();
        }

        //private static PageName CurrentPage;
        public static string BuildUserPage(FamoPassport pass, FamoBlock.enmObjectCode usertype, int objid, PageName Page, int tab)
        {
            //بناء صفحه المستخدم
            if (objid == 0) return "";
            StringBuilder output = new StringBuilder();
            FamUser user = new FamUser(pass, objid);
            output.Append(BuildRandomPhotos(pass, objid));//الصور العشوائية
            output.Append(BuildStatisticsBar(pass, user));//شريط الاحصائيات
            HttpContext.Current.Session["CurrentPage"] = (int)Page;
            if (Page == PageName.Arena)
                output.Append(BuildTopicsArea(pass, user));//محتويات المقالات
            else if (Page == PageName.Friends)
                output.Append(BuildFriendArea(pass, user, (FriendArea)tab));//محتويات صفحة الأصدقاء
            else if (Page == PageName.Shops)
                output.Append(BuildShopPage(pass, user, new FamActivity(pass)));//صفحة المنتجات والمتاجر
            else if (Page == PageName.Photos)
                output.Append(BuildAlbumsArea(pass, user));//صفحة المنتجات والمتاجر
            else if (Page == PageName.Setting)
                output.Append(BuildSettingBox(pass));//صفحة المنتجات والمتاجر
            return output.ToString();
        }

        // hisham Change LoadPage Function

        public static string BuildUserPage(FamoPassport pass, int userid, PageName Page, int tab)
        {
            //بناء صفحه المستخدم
            if (userid == 0) return "";
            StringBuilder output = new StringBuilder();
            FamUser user = new FamUser(pass, userid);
            output.Append(BuildRandomPhotos(pass, userid));//الصور العشوائية
            output.Append(BuildStatisticsBar(pass, user));//شريط الاحصائيات
            HttpContext.Current.Session["CurrentPage"] = (int)Page;
            if (Page == PageName.Arena)
                output.Append(BuildTopicsArea(pass, user));//محتويات المقالات
            else if (Page == PageName.Friends)
                output.Append(BuildFriendArea(pass, user, (FriendArea)tab));//محتويات صفحة الأصدقاء
            else if (Page == PageName.Shops)
                output.Append(BuildShopPage(pass, user, new FamActivity(pass)));//صفحة المنتجات والمتاجر
            else if (Page == PageName.Photos)
                output.Append(BuildAlbumsArea(pass, user));//صفحة المنتجات والمتاجر
            else if (Page == PageName.Setting)
                output.Append(BuildSettingBox(pass));//صفحة المنتجات والمتاجر
            return output.ToString();
        }



        private static string BuildRandomPhotos(FamoPassport pass, int userid)
        {
            //الجزء العلوي من صفحة المستخدم ويحتوي على 
            //الصور العشوائيه
            //القائمة الرئيسية
            //شريط الاحصائيات

            StringBuilder output = new StringBuilder();
            output.Append("<div class='gridContainer clearfix'>" +
                          "<div id='Header_Center_Wrapper'>" +
                          "<div class='status'>");
            output.Append(BuildUserInfoPanel(pass, userid));//معلومات المستخدم الرئيسية
            output.Append("</div>" +
                          "<div class='galleryWrapper'>" +
                          "<div class='galleryInnerWrapper'>" +
                          "<div class='galleryContainer'>");
            output.Append(BuildUserImagesHeader(pass, userid));//الصور العشوائيه
            output.Append("</div>" +
                          "</div>");//galleryInnerWrappe
            output.Append("</div>");//galleryWrapper
            output.Append("</div>");//Header_Center_Wrapper
            output.Append("</div>");//gridContainer clearfix

            return output.ToString();
        }
        private static string BuildStatisticsBar(FamoPassport pass, FamUser user)
        {
            StringBuilder output = new StringBuilder();
            /*
            <div class="gridContainer clearfix">
                <div id="sticky-anchor">
                </div>
                <div id="sticky">
                    <!-- Navbar Wrapper Start -->
                    <div id="Navbar" class="stick">
                        
                        القائمه الرئيسية
                        شريط الاحصائيات
             
                    </div>
                </div>
            </div>
             
             */

            output.Append("<div class='gridContainer clearfix'>" +
                    "<div id='sticky-anchor'>" +
                    "</div>" +
                    "<div id='sticky'>" +
                    "<div id='Navbar' class='stick'>");
            output.Append(BuildMainNavigationMenu(pass, user));//القوائم الرئيسية
            output.Append(BuildUserStatisticsInfo(pass, user));//شريط الاحصائيات
            output.Append("</div>");//Navbar
            output.Append("</div>");//sticky
            output.Append("</div>");//gridContainer clearfix
            return output.ToString();
        }
        private static string BuildMainNavigationMenu(FamoPassport pass, FamUser user)
        {
            //بناء القوائم الرئيسية لصفحة المستخدم
            StringBuilder output = new StringBuilder();
            /*
             <div id="navbar_first_menu">
                        <ul>
                            <li><a id="btnclick" href="#">صفحتي</a></li>
                            <li><a href="#">الصور والفيديو</a></li>
                            <li><a href="#">اصدقائي</a></li>
                            <li><a href="#">متاجر ومنتجات</a></li>
                            <li><a href="#">فيمو سيتي</a></li>
                        </ul>
                        <!-- Menu1 End -->
                    </div>
             */
            //.navactive

            string clsArena = "", clsPhoto = "", clsFrnd = "";
            string pageLink = "usrmain.aspx?uid=" + pass.UserID + "&pg=";
            /*PageName CurrentPage = (PageName)(HttpContext.Current.Session["CurrentPage"] == null ? 0 : HttpContext.Current.Session["CurrentPage"]);
            if (pass.UserID == user.ID)
            {
                clsArena = CurrentPage == PageName.Arena ? "navactive" : "";
                clsPhoto = CurrentPage == PageName.Photos ? "navactive" : "";
                clsFrnd = CurrentPage == PageName.Friends ? "navactive" : "";
            }*/
            //" + (clsFrnd == "" ? "onclick='LoadPage(1," + pass.UserID + "," + (int)PageName.Friends + ")'" : "") + "
            output.Append("<div id='navbar_first_menu'>" +
                          "<ul>" +
                //الصفحة الشخصية
                //"<li><a class='" + clsArena + "' href='" + pageLink + (int)PageName.Arena + "'>صفحتي</a></li>" +//,\"uid=" + pass.UserID + "\"
                          "<li><a class='" + clsArena + "' href='javascript:void(0);' onclick='LoadPage(" + pass.UserID + "," + (int)PageName.Arena + ",1);'>صفحتي</a></li>" +//,\"uid=" + pass.UserID + "\"
                //الصور
                //"<li><a class='" + clsPhoto + "' href='" + pageLink + (int)PageName.Photos + "'>الصور والفيديو</a></li>" +
                          "<li><a class='" + clsPhoto + "' href='javascript:void(0);' onclick='LoadPage(" + pass.UserID + "," + (int)PageName.Photos + ",1);'>الصور والفيديو</a></li>" +
                //الاصدقاء
                //"<li><a class='" + clsFrnd + "' href='" + pageLink + (int)PageName.Friends + "&tab=" + (int)FriendArea.MyFriends + "'>اصدقائي</a></li>" +
                          "<li><a class='" + clsFrnd + "' href='javascript:void(0);' onclick='LoadPage(" + pass.UserID + "," + (int)PageName.Friends + ",1);'>اصدقائي</a></li>" +
                //المتاجر
                //"<li><a class='" + clsFrnd + "' href='" + pageLink + (int)PageName.Shops + "'>متاجر ومنتجات</a></li>" +
                          "<li><a class='" + clsFrnd + "' href='javascript:void(0);' onclick='LoadPage(" + pass.UserID + "," + (int)PageName.Shops + ",1);'>متاجر ومنتجات</a></li>");
            //"<li><a href='" + pageLink + (int)PageName.FamoCityNews + "'>فيمو سيتي</a></li>" +

            string red = GetEncryption(user.Email, user.Password, "http://www.famocity.com/shop");

            //output.Append("<li><a class='" + clsFrnd + "' href='/shop' >Go To Store</a></li>" );
            output.Append("</ul>" +
                          "</div>");
            return output.ToString();

        }
        private static string BuildUserStatisticsInfo(FamoPassport pass, FamUser user)
        {
            //بناء شريط الاحصائيات الاخضر
            StringBuilder output = new StringBuilder();
            output.Append("<div id='navbar_second_menu'>" +
                          "<ul>");
            //nationality
            List<FamContactProfile> c = user.ContractProfile(user.ID);
            string nation = c.Count > 0 ? c[0].Nationality.Text : "";
            string city = c.Count > 0 ? c[0].City : "";
            //output.Append(BuildStatInfo("الجنسية", nation));
            //output.Append(BuildStatInfo("مدينتي", city));
            //number of friends
            output.Append(BuildStatInfoLink(user.FillMyFriendslist().Count, "PreparePage(" + user.ID + "," + (int)PageName.Friends + ");", "الأصدقاء"));//LoadPage('pgfriends')PreparePage(285,3);
            //number of photos
            output.Append(BuildStatInfoLink(user.File.GetFilesCount(user.ID, FamoBlock.enmFileType.Photo), "PreparePage(" + user.ID + "," + (int)PageName.Photos + ");", "الصور"));//LoadPage('pgfriends')
            //number of videos
            //output.Append(BuildInfoLink(user.File.GetFilesCount(user.ID,FamoBlock.enmFileType.Video), "", "الصور"));//LoadPage('pgfriends')
            //number of shop's following
            FamFollow fl = new FamFollow(pass);
            output.Append(BuildStatInfoLink(fl.GetCountFollowers(user.ID, (int)FamoBlock.enmFollowStatus.Follow), "PreparePage(" + user.ID + "," + (int)PageName.Shops + ");", "المتاجر"));//LoadPage('pgfriends')
            output.Append("</ul></div>");//navbar_second_menu

            //زر عدد المشاركات جديدة
            output.Append("<div id='new_posts'></div>");

            //عرض ملخص بيانات الشخص 
            /* output.Append("<div class='almazed'>" +
                           "<img src='/newfiles/images/plus.png' /><p></p>" +
                           "<a href='#'>المزيد</a>" +
                           "</div>");*/
            return output.ToString();
        }
        private static string BuildStatInfoLink(int count, string OnClickEvent, string text)
        {
            //بناء احصائيه واحده في شريط الاحصائيات
            StringBuilder output = new StringBuilder();
            output.Append("<li><div class='menucounter'>" + count +
                          "<div class='arrow_box_white'></div>" + "</div>" +
                          "<div class='menulink'>" +
                          "<a href='javascript:void(0);' onclick='" + OnClickEvent + "'>" + text + "</a>" +
                          "</div>" +
                          "</li>");
            return output.ToString();
        }
        private static string BuildStatInfo(string label, string value)
        {
            //بناء احصائيه واحده في شريط الاحصائيات
            StringBuilder output = new StringBuilder();
            output.Append("<li class='nationality'>" +
                          "<div class='nation'>" + value + "</div>" +
                          "<div class='nation_country'>" + label + "</div>" +
                          "</li>");
            return output.ToString();
        }
        public static string BuildCountOfNewPosts(FamoPassport pass, int userid, int firstid, int count)
        {
            //عدد المشاركات الجديدة
            //اظهار زر مشاركة جديده في حالة وجود مشاركات جديده
            StringBuilder output = new StringBuilder();
            string title = "اضغط للحصول على المشاركات الجديدة";
            if (count > 0)
                output.Append("<div class='mosharka' onclick='LoadNewTopics();'><a href='javascript:void(0);' title='" + title + "'>جديد</a><span>" + count + "</span></div>");

            return output.ToString();
        }
        public static void BuildRandomImagesAFT(FamoPassport pass, FamoBlock.enmUserType usrtype, int id)
        {
            //Build Random Images At First Time in Header of main page
            //this function called from user register page, shop register page and BuildUserImagesHeader function
            string key = usrtype == FamoBlock.enmUserType.User ? "usr" : "shp";
            key += id.ToString() + "_" + FamoBlock.Const_Option_Usr_Banar;
            FamOption op = new FamOption(pass);
            Random r = new Random();
            int rnd = r.Next(1, MaxRandomImages);
            for (int i = 1; i < 6; i++)
            {
                op.SetValue(key + "_" + i.ToString(), DefaultsFolder + "/" + rnd.ToString() + "/" + i.ToString() + ".png");
            }
        }
        public static string BuildUserImagesHeader(FamoPassport pass, int userid, bool Renew = false)
        {
            //بناء الصور العشوائيه في اعلى شاشة المستخدم الرئيسية
            //Renew => اعادة خلط الصور العشوائية من جديد 
            StringBuilder output = new StringBuilder();

            string dfltimg = DefaultPhoto1;// "images/popup_bg.png";
            string image = "";
            FamOption op = new FamOption(pass);
            //ex. key = 'usr20_BanarFile' or 'shp25_BanarFile'
            string key = "usr" + userid.ToString() + "_" + FamoBlock.Const_Option_Usr_Banar;
            DataTable dt = op.GetOptionLike(key);//
            if (dt.Rows.Count <= 0 || Renew)
            {
                //if there is no images at first time
                BuildRandomImagesAFT(pass, FamoBlock.enmUserType.User, userid);
                dt = op.GetOptionLike(key);
            }

            DataRow[] result;
            for (int i = 1; i < 6; i++)
            {
                result = dt.Select("keys like '%_" + i + "'");
                foreach (DataRow row in result)
                {
                    if (row["value"] != "")
                    {
                        image = row["value"].ToString().Replace("~/", "");
                    }
                    else
                        image = dfltimg;

                    output.Append("<div class='galleryPhoto galleryPhoto" + i + "'>" +
                        //"<a href='#'>" +
                                "<div class='Image iLoaded' style='background-image: url(/thumb.aspx?image=" + image + "&size=" + (i == 4 ? 800 : 400) + "); width: 100%;" +
                                "height: 100%;' >" + //src='" + image + "'
                                "</div>" +
                        //"</a>" +
                                "</div>");
                    break;
                }
            }

            return output.ToString();
        }
        private static string BuildUserInfoPanel(FamoPassport pass, int userid)
        {
            //بناء معلومات المستخدم الرئيسية اعلى الصفحه 
            StringBuilder output = new StringBuilder();
            /*
             <div class="status_info">
                        <ul class="status_ino_ul">
                            <li class="user_info_name">داليا محمد مصطفي</li>
                            <li class="user_info_country">المملكة العربية السعودية الرياض</li>
                            <li class="user_info_descriptin">نادي النصر السعودي تأسس عام 1985 ميلادي</li>
                        </ul>
                    </div>
                    <div class="status_profile_pic">
                        <img src="/newfiles/images/prof_pic.jpg" /></div>
             */
            FamUser u = new FamUser(pass, userid);
            List<FamContactProfile> cont = u.FillContractProfile();
            string filepath = getUserLogoPath(u);
            //check if file exist
            // window.event ? e.keyCode : e.which;
            string country = "";
            string city = "";
            if (cont.Count > 0)
            {
                country = cont[0].Country.Text;
                city = cont[0].City;
            }


            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();
            string firstname = "", lastname = "", countryy = "", cityy = "", gender = "", birthdate = "", image = "";
            try
            {
                dt = serv.GetCustomerData(serviceUser, servicePass, u.CustomerId, true).Tables[0];
                firstname = dt.Rows[0]["FirstName"].ToString();
                lastname = dt.Rows[0]["lastName"].ToString();
                countryy = dt.Rows[0]["Country"].ToString();
                cityy = dt.Rows[0]["City"].ToString();
                gender = dt.Rows[0]["Gender"].ToString();
                birthdate = dt.Rows[0]["BirthDate"].ToString();
                image = dt.Rows[0]["CustomerImage"].ToString();
            }
            catch (Exception ex) { }
            output.Append("<div class='status_info'>" +
                            "<ul class='status_ino_ul'>" +
                            "<li class='user_info_name'>" + firstname + " " + lastname + "</li>" +
                            "<li class='user_info_country'>" + countryy + " " + " - " + cityy + "</li>" +
                            "<li class='user_info_descriptin'>" + gender + " - " + birthdate + "</li>" +
                            "</ul>");

            if (pass.UserID != userid)//اظهار الازرار الخاصه بصفحة صديقي فقط
                output.Append(BuildFriendButtonsOptions(pass, userid));

            output.Append("</div>" + //status_info
                            "<div class='status_profile_pic'>" +
                            "<img src='" + image + "' />" +
                            "</div>");

            return output.ToString();
        }
        private static string BuildFriendButtonsOptions(FamoPassport pass, int friendid)
        {
            //بناء الأزار التي ستظهر في صفحة صديقي
            StringBuilder output = new StringBuilder();
            FamLink l = new FamLink(pass);
            FamoBlock.enmLinkeStatus nextaction = l.GetAction(pass.UserID, friendid);
            int lastlnkid;
            FamoBlock.enmLinkeStatus status = 0;
            try
            {
                DataTable dt = l.GetLastLinksWithMyFriends(pass.UserID, friendid);
                lastlnkid = Convert.ToInt32(dt.Rows[0]["link_id"]);
                status = (FamoBlock.enmLinkeStatus)Convert.ToInt32(dt.Rows[0]["status"]);
            }
            catch (Exception ex)
            {
                lastlnkid = 0;
            }


            string lnkTags = "";
            output.Append("<div class='status-options'>" +
                        "<ul>");
            //if (nextaction == FamoBlock.enmLinkeStatus.Unlink || nextaction == FamoBlock.enmLinkeStatus.Cancel_Request)
            //    lnkTags = getLinkLiFriendship(status, lastlnkid, nextaction, friendid);
            //else if (nextaction == FamoBlock.enmLinkeStatus.Request)
            //    lnkTags = getLinkLiFriendship(status, lastlnkid, nextaction, friendid);
            //else 
            lnkTags = getLinkLiFriendship(lastlnkid, nextaction, friendid);
            if (nextaction == FamoBlock.enmLinkeStatus.Allow)
                lnkTags += getLinkLiFriendship(lastlnkid, FamoLibrary.FamoBlock.enmLinkeStatus.Not_Allow, friendid);

            output.Append(lnkTags +
                        "<li><img src='/newfiles/images/friends-icons/chat_2.png' title='ارسال رسالة'></li>" +
                        "</ul>" +
                        "</div>");
            return output.ToString();
        }
        private static string getLinkLiFriendship(int lnkid, FamoBlock.enmLinkeStatus nextaction, int user)
        {
            string imgtag = "";
            string title = "";
            string img = "";
            if (nextaction == FamoBlock.enmLinkeStatus.Unlink || nextaction == FamoBlock.enmLinkeStatus.Cancel_Request)
            {
                img = "Delete-Request_1";
                title = "الغاء";
            }
            else if (nextaction == FamoBlock.enmLinkeStatus.Request)
            {
                img = "add_1";
                title = "طلب صداقة";
            }
            else if (nextaction == FamoBlock.enmLinkeStatus.Allow)
            {
                img = "Confirm_1";
                title = "قبول الصداقة";
            }
            else if (nextaction == FamoBlock.enmLinkeStatus.Not_Allow)
            {
                img = "Delete-Request_1";
                title = "رفض الصداقة";
            }

            imgtag = "<img src='/newfiles/images/friends-icons/" + img + ".png' title='" + title + "'>";
            return "<li class='newfrndbuttons' onclick=\"FriendshipAction(document.getElementsByClassName('newfrndbuttons'),'" + lnkid + "','" + (int)nextaction + "'," + user + ")\">" +
                    imgtag + "</li>";
        }

        public static string getUserLogoPath(FamoPassport pass, int user_id)
        {
            FamUser u = new FamUser(pass, user_id);
            return getUserLogoPath(u);
        }
        public static string getUserLogoPath(FamUser user)
        {
            string filepath = user.File.Path == null ? "" : user.File.Path.Replace("~", "");
            if (filepath != "")
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath("~") + filepath)) filepath = getDefaultPhoto(user);
            }
            else
                filepath = getDefaultPhoto(user);
            return "/thumb.aspx?image=" + filepath.Replace("/files_upload", "files_upload") + "&size=140";
        }
        public static string getDefaultPhoto(FamUser user)
        {
            return user.Gender == FamoBlock.enmGender.Male ? DefaultMalePhoto : DefaultFemalePhoto;
        }
        public static string CheckIfImageExist(string file, string defaultPhoto)
        {
            string filepath = defaultPhoto;
            string x = HttpContext.Current.Server.MapPath(".") + file;
            if (!File.Exists(HttpContext.Current.Server.MapPath(".") + file)) return filepath;
            else return file;
        }
        public static string CheckIfImageExist(string file, FamoBlock.enmGender gender)
        {
            string filepath = gender == FamoBlock.enmGender.Male ? DefaultMalePhoto : DefaultFemalePhoto;
            if (!File.Exists(HttpContext.Current.Server.MapPath("") + file)) return filepath;
            else return file;
        }

        #endregion //end region user

        #region Friends Page
        public enum FriendArea
        {
            MyFriends = 1,        //عرض الاصدقاء
            OtherRequests = 2,    //عرض طلبات الصداقة
            MayKnow = 3,          //اشخاص قد اعرفهم
            MyRequests = 4,       //طلبات الصداقة الخاصه بي
            DontKnow = 5,         //أشخاص لا أعرفهم

        }
        public static string BuildFriendArea(FamoPassport pass, FamUser user, FriendArea frndArea)
        {
            //build hole friendship page with friend bar and friend blocks
            StringBuilder output = new StringBuilder();

            //friends_bar
            output.Append(BuildFriendBar(pass, user, frndArea));
            //friends_request_blocks
            output.Append(BuildFriendsBlocks(pass, user, frndArea));
            return output.ToString();
            /*
             <!-- BuildFriendBar -->
             <div id="friends_request_blocks">
                <ul>
            //loop
                    <!-- BuildFriendBox -->
                </ul>
                
            </div>
             */
        }

        public static string BuildFriendBar(FamoPassport pass, FamUser user, FriendArea area)
        {
            //الاحصائيات
            StringBuilder output = new StringBuilder();
            FamLink lnk = new FamLink(pass);
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='friends_bar' class='content-insider'>");
            output.Append("<div class='inside_friends_bar'>");
            output.Append("<ul>" +
                          "<li class='friends_title'>الاصدقاء</li>");








            string clsactv = "red-active";
            //عرض اصدقائي فقط
            output.Append(BuildFriendBarItem(user, FriendArea.MyFriends, lnk.GetCountOfFriends(user.ID, FamoBlock.enmLinkeStatus.Allow), "اصدقائي", (area == FriendArea.MyFriends ? clsactv : "")));
            output.Append(BuildFriendBarItem(user, FriendArea.OtherRequests, lnk.GetCountOfRequest(user.ID, FamoBlock.enmLinkeStatus.Request), "طلبات الصداقة", (area == FriendArea.OtherRequests ? clsactv : "")));
            //الطلبات المرسله
            output.Append(BuildFriendBarItem(user, FriendArea.MyRequests, lnk.GetCountOfMyRequest(user.ID, FamoBlock.enmLinkeStatus.Request), "الطلبات المرسله", (area == FriendArea.MyRequests ? clsactv : "")));
            //عرض جميع الاشخاص في الموقع ماعدا اصدقائي
            output.Append(BuildFriendBarItem(user, FriendArea.DontKnow, new FamUser(pass).GetUsersNotFriendsCount(pass.UserID), "جميع الأعضاء", (area == FriendArea.DontKnow ? clsactv : "")));

            output.Append("<div class='search_friends' >");//onclick='Acomplete();'
            output.Append("<input type='text' name='friends_search' id='fsearch' class='fsearchcss' value=''>");
            output.Append("<img src='/newfiles/images/friends_search_icon.png'></div> ");
            output.Append(BuildFriendBarLoader());//loader

            output.Append("</ul>");
            /*output.Append("<div class='search_friends'>" +
                          "<input type='text' value='ابحث بالاصدقاء' name='friends_search'>" +
                          "<img src='/newfiles/images/friends_search_icon.png'>" +
                          "</div>"); //search_friends
            */
            output.Append("</div>");//inside_friends_bar
            output.Append("</div>");//friends_bar
            return output.ToString();



            /*
                <div id="friends_bar">
                    <div class="inside_friends_bar">
                        <ul>
                            <li class="friends_title">الاصدقاء</li>
                            <li>
                                <div class="friends_bar_counter">
                                    1420<div class="arrow_box">
                                    </div>
                                </div>
                                <div class="friends_bar_link">
                                    <a href="#">كل الاصدقاء</a></div>
                            </li>
                            <li>
                                <div class="friends_bar_counter">
                                    32<div class="arrow_box">
                                    </div>
                                </div>
                                <div class="friends_bar_link">
                                    <a href="#">اقتراحات الصداقة</a></div>
                            </li>
                            <li>
                                <div class="friends_bar_counter red-active">
                                    لايوجد<div class="arrow_box_active">
                                    </div>
                                </div>
                                <div class="friends_bar_link">
                                    <a href="#">طلبات الصداقة</a></div>
                            </li>
                            <li>
                                <div class="friends_bar_counter">
                                    70<div class="arrow_box">
                                    </div>
                                </div>
                                <div class="friends_bar_link">
                                    <a href="#">اشخاص قد تعرفهم</a></div>
                            </li>
                        </ul>
                        <!-- Search Friends Start -->
                        <div class="search_friends">
                            <input type="text" value="ابحث بالاصدقاء" name="friends_search">
                            <img src="/newfiles/images/friends_search_icon.png"><!-- Search Friends End --></div>
                    </div>
                </div>         
                 */
        }
        public static string BuildFriendsBlocks(FamoPassport pass, FamUser user, FriendArea frndArea)
        {
            StringBuilder output = new StringBuilder();
            List<FamUser> Friends = new List<FamUser>();
            if (frndArea == FriendArea.DontKnow)
                Friends = user.FillNotFriendslist_Top();//persons who I don't know them
            else if (frndArea == FriendArea.MyFriends)
                Friends = user.FillMyFriendslist();//my friends
            else if (frndArea == FriendArea.OtherRequests)
                Friends = user.RequestsRecived(pass.UserID);//people who sent request to me
            else if (frndArea == FriendArea.MyRequests)
                Friends = user.RequestsSent(pass.UserID);//people who I sent request to them

            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='friends_request_blocks' class='content-insider'>" +
                          "<ul>");

            //<!-- friend-li-block-hover -->
            foreach (FamUser frnd in Friends)
                output.Append(BuildFriendBox(pass, frnd));
            if (Friends.Count > 0)
                output.Append("<div id='" + (int)frndArea + "_FriendsSeeMore' class='FreiendsLinkStatus friend-li-block-hover' onclick='seemorePepole()'><li class='friend-li-block-hover'><span class='span-seemore-friends'> See More </span></li></div>");
            output.Append("</ul>");
            //hisham code Add See more link for Dont know Friends

            ////////

            //output.Append(BuildSeeMoreShops(pass, "", "", "see more"));
            output.Append("</div>");//friends_request_blocks

            return output.ToString();
        }

        public static string BuildFriendsBlocks_ForSearchByUserName(FamoPassport pass, FriendArea frndArea, string UserName, int lastid)
        {
            FamUser user = new FamUser(pass);
            StringBuilder output = new StringBuilder();
            List<FamUser> Friends = new List<FamUser>();
            Friends = user.FillNotFriendslist_ForFriendsSearch((int)frndArea, UserName, lastid);
            //if (frndArea == FriendArea.DontKnow)
            //    Friends = user.FillNotFriendslist_Top();//persons who I don't know them
            //else if (frndArea == FriendArea.MyFriends)
            //    Friends = user.FillMyFriendslist();//my friends
            //else if (frndArea == FriendArea.OtherRequests)
            //    Friends = user.RequestsRecived(pass.UserID);//people who sent request to me
            //else if (frndArea == FriendArea.MyRequests)
            //    Friends = user.RequestsSent(pass.UserID);//people who I sent request to them

            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            if (lastid == 0)
                output.Append("<div id='friends_request_blocks' class='content-insider'>" +
                              "<ul>");

            //<!-- friend-li-block-hover -->
            foreach (FamUser frnd in Friends)
                output.Append(BuildFriendBox(pass, frnd));
            if (Friends.Count > 0)
                output.Append("<div id='" + (int)frndArea + "_SearchFriendsSeeMore' class='SearchFreiendsLinkStatus friend-li-block-hover' onclick='SearchseemorePepole()'><li class='friend-li-block-hover'><span class='span-seemore-friends'> See More </span></li></div>");
            if (lastid == 0)
            {
                output.Append("</ul>");
                //hisham code Add See more link for Dont know Friends

                ////////

                //output.Append(BuildSeeMoreShops(pass, "", "", "see more"));
                output.Append("</div>");//friends_request_blocks
            }

            return output.ToString();
        }
        public static string BuildFriendsBlocksSeemore(FamoPassport pass, FamUser user, FriendArea frndArea, int lastid)
        {
            StringBuilder output = new StringBuilder();
            List<FamUser> Friends = new List<FamUser>();
            if (frndArea == FriendArea.DontKnow)
                Friends = user.FillNotFriendslist_Next(lastid);//persons who I don't know them
            else if (frndArea == FriendArea.MyFriends)
                Friends = user.FillMyFriendslistSeeMore(lastid);//my friends
            else if (frndArea == FriendArea.OtherRequests)
                Friends = user.RequestsRecivedSeeMore(pass.UserID, lastid);//people who sent request to me
            else if (frndArea == FriendArea.MyRequests)
                Friends = user.RequestsSentSeeMore(pass.UserID, lastid);//people who I sent request to them

            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            //output.Append("<div id='friends_request_blocks' class='content-insider'>" +
            //              "<ul>");

            //<!-- friend-li-block-hover -->
            foreach (FamUser frnd in Friends)
                output.Append(BuildFriendBox(pass, frnd));
            //output.Append("</ul>");
            //hisham code Add See more link for Dont know Friends
            if (Friends.Count > 0)
                //output.Append("<div id='" + (int)frndArea + "_FriendsSeeMore' class='FreiendsLinkStatus friend-li-block-hover' onclick='seemorePepole()'> See More .... </div>");
                output.Append("<div id='" + (int)frndArea + "_FriendsSeeMore' class='FreiendsLinkStatus friend-li-block-hover' onclick='seemorePepole()'><li class='friend-li-block-hover'><span class='span-seemore-friends'> See More </span></li></div>");
            ////////

            //output.Append(BuildSeeMoreShops(pass, "", "", "see more"));
            output.Append("</div>");//friends_request_blocks

            return output.ToString();
        }

        private static string BuildFriendBarItem(FamUser user, FriendArea friendArea, int link, string text, string activeclass = "")
        {
            StringBuilder output = new StringBuilder();
            Random r = new Random();
            string id = Guid.NewGuid().ToString();//r.Next(10000000, 99999999).ToString();  DateTime.Now.Ticks.ToString()
            output.Append("<li class='friendbar-item' >" +//are='" +(int) friendArea + "'
                          "<div id='" + id + "' class='friends_bar_counter " + activeclass + "' area='" + (int)friendArea + "'> " + link +
                          "<div class='arrow_box'></div>" +
                          "</div>" + //friends_bar_counter
                          "<div class='friends_bar_link'>" +
                          "<a href='javascript:void(0);' onclick=\"LoadFriends(" + user.ID + "," + (int)friendArea + ",document.getElementById('" + id + "'));\">" + text + "</a></div>" +
                          "</li>");
            return output.ToString();
        }

        private static string BuildFriendBarLoader()
        {
            StringBuilder output = new StringBuilder();

            output.Append("<li class='friendbar-item'>" +
                          "<div id='loader-mnubar' class='friends_bar_link'></div>" +
                          "</li>");
            return output.ToString();
        }
        private static string BuildFriendBox(FamoPassport pass, FamUser user)
        {
            StringBuilder output = new StringBuilder();
            string divid = "frndbox_" + user.ID;
            output.Append("<div id='" + divid + "' class='friend-li-block-hover'>" +
                          "<li>");
            //friends_blocks_photo_pic
            output.Append(BuildFriendData(pass, user));
            output.Append("<ul id='frndboxopt_" + user.ID + "'>");
            //<a>
            output.Append(BuildFriendshipLinks(pass, user, divid));
            //drpdwn_toolbar listdowntoolbar
            output.Append(BuildFriendMenu(pass, user));
            output.Append("</ul>");
            output.Append("</li>");
            output.Append("</div>");//friend-li-block-hover
            return output.ToString();
            /*
                <div class="friend-li-block-hover">
                    <li>
                        <!-- BuildFriendData -->
                        <ul>
                            
                            <!-- BuildFriendshipLinks -->
             
                            <!-- BuildFriendMenu -->
                        </ul>
                    </li>
                </div>             
             */
        }
        private static string BuildFriendData(FamoPassport pass, FamUser user)
        {
            StringBuilder output = new StringBuilder();
            List<FamContactProfile> c = user.ContractProfile(user.ID);
            string country = c.Count > 0 ? c[0].Country.Text : "";
            string onclick = "onclick='LoadPage(" + user.ID + ",1,1);'";
            string Linkpage = "javascript:void(0);";//= "/usrmain.aspx?pg=" + (int)PageName.Arena + "&uid=" + user.ID;
            //Linkpage = /*WebHosting +*/ "/person/" + user.ID + "/Arena";
            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();
            string photo = getUserLogoPath(pass, user.ID), username = user.FullName;
            if (user.CustomerId > 0)
            {
                try
                {
                    dt = serv.GetCustomerData(serviceUser, servicePass, user.CustomerId, true).Tables[0];

                }
                catch (Exception ex) { }
                try
                {
                    photo = dt.Rows[0]["CustomerImage"].ToString();
                    username = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();
                    country = dt.Rows[0]["country"].ToString() + " " + dt.Rows[0]["city"].ToString();
                }
                catch (Exception ex) { }

            }
            output.Append("<div class='friends_blocks_photo_pic'>" +
                          "<a href='" + Linkpage + "' " + onclick + ">" +
                          "<img src='" + photo + "'/>" + "</a></div>" +
                          "<div class='friends_block_info'>" +
                          "<div class='friend_request_name'>" + username + "</div>" +
                          "<div class='friend_request_country'>" +
                          country + "</div>" +
                          "<div class='friend_request_num_counter'> " + "</div>" + //صديق مشترك
                          "</div>");//friends_block_info

            return output.ToString();
            /*
                    <div class="friends_blocks_photo_pic">
                            <img src="/newfiles/images/test.jpg" />
                        </div>
                        <div class="friends_block_info">
                            <div class="friend_request_name">
                                محمد مصطفى صلاح</div>
                            <div class="friend_request_country">
                                </div>المملكة العربية السعودية
                            <div class="friend_request_num_counter">
                                53 صديق مشترك</div>
                        </div>
             */
        }
        public static string BuildFriendshipLinks(FamoPassport pass, FamUser user, string divid)
        {
            //relationship between me (pass.UserId) and other user (FamUser)
            StringBuilder output = new StringBuilder();
            FamLink lnk = new FamLink(pass);
            DataTable dt = lnk.GetLastLinksWithMyFriends(pass.UserID, user.ID);

            int linkid = 0;
            bool ChatButton = true;//show chat button
            if (dt.Rows.Count < 1)
            {
                //new request
                output.Append(BuildFriendshipButtons(user, divid, user.ID, "sep-icon-1", "", imgadd, "طلب صداقة", FamoBlock.enmLinkeStatus.Request, false));//add request button
            }
            else
            {
                linkid = Convert.ToInt32(dt.Rows[0]["link_id"]);
                int userid = Convert.ToInt32(dt.Rows[0]["user_id"]);
                FamoBlock.enmLinkeStatus status = (FamoBlock.enmLinkeStatus)Convert.ToInt32(dt.Rows[0]["status"]);
                if (userid == pass.UserID)
                {
                    //the source request friendship
                    if (status == FamoBlock.enmLinkeStatus.Request)
                        output.Append(BuildFriendshipButtons(user, divid, linkid, "sep-icon-1", "", imgcancel, "الغاء طلب الصداقة", FamoBlock.enmLinkeStatus.Cancel_Request, false));//cancel request button
                    else if (status == FamoBlock.enmLinkeStatus.Cancel_Request || status == FamoBlock.enmLinkeStatus.Not_Allow || status == FamoBlock.enmLinkeStatus.Unlink || status == FamoBlock.enmLinkeStatus.Unbolck)
                        //يظهر زر اضافة صديق في احد الحالات التالية : الغاء الطلب ، عدم قبول الصداقة ، الغاء الصداقة
                        output.Append(BuildFriendshipButtons(user, divid, linkid, "sep-icon-1", "", imgadd, "طلب صداقة", FamoBlock.enmLinkeStatus.Request, false));//add request button
                    else if (status == FamoBlock.enmLinkeStatus.Allow)
                        //في حالة انه صديق
                        output.Append(BuildFriendshipButtons(user, divid, linkid, "sep-icon-1", "", imgunfr, "الغاء الصداقة", FamoBlock.enmLinkeStatus.Cancel_Request, false));//unfriend button
                }
                else
                {
                    //the target request friendship
                    if (status == FamoBlock.enmLinkeStatus.Request)
                    {
                        output.Append(BuildFriendshipButtons(user, divid, linkid, "sep-icon-1", "", imgallow, "قبول الصداقة", FamoBlock.enmLinkeStatus.Allow, false));//allow button
                        output.Append(BuildFriendshipButtons(user, divid, linkid, "sep-icon-2ico", "sep-icon-2", imgcancel, "عدم قبول الطلب", FamoBlock.enmLinkeStatus.Not_Allow, false));//not allow button
                        ChatButton = false;
                    }
                    else if (status == FamoBlock.enmLinkeStatus.Cancel_Request || status == FamoBlock.enmLinkeStatus.Not_Allow || status == FamoBlock.enmLinkeStatus.Unlink || status == FamoBlock.enmLinkeStatus.Unbolck)
                        //يظهر زر اضافة صديق في احد الحالات التالية : الغاء الطلب ، عدم قبول الصداقة ، الغاء الصداقة
                        output.Append(BuildFriendshipButtons(user, divid, linkid, "sep-icon-1", "", imgadd, "طلب صداقة", FamoBlock.enmLinkeStatus.Request, false));//add request button
                    else if (status == FamoBlock.enmLinkeStatus.Allow)
                        //في حالة انه صديق
                        output.Append(BuildFriendshipButtons(user, divid, linkid, "sep-icon-1", "", imgunfr, "الغاء الصداقة", FamoBlock.enmLinkeStatus.Unlink, false));//unfriend button
                }
            }
            string fullname = "'" + user.FullName + "'";
            if (ChatButton) output.Append(BuildFriendshipButtons(user, "", user.ID, "sep-icon-2ico", "sep-icon-2", imgchat, "دردشة", 0, true, "MessagePopup(" + pass.UserID + "," + user.ID + ",\"" + user.FullName + "\");"));//," + user.FullName+ "

            return output.ToString();
            /*
                         <a href="#">
                            <li class="lioptions sep-icon-1" imgorg="/newfiles/images/friends-icons/add_2.png"
                                imgover="/newfiles/images/friends-icons/add_1.png"></li>
                        </a>
                        <a href="#">
                            <li class="lioptions sep-icon-2" imgorg="/newfiles/images/friends-icons/chat_1.png"
                                imgover="/newfiles/images/friends-icons/chat_2.png"></li>
                        </a>
                         */
        }
        private static string BuildFriendshipButtons(FamUser user, string divid, int linkid, string className, string liclass, string img, string title, FamoBlock.enmLinkeStatus nextaction, bool chatbutton, string hrefOnclick = "")
        {
            StringBuilder output = new StringBuilder();
            //string onclick = "onclick='FriendshipAction(document.getElementById(\"frlnk_" + linkid + "\")," + linkid + "," + (int)nextaction + ")'";
            string onclick = "";
            if (!chatbutton)
                onclick = "onclick=\"FriendshipAction(document.getElementById('" + divid + "'),'" + linkid + "','" + (int)nextaction + "'," + user.ID + ")\"";
            else
                onclick = "MessagePopup()";
            output.Append("<li id='frlnk_" + linkid + "' class='lioptions " + liclass + "' " + (linkid > 0 ? onclick : "") + " title='" + title + "' " + (hrefOnclick != "" ? "liname='" + user.FullName + "'" : "") + ">" +
                "<a class='" + className + "' href='javascript:void(0);' style='background-image:url(" + img + ");' " + (hrefOnclick != "" ? "onclick='" + hrefOnclick + "'" : "") + ">" + "  </a></li>");

            return output.ToString();
            ////"+GetPassport().UserID+","+user.ID+",'"+user.FullName+"'
        }

        private static string BuildFriendMenu(FamoPassport pass, FamUser user)
        {
            //خيارات الصداقة
            StringBuilder output = new StringBuilder();
            string divid = "lst_" + user.ID;
            output.Append("<div id='" + divid + "' class='drpdwn_toolbar listdowntoolbar'>" +
                          "<ul>" +
                //  "<li><a href='javascript:void(0);'>حظر الشخص</a></li>" +
                //  "<li><a href='javascript:void(0);'>الاعدادات</a></li>" +
                //"<li><a href='#'>الاصدقاء المقربون</a></li>" +
                //"<li><a href='#'>المعارف</a></li>" +
                //"<li><a href='#'>اقتراح الاصدقاء</a></li>" +
                //"<li><a href='#'>الغاء الاصدقاء</a></li>" +
                         "</ul>" +
                         "<div class='arrow_toolbar'>" +
                         "</div>" +
                         "</div>" +
                         "<li class='lioptions'>" +
                         "<a href='javascript:void(0);' class='tool-option sep-icon-3' evnt='" + divid + "'></a>" +
                         "</li>");
            return output.ToString();
            /*
             <div id="lst_7" class="drpdwn_toolbar listdowntoolbar">
                <ul>
                    <li><a href="#">احصل على اشعارات</a></li>
                    <li><a href="#">الاعدادات</a></li>
                    <li><a href="#">الاصدقاء المقربون</a></li>
                    <li><a href="#">المعارف</a></li>
                    <li><a href="#">اقتراح الاصدقاء</a></li>
                    <li><a href="#">الغاء الاصدقاء</a></li>
                </ul>
                <div class="arrow_toolbar">
                </div>
            </div>
            <!-- -->
            <a href="#" class="tool-option" evnt="lst_7">
                <li class="lioptions sep-icon-3" imgorg="/newfiles/images/friends-icons/Tools_1.png"
                    imgover="/newfiles/images/friends-icons/Tools_2.png"></li>
            </a>
             */
        }


        /*
         
         */
        #endregion //end region friends

        #region Shops Page
        public static string BuildShopPage(FamoPassport pass, FamUser user, FamActivity actv)
        {
            //صفحة المتاجر والمنتجات
            StringBuilder output = new StringBuilder();
            output.Append(BuildShopsBar(pass, user));//shop bar
            //output.Append(BuildActiviesItems(pass));//the icons of activities
            output.Append(BuildShopContentPage(pass, user, actv));//show other bolcks
            return output.ToString();
        }

        public static string BuildShopsBar(FamoPassport pass, FamUser user)
        {
            //احصائيات المتاجر والمنتجات
            StringBuilder output = new StringBuilder();
            FamLink lnk = new FamLink(pass);
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='friends_bar' class='content-insider'>");
            output.Append("<div class='inside_friends_bar'>");
            output.Append("<ul>" +
                          "<li class='friends_title'>المتاجر</li>");
            int vendors = 0, products = 0, brands = 0, follows = 0;
            try
            {
                NopService.NopService client = new NopService.NopService();
                DataTable dt = client.GetStatisticsVendor(ClassMain.serviceUser, ClassMain.servicePass).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    vendors = Convert.ToInt32(dt.Rows[0]["VendorsCount"]);
                    products = Convert.ToInt32(dt.Rows[0]["ProductsCount"]);
                    brands = Convert.ToInt32(dt.Rows[0]["ManufacturersCount"]);
                }
                follows = client.GetVendorsFollowing(ClassMain.serviceUser, ClassMain.servicePass, pass.Customer, true).Tables[0].Rows.Count;
            }
            catch (Exception ex) { }

            output.Append(BuildShopBarItem(user, vendors, "الكل", "/shop/vendor/all"));
            output.Append(BuildShopBarItem(user, follows, "مُتابعه"));
            output.Append(BuildShopBarItem(user, products, "المنتجات"));
            output.Append(BuildShopBarItem(user, brands, "الماركات"));
            output.Append("</ul>");
            /*output.Append("<div class='search_friends'>" +
                          "<input type='text' value='ابحث عن المتاجر والمنتجات' name='friends_search'>" +
                          "<img src='/newfiles/images/friends_search_icon.png'>" +
                          "</div>"); //search_friends
            */
            output.Append("</div>");//inside_friends_bar
            output.Append("</div>");//friends_bar
            return output.ToString();



            /*
                <div id="friends_bar">
                    <div class="inside_friends_bar">
                        <ul>
                            <li class="friends_title">الاصدقاء</li>
                            <li>
                                <div class="friends_bar_counter">
                                    1420<div class="arrow_box">
                                    </div>
                                </div>
                                <div class="friends_bar_link">
                                    <a href="#">كل الاصدقاء</a></div>
                            </li>
                            <li>
                                <div class="friends_bar_counter">
                                    32<div class="arrow_box">
                                    </div>
                                </div>
                                <div class="friends_bar_link">
                                    <a href="#">اقتراحات الصداقة</a></div>
                            </li>
                            <li>
                                <div class="friends_bar_counter red-active">
                                    لايوجد<div class="arrow_box_active">
                                    </div>
                                </div>
                                <div class="friends_bar_link">
                                    <a href="#">طلبات الصداقة</a></div>
                            </li>
                            <li>
                                <div class="friends_bar_counter">
                                    70<div class="arrow_box">
                                    </div>
                                </div>
                                <div class="friends_bar_link">
                                    <a href="#">اشخاص قد تعرفهم</a></div>
                            </li>
                        </ul>
                        <!-- Search Friends Start -->
                        <div class="search_friends">
                            <input type="text" value="ابحث بالاصدقاء" name="friends_search">
                            <img src="/newfiles/images/friends_search_icon.png"><!-- Search Friends End --></div>
                    </div>
                </div>         
                 */
        }
        private static string BuildShopBarItem(FamUser user, int link, string text, string href = "javascript:void(0);")
        {
            StringBuilder output = new StringBuilder();
            string onclick = "onclick=''";
            output.Append("<li>" +
                          "<div class='friends_bar_counter'> " + link +
                          "<div class='arrow_box'></div>" +
                          "</div>" + //friends_bar_counter
                          "<div class='friends_bar_link'>" +
                          "<a href='" + href + "' >" + text + "</a></div>" +
                          "</li>");
            return output.ToString();
        }

        private static string BuildActiviesItems(FamoPassport pass)
        {
            //عرض جميع النشاطات
            StringBuilder output = new StringBuilder();
            FamActivity comm = new FamActivity(pass);
            List<FamActivity> fa = comm.GetActivitiesByLanguageList(0, pass.Language_ID, "name");
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='shop_blocks' class='content-insider'>" +
                          "<div class='shop_block_cat_title'> جميع الفئات</div>" +
                          "<ul>" +
                          "<li class='all_cat_shop'><a href='javascript:void(0);' onclick='LoadContentActv(" + pass.UserID + ",0);'>الجميع</a></li>");

            foreach (FamActivity fc in fa)
            {
                output.Append(BuildActivity(pass, fc));
            }

            output.Append("</ul>" +
                          " </div>");
            return output.ToString();
            /*
             
        <div id="shop_blocks">
            <div class="shop_block_cat_title">
                جميع الفئات</div>
            <ul>
                <li class="all_cat_shop"><a href="javascript:void(0);" onclick='LoadContentActv(userid,actvid);'>الجميع</a></li>
              
                <!-- loop to call BuildActivity -->
              
            </ul>
            
        </div>     
              
             */
        }

        private static string BuildActivity(FamoPassport pass, FamActivity actv)
        {
            //عرض نشاط واحد فقط
            StringBuilder output = new StringBuilder();
            output.Append("<li>" +
                          "<div class='cat-photo'>" +
                          "<img src='" + SetDefaultActvItem(actv.File.Path) + "'/></div>" +
                          "<a href='javascript:void(0);' onclick='LoadContentActv(" + pass.UserID + "," + actv.ID + ");'>" +
                          "<div class='sub-cat-title'>" + (actv.Name == null ? "" : actv.Name) + "</div>" +
                          "</a>" +
                          "</li>");
            return output.ToString();

            /*
                <li>
                    <div class="cat-photo">
                        <img src="/newfiles/images/shop-icons/clothes.png" /></div>
                    <a href="javascript:void(0);" onclick='LoadContentActv(userid,actvid);'>
                        <div class="sub-cat-title">
                            ملابس</div>
                    </a>
                </li>             
             */
        }
        public static string SetDefaultActvItem(string ImagePath)
        {
            //اعادة الصورة الافتراضية في حالة عدم وجودها
            string filepath = ImagePath == null ? "" : ImagePath.Replace("~", "");
            if (filepath != "")
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath("") + filepath)) filepath = DefaultPhoto2;
            }
            else
                filepath = DefaultPhoto2;
            return filepath;
        }
        public static string BuildShopContentPage(FamoPassport pass, FamUser user, FamActivity actv)
        {
            //صفحة المتاجر والمنتجات
            StringBuilder output = new StringBuilder();


            output.Append(BuildShops(pass, actv, true));//عرض جميع الشركات التي يتابعها المستخدم
            //output.Append(BuildShops(pass, actv));//عرض جميع الشركات التي لا يتابعها المستخدم

            //output.Append(BuildProducts(pass, actv));//products

            return output.ToString();
        }
        private static string BuildShops(FamoPassport pass, FamActivity actv, bool isFollowed = false)
        {
            //عرض المتاجر التي يتابعها المستخدم بحسب نشاط معين 
            //اذا كان النشاط ليس فيه قيمة فهذا يعني عرض جميع الشركات المتابعه لجميع النشاطات

            //isFollowed = اذا كانت 'نعم' فإنه سيتم عرض المتاجر التي يتابعها المستخدم 
            //              واذا كانت القيمة 'لا' فإنه سيتم عرض الشركات التي لا يتابعها المستخدم




            StringBuilder output = new StringBuilder();
            FamShop fs = new FamShop(pass);
            List<FamShop> fa = new List<FamShop>();
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='m3ared_blocks' class='content-shop content-insider'>" +
                         "<div class='shop_block_cat_title'> المعارض المفضلة </div>");

            /* if (actv.ID > 0 && isFollowed)
                 fa = fs.getShopsFollowingByActivitylist(pass.UserID, actv.ID);
             else if (actv.ID > 0 && !isFollowed)
                 fa = fs.getShopsNotFollowingByActivitylist(pass.UserID, actv.ID);
             else if (actv.ID == 0 && !isFollowed)
                 fa = fs.getShopsNotFollowinglist(pass.UserID);
             else if (actv.ID == 0 && isFollowed)
                 fa = fs.getShopsFollowinglist(pass.UserID);*/

            string UlId = "ShopsGroup_" + isFollowed.ToString();

            NopService.NopService client = new NopService.NopService();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (isFollowed)
            {
                ds = client.GetVendorsFollowing(ClassMain.serviceUser, ClassMain.servicePass, pass.Customer, true);

                dt = ds.Tables[0];
            }
            else
            {

            }
            //BuildVendorBox

            output.Append("<ul id='" + UlId + "'>");
            //foreach (FamShop fshp in fa)
            //    output.Append(BuildShopBox(pass, fshp, actv.ID));//<li>
            //output.Append("</ul>");


            int countseemor = 0;
            foreach (DataRow fshp in dt.Rows)
            {

                output.Append(BuildShopBox(pass, fshp, actv.ID));//<li>
                countseemor++;
                if (countseemor == 5) break;

            }
            output.Append("</ul>");



            if (countseemor == 5)
                output.Append(BuildSeeMoreShops(pass, "SeeMoreShops(\"" + UlId + "\");", UlId, "عرض المزيد"));

            output.Append("</div>");//m3ared_blocks

            return output.ToString();
            /*
            <div id="m3ared_blocks">
                <div class="shop_block_cat_title">
                    المعارض المفضلة</div>
                <ul>
              
                    <!-- loop to call BuildShopBox -->
             
                </ul>
            </div>
             */
        }

        public static string BuildShopBox(FamoPassport pass, DataRow vendor, int actvid)
        {
            int vendorid = 0;
            string vendorname = "", url = "", country = "", city = "", filepath = "";
            if (vendor == null)
                return "";
            else
            {
                vendorid = Convert.ToInt32(vendor["VendorFollowerId"]);
                vendorname = vendor["vendorName"].ToString();
                url = vendor["url"].ToString();
                country = vendor["country"].ToString();
                city = vendor["city"].ToString();
                filepath = vendor["image"].ToString();
            }
            if (vendorid == 0)
                return "";

            //عرض متجر واحد فقط مُتابْع
            StringBuilder output = new StringBuilder();
            FamProduct fp = new FamProduct(pass);
            FamFollow ffollow = new FamFollow(pass);
            FamContactProfile c = new FamContactProfile(pass);
            //string country;
            //try { country = c.GetContactsList((int)FamoBlock.enmObjectCode.Shops, shop.ID)[0].Country.Text; }
            //catch (Exception ex) { country = ""; }

            //int countproduct = dt.Rows[0]["prod_id"];
            //string filepath =
            // window.open ('YourNewPage.htm','_self',false)
            string actionurl = "window.open('" + url + "' ,'_self',false)";
            output.Append("<li id='shopbox_" + vendorid + "' shpid='" + vendorid + "' actvid='" + actvid + "'>" +
              "<div class='m3rad_pic'>" +
                //"<a href='" + actionurl + "'  ><img src='" + filepath + "' /></a></div>" +
               "<a href='" + url.Replace("http://", "").Replace("www.famocity.com", "") + "'  ><img src='" + filepath + "' /></a></div>" +
              "<div class='m3rad_info'>" +
              "<div class='ma3rad_data_info'>" +
              "<div class='ma3rad-title'>" + vendorname + "</div>" +
              "<div class='ma3rad-country'>" + country + " - " + city + "</div>" +
              "<ul>");


            //عدد المتابعين
            //int numCount = "<%#Eval("+ countproduct() +"%>";
            output.Append(BuildShopBoxDetailCounts("المتابعين", Convert.ToInt32(vendor["FollowersCount"])));

            //عدد المنتجات
            output.Append(BuildShopBoxDetailCounts("المنتجات", Convert.ToInt32(vendor["ProductsCount"])));

            output.Append("</ul>" +
                          "</div>");//ma3rad_data_info
            //output.Append(BuildShopBoxOptions(pass));

            output.Append("</div>" + //m3rad_info
                          "</li>");


            return output.ToString();

            /*
                <li id='shopbox_20' shpid='20'>
                    <div class="m3rad_pic">
                        <img src="/newfiles/images/store-logos/victoria.jpg" /></div>
                    <div class="m3rad_info">
                        <div class="ma3rad_data_info">
                            <div class="ma3rad-title">
                                متجر سنتر بوينت</div>
                            <div class="ma3rad-country">
                                المملكة العربية السعودية</div>
                            <ul>
                                
                                <!-- BuildShopBoxDetailCounts -->
             
                            </ul>
                        </div>
                        
                        <!-- shop options -->
              
                    </div>
                </li>
             */
        }



        //public static string BuildShopBox(FamoPassport pass, FamShop shop, int actvid)
        //{
        //    if (shop == null)
        //        return "";
        //    else if (shop.ID == 0)
        //        return "";

        //    //عرض متجر واحد فقط مُتابْع
        //    StringBuilder output = new StringBuilder();
        //    FamProduct fp = new FamProduct(pass);
        //    FamFollow ffollow = new FamFollow(pass);
        //    FamContactProfile c = new FamContactProfile(pass);
        //    string country;
        //    try { country = c.GetContactsList((int)FamoBlock.enmObjectCode.Shops, shop.ID)[0].Country.Text; }
        //    catch (Exception ex) { country = ""; }

        //    //int countproduct = dt.Rows[0]["prod_id"];
        //    string filepath = getShopLogoPath(shop);

        //    output.Append("<li id='shopbox_" + shop.ID + "' shpid='" + shop.ID + "' actvid='" + actvid + "'>" +
        //      "<div class='m3rad_pic'>" +
        //      "<a href='/shop/" + (shop.WebName != "" ? shop.WebName : shop.ID.ToString()) + "' ><img src='" + filepath + "' /></a></div>" +
        //      "<div class='m3rad_info'>" +
        //      "<div class='ma3rad_data_info'>" +
        //      "<div class='ma3rad-title'>" + shop.Name + "</div>" +
        //      "<div class='ma3rad-country'>" + country + "</div>" +
        //      "<ul>");


        //    //عدد المتابعين
        //    //int numCount = "<%#Eval("+ countproduct() +"%>";
        //    output.Append(BuildShopBoxDetailCounts("المتابعين", ffollow.GetCountFollowers(shop.ID)));

        //    //عدد المنتجات
        //    output.Append(BuildShopBoxDetailCounts("المنتجات", fp.GetCountProducts(shop.ID)));

        //    output.Append("</ul>" +
        //                  "</div>");//ma3rad_data_info
        //    output.Append(BuildShopBoxOptions(pass, shop));

        //    output.Append("</div>" + //m3rad_info
        //                  "</li>");


        //    return output.ToString();

        //    /*
        //        <li id='shopbox_20' shpid='20'>
        //            <div class="m3rad_pic">
        //                <img src="/newfiles/images/store-logos/victoria.jpg" /></div>
        //            <div class="m3rad_info">
        //                <div class="ma3rad_data_info">
        //                    <div class="ma3rad-title">
        //                        متجر سنتر بوينت</div>
        //                    <div class="ma3rad-country">
        //                        المملكة العربية السعودية</div>
        //                    <ul>

        //                        <!-- BuildShopBoxDetailCounts -->

        //                    </ul>
        //                </div>

        //                <!-- shop options -->

        //            </div>
        //        </li>
        //     */
        //}

        public static string BuildSeeMoreShops(FamoPassport pass, string OnClick, string ExtraClass, string text)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<div class='se-more " + ExtraClass + "'>" +
              "<div class='se-more-button'>" +
              "<input type='button' name='more' value='" + text + "' onclick='" + OnClick + "' />" +
              "</div>" +
              "</div>");
            return output.ToString();
        }

        private static string BuildShopBoxDetailCounts(string text, int count)
        {
            //عرض احصائيه واحده للمتجر
            StringBuilder output = new StringBuilder();

            output.Append("<li>" +
                          "<div class='likers-counter'>" + count + "</div>" +
                          "<div class='arrow_shop'>" +
                          "</div>" +
                          "<div class='liks'>" + text + " </div>" +
                          "</li>");

            return output.ToString();

            /*
                 <li>
                    <div class="likers-counter">
                        1370</div>
                    <div class="arrow_shop">
                    </div>
                    <div class="liks">
                        معجباً</div>
                </li>

             */
        }

        private static string BuildShopBoxOptions(FamoPassport pass)
        {
            //عرض الخيارات التي في القائمة المنسدله لكل متجر
            StringBuilder output = new StringBuilder();

            output.Append("<div class='ma3rad_tool_option' shpid='22'>" +
                         "<a class='shop-option' evntt='lst_22' href='#'>" +
                         " <img src='/newfiles/images/shop-settings.png' /></a>" +
                         " <div id='lst_22' class='drpdwn_shop listdownshop'>" +
                         "<div class='arrow_option-shop'>" +
                         "</div>" +
                         " <ul>" +
                         "  <li><a href='#'>متابعة</a></li>" +
                         " <li><a href='#'>الاعدادات</a></li>" +
                         " <li><a href='#'>الاصدقاء المقربون</a></li>" +
                         " <li><a href='#'>المعارف</a></li>" +
                         " <li><a href='#'>اقتراح الاصدقاء</a></li>" +
                         " <li><a href='#'>الغاء الاصدقاء</a></li>" +
                      "</ul>" +
                 " </div>" +
              "</div>");


            return output.ToString();

            /*
                <div class="ma3rad_tool_option" shpid='22'>
                    <a class="shop-option" evntt="lst_22" href="#">
                        <img src="/newfiles/images/shop-settings.png" /></a>
                    <div id="lst_22" class="drpdwn_shop listdownshop">
                        <div class="arrow_option-shop">
                        </div>
                        <ul>
                            <li><a href="#">متابعة</a></li>
                            <li><a href="#">الاعدادات</a></li>
                            <li><a href="#">الاصدقاء المقربون</a></li>
                            <li><a href="#">المعارف</a></li>
                            <li><a href="#">اقتراح الاصدقاء</a></li>
                            <li><a href="#">الغاء الاصدقاء</a></li>
                        </ul>
                    </div>
                </div>
             */
        }

        private static string BuildShopBoxSeeMore(FamoPassport pass, FamActivity actv, int lastShopId)
        {
            //عرض المزيد من المعارض
            StringBuilder output = new StringBuilder();


            return output.ToString();


            /*
                <li id="seemoreshop_30" class="more_store">
                    <a href='javascript:void(0);' onclick='SeeMoreShops(document.getElementById(\"shopbox_30\"),lastid);'>
                        <div class="see-more-butt">
                            <div class="see-more-butt-num">
                                <div class="see-more-butt-num2">
                                    <div class="shop_count">
                                        +68</div>
                                </div>
                            </div>
                        </div>
                        <div class="more-txt-left">
                            للمزيد من المعارض</div>
                    </a>
                </li>             
            */
        }

        public static string getShopLogoPath(FamShop shop)
        {
            string filepath = shop.File.Path == null ? "" : shop.File.Path.Replace("~", "");
            if (filepath != "")
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath("") + filepath)) filepath = DefaultShopPhoto;
            }
            else
                filepath = DefaultShopPhoto;
            return "/thumb.aspx?image=" + filepath.Replace("/files_upload", "files_upload") + "&size=140";
        }

        public static string BuildProducts(FamoPassport pass, FamActivity actv)
        {
            //عرض منتجات حسب نشاط معين
            //وفي حالة ان النشاط صفر فإنه سيتم عرض عن كل المنتجات



            StringBuilder output = new StringBuilder();
            FamProduct fp = new FamProduct(pass);


            List<FamProduct> lfp = new List<FamProduct>();
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='store-offers' class='content-shop content-insider'>" +
                          "<div class='store-offers_title'>" +
                          "أحدث المنتجات</div>");
            if (actv.ID > 0)
                lfp = fp.GetproductList(actv.ID);//23
            else if (actv.ID == 0)
                lfp = fp.GetproductsList();//24

            output.Append("<ul>");//start ul
            foreach (FamProduct fprod in lfp)
                output.Append(BuildProduct(pass, fprod));
            output.Append("</ul>" +
                          "</div>");//store-offers
            return output.ToString();




            /*
            <div id="store-offers">
                <div class="store-offers_title">
                    أحدث العروض</div>
                <ul>
                    
                    <!-- loop for BuildProduct -->
              
                </ul>
            </div>

             */
        }

        private static string BuildProduct(FamoPassport pass, FamProduct prod)
        {
            //عرض منتج واحد
            StringBuilder output = new StringBuilder();
            FamFile c = new FamFile(pass);
            DataTable dt = c.GetFiles(FamoBlock.enmObjectCode.Products, prod.ID);
            string picture = "";//image default
            if (dt.Rows.Count > 0)
                picture = Convert.ToString(dt.Rows[0]["file_path"]);

            output.Append("<li id='product_" + prod.ID + "' >" +
                        "<div class='shop-price-value'>" + prod.NewPrice + "</div>" +
                        "<div class='shop-price'>" +
                        "</div>" +
                        "<div class='shop-info-value'>" +
                        "<div class='shopping-cart-icon'>" +
                //canceled temporarily
                //"<img src='/newfiles/images/shop-products/cart-icon.png' />" +
                        "</div>" +
                        "<div class='shopping-store-info'>" +
                        "<div class='shop-kind'>" + prod.Name + "</div>" +
                        "<div class='shop-store-name'>" + prod.Shop.Name + "</div>" +
                        "</div>" + //shopping-store-info
                        "</div>" + //shop-info-value
                        "<div class='shop-info'>" + "</div>");
            if (prod.OldPrice > 0)
                output.Append("<div class='sale_icon'><img src='/newfiles/images/shop-products/sale_ico.png'/></div>");

            output.Append("<img src='" + CheckIfImageExist(picture, DefaultPhoto3) + "' />" +
                "</li>");
            return output.ToString();


            /*
                <li>
                    <div class="shop-price-value">
                         سعر المنتج</div>
                    <div class="shop-price">
                    </div>
                    <div class="shop-info-value">
                        <div class="shopping-cart-icon">
                            <img src="/newfiles/images/shop-products/cart-icon.png" /></div>
                        <div class="shopping-store-info">
                            <div class="shop-kind">
                                اسم المنتج</div>
                            <div class="shop-store-name">
                                اسم المتحر</div>
                        </div>
                    </div>
                    <div class="shop-info">
                    </div>
                    <!-- في حالة وجود تخفيضات يتم وضع هذا السطر -->
                    <div class="sale_icon"><img src="/newfiles/images/shop-products/sale_ico.png" /></div>
              
                    <!-- صورة المنتج -->
                    <img src="/newfiles/images/shop-products/1.jpg" />
                </li>

             */
        }

        #endregion

        #region Albums
        public static string BuildAlbumsArea(FamoPassport pass, FamUser user)
        {
            StringBuilder output = new StringBuilder();
            output.Append(BuildAlbumsBlocks(pass, user));//albums

            //get first album of user
            FamAlbum a = new FamAlbum(pass);
            int usr = a.GetFirstAlbumId(user.ID);
            output.Append(BuildPhotosContent(pass, new FamAlbum(pass, usr), user));//photos of album
            return output.ToString();
        }

        public static string BuildAlbumsBlocks(FamoPassport pass, FamUser user)
        {

            //عرض اعدادات الالبوم
            StringBuilder output = new StringBuilder();
            FamAlbum fa = new FamAlbum(pass);

            List<FamAlbum> fmlist = fa.GetUserAlbumList(user.ID);
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='my-albums' class='content-insider'>" +
                          "<div class='shop_block_cat_title'>" +
                          "<a href='javascript:void(0);' onclick='NewAlbum();'><img class='add-album' src='/newfiles/images/tablet/add.png' title='اضافة البوم جديد'></a>" +
                          " ألبوماتي</div>" +
                          "<ul>");

            string selected = "background:#92af4b;";//this style applied on first album only
            foreach (FamAlbum album in fmlist)
            {
                output.Append(BuildAlbumContent(pass, user, album, selected));
                selected = "";
            }

            output.Append("<div id='seemorealbum'></div>");
            output.Append("</ul>" +
                          "</div>");//my-albums
            return output.ToString();

            /*
                <div id="my-albums">
                    <div class="shop_block_cat_title">
                        ألبوماتي</div>
                    <ul>
                        <!-- loop to call BuildAlbumContent -->
              
                    </ul>
                </div>
             */
        }

        public static string BuildAlbumContent(FamoPassport pass, FamUser user, FamAlbum album, string title_style = "")
        {
            //عرض اعدادات الالبوم
            string pathfile = "";
            StringBuilder output = new StringBuilder();

            FamFile file = new FamFile(pass);
            DataTable dt = file.GetAlbumFiles(album.ID);
            if (dt.Rows.Count > 0)
                pathfile = CheckIfImageExist(Convert.ToString(dt.Rows[0]["file_path"]).Replace("~", ""), DefaultPhoto3);
            else
                pathfile = DefaultPhoto4;
            if (album.File.ID > 0)
            {
                try
                {
                    pathfile = CheckIfImageExist(album.File.Path.Replace("~", ""), DefaultPhoto3);
                }
                catch (Exception ex) { }
            }
            output.Append("<li id='albm_" + album.ID + "' albmid='" + album.ID + "' uid='" + user.ID + "' class='album_blockli'>");
            //options menu
            output.Append(BuildAlbumOptions(pass, album));
            output.Append("<div class='album-bg-1'>" +
                        "</div>" +
                        "<div class='album-bg-2'>" +
                        "</div>" +
                        "<div class='album-bg-3'>" +
                        "<div class='album-counter'>" +
                        file.GetFilesCount(user.ID, album.ID) + "</div>" +
                        " <img id='albumimagecover_" + album.ID + "' src='" + pathfile.Replace("~", "") + "' />" +
                        "</div>" +
                        "<div id='albmname_" + album.ID + "' albmid='" + album.ID + "' class='album-title' title='" + album.Description + "' " + (title_style != "" ? "style='" + title_style + "'" : "") + ">" +
                          album.Name + "</div>" +
                        "</li>");
            return output.ToString();


            /*
                <li id="albm_22" albmid="22">
                    
                    <!-- BuildAlbumOptions -->
              
             
                    <div class="album-bg-1">
                    </div>
                    <div class="album-bg-2">
                    </div>
                    <div class="album-bg-3">
                        <div class="album-counter">
                            999</div>
                        <img src="/newfiles/images/video-gallery/kid.jpg" /> 
                    </div>
                    <div class="album-title" title="وصف الالبوم">
                        عنوان الالبوم</div>
                </li>

             */
        }
        private static string BuildAlbumOptions(FamoPassport pass, FamAlbum album)
        {
            //عرض اعدادات الالبوم
            StringBuilder output = new StringBuilder();
            output.Append("<div class='album_tool_option'>" +
                          "<a href='javascript:void(0);' evvntt='lstalb_" + album.ID + "' class='album-option'>" +
                          "<img src='/newfiles/images/video-gallery/tools.png' /></a>" +
                          "</div>" +
                          " <div id='lstalb_" + album.ID + "' class='drpdwn_album listdownalbum'>" +
                          "<div class='arrow_option-album'>" +
                          "</div>" +
                          "<ul>" +
                          "<li id='optalb_rename' onclick=\"showInputRenameAlbum(" + album.ID + ",document.getElementById('albmname_" + album.ID + "'));\">تغيير الاسم</li>" +
                          "<li id='optalb_delete' onclick=\"DeleteAlbum(document.getElementById('albm_" + album.ID + "'));\">حذف الالبوم</li>" +
                          "</ul>" +
                          "</div>");

            return output.ToString();


            /*
                <div class="album_tool_option">
                        <a href="#" evvntt="lst_24" class="album-option">
                            <img src="/newfiles/images/video-gallery/tools.png" /></a>عدم تغيير الصورة
                </div>
             
                <div id="lstopt_24" class="drpdwn_album listdownalbum">
                    <div class="arrow_option-album">
                    </div>
                    <ul>
                        <li><a href="#">متابعة</a></li>
                        <li><a href="#">الاعدادات</a></li>
                        <li><a href="#">الاصدقاء المقربون</a></li>
                        <li><a href="#">المعارف</a></li>
                        <li><a href="#">اقتراح الاصدقاء</a></li>
                        <li><a href="#">الغاء الاصدقاء</a></li>
                    </ul>
                </div>             

             */
        }

        public static string BuildPhotosContent(FamoPassport pass, FamAlbum album, FamUser user)
        {
            StringBuilder output = new StringBuilder();
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div id='photos-section' class='content-insider'>");
            //build photos
            output.Append(BuildPhotosBlock(pass, album, user));
            output.Append("</div>");//photos-section
            return output.ToString();

            /*    <div id="photos-section">
            <div class="shop_block_cat_title">
                صوري</div>
            <ul>
                <li>
                    <!-- LoopBuildTopCaption -->
                    
                   
                </li>
            </ul>
            <!-- Photos End -->
       </div> */
        }

        public static string BuildPhotosBlock(FamoPassport pass, FamAlbum album, FamUser user)
        {
            StringBuilder output = new StringBuilder();
            FamFile fa = new FamFile(pass);
            List<FamFile> files = fa.GetUserAlbumFilesList(user.ID, album.ID);
            string album_name = files.Count > 0 ? album.Name : "åÐÇ ÇáÇáÈæã áÇ íÍÊæí Úáì ÕæÑ";

            output.Append("<div class='shop_block_cat_title'>" +
                          "<a id='new-photo' href='javascript:void(0);'><img class='add-album' src='/newfiles/images/tablet/add.png' title='اضافة صورة جديدة'/></a>" +
                          album_name + " </div>" +
                          "<ul>");
            foreach (FamFile f in files)
                output.Append(BuildPhotoItem(pass, f));
            output.Append("</ul>");

            return output.ToString();
        }

        private static string BuildPhotoItem(FamoPassport pass, FamFile file)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<li id='photo_file_" + file.ID + "' file='" + file.ID + "'>");
            output.Append(BuildPhotoOption(pass, file));
            //top of photo
            output.Append("<div class='photo-top-caption'>" +
                       "<ul>" +
                       "<li class='h8'><a href='javascript:void(0);'>" + file.Name + "</a></li>" +
                //"<li class='h9'><a href='#'>" + file.Description + "</a></li>" +
                       "</ul>" +
                       "</div>");
            output.Append(BuildButtonCaption(pass, file));

            output.Append("<div class='photo-wrapper'>" +
                          "<a href='javascript:void(0);'>" +
                          "<img src='" + file.Path.Replace("~", "") + "' />" +
                          "</a>" +
                          "</div>");
            output.Append("</li>");
            return output.ToString();

            /*  <!-- Caption Top Start -->
                       <div class="photos_tool_option">
                           <a href="#" eevvntt="lst_25" class="photos-option">
                               <img src="/newfiles/images/video-gallery/tools.png" /></a>
                       </div>
                       <!-- -->
                       <div id="lst_25" class="drpdwn_photos listdownphotos">
                           <div class="arrow_option-photos">
                           </div>
                           <ul>
                               <li><a href="#">متابعة</a></li>
                               <li><a href="#">الاعدادات</a></li>
                               <li><a href="#">الاصدقاء المقربون</a></li>
                               <li><a href="#">المعارف</a></li>
                               <li><a href="#">اقتراح الاصدقاء</a></li>
                               <li><a href="#">الغاء الاصدقاء</a></li>
                           </ul>
                       </div>
                       <!-- -->
                       <div class="photo-top-caption">
                           <ul>
                               <li class="h8"><a href="#">صورة الطفل الباكي</a></li>
                               <li class="h9"><a href="#">البوم الاطفال</a></li>
                           </ul>
                       </div>
                       <!-- BuildButtonCaption -->*/

        }
        private static string BuildPhotoOption(FamoPassport pass, FamFile file)
        {
            StringBuilder output = new StringBuilder();
            FamVote fv = new FamVote(pass);
            FamComment fcm = new FamComment(pass);
            output.Append("<div class='photos_tool_option'>" +
                       "<a href='javascript:void(0);' eevvntt='lst_" + file.ID + "' class='photos-option'>" +
                       " <img src='/newfiles/images/video-gallery/tools.png' /></a>" +
                       "</div>" +
                       "<div id='lst_" + file.ID + "' class='drpdwn_photos listdownphotos'>" +
                       "<div class='arrow_option-photos'>" +
                       "</div>" +
                       "<ul>" +
                       "<li onclick=\"DeleteFile(document.getElementById('photo_file_" + file.ID + "'));\">حذف</li>" +
                       "<li onclick='ChangeAlbumCover(" + file.ID + ")'>تعيين كصورة للألبوم</li>" +
                       "</ul>" +
                       "</div>");//drpdwn_photos listdownphotos

            return output.ToString();


            /*          "<li>" +
                        "<div class='photos_tool_option'>" +
                       "<a href='#' eevvntt='lst_" + file.ID + "' class='photos-option'>" +
                       " <img src='/newfiles/images/video-gallery/tools.png' /></a>" +
                       "</div>" +
                       "<div id='lst_" + file.ID + "' class='drpdwn_photos listdownphotos'>" +
                       "<div class='arrow_option-photos'>" +
                       "</div>" +
                       "<ul>" +
                       "<li><a href='#'>متابعة</a></li>" +
                       "<li><a href='#'>الاعدادات</a></li>" +
                       "<li><a href='#'>الاصدقاء المقربون</a></li>" +
                       "<li><a href='#'>المعارف</a></li>" +
                       "<li><a href='#'>اقتراح الاصدقاء</a></li>" +
                       "<li><a href='#'>الغاء الاصدقاء</a></li>" +
                       "</ul>" +
                       "</div>
             
             */

        }

        private static string BuildButtonCaption(FamoPassport pass, FamFile file)
        {
            //bottom of photo عدد التعليقات والاعجاب للصورة
            StringBuilder output = new StringBuilder();
            /*
              
              
              تم الغاء الكود مؤقتاً حتى يتم الانتهاء من تصميم النافذه الخاصه بعرض الصورة
              
              
              
            FamVote fv = new FamVote(pass);
            FamComment fcm = new FamComment(pass);
            output.Append("<div class='photo-bottom-caption'>" +
                          "<div class='info-opt-view'>" +
                          "<a fileid='" + file.ID + "' onclick='UnLike(" + file.ID + ");' href='javascript:void(0);'>" +
                          "<img src='/newfiles/images/unlike_ico.png'>" +
                          "<label title='عدد الغير معجبين' class='unlikes_" + file.ID + "'>" +
                          fv.GetalblemUnlike((int)FamoBlock.enmObjectCode.Files, file.ID, pass.UserID) + "</label>" +
                          "</a>" +
                          "</div>" +
                          "<div class='info-opt-view'>" +
                          "<a tpcid='" + file.ID + "' onclick='AddLike(" + file.ID + ");' href='javascript:void(0);'>" +
                          "<img src='/newfiles/images/like_ico.png'>" +
                          "<label title='عدد الاعجاب' class='likes_" + file.ID + "'>" + fv.GetAlbomelike((int)FamoBlock.enmObjectCode.Files, file.ID, pass.UserID) + "</label>" +
                          "</a>" +
                          "</div>" +
                          "<div class='info-opt-view'>" +
                          "<a tpcid='" + file.ID + "' onclick='Display(" + file.ID + ");' href='javascript:void(0);'>" +
                          "<img src='/newfiles/images/comments_ico.png'>" +
                          "<label title='عدد التعليقات' class='comments_" + file.ID + "'>" +
                          fcm.GetNumbersOfComments((int)FamoBlock.enmObjectCode.Comments, file.ID) + "</label>" +
                          "</a>" +
                          "</div>" +
                          "</div>");
             */
            return output.ToString();


            /* <!-- Caption Bottom Start -->
                     <div class="photo-bottom-caption">
                         <div class="info-opt-view">
                             <!-- Display Topic Details -->
                             <a tpcid="2" onclick="UnLike(2);" href="javascript:void(0);">
                                 <img src="/newfiles/images/unlike_ico.png">
                                 <label title="عدد الغير معجبين" class="unlikes_2">
                                     2233</label>
                             </a>
                         </div>
                         <div class="info-opt-view">
                             <!-- Display Topic Details -->
                             <a tpcid="2" onclick="AddLike(2);" href="javascript:void(0);">
                                 <img src="/newfiles/images/like_ico.png">
                                 <label title="عدد الاعجاب" class="likes_2">
                                     2233</label>
                             </a>
                         </div>
                         <div class="info-opt-view">
                             <!-- Display Topic Details -->
                             <a tpcid="2" onclick="Display(2);" href="javascript:void(0);">
                                 <img src="/newfiles/images/comments_ico.png">
                                 <label title="عدد التعليقات" class="comments_2">
                                     2233</label>
                             </a>
                         </div>
                     </div>
                     <!-- BuildWrapper -->*/

        }
        #endregion

        #region Adminin Pages
        public static void FillShop(DropDownList ddl, string SelectedElement = "")
        {
            FamShop Fm = new FamShop(GetPassport());
            ddl.DataSource = Fm.GetShops();
            ddl.DataValueField = "shop_id";
            ddl.DataTextField = "name";
            ddl.DataBind();
            if (SelectedElement == "")
            {
                ddl.Items.Add(new ListItem("- اختر من القائمة -", "0"));
                ddl.SelectedValue = "0";
            }
        }

        public static void FillActivities(DropDownList ddl, string SelectedElement = "")
        {
            FamActivity Fm = new FamActivity(GetPassport());
            ddl.DataSource = Fm.GetActivitiesByLanguage(GetPassport().Language_ID, "name");
            ddl.DataValueField = "actv_id";
            ddl.DataTextField = "text";
            ddl.DataBind();
            if (SelectedElement == "")
            {
                ddl.Items.Add(new ListItem("- اختر من القائمة -", "0"));
                ddl.SelectedValue = "0";
            }
        }

        public static void FillTrigger(int Scene, DropDownList ddl, string SelectedElement = "")
        {
            FamTrigger Fm = new FamTrigger(GetPassport());
            ddl.DataSource = Fm.GetScenTrigger(Scene);
            ddl.DataValueField = "trg_id";
            ddl.DataTextField = "code";
            ddl.DataBind();
            if (SelectedElement == "")
            {
                ddl.Items.Add(new ListItem("- اختر من القائمة -", "0"));
                ddl.SelectedValue = "0";
            }
        }

        public static void FillScenes(DropDownList ddl, string SelectedElement = "")
        {

            FamScene Fm = new FamScene(GetPassport());

            ddl.DataSource = Fm.Getscens();
            ddl.DataValueField = "scn_id";
            ddl.DataTextField = "code";
            ddl.DataBind();
            if (SelectedElement == "")
            {
                ddl.Items.Add(new ListItem("- اختر من القائمة -", "0"));
                ddl.SelectedValue = "0";
            }
        }
        #endregion

        #region Setting Page


        /************************  دوال خاصة بتعبئة البيانات للإعدادات  ***********************/

        public static string BuildSettingBox(FamoPassport pass)
        {
            //setting box
            StringBuilder output = new StringBuilder();
            FamUser u = new FamUser(pass, pass.UserID);

            output.Append(BuildSettingPersonalBox(pass, u));//الإعدادات الشخصية
            output.Append(BuildSettingJobBox(pass, u));//الإعدادات العامة
            output.Append(BuildSettingSecurityBox(pass, u));//اعدادات الأمان
            output.Append(BuildSettingPhotoBox(pass, u));//الصورة الشخصية

            return output.ToString();
        }

        public static string BuildSettingJobBox(FamoPassport pass, FamUser user)
        {
            //عرض تبويب الاسم الاول والاخير 
            StringBuilder output = new StringBuilder();
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div class='setting_box content-insider'>");
            output.Append(BuildSettingBoxHeader("معلومات وظيفية", "/newfiles/images/setting_icon1.png"));

            output.Append("<div id='' class='setting_box_hed setting_box_form firstpane'>");

            FamWorkProfile w = user.FillWorkProfile();
            w.FillUserWorkProfile(pass.UserID);

            if (w.ID == 0)
            {
                //في حالة عدم وجود سجل وظيفة للمستخدم فإنه يتم اضافته بقيم افتراضية
                w.User.ID = pass.UserID;
                w.Save();
            }
            //معلومات وظيفية
            output.Append(BuildSettingRowHeader("sett_jobs", "الوظيفة", w.Employer, "", true));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_jobs_valid", ""));
            output.Append(BuildSettingRowDataInput("txtjob", "الوظيفة", w.Employer));
            output.Append(BuildSettingRowDataInput("txtcompany", "اسم الشركة", w.CompanyName));
            output.Append(BuildSettingRowDataInput("txtcollage", "المستوى الدراسي", w.College));

            output.Append(BuildSettingRowSaveButton("lnksavejobs", "حفظ", ""));
            output.Append("</span>");


            //عني
            output.Append(BuildSettingRowHeader("sett_aboutme", "عني", "", "", false));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_aboutme_valid", ""));
            output.Append(BuildSettingRowDataTextarea("txtaboutme", "نبذة عني", w.About));
            output.Append(BuildSettingRowSaveButton("lnksaveabout", "حفظ", ""));
            output.Append("</span>");



            output.Append("</div>");//setting_box_hed setting_box_form firstpane
            output.Append("</div>");//setting_box
            return output.ToString();
        }
        public static string BuildSettingPersonalBox(FamoPassport pass, FamUser user)
        {
            //عرض تبويب الاسم الاول والاخير 
            StringBuilder output = new StringBuilder();
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div class='setting_box content-insider'>");
            output.Append(BuildSettingBoxHeader("معلومات شخصية", "/newfiles/images/setting_icon2.png"));

            output.Append("<div id='' class='setting_box_hed setting_box_form firstpane'>");

            //الاسم الكامل
            output.Append(BuildSettingRowHeader("sett_name", "الاسم", pass.FullName, "", true));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_name_valid", ""));
            output.Append(BuildSettingRowDataInput("txtfirstname", "الاسم الاول", user.FirstName));
            output.Append(BuildSettingRowDataInput("txtlastname", "الاسم الأخير", user.LastName));
            output.Append(BuildSettingRowSaveButton("lnksavepersonal", "حفظ", ""));
            output.Append("</span>");

            List<FamContactProfile> cont = user.FillContractProfile();
            string cty = "", cntry = "", nation = "", add = "";
            if (cont.Count > 0)
            {
                cntry = cont[0].Country.Text;
                cty = cont[0].City;
                nation = cont[0].Nationality.Text;
                add = cont[0].Address;
            }
            //العنوان
            output.Append(BuildSettingRowHeader("sett_addre", "العنوان", cntry + " - " + cty, "", true));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_addre_valid", ""));

            FamList fl = new FamList(pass);

            //fill countries from list table
            DataTable dt = fl.GetListsByName(FamoLibrary.FamoBlock.Const_List_Country, Convert.ToInt32(ClassMain.GetOptionValue(FamoBlock.Const_Option_Pub_MainLanguage)));
            output.Append(BuildSettingRowDataSelect(dt, "text", "list_id", "selc_country", "الدولة", cntry));

            output.Append(BuildSettingRowDataInput("txtcity", "المدينة", cty));
            output.Append(BuildSettingRowDataInput("txtaddreess", "العنوان", add));

            //fill nationalities from list table
            dt = fl.GetListsByName(FamoLibrary.FamoBlock.Const_List_Nationality, Convert.ToInt32(ClassMain.GetOptionValue(FamoBlock.Const_Option_Pub_MainLanguage)));
            output.Append(BuildSettingRowDataSelect(dt, "text", "list_id", "selc_nation", "الجنسية", nation));


            output.Append(BuildSettingRowSaveButton("lnksavesec", "حفظ", ""));
            output.Append("</span>");

            //رقم الجوال
            List<FamPhoneProfile> phone = user.FillPhoneProfile(FamoBlock.enmPhoneType.Mobile);
            string mob = "";
            if (phone.Count > 0) mob = phone[0].Number;
            output.Append(BuildSettingRowHeader("sett_mobile", "رقم الجوال", mob, "", true));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_name_valid", ""));
            output.Append(BuildSettingRowDataInput("txtMobile", "رقم الجوال", mob));
            output.Append(BuildSettingRowSaveButton("lnksaveMobile", "حفظ", ""));
            output.Append("</span>");


            output.Append("</div>");//setting_box_hed setting_box_form firstpane
            output.Append("</div>");//setting_box
            return output.ToString();
        }

        public static string BuildSettingSecurityBox(FamoPassport pass, FamUser user)
        {
            //عرض تبويب الاسم الاول والاخير 
            StringBuilder output = new StringBuilder();
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div class='setting_box content-insider'>");
            output.Append(BuildSettingBoxHeader("اعدادات الأمان", "/newfiles/images/setting_icon3.png"));

            output.Append("<div id='' class='setting_box_hed setting_box_form firstpane'>");

            output.Append(BuildSettingRowHeader("", "البريد الالكتروني", user.Email, "", true));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_email_valid", ""));//valid summary
            output.Append(BuildSettingRowDataInput("txtemail", "البريد الإلكتروني", user.Email));
            output.Append(BuildSettingRowSaveButton("lnksaveemail", "حفظ", ""));
            output.Append("</span>");



            //تاريخ الميلاد
            output.Append(BuildSettingRowHeader("", "تاريخ الميلاد", user.BirthDate, "", false));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_birth_valid", ""));//valid summary
            output.Append(BuildSettingRowDataDate("bdate", "تاريخ الميلاد", user.BirthDate));
            output.Append(BuildSettingRowSaveButton("lnksavebirth", "حفظ", ""));
            output.Append("</span>");

            //تغيير كلمة المرور
            output.Append(BuildSettingRowHeader("", "تغيير كلمة المرور", "*******", "", false));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_pass_valid", ""));//valid summary
            output.Append(BuildSettingRowDataInput("txtoldpassword", "كلمة المرور الحالية", "", true));
            output.Append(BuildSettingRowDataInput("txtnewpassword", "كلمة المرور الجديدة", "", true));
            output.Append(BuildSettingRowDataInput("txtrenewpassword", "اعادة كلمة المرور ", "", true));
            output.Append(BuildSettingRowSaveButton("lnksavepass", "حفظ", ""));
            output.Append("</span>");

            //اسم المستخدم
            output.Append(BuildSettingRowHeader("sett_username", "اسم المستخدم", user.UserName, "", false));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_uname_valid", ""));
            output.Append(BuildSettingRowDataInput("txtUserName", "اسم المستخدم", user.UserName));

            output.Append(BuildSettingRowSaveButton("lnksaveuname", "حفظ", ""));
            output.Append("</span>");

            output.Append("</div>");
            output.Append("</div>");//setting_box
            return output.ToString();
        }

        public static string BuildSettingPhotoBox(FamoPassport pass, FamUser user)
        {
            //عرض تبويب الاسم الاول والاخير 
            StringBuilder output = new StringBuilder();
            //.content-insider يتم حذف محتوى الصفحة عن طريق هذا الكلاس 
            output.Append("<div class='setting_box content-insider'>");
            output.Append(BuildSettingBoxHeader("الصورة الشخصية", "/newfiles/images/setting_icon3.png"));

            output.Append("<div id='' class='setting_box_hed setting_box_form firstpane'>");

            output.Append(BuildSettingRowHeader("", "الصورة الشخصية", "", "", true));
            output.Append("<span class='setting_box_form_lable menu_body'>");
            output.Append(BuildSettingRowValid("sett_photouser_valid", ""));//valid summary
            output.Append(BuildSettingRowDataUploader("fileupload2", "اختر صورة"));
            output.Append(BuildSettingRowSaveButton("lnksaveUpload", "حفظ", ""));
            output.Append("</span>");
            /*
                         <div id="fileupload2">
                <div class="fileupload-content un-ui-corner-bottom un-ui-widget-content">
                    <div class="files">
                    </div>
                </div>
                <div id="filesUploaded" style="display: none">
                </div>
                <form action="/FileTransferHandler.ashx" method="post" enctype="multipart/form-data">
                <div class="fileupload-buttonbar btnandlode">
                    <button title="مسح جميع الملفات المختارة" type="button" class="delete button deleteico deleteico_m">
                    </button>
                    <label class="fileinput-button addico addico_m">
                        <span class="hide_all" title="">اضافة ملف</span>
                        <input id="btnUploadFilesTpc" type="file" name="files[]" multiple="multiple" />
                    </label>
                    <a href="javascript:void(0);" class="youtube_btn" title="مشاركة مقطع فيديو من اليوتيوب">
                    </a>
                    <div class="fileupload-progressbar" style=" height: 16px; border-radius: 3px;
                        margin-top: 5px; margin-right: 13px;">
                    </div>
                </div>
                </form>
                
            </div>
            <script id="template-upload2" type="text/x-jquery-tmpl">
                <span class="template-view template-upload{{if error}} ui-state-error{{/if}}">
                    <div class="preview preview_ico"></div>
                    <div class="name name-view">${name}</div>
                    <div class="size size-view">${sizef}</div>
                    <div class="cancel"><button>Cancel</button></div>
                    {{if error}}
                        <div class="error error-view" colspan="2">Error:
                            {{if error === 'maxFileSize'}}File is too big
                            {{else error === 'minFileSize'}}File is too small
                            {{else error === 'acceptFileTypes'}}Filetype not allowed
                            {{else error === 'maxNumberOfFiles'}}Max number of files
                            {{else}}${error}
                            {{/if}}
                        </div>
                        
                    {{else}}
                        <div class="progress progress-view"><div></div></div>
                        <div class="start"><button>Start</button></div>
                    {{/if}}
                        
                    
                </span>
            </script>
            <script id="template-download2" type="text/x-jquery-tmpl">
                <span class="template-view template-download{{if error}} ui-state-error{{/if}}">
                    {{if error}}
                        <div class="preview_ico"></div>
                        <div class="name">${name}</div>
                        <div class="size">${sizef}</div>
                        <div class="error error-view" colspan="2">Error:
                            {{if error === 1}}File exceeds upload_max_filesize (php.ini directive)
                            {{else error === 2}}File exceeds MAX_FILE_SIZE (HTML form directive)
                            {{else error === 3}}File was only partially uploaded
                            {{else error === 4}}No File was uploaded
                            {{else error === 5}}Missing a temporary folder
                            {{else error === 6}}Failed to write file to disk
                            {{else error === 7}}File upload stopped by extension
                            {{else error === 'maxFileSize'}}File is too big
                            {{else error === 'minFileSize'}}File is too small
                            {{else error === 'acceptFileTypes'}}Filetype not allowed
                            {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                            {{else error === 'uploadedBytes'}}Uploaded bytes exceed file size
                            {{else error === 'emptyResult'}}Empty file upload result
                            {{else}}${error}
                            {{/if}}
                        </div>
                    {{else}}
                        <div class="preview preview_ico">
                            {{if thumbnail_url}}
                                <a href="${url}" target="_blank"><img src="${thumbnail_url}"></a>
                            {{/if}}
                        </div>
                        <div class="name">
                            <a href="${url}"{{if thumbnail_url}} title="${name}" target="_blank"{{/if}}>${name}</a>
                        </div>
                        <div class="size">${sizef}</div>
                        <div colspan="2"></div>
                    {{/if}}
                    <div class="delete">
                        <button data-type="${delete_type}" data-url="${delete_url}">Delete</button>

                    </div>
                </span>

            </script>

             */

            output.Append("</div>");//setting_box_hed
            output.Append("</div>");//setting_box
            return output.ToString();
        }

        /**********************  دوال بناء هيكلية الإعدادات  ************************/


        public static string BuildSettingBoxHeader(string title, string image)
        {
            //عنوان صندوق الاعدادات
            StringBuilder output = new StringBuilder();
            output.Append("<div class='setting_box_hed'>" +
                          "<img src='" + image + "' />" +
                          "<p>" + title + "</p>" +
                          "</div>");
            return output.ToString();
        }

        public static string BuildSettingRowHeader(string divid, string title, string value, string spanid, bool tabOpen)
        {
            //عنوان التاب الواحدة
            StringBuilder output = new StringBuilder();

            //status = (0) tab not oppened , (1) tab is oppened
            output.Append("<div id='" + divid + "' class='setting_box_form_title menu_head' status='" + Convert.ToInt16(tabOpen) + "' spanid='" + spanid + "'>" +
                          "<div class='form_title_rname'>" + title + "</div>" +
                          "<div class='form_title_lname'>" + value + "</div>" +
                          "<div class='form_title_rowicon " + (tabOpen ? "arrowup" : "") + "'></div>" +
                          "</div>");
            return output.ToString();
        }

        public static string BuildSettingRowSpan(string spanid)
        {
            //تفاصيل التاب الواحدة
            StringBuilder output = new StringBuilder();
            output.Append("<span id='" + spanid + "'" +
                          "class='setting_box_form_lable menu_body' style='display: inline-block;'>" +
                          "</span>");
            return output.ToString();
        }

        public static string BuildSettingRowValid(string validid, string ErrorText)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<div class='form_lable' id='" + validid + "'>" +
                //"<div class='form_lable_v'>" + ErrorText + "</div>" +
                          "</div>");
            return output.ToString();
        }

        public static string BuildSettingRowDataInput(string inputid, string title, string value, bool password = false)
        {
            //انشاء صف بيانات يتحوي على ليبل + تكست بوكس
            StringBuilder output = new StringBuilder();

            output.Append("<div class='form_lable'>" +
                          "<div class='form_lable_l'>" + title + "</div>" +
                          "<input name='' type='" + (password ? "password" : "text") + "' id='" + inputid + "' class='form_lable_r' value='" + value + "'>" +
                          "</div>");
            return output.ToString();
        }

        public static string BuildSettingRowDataUploader(string inputid, string title)
        {
            //انشاء صف بيانات يتحوي على ليبل + تكست بوكس
            StringBuilder output = new StringBuilder();

            output.Append("<div class='form_lable'>" +
                          "<div class='form_lable_l'>" + title + "</div>" +
                          "<form action='/FileTransferHandler.ashx' method='post' enctype='multipart/form-data'>" +
                          "<input type='file' multiple='' name='files[]' id='" + inputid + "'>" +//
                          "</form>" +
                //"<input name='' type='" + (password ? "password" : "text") + "' id='" + inputid + "' class='form_lable_r' value='" + value + "'>" +
                          "</div>");
            return output.ToString();
        }

        public static string BuildSettingRowDataTextarea(string inputid, string title, string value)
        {
            //انشاء صف بيانات يتحوي على ليبل + تكست بوكس
            StringBuilder output = new StringBuilder();

            output.Append("<div class='form_lable'>" +
                          "<div class='form_lable_l'>" + title + "</div>" +
                          "<textarea id='" + inputid + "' class='form_lable_r' >" + value + "</textarea>" +
                          "</div>");
            return output.ToString();
        }

        public static string BuildSettingRowDataSelect(DataTable datasource, string DataTextField, string DataValueField, string selectid, string title, string value)
        {
            //انشاء صف بيانات يتحوي على ليبل + قائمة منسدلة
            //create row contain <div> as label and <select> tag 
            StringBuilder output = new StringBuilder();
            output.Append("<div class='form_lable'>" +
                          "<div class='form_lable_l'>" + title + "</div>" +
                          "<select id='" + selectid + "' class='form_lable_r'>");
            output.Append("<option value='0' selected='selected'>" + "- اختر من القائمة -" + "</option>");
            foreach (DataRow dr in datasource.Rows)
            {
                output.Append("<option value='" + dr[DataValueField].ToString() + "' " + (dr["text"].ToString() == value ? "selected='selected'" : "") + ">" + dr[DataTextField].ToString() + "</option>");
            }
            output.Append("</select>" +
                          "</div>");
            return output.ToString();
        }

        public static string BuildSettingRowDataDate(string divid, string title, string value)
        {
            //انشاء صف بيانات يتحوي على ليبل + قوائم منسدلة لتاريخ الميلاد
            //create row contain <div> as label and three <select> tags as date field
            StringBuilder output = new StringBuilder();
            output.Append("<div class='form_lable'>" +
                          "<div class='form_lable_l'>" + title + "</div>");

            output.Append("<div class='form_lable_r_multe'>");

            string[] d = value.Split(new char[] { '/' });
            //ddlDay.SelectedValue = d[0];
            //ddlMonth.SelectedValue = d[1];
            //ddlYear.SelectedValue = d[2];


            //fill years
            output.Append("<select id='" + divid + "_select_years'>");
            output.Append("<option value='0'>- السنة -</option>");
            for (int i = 1950; i < 2010; i++)
                if (value != null && value != "")
                    output.Append("<option " + (d[2] == i.ToString() ? "selected='selected'" : "") + " value='" + i.ToString() + "'>" + i.ToString() + "</option>");
            output.Append("</select>");//years

            //fill months
            output.Append("<select id='" + divid + "_select_months'>");
            output.Append("<option value='0'>- الشهر -</option>");
            for (int i = 1; i < 13; i++)
                if (value != null && value != "")
                    output.Append("<option " + (d[1] == i.ToString() ? "selected='selected'" : "") + " value='" + i.ToString() + "'>" + i.ToString() + "</option>");
            output.Append("</select>");//months

            //fill days
            output.Append("<select id='" + divid + "_select_days'>");
            output.Append("<option value='0'>- اليوم -</option>");
            for (int i = 1; i < 32; i++)
                if (value != null && value != "")
                    output.Append("<option " + (d[0] == i.ToString() ? "selected='selected'" : "") + " value='" + i.ToString() + "'>" + i.ToString() + "</option>");
            output.Append("</select>");//days

            output.Append("</div>");//form_lable_r_multe
            output.Append("</div>");//form_lable

            return output.ToString();
        }

        public static string BuildSettingRowSaveButton(string tagid, string title, string onClick)
        {
            //زر الحفظ
            StringBuilder output = new StringBuilder();
            output.Append("<a id='" + tagid + "' class='form_lable_savebtn' href='javascript:void(0);' onclick='" + onClick + "'>" + title + "</a>");
            return output.ToString();
        }

        public static string BuildSettingRowDataDate(string title)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<div class='form_lable_l'>" + title + "</div>");
            FillSelectList(1, 32);// يوم 
            FillSelectList(1, 13); // شهر 
            FillSelectList(1700, 2000); // سنة 
            return output.ToString();
        }

        public static string FillSelectList(int From, int To)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<select id='Select2'>");

            for (int i = From; i < To; i++)
            {
                output.Append("<option value='" + i + "'>" + i + "</option>");
            }

            output.Append("</select>");
            return output.ToString();
        }

        #endregion

        #region MenuTablet

        public static string BuildTabletUserPhotoMenu(FamoPassport pass)
        {
            return BuildTabletUserPhotoMenu(ClassMain.getUserLogoPath(new FamUser(pass, pass.UserID)), pass.FullName);
        }

        public static string BuildTabletUserPhotoMenu(string photopath,string fullname)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<div class='sidebar-userinfo'>" +
                          "<div class='sidebar-userpic'>");
            output.Append("<img src='" + photopath + "'/>");
            output.Append("</div>" +  //sidebar-userpic
                          "<div class='sidebar-username'>");
            output.Append(fullname);
            output.Append("</div>" + //sidebar-username
                          "<a href='/shop/customer/info'>تعديل صورة البروفايل</a>" +
                          "</div>");
            return output.ToString();

            /*
             
            <div class="sidebar-userinfo">
                <div class="sidebar-userpic">
                    <img src="/newfiles/images/img.png" />
                </div>
                <div class="sidebar-username">
                    محمد مصطفي صلاح
                </div>
                <a href="#">تعديل صورة البروفايل</a>
            </div>
             */


        }

        public static string BuildTabletMainMenu(FamoPassport pass)
        {
            StringBuilder output = new StringBuilder();
            string pageLink = "usrmain.aspx?uid=" + pass.UserID + "&pg=";
            output.Append("<div class='sidebar-main-menu'>" +
                          "<ul>");
            //تم نسخ القيم التالية للصفحات من فنكشن BuildMainNavigationMenu 
            output.Append(BuildTabletMainMenuItem("صفحتي", "/newfiles/images/tablet/my-page-icon.png", "/person/" + pass.UserName + "/" + PageName.Arena.ToString(), ""));//pageLink + (int)PageName.Arena
            output.Append(BuildTabletMainMenuItem("الصور والفيديو", "/newfiles/images/tablet/photos-videos-icon.png", "/person/" + pass.UserName + "/" + PageName.Photos.ToString(), "", 0));//pageLink + (int)PageName.Photos
            output.Append(BuildTabletMainMenuItem("اصدقائي", "/newfiles/images/tablet/friends-icon.png", "/person/" + pass.UserName + "/" + PageName.Friends.ToString(), "", 0));//pageLink + (int)PageName.Friends + "&tab=" + (int)FriendArea.MyFriends
            output.Append(BuildTabletMainMenuItem("متاجر ومنتجات", "/newfiles/images/tablet/stores-products-icon.png", "/person/" + pass.UserName + "/" + PageName.Shops.ToString(), "", 0));//pageLink + (int)PageName.Shops
            output.Append("</ul>" +
                          "</div>");

            //قائمة الاعدادات
            output.Append(BuildTabletSettingMenu());
            return output.ToString();

            /*
        <div class="sidebar-main-menu">
            <ul>
                <li><a href="#">أخر الاخبار
                    <img src="/newfiles/images/tablet/last-news-icon.png" /></a></li>
                <li><a href="#">صفحتى
                    <img src="/newfiles/images/tablet/my-page-icon.png" /></a></li>
                <li><span class="sidebar-counter">4520</span><a href="#">الصور والفيديو
                    <img src="/newfiles/images/tablet/photos-videos-icon.png" /></a></li>
                <li><span class="sidebar-counter">220</span><a href="#">اصدقائي
                    <img src="/newfiles/images/tablet/friends-icon.png" /></a></li>
                <li><a href="#">متاجر ومنتجات
                    <img src="/newfiles/images/tablet/stores-products-icon.png" /></a></li>
                <li><a href="#">فيمو سيتي
                    <img src="/newfiles/images/tablet/famo-icon.png" /></a></li>
            </ul>
        </div>
             
             
             */
        }

        public static string BuildTabletMainMenuItem(string title, string icon, string href = "javascript:void(0);", string onClick = "", int counter = 0)
        {
            StringBuilder output = new StringBuilder();
            if (counter == 0)
                //menu link without counter
                output.Append("<li><a href='" + href + "' onclick='" + onClick + "'>" + title + "<img class='mnu-img' src='" + icon + "' /></a></li>");
            else
                //menu link with counter, if counter parameter is greater than zero
                output.Append("<li><span class='sidebar-counter'>" + counter + "</span><a href='" + href + "' onclick='" + onClick + "'>" + title + "<img class='mnu-img' src='" + icon + "' /></a></li>");
            return output.ToString();

            /*
             
             <li><a href="#">أخر الاخبار<img src="/newfiles/images/tablet/last-news-icon.png" /></a></li>
             */
        }
        public static string BuildTabletSettingMenu()
        {
            StringBuilder output = new StringBuilder();
            output.Append("<div class='sidebar-main-title'>الإعدادات</div>");
            output.Append("<div class='sidebar-second-menu'>" +
                          "<ul>");
            output.Append(BuildTabletSettingMenuItem("", "الاعدادات", "", "/usrmain.aspx?uid=10&pg=6"));
            output.Append(BuildTabletSettingMenuItem("", "لديك مشكلة ؟", "", "/feedback"));
            output.Append(BuildTabletSettingMenuItem("", "تسجيل الخروج", "", "/logout"));
            output.Append("</ul>" +
                          "</div>");
            return output.ToString();
        }

        public static string BuildTabletSettingMenuItem(string id, string title, string onclick = "", string href = "javascript:void(0);")
        {
            StringBuilder output = new StringBuilder();
            output.Append("<li><a id='" + id + "' href='" + href + "'>" + title + "</a></li>");
            return output.ToString();
        }
        #endregion

        #region Timers
        public static string GetPrettyDate(DateTime d)
        {
            string resjustnow = "الان";//getkeyval(respublic, "resjustnow");
            string resminuteago = "قبل دقيقة";//getkeyval(respublic, "resminuteago");
            string reshourago = "قبل ساعة";//getkeyval(respublic, "reshourago");
            string resyesterday = "الأمس";//getkeyval(respublic, "resyesterday");
            string resdaysago = "قبل يوم";//getkeyval(respublic, "resdaysago");
            string resweeksago = "قبل اسبوع";//getkeyval(respublic, "resweeksago");
            // 1.
            // Get time span elapsed since the date.
            TimeSpan s = DateTime.Now.Subtract(d);

            // 2.
            // Get total number of days elapsed.
            int dayDiff = (int)s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            int secDiff = (int)s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }

            // 5.
            // Handle same-day times.
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return resjustnow;// "just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 " + resminuteago;//"minute ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} " + resminuteago,
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 " + reshourago;//hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} " + reshourago,
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return resyesterday;//"yesterday";
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} " + resdaysago, dayDiff);//days ago",

            }
            if (dayDiff < 31)
            {
                return string.Format("{0} " + resweeksago + "",
                Math.Ceiling((double)dayDiff / 7));
            }
            return null;
        }
        public static string getkeyval(string resfile, string key)//, string calt
        {
            string cal = "";
            try
            {
                cal = HttpContext.Current.Request.Cookies["lang"].Value.ToString();
                if (cal == "1")
                    cal = "ar-sa";
                else cal = "en-US";
                if (string.IsNullOrEmpty(cal))
                    cal = "en-US";
                //else
                // cal = calt;
            }
            catch (Exception xp) { cal = "en-US"; }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cal);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            ResourceManager rm = new ResourceManager(resfile, Assembly.GetExecutingAssembly());
            string keyss = rm.GetString(key).ToString();
            return keyss;

        }

        #endregion


        #region Message

        public static string BuildMessageLiItem(FamoPassport pass, int lastid, string type)
        {
            FamMessage message = new FamMessage(pass);
            DataTable dt = new DataTable();
            StringBuilder str = new StringBuilder();

            //<li>
            //                      <div class='msg_item'>
            //                          <div class='msg_sender_name'>
            //                              محمد مصطفى صلاح</div>
            //                          <div class='msg_content'>
            //                              انا جايلك النهاردة استنانى ضرورى متنساش</div>
            //                          <div class='msg_time'>
            //                              الثلاثاء 25 نوفمبر</div>
            //                      </div>
            //                      <div class='msg_item_pic'>
            //                          <img src='/newfiles/images/prof_pic.jpg' /></div>
            //                  </li>
            if (lastid == 0)
                dt = message.GetLastPersonsMessages(pass.UserID);
            else
                if (type == "old")
                    dt = message.getOldPersonsMessages(lastid, pass.UserID);
                else
                    dt = message.GetNewPersonsMessage(lastid, pass.UserID);



            NopService.NopService serv = new NopService.NopService();


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {



                        MessageData msgdata = new MessageData();
                        msgdata.ID = Convert.ToInt32(dr["msg_id"]);
                        // msgdata.Message = dr["message"].ToString();

                        User usrsender = new User();
                        FamUser senderuser = new FamUser(pass, Convert.ToInt32(dr["sender_id"]));
                        usrsender = GetNopCustomerData(senderuser);

                        User userreciver = new User();
                        FamUser receiveruser = new FamUser(pass, Convert.ToInt32(dr["user_id"]));
                        userreciver = GetNopCustomerData(receiveruser);



                        string username = "";
                        string path = "";
                        int reciverid = 0;
                        if (pass.UserID == Convert.ToInt32(dr["sender_id"]))
                        {
                            username = userreciver.Name;
                            path = userreciver.PhotoPath;
                            reciverid = Convert.ToInt32(dr["user_id"]);
                        }
                        else
                        {
                            username = usrsender.Name;
                            path = usrsender.PhotoPath;
                            reciverid = Convert.ToInt32(dr["sender_id"]);
                        }

                        str.Append("<li>");
                        str.Append("<a href='/Chat/" + reciverid + "'>");
                        //str.Append("<a href='/message.aspx?uid=" + reciverid + "'>");
                        str.Append("<div class='msg_item'>");
                        str.Append(" <div class='msg_sender_name'>");
                        str.Append(username + "</div>");
                        str.Append("<div class='msg_content'>");
                        str.Append(dr["message"].ToString() + "</div>");
                        str.Append("<div class='msg_time'>");
                        str.Append(ClassMain.GetPrettyDate(Convert.ToDateTime(dr["cud"].ToString())) + "</div>");
                        str.Append("</div>");
                        str.Append("<div class='msg_item_pic'>");
                        str.Append("<img src='" + path + "' />");
                        str.Append("</div>");
                        str.Append("</a>");
                        str.Append(" </li>");



                    }


                }

            }


            return str.ToString();

        }


        private static User GetNopCustomerData(FamUser users)
        {
            User user = new User();
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];


            if (pass == null) return user;
            DataTable nopdt = new DataTable();
            user.Name = pass.FullName;
            user.Link = ClassMain.WebHosting + "person/" + users.ID + "/Arena";
            user.PhotoPath = ClassMain.getUserLogoPath(new FamUser(pass, users.ID));
            user.ID = users.ID;


            try
            {
                NopService.NopService serv = new NopService.NopService();
                nopdt = serv.GetCustomerData(ClassMain.serviceUser, ClassMain.servicePass, users.CustomerId, true).Tables[0];
                user.Name = nopdt.Rows[0]["firstname"].ToString() + " " + nopdt.Rows[0]["lastname"].ToString();
                user.PhotoPath = nopdt.Rows[0]["CustomerImage"].ToString();

            }
            catch (Exception exp) { }

            return user;
        }
        #endregion

        //public

        #region NotificationsInserting
        public static int SaveNotification(FamoPassport pass, FamoBlock.enmObjectCode ProcessType, int processid, FamoBlock.enmObjectCode targtCode, int TargetId, int Targetuserid)
        {
            FamEvent fevent = new FamEvent(pass);
            fevent.User.ID = pass.UserID;
            fevent.ObjectCode = ProcessType;
            fevent.ObjectId = processid;
            fevent.TargetObjectCode = targtCode;
            fevent.TargetObjectId = TargetId;
            fevent.TargetId = Targetuserid;
            FamoPersonalService pers = new FamoPersonalService();
            pers.IncreaseNotificationCounter(pass.Email, pass.Password, Targetuserid, 2);
           // IncreaseNotificationCounter(pass, Targetuserid, NotificationEnum.Notification_Event);


            return fevent.Save();
        }
        #endregion

        #region Notifications

        //private static List<int> GetMyUniqueTargetCode( DataTable dat)
        //{
        //    List<int> targecodeList = new List<int>();
        //    foreach (DataRow dr in dat.Rows)
        //    {
        //        int init = 0;
        //        foreach (int code in targecodeList)
        //        {
        //            if (Convert.ToInt32(dr["target_objcode"]) == code)
        //            {
        //                init = 1;
        //                break;
        //            }


        //        }
        //        if (init == 0)
        //            targecodeList.Add(Convert.ToInt32(dr["target_objcode"]));
        //    }
        //    return targecodeList;
        //}


        //private static List<int> GetMyUniqueTargetObjId(int tragetobjcode, DataTable dat)
        //{
        //    List<int> targeobjidList = new List<int>();
        //    foreach (DataRow dr in dat.Rows)
        //    {
        //        int init = 0;

        //            if (Convert.ToInt32(dr["target_objcode"]) == tragetobjcode)
        //            {

        //                foreach(int id in targeobjidList)
        //                {

        //                    if(id==Convert.ToInt32(dr["target_objid"]))
        //                    {
        //                        init=1;
        //                        break;
        //                    }
        //                }

        //                if(init==0)
        //                    targeobjidList.Add(Convert.ToInt32(dr["target_objid"]));
        //            }


        //        }

        //    return targeobjidList;


        //}

        //private static List<int> NotificationMaker(int targetobjcode, int targetobjid, int notyobjcode, DataTable dat)
        //{
        //    List<int> listofnotytype = new List<int>();
        //    foreach (DataRow dr in dat.Rows)
        //    {
        //        if (Convert.ToInt32(dr["target_objcode"]) == targetobjcode && Convert.ToInt32(dr["target_objid"]) == targetobjid && Convert.ToInt32(dr["obj_code"]) == notyobjcode)
        //        {
        //            int init = 0;
        //            foreach (int user in listofnotytype)
        //            {
        //                if (Convert.ToInt32(dr["user_id"]) == user)
        //                {
        //                    init = 1;
        //                    break;
        //                }

        //            }
        //            if (init == 0)
        //            {
        //                listofnotytype.Add(Convert.ToInt32(dr["user_id"]));
        //            }

        //        }

        //    }
        //    return listofnotytype;
        //}

        //private static Dictionary<string, List<int>> GetNotifications(FamoPassport pass, DataTable dat, int targetobjcode, int targetobjid)
        //{
        //    Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
        //    Hashtable tabl=new Hashtable();
        //    foreach(DataRow dr in dat.Rows)
        //    {
        //        if (Convert.ToInt32(dr["target_objcode"]) == targetobjcode && Convert.ToInt32(dr["target_objid"]) == targetobjid)
        //        {
        //            int init = 0;


        //            foreach (string key in dictionary.Keys)
        //            {
        //                if (key == targetobjcode.ToString() + "-" + targetobjid.ToString() + "-" + Convert.ToInt32(dr["obj_code"]).ToString())
        //                {
        //                    init = 1;
        //                    break;
        //                }



        //            }
        //            if (init == 0)
        //            {
        //                dictionary.Add(targetobjcode.ToString() + "-" + targetobjid.ToString() + "-" + Convert.ToInt32(dr["obj_code"]).ToString(), NotificationMaker(targetobjcode, targetobjid, Convert.ToInt32(dr["obj_code"]), dat));
        //            }

        //        }

        //    }
        //    //key= targetobjcode-targetobjid-obj_code
        //    //value=list[Userid]
        //    return dictionary;
        //}


        //private static string LastUserAndDate(int targetobjcode,int targetobjid,int objid,DataTable dat)
        //{
        //    int lastuser = 0;
        //    string  lastdate = "";
        //    foreach (DataRow dr in dat.Rows)
        //    {


        //        if (Convert.ToInt32(dr["target_objcode"]) == targetobjcode && Convert.ToInt32(dr["target_objid"]) == targetobjid && Convert.ToInt32(dr["obj_code"]) == objid)
        //        {
        //            lastuser = Convert.ToInt32(dr["user_id"]);
        //            lastdate = dr["cud"].ToString();

        //        }
        //    }

        //    return lastuser+"@"+lastdate;
        //}

        //private static Dictionary<string, string>  PreperNotify(FamoPassport pass, DataTable dat)
        //{
        //    Hashtable hash = new Hashtable();
        //    Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
        //    //List<Hashtable> AllNotification = new List<Hashtable>();
        //    List<Dictionary<string, List<int>>> AllNotification = new List<Dictionary<string, List<int>>>();

        //    foreach (int objcode in GetMyUniqueTargetCode(dat))
        //    {
        //        dictionary.Add(objcode, GetMyUniqueTargetObjId(objcode, dat));
        //    }

        //    //foreach (int objcode in GetMyUniqueTargetCode(dat))
        //    //{
        //    //    hash.Add(objcode,GetMyUniqueTargetObjId(objcode,dat) );
        //    //}

        //   // List<int> targetids=hash.Keys.OfType<int>().ToList();
        //    foreach(int key in dictionary.Keys)
        //    {
        //    List<int> targetids = dictionary[key];//.OfType<int>().ToList();
        //    {
        //        foreach (int objid in targetids)
        //        {
        //            AllNotification.Add(GetNotifications(pass, dat, key, objid));
        //   //AllNoifications(pass,dat,key,objid));
        //        }

        //    }
        //    }
        //    List<string> Allnotiy = new List<string>();
        //    Dictionary<string, string> diction = new Dictionary<string, string>();

        //    foreach (Dictionary<string, List<int>> noti in AllNotification)
        //    {

        //        foreach (string key in noti.Keys)
        //        {
        //            //key= targetobjcode-targetobjid-obj_code
        //            //value=list[Userid]

        //            int targetobjcode =Convert.ToInt32(key.Split('-')[0]);
        //            int targetobjid = Convert.ToInt32(key.Split('-')[1]);
        //            int objcode = Convert.ToInt32(key.Split('-')[2]);
        //            string usersnames = "";
        //             string topictxt ="";
        //             string lastuserdate = LastUserAndDate(targetobjcode, targetobjid, objcode,dat);
        //            if (objcode == (int)FamoBlock.enmObjectCode.Comments || objcode == (int)FamoBlock.enmObjectCode.Votes)
        //            {
        //                int length = noti.Values.Count;
        //                FamTopic topc = new FamTopic(pass, targetobjid);
        //                 topictxt = topc.Description;
        //                 List<int> Users = noti[key];// Values.OfType<int>().ToList();
        //                 int mor2 = 0;
        //                if (Users.Count > 2)
        //                    mor2 = Users.Count - 2;
        //                int increase = 0;

        //                foreach (int user in Users)
        //                {
        //                    increase++;
        //                    FamUser usr = new FamUser(pass, user);
        //                    usersnames = usersnames + "," + usr.FirstName + " " + usr.LastName;
        //                    if (increase > 2) { usersnames = usersnames + " , " + mor2; break; }

        //                }
        //            }

        //            //Allnotiy
        //            if (objcode == (int)FamoBlock.enmObjectCode.Comments)
        //            {
        //                string toptxt = "";
        //                if (topictxt.Length > 50)
        //                    toptxt = topictxt.Substring(1, 50);
        //                else
        //                    toptxt = topictxt;
        //                usersnames = usersnames + " علق على مشاركتك " + toptxt;
        //            }

        //            if (objcode == (int)FamoBlock.enmObjectCode.Votes)
        //            {

        //                string toptxt = "";
        //                if (topictxt.Length > 50)
        //                    toptxt = topictxt.Substring(1, 50);
        //                else
        //                    toptxt = topictxt;
        //                usersnames = usersnames + " اعجب على مشاركتك " + toptxt;
        //            }

        //            Allnotiy.Add(usersnames);
        //            diction.Add(lastuserdate, usersnames);
        //        }


        //    }
        //   // return Allnotiy;
        //    return diction;

        //}
        //public static string BuildNotification(FamoPassport pass)
        //{

        //    /*

        //     <li>
        //                            <div class="alert_item">
        //                                <div class="alert_content">
        //                                    انا جايلك النهاردة استنانى ضرورى متنساش</div>
        //                                <div class="alert_time">
        //                                    الثلاثاء 25 نوفمبر</div>
        //                            </div>
        //                            <div class="alert_item_pic">
        //                                <img src="/newfiles/images/prof_pic.jpg" /></div>
        //                        </li> 


        //     */

        //    string Lis = "";//<li> <div class='alert_item'> <div class='alert_content'>";

        //    FamEvent even = new FamEvent(pass);

        //    DataTable dt = new DataTable();
        //    //dt = even.GetMyTargetNotification(pass.UserID);
        //    dt = even.GetEvetsByTargetUser(pass.UserID); 
        //    //List<string> not =PreperNotify(pass,dt);
        //    Dictionary<string, string> not = PreperNotify(pass, dt);
        //    foreach (string noty in not.Keys)
        //    {
        //        string filepath="/newfiles/images/prof_pic.jpg";
        //        Lis = Lis + "<li> <div class='alert_item'> <div class='alert_content'>" +/*GetPrettyDate(Convert.ToDateTime(*/not[noty]/*))*/ + " </div>";
        //        Lis = Lis + "<div class='alert_time'>   " + noty.Split('@')[1]+ "  </div> </div>";
        //        try {
        //            int userid = Convert.ToInt32(noty.Split('@')[0]);
        //            FamUser User= new FamUser(pass, userid);
        //            if(User.File.Path==null ||  User.File.Path==""){}
        //            else
        //                filepath = User.File.Path.Replace("\\", "/").Replace("~","");
        //            }
        //        catch (Exception ex)
        //            {

        //            }
        //        Lis = Lis + " <div class='alert_item_pic'> <img src='" + filepath + "' /></div> </li> ";

        //    }
        //    return Lis;

        //}

      
     
        #endregion

        #region LastNotifications

        public static int MaxEvent(DataTable dat)
        {
            int max = 0;
            foreach (DataRow dr in dat.Rows)
            {
                if (Convert.ToInt32(dr["event"]) > max)
                {
                    max = Convert.ToInt32(dr["event"]);

                }

            }

            return max;
        }
        public static string BuildHTMLNotification(int userid, string filepath, string description, string timers, int targetobjectid, int targetobjectcode, int objcode, int maxevent)
        {


            string Lis = "";

            string filepaths = "/newfiles/images/prof_pic.jpg";
            Lis = Lis + "<li class='maxevent' max='" + maxevent + "' ids='" + objcode + "-" + targetobjectcode + "-" + targetobjectid + "'> <div class='alert_item' > <div class='alert_content'>" + description + " </div>";
            Lis = Lis + "<div class='alert_time'>   " + timers + "  </div> </div>";
            try
            {

                if (filepath == null || filepath == "") { }
                else
                { filepaths = filepath; }
            }
            catch (Exception ex)
            {

            }
            Lis = Lis + " <div class='alert_item_pic'><a href='" + WebHosting + "person/" + userid + "/Arena" + "'> <img src='" + filepaths + "' /></a></div> </li> ";


            return Lis;

        }

        public static string Usersevent(FamoPassport pass, List<int> users)
        {
            List<int> UserInvers = new List<int>();
            for (int i = users.Count - 1; i >= 0; i--)
                UserInvers.Add(users[i]);
            string valueback = "";
            string fullname = "";
            if (users.Count > 0)
            {
                int count = 0;
                NopService.NopService serv = new NopService.NopService();
                DataTable dt = new DataTable();
                foreach (int us in UserInvers)
                {
                    if (count > 2)
                        break;
                    FamUser usersobj = new FamUser(pass, us);

                    try
                    {

                        dt = serv.GetCustomerData(serviceUser, servicePass, usersobj.CustomerId, true).Tables[0];
                        fullname = dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString();

                    }
                    catch (Exception ex) { }
                    if (fullname == "")
                        valueback = valueback + "<a href='/person/" + usersobj.ID + "/area' >" + usersobj.FullName + "</a>" + ",";
                    else
                        valueback = valueback + "<a href='/person/" + usersobj.ID + "/area' >" + fullname + "</a>" + ",";

                    count++;
                    fullname = "";
                }
            }

            return valueback;

        }

        public static string EventMaking(FamoPassport pass, DataTable dat)
        {
            List<string> ListOfNotyStr = new List<string>();
            string ReturnNotification = "";
            string commentontopictext = "علق على منشورك  ";
            string Likeontopictext = "  اعجب بمنشورك  ";
            string completedesc = "";
            if (dat == null) return "";
            foreach (DataRow dr in dat.Rows)
            {
                List<int> Usersids = new List<int>();
                DateTime lastdate = Convert.ToDateTime(dr["cud"]);
                FamObject.enmObjectCode objectcode = (FamObject.enmObjectCode)Convert.ToInt32(dr["obj_code"]);
                FamObject.enmObjectCode Targetobjectcode = (FamObject.enmObjectCode)Convert.ToInt32(dr["target_objcode"]);
                int targetobjid = Convert.ToInt32(dr["target_objid"]);
                int maxevent = Convert.ToInt32(dr["event"]);
                int lastUser = 0;
                foreach (string x in dr["users"].ToString().Split(','))
                {
                    if (x == null || x == "") { }
                    else
                    {
                        if (Convert.ToInt32(x) != pass.UserID)
                        {
                            Usersids.Add(Convert.ToInt32(x));
                            lastUser = Convert.ToInt32(x);
                        }
                    }
                }

                string description = "";
                completedesc = "";
                NopService.NopService serv = new NopService.NopService();
                DataTable dt = new DataTable();
                FamUser lastuserobject = new FamUser(pass, lastUser);
                if (Targetobjectcode == FamObject.enmObjectCode.Topics)
                {
                    if (targetobjid > 0)
                    {
                        FamTopic topc = new FamTopic(pass, targetobjid);

                        if (topc.Description.Length > 50)
                            description = topc.Description.Substring(1, 50);
                        else
                            description = topc.Description;

                        if (objectcode == FamoBlock.enmObjectCode.Comments)
                        {
                            completedesc = completedesc + Usersevent(pass, Usersids) + " " + commentontopictext + " " + description;

                        }

                        if (objectcode == FamoBlock.enmObjectCode.Votes)
                        {
                            completedesc = completedesc + Usersevent(pass, Usersids) + " " + Likeontopictext + " " + description;

                        }
                    }

                    string paths = getUserLogoPath(lastuserobject);
                    try
                    {
                        dt = serv.GetCustomerData(serviceUser, servicePass, lastuserobject.CustomerId, true).Tables[0];
                        paths = dt.Rows[0]["CustomerImage"].ToString();
                    }
                    catch (Exception ex) { }

                    /*                    try
                                        {
                                            paths = lastuserobject.File.Path.Replace("~", "");

                                        }
                                        catch (Exception ex) { paths = ""; }*/

                    string strget = BuildHTMLNotification(lastuserobject.ID, paths, completedesc, GetPrettyDate(lastdate), targetobjid, (int)Targetobjectcode, (int)objectcode, maxevent /*MaxEvent(dat)*/);
                    ListOfNotyStr.Add(strget);
                    ReturnNotification = ReturnNotification + strget;

                }








            }





            return ReturnNotification;
        }


        public static string FriendshipEventMaking(FamoPassport pass, DataTable dat)
        {
            string ReturnNotification = "";
            FamObject.enmObjectCode Targetobjectcode, objectcode;
            int targetobjid = 0;
            string fullname = "";
            string completedesc = "";
            int lastUser = 0, maxevent = 0;
            List<string> ListOfNotyStr;
            string path = "";
            DateTime lastdate = DateTime.Now;
            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();
            if (dat.Rows.Count > 0)
            {

                foreach (DataRow dr in dat.Rows)
                {
                    Targetobjectcode = (FamObject.enmObjectCode)Convert.ToInt32(dr["target_objcode"].ToString());
                    objectcode = (FamObject.enmObjectCode)Convert.ToInt32(dr["obj_code"].ToString());
                    FamUser lastuserobject = new FamUser(pass, Convert.ToInt32(dr["user_id"].ToString()));
                    try
                    {
                        lastdate = Convert.ToDateTime(dr["cud"].ToString());
                    }
                    catch (Exception exx) { }
                    maxevent = Convert.ToInt32(dr["evnt_id"].ToString());

                    completedesc = " Accept Freindship ";
                    FamUser fus = new FamUser(pass, lastUser);
                    try
                    {
                        dt = serv.GetCustomerData(serviceUser, servicePass, lastuserobject.CustomerId, true).Tables[0];
                        fullname = dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString();
                        path = dt.Rows[0]["CustomerImage"].ToString();
                    }
                    catch (Exception ex) { }

                    if (fullname == "") fullname = fus.FullName;


                    completedesc = completedesc + " \n" + "<a href='/person/" + fus.ID + "/setting'>" + fullname + "</a>";
                    string strget = BuildHTMLNotification(fus.ID, path, completedesc, GetPrettyDate(lastdate), targetobjid, (int)Targetobjectcode, (int)objectcode, maxevent /*MaxEvent(dat)*/);
                    //ListOfNotyStr.Add(strget);
                    ReturnNotification = ReturnNotification + strget;

                }
            }

            else
                return "";

            return ReturnNotification;
        }



        public static string BuildLastNotification(FamoPassport pass, out int count)
        {


            FamEvent even = new FamEvent(pass);

            DataTable dt = new DataTable();

            dt = even.GetLastEvents(pass.UserID);
            int cou = 0;
            foreach (DataRow dr in dt.Rows)
            {
                //Convert.ToInt32(dr["obj_code"]) == (int)FamoBlock.enmObjectCode.Links ||
                if (Convert.ToInt32(dr["obj_code"]) == (int)FamoBlock.enmObjectCode.Comments || Convert.ToInt32(dr["obj_code"]) == (int)FamoBlock.enmObjectCode.Votes)
                    cou++;


            }

            count = cou;
            string output = "";
            DataTable dtfr = new DataTable();

            dtfr = even.GetAcceptFriendShipEvents(pass.UserID);
            if (dtfr != null)
            {
                output = FriendshipEventMaking(pass, dtfr);
                count += dtfr.Rows.Count;
            }
            output += EventMaking(pass, dt);


            return output;



        }

        public static string BuildNewestNotification(FamoPassport pass, int lastid)
        {


            FamEvent even = new FamEvent(pass);

            DataTable dt = new DataTable();

            dt = even.NewestEvent(pass.UserID, lastid);



            return EventMaking(pass, dt); ;

        }


        public static string BuildOldestNotification(FamoPassport pass, int lastid)
        {


            FamEvent even = new FamEvent(pass);

            DataTable dt = new DataTable();

            dt = even.OldestEvent(pass.UserID, lastid);



            return EventMaking(pass, dt); ;

        }


        public static int NotReadingEvents(FamoPassport pass, int id)
        {
            FamEvent even = new FamEvent(pass);

            DataTable dt = new DataTable();

            dt = even.NotReadingEvents(pass.UserID, id);

            try
            {
                if (dt.Rows.Count > 0)
                    return dt.Rows.Count;
            }
            catch (Exception ex) { }
            return 0;
        }
        #endregion

    }
}
