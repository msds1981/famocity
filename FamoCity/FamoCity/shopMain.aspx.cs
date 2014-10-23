using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Web.Services;
using mUtilities;
using System.Data;

namespace FamoCity
{
    public partial class shopMain : System.Web.UI.Page
    {
        FamoPassport pass;
        public static int shopID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null)
            {
                pass = new FamoPassport(ClassMain.ConnectionString);
                Session["passport"] = pass;
            }
            else
                pass = (FamoPassport)Session["passport"];
            
            try
            {
                FamOption o = new FamOption(pass);
                o.GetValue(FamoBlock.Const_Option_Shp_VideoIntro + "");
            }
            catch (Exception ex)
            {
                Response.Redirect("/login");
            }

            FamShop shp = ClassMain.GetShop(Page.RouteData.Values["shopname"].ToString());
            if (shp.ID > 0)
                shopID = shp.ID;
            else
                Response.Redirect("/Main");

            if (shopID == 0) Response.Redirect("/Main");

            ClassMain.RecordNewVisit(pass, shopID);
            //string shID = Request.QueryString["p"].ToString();
            //shopID = getShopID(shID);
            FillCounters();
               
            showShopData(shopID);
            showFollowers(shopID);
            showVid(shopID);
            followButton(shopID);
            setBanner(shopID);

        }

        protected void FillCounters() {

            FamProduct fproduct = new FamProduct(pass);
            FamFollow ffollow = new FamFollow(pass);
            FamShopVisits fsv = new FamShopVisits(pass);
            ltCountProduct.Text = fproduct.GetCountProducts(shopID).ToString();
            ltFollow.Text = ffollow.GetCountFollowers(shopID).ToString();
            ltNumVist.Text = fsv.GetCountShopVisits((int)FamoBlock.enmObjectCode.Shops, shopID).ToString();
            
        }
        protected int getShopID(string encryptydString)
        {
            try
            {
                return Convert.ToInt32(ClassMain.DecryptParameter(encryptydString.Replace(" ", "+")));
            }
            catch (Exception ex) { }
            return 0;
        }

        protected void showShopData(int shopID)
        {
            FamShop sh = new FamShop(pass, shopID);
            lblAbout.Text = sh.About;
            int objCode = (int)FamoBlock.enmObjectCode.Shops;

            //adress
            FamContactProfile c = new FamContactProfile(pass);
            DataTable dt = new DataTable();
            dt = c.GetContacts(objCode, shopID);
            int contactID = 0;
            if (dt != null)
            {
                foreach (DataRow dr1 in dt.Rows)
                {
                    contactID = Convert.ToInt32(dr1["contact_id"]);
                }
                FamContactProfile con = new FamContactProfile(pass, contactID);
                lblAddress.Text = con.Address;
            }

            //phoneNum
            FamPhoneProfile ph = new FamPhoneProfile(pass);
            int phoneID = 0;
            DataTable dt1 = new DataTable();
            dt1 = ph.GetPhoneProfiles(objCode, shopID, FamoBlock.enmPhoneType.Mobile);
            if (dt1 != null)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    phoneID = Convert.ToInt32(dr1["phon_id"]);
                }
                FamPhoneProfile pho = new FamPhoneProfile(pass, phoneID);
                lblPhone.Text = pho.Number;
            }

            //fax
            FamPhoneProfile f = new FamPhoneProfile(pass);
            int faxID = 0;
            DataTable dt2 = new DataTable();
            dt2 = ph.GetPhoneProfiles(objCode, shopID, FamoBlock.enmPhoneType.Fax);
            if (dt2 != null)
            {
                foreach (DataRow dr1 in dt2.Rows)
                {
                    faxID = Convert.ToInt32(dr1["phon_id"]);
                }
                FamPhoneProfile fa = new FamPhoneProfile(pass, faxID);
                lblFax.Text = fa.Number;
            }

        }
        protected void showFollowers(int shopID)
        {
            FamFollow f = new FamFollow(pass);
            int count = f.GetCountFollowers(shopID);
            string onclick = " onclick='FlollowShop()'";
            //if (!pass.Logged && )
            string text = "انضم الى ";
            string text2 = " متابع";
            ltrFollow.Text = "<a href='javascript:void(0);' " + onclick + ">" +
                             "<span> " + text + " (" + count + ")" + text2 + "</span>" +
                             "</a>";

        }
        protected void showVid(int shopID)
        {
            /*
            string url;
            FamOption o = new FamOption(pass);
            url = o.GetValue(FamoBlock.Const_Option_Shp_VideoIntro + shopID + "");
            vid.Attributes["src"] = url;*/
            ltrVideo.Text = ClassMain.GetVideoYoutubeEmbed(FamoBlock.Const_Option_Shp_VideoIntro + shopID.ToString(), 286, 200);
        }
        protected void setBanner(int shopID)
        {

            String html = "";
            FamBanar b = new FamBanar(pass);
            DataTable dt = b.GetShopBanars(shopID);
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    String imgPath;
                    int fileID = Convert.ToInt32(dr["file_id"].ToString());
                    FamFile f = new FamFile(pass, fileID);
                    imgPath = f.Path.Substring(1);
                    //html += "<li> <img src='/thumb.aspx?image=" + imgPath + "&size=400'";
                    html += "<li> <img src='" + imgPath + "'";
                    html += " width='800' height='250' /></li>";
                }
                catch (Exception ex) { }
            }
            ltrBaner.Text = html;
        }

        protected void followButton(int shopID)
        {
            int userID = pass.UserID;
            if (userID == 0)//guest
            {
                btnFollow.Visible = true;
            }
            else // user
            {
                if (pass.ShopID == shopID)
                {
                    btnFollow.Visible = false;
                }
                else
                {
                    FamFollow f = new FamFollow(pass);
                    int followID = 0;
                    DataTable dt1 = f.GetShopFollow(pass.UserID, shopID);

                    if (dt1.Rows.Count == 0)// didnt follow before
                    {
                        btnFollow.Text = "متابعة";
                    }
                    else
                    {

                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            followID = Convert.ToInt32(dr1["folw_id"].ToString());
                        }

                        FamFollow f1 = new FamFollow(pass, followID);

                        if (f1.Status == FamoBlock.enmFollowStatus.Follow)
                        {
                            btnFollow.Text = "إلغاء";
                        }
                        else
                        {
                            btnFollow.Text = "متابعة";
                        }
                    }
                }
            }
       
        }

        [WebMethod]
        public static double getLocationLat()
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            string position;
            FamOption op = new FamOption(pass);
            position = op.GetValue(FamoBlock.Const_Option_Shp_Map + shopID);
            double lat = 0;
            String[] pos = position.Split('-');
            try
            {
                lat = Convert.ToDouble(pos[0].ToString());
            }
            catch (Exception ex) { }
            return lat;
        }

        [WebMethod]
        public static double getLocationLng()
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            string position;
            FamOption op = new FamOption(pass);
            position = op.GetValue(FamoBlock.Const_Option_Shp_Map + shopID);
            double lng = 0;
            String[] pos = position.Split('-');
            try
            {
                lng = Convert.ToDouble(pos[1].ToString());
            }
            catch (Exception ex) { }
            return lng;
        }

        protected void btnFollow_Click(object sender, EventArgs e)
        {
            int userID = pass.UserID;

            if (userID == 0)//guest
            {
                Response.Redirect("login.aspx");
            }
            else // user
            {
                FamFollow f = new FamFollow(pass);
                int followID = 0;
                DataTable dt1 = f.GetShopFollow(pass.UserID, shopID);

                if (dt1.Rows.Count == 0)// didnt follow before
                {
                    FamFollow fol = new FamFollow(pass);
                    fol.Status = FamoBlock.enmFollowStatus.Follow;
                    fol.User.ID = userID;
                    fol.Shop.ID = shopID;
                    fol.Save();
                }
                else
                {

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        followID = Convert.ToInt32(dr1["folw_id"].ToString());
                    }

                    FamFollow f1 = new FamFollow(pass, followID);

                    if (f1.Status == FamoBlock.enmFollowStatus.Follow)
                    {
                        f1.Status = FamoBlock.enmFollowStatus.Cancle;
                        f1.Save();
                    }
                    else
                    {
                        f1.Status = FamoBlock.enmFollowStatus.Follow;
                        f1.Save();
                    }
                }
            }
          }
    }
}