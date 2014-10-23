using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using System.IO;
using System.Text;
namespace FamoCity
{
    public partial class Message : System.Web.UI.Page
    {

        FamoPassport Passport;
        public int userid = 0;
        int pageid = 0;
        int actvid = 0;
        int objcode = 0;
        int tabid = 0;
        int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/usrMain.aspx";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/usrMain.aspx";
                Response.Redirect("/login");
            }

            //main menu
            string FullName = Passport.FullName;

            NopService.NopService serv = new NopService.NopService();
            DataTable dt = new DataTable();

            string uphoto = ClassMain.getUserLogoPath(Passport, Passport.UserID);
            try
            {
                dt = serv.GetCustomerData(ClassMain.serviceUser, ClassMain.servicePass, Passport.Customer, true).Tables[0];
                uphoto = dt.Rows[0]["CustomerImage"].ToString();
                FullName = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString(); ;
            }
            catch (Exception ex) { }

            ltrFullName.Text = FullName;
            ltrImageSmall.Text = "<img src='" + uphoto + "' />";
            fillParameters();
            //hdnPgNo.Value = pageid.ToString();

            //redirect to setting page in Famo commerce site
            string red = ClassMain.GetEncryption(Passport.Email, new FamUser(Passport, Passport.UserID).Password, "http://www.famocity.com/shop/customer/info");
            //lnkSettingPage.NavigateUrl = "http://www.famocity.com/shop/login/route?em=" + red;
            //lnkProfilePage.NavigateUrl = "http://www.famocity.com/shop/login/route?em=" + red;


            string logo = "";
            logo = "<img src='/newfiles/images/logo.png' alt='Logo' title='FamoCity' />";
            if (Passport != null)
            {
                famologo.Text = "<a href='" + ClassMain.WebHosting + "person/" + Passport.UserID + "/Arena" + "'>" + logo + "</a>";
            }
            else
                famologo.Text = logo;

            //الصورة الشخصية لقائمة التابليت
            //ltrTabUserPhoto.Text = ClassMain.BuildTabletUserPhotoMenu(uphoto, FullName);
            //القائمة الرئيسية للتابليت
            //ltrTabMainMenu.Text = ClassMain.BuildTabletMainMenu(Passport);

            if (!IsPostBack)
            {
                BindReqFriends();
                //lblCount.Text = Methods.GetCountOfRequest(Passport.UserID).ToString();
                FamOption fop = new FamOption(Passport);
                string value = fop.GetValue(FamoBlock.Const_Option_Usr_NotifFriend + Passport.UserID);
                if (value == "")
                    value = "0";
                if (value != "0")
                {
                    lblCount.Text = value;// counts.ToString();
                    lblCount.Attributes.Add("class", "notired");
                }

                //load content of page
                //ltrPageContent.Text = ClassMain.BuildUserPageWithoutContent(Passport, (FamoBlock.enmObjectCode)objcode, userid, (ClassMain.PageName)pageid, tabid);

                //load page content based on page number (topics,photos,...etc)
                FireFunctionScript();
                Notification();
            }

            //if (Passport != null)
            //    hdnUsrNo.Value = Passport.UserID.ToString();
            if (userid == Passport.UserID)
                LinkuserId.Value = "0";
            else
                LinkuserId.Value = userid.ToString();

