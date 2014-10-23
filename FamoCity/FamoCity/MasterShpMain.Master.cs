using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using System.Web.Services;

namespace FamoCity
{
    public partial class MasterShpMain : System.Web.UI.MasterPage
    {
        FamoPassport pass;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                pass = (FamoPassport)Session["passport"];
                if (pass.Logged)
                {
                    showUserData();
                    if (pass.UserType == FamoBlock.enmUserType.Shop)
                    {
                        showShopMenu();
                    }
                    else if (pass.UserType == FamoBlock.enmUserType.User)
                    {
                        showUserMenu();
                    }
                    //check user type here and call suitable function
                }
                else
                {
                    displayLoginDiv();
                    //guest
                }

            }

            if (pass.UserType == FamoBlock.enmUserType.User)
            {
                lnkMain.NavigateUrl = "usrmain.aspx";
                if (Page.RouteData.Values["shopname"] != null)
                    lnkProducts.Visible = false;
            }
            
            if (Page.RouteData.Values["shopname"] != null)
            {

                lnkMain.NavigateUrl = "/shop/" + Page.RouteData.Values["shopname"].ToString();
                lnkProducts.NavigateUrl = "/Shop/" + Page.RouteData.Values["shopname"].ToString() + "/products";
            }
            else
            {
                try
                {
                    if (pass.ShopID > 0)
                    {
                        FamShop sh = new FamShop(pass, pass.ShopID);
                        lnkMain.NavigateUrl = "/shop/" + sh.WebName;
                        lnkProducts.NavigateUrl = "/Shop/" + sh.WebName + "/products";
                    }
                }
                catch (Exception ex) { }
            }

        }

        protected void displayLoginDiv()
        {
            string loginDiv = "";
            //div building no asp controlls
            loginDiv += "<div id='user_login'><form action='#' method='post' class='user_login'>" +
                       "<div class='login'><ul><li class=''>" +
                       "<input type='button' name='submit' value='دخول' class='orange-btn bg user_submit' onclick='redirectToLogin();' /></li>" +
                        "<li class='password'> <input type='password' name='password' value='' id='password' /></li>" +
                        "<li class='lock'><div class='lock_border'><img src='/images/lock.png' /></div></li>" +
                        "<li class='username'><input type='text' name='username' id='username' value='' /></li>" +
                        "</ul></div></form><div id='forget'><ul class='forget'><li><a href='/main'>تسجيل عضوية جديدة ؟</a></li>" +
                        "<span><li class='forgetli'><a href='/forgetpassword'>هل نسيت كلمة المرور ؟</a> </li></span></ul></div></div>";
            ltrUserInfo.Text = loginDiv;

        }
        protected void showUserData()
        {
            //needed data
            FamUser user = new FamUser(pass, pass.UserID);
            string userDataDiv = "";
            
            //user status
            FamUserStatus s = new FamUserStatus(pass);
            DataTable sdt = s.GetAllUserStatus(pass.UserID);
            string statusS = "";
            if (sdt.Rows.Count > 0)
            {
                int statusID = Convert.ToInt32(sdt.Rows[0]["stat_id"]);
                FamUserStatus status = new FamUserStatus(pass, statusID);
                statusS = status.Status;
            }

            //userImage
            FamFile f = new FamFile(pass);
            DataTable dt = f.GetFiles(FamoBlock.enmObjectCode.Users, pass.UserID);
            string imgPath;
            if (dt.Rows.Count > 0)
            {
                int fileID = Convert.ToInt32(dt.Rows[0]["file_id"]);
                FamFile file = new FamFile(pass, fileID);
                imgPath = file.Path.Substring(1);
            }
            else
            {
                imgPath = "/images/user_pic.png";
            }


            string userpage = "";
            string photo = "";
            if (pass.Logged && pass.UserType == FamoBlock.enmUserType.User)
            {
                userpage = "/usrmain.aspx";

                FamUser u = new FamUser(pass, pass.UserID);
                photo = ClassMain.getDefaultPhoto(u);
            }
            else
                userpage = "javascript:void(0);";
            //div Composition
            userDataDiv += "<div id='user_data'><div class='user_nickname'>";
            userDataDiv += user.UserName;
            userDataDiv += "</div><div class='user_picture'>";
            userDataDiv += "<a href='" + userpage + "'><img class='user_picture2' src='" + 0 + "' alt='' title='' /></a>";
            userDataDiv += "</div><div class='user_status'><div class='status'>";//<div class='change_picture'><a href='#' class='orange-btn bg'>غير صورتك</a></div>
            userDataDiv += statusS;
            userDataDiv += "</div></div></div>";
            ltrUserInfo.Text = userDataDiv;
        }

        protected void showUserMenu()
        {
            //القائمة الرئيسية للمستخدم العادي
            ltrLinkList.Text = "<div class='gridContainer clearfix'><ul id='user_info_list'>" +
               " <li><a href='#'>لوحة التحكم <span><img src='/images/user_info_icon.png' alt='' title='' />" +
                "</span></a></li><li><a href='#'>الخصوصية <span><img src='/images/user_info_icon.png' alt='' title='' />" +
                "</span></a></li>" +
                "<li><a href='/logout'>تسجيل الخروج <span><img src='/images/user_info_icon.png' alt='' title='' /></span></a></li>" + 
                "</ul></div>";
            ltrBarWrapper.Text = "<div id='bar_wrapper'><div class='gridContainer clearfix'>" +
"<div class='slide'><a href='#' class='btn-slide'>بياناتك الشخصية</a></div></div></div>";
        }

        protected void showShopMenu()
        {
            //القائمة الرئيسية للمتجر
            ltrLinkList.Text = "<div class='gridContainer clearfix'><ul id='user_info_list'>" +
               " <li><a href='/Shop/edit/main'>لوحة التحكم <span><img src='/images/user_info_icon.png' alt='' title='' /></span></a></li>" +
                "<li><a href='/logout'>تسجيل الخروج <span><img src='/images/user_info_icon.png' alt='' title='' /></span></a></li>" +
                "</ul></div>";
            ltrBarWrapper.Text = "<div id='bar_wrapper'><div class='gridContainer clearfix'>" +
            "<div class='slide'><a href='#' class='btn-slide'>بياناتك الشخصية</a></div></div></div>";
        }


    }
}