            myid.Value = Passport.UserID.ToString();
        }


        private void Notification()
        {
            int counts = 0;
            Notificationlit.Text = ClassMain.BuildLastNotification(Passport, out counts);
            FamOption fop = new FamOption(Passport);
            string value = fop.GetValue(FamoBlock.Const_Option_Usr_NotifEvent + Passport.UserID);
            if (value == "")
                value = "0";
            if (value != "0")
            {
                notiredEvent110.InnerText = value;// counts.ToString();
                notiredEvent110.Attributes.Add("class", "notired");


            }

        }

       

        private void LoadMessageNotification()
        {

            FamMessage message = new FamMessage(Passport);
            DataTable dt = new DataTable();
            dt = message.GetLastPersonsMessages(Passport.UserID);
            MessageList.Text = ClassMain.BuildMessageLiItem(Passport, 0, "new");

            FamOption fop = new FamOption(Passport);
            string value = fop.GetValue(FamoBlock.Const_Option_Usr_NotifEvent + Passport.UserID);
            if (value == "")
                value = "0";
            if (value != "0")
            {
                msgcountLable.Attributes.Add("class", "notired");
                msgcountLable.Text = value;
            }
            // counts.ToString();
            //notiredEvent110.Attributes.Add("class", "notired");



            //  msgcountLable.Text=
        }
        private void FireFunctionScript()
        {
            ClassMain.PageName pg = (ClassMain.PageName)pageid;
            string script = "";
            if (userid == Passport.UserID)
                script = "PreparePage(" + Passport.UserID + "," + (int)pg + ")";
            else
                script = "PreparePage(" + userid + "," + (int)pg + ")";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", script, true);

        }

        private void fillParameters()
        {
            try
            {
                if (Page.RouteData.Values["userid"] != null)
                {
                    string uprm = Page.RouteData.Values["userid"].ToString();
                    if (int.TryParse(uprm, out userid))
                    {
                        userid = Convert.ToInt32(uprm);
                    }
                    else
                    {
                        FamUser u = new FamUser(Passport);
                        userid = u.GetUserIdByName(uprm);
                        if (userid == 0) userid = Passport.UserID;
                    }
                }
                else if (Request["uid"] != null)
                    userid = Convert.ToInt32(Request["uid"]);
                else
                    userid = Passport.UserID;
            }
            catch (Exception ex) { userid = Passport.UserID; }

            //page name
            try
            {
                if (Page.RouteData.Values["page"] != null)
                {
                    string pprm = Page.RouteData.Values["page"].ToString();
                    if (int.TryParse(pprm, out pageid))
                    {
                        pageid = Convert.ToInt32(pprm);
                    }
                    else
                    {
                        pageid = ConvertToPageName(pprm);
                    }
                }
                else if (Request["pg"] != null)
                    pageid = Convert.ToInt32(Request["pg"]);
                else
                    pageid = (int)ClassMain.PageName.Arena;
            }
            catch (Exception ex) { pageid = (int)ClassMain.PageName.Arena; }

            //objectcode or type of user
            if (Request["ty"] != null)
                objcode = Convert.ToInt32(Request["ty"]);
            else
                objcode = (int)FamoBlock.enmObjectCode.Users;

            //actvity id
            if (Request["actv"] != null)
                actvid = Convert.ToInt32(Request["actv"]);
            else
                actvid = 0;


            //tab id
            if (Page.RouteData.Values["tab"] != null)
            {
                string pprm = Page.RouteData.Values["tab"].ToString();
                if (int.TryParse(pprm, out tabid))
                {
                    tabid = Convert.ToInt32(pprm);
                }
            }
            if (tabid == 0)
            {
                if (Request["tab"] != null)
                    tabid = Convert.ToInt32(Request["tab"]);
                else
                    tabid = 1;
            }
        }

        private int ConvertToPageName(string page)
        {
            ClassMain.PageName pgid = 0;
            if (page.ToLower() == "arena")
                pgid = ClassMain.PageName.Arena;
            else if (page.ToLower() == "photos")
                pgid = ClassMain.PageName.Photos;
            else if (page.ToLower() == "friends")
                pgid = ClassMain.PageName.Friends;
            else if (page.ToLower() == "shops")
                pgid = ClassMain.PageName.Shops;
            else if (page.ToLower() == "setting")
                pgid = ClassMain.PageName.Setting;


            return (int)pgid;
        }
        private void BindReqFriends()
        {
            FamLink fl = new FamLink(Passport);
            DataTable dt = fl.GetListLastStatuByUser(Passport.UserID, FamoBlock.enmLinkeStatus.Request);
            if (dt != null) { if (dt.Rows.Count <= 0) return; } else return;
            NopService.NopService serv = new NopService.NopService();
            DataTable dtb = new DataTable();
            dt.Columns.Add("Link");
            dt.Columns.Add("MoreFriends");
            //الغاء الشرطه ~ من الروابط
            foreach (DataRow dr in dt.Rows)
            {
                string x = dr["user_file"].ToString();

                FamUser usr = new FamUser(Passport, Convert.ToInt32(dr["user_id"]));

                if (x != null || x != "")
                {
                    x = x.Replace("~/", "");

                    if (!File.Exists(Server.MapPath("~") + x.Replace("/", "\\")))

                        if (usr.Gender == FamoBlock.enmGender.Male)
                        {
                            dr["user_file"] = ClassMain.DefaultMalePhoto;// "/newfiles/images/prof_pic.jpg";
                        }
                        else
                            dr["user_file"] = ClassMain.DefaultFemalePhoto;// "/newfiles/images/prof_pic.jpg";

                    else
                        dr["user_file"] = "/" + x;
                }
                else

                    if (usr.Gender == FamoBlock.enmGender.Male)
                    {
                        dr["user_file"] = ClassMain.DefaultMalePhoto;// "/newfiles/images/prof_pic.jpg";
                    }
                    else
                        dr["user_file"] = ClassMain.DefaultFemalePhoto;// "/newfiles/images/prof_pic.jpg";


                try
                {

                    FamUser requser = new FamUser(Passport, Convert.ToInt32(dr["user_id"].ToString()));
                    dtb = serv.GetCustomerData(ClassMain.serviceUser, ClassMain.servicePass, requser.CustomerId, true).Tables[0];

                    x = dtb.Rows[0]["CustomerImage"].ToString();
                    if (x != null && x != "")
                        dr["user_file"] = x;
                    if (dtb.Rows[0]["firstname"].ToString() != null && dtb.Rows[0]["firstname"].ToString() != "")
                        dr["UserName"] = dtb.Rows[0]["firstname"].ToString() + " " + dtb.Rows[0]["lastname"].ToString();
                    dr["link"] = ClassMain.WebHosting + "person/" + requser.ID + "/Arena";
                    //  dr["MoreFriends"] = ClassMain.WebHosting + "person/" + Passport.UserID + "/Friends";
                }
                catch (Exception ex) { }

            }
            //التأكد من وجود الملف اذا لم يوجد يتم وضع صوره افتراضيه
            //
            MoreFriends.Text = "<a href='" + ClassMain.WebHosting + "person/" + Passport.UserID + "/Friends" + "' >مشاهدة باقى الطلبات  </a>";
            RreqFriends.DataSource = dt;
            RreqFriends.DataBind();
        }
    }
}