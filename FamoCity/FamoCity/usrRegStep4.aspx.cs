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
    public partial class usrRegStep4 : System.Web.UI.Page
    {
        int id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
               if (!Passport.Logged || Passport.UserType != FamoBlock.enmUserType.User) Response.Redirect("/login");
            }
            else
            {
                Response.Redirect("/login");
            }
            id = Passport.UserID;

            //if (!IsPostBack)
            //{
            //try
            //{
                ltrShops.Text = BulidDivs();
            //}
            //catch (Exception ex) { }
            //}

        }

        private string getFollowStatus(int shopID)
        {
            String status = "";
            FamFollow f = new FamFollow(Passport);
            int followID = 0;
            DataTable dt1 = f.GetShopFollow(Passport.UserID, shopID);
            foreach (DataRow dr1 in dt1.Rows)
            {
                followID = Convert.ToInt32(dr1["folw_id"].ToString());
            }

            FamFollow f1 = new FamFollow(Passport, followID);

            if (f1.Status == FamoBlock.enmFollowStatus.Follow)
            {
                status = "class='css_btn_class-no' id='f" + shopID + "'>الغاء</a>";
            }
            else
            {
                status = "class='css_btn_class-add' id='f" + shopID + "'>متابعة</a>";
            }

            return status;
        }
        private string BulidDivs()
        {
            FamActivity act = new FamActivity(Passport);
            DataTable dt = act.GetActivitiesByCol("name");
            if (dt != null) { if (dt.Rows.Count <= 0) return ""; } else return "";
            string str = " <div class='interests-box'>";
            foreach (DataRow dr in dt.Rows)
            {
                int act_id = Convert.ToInt32(dr["actv_id"].ToString());
                act = new FamActivity(Passport, act_id);
                str += "<div class='title-line-small'>" + act.Name + "</div>";
                str += "<div class='box-ul'>";
                FamShopInterest shI = new FamShopInterest(Passport);
                DataTable dt1 = shI.GetInterestsByActivities(act_id);
                foreach (DataRow dr1 in dt1.Rows)
                {
                    string nstr = "";
                    try
                    {
                        int shop_id = Convert.ToInt32(dr1["shop_id"].ToString());
                        FamShop shop = new FamShop(Passport, shop_id);
                        string followStatus = getFollowStatus(shop_id);

                        nstr += " <div class='box-li'>";

                        nstr += "<img src='" + shop.File.Path.Replace("~", "") + "' />";

                        nstr += "<span>" + shop.Name + "</span>";
                        nstr += "<div class='hide-btn'>";
                        nstr += "<a href='javascript:changeFollow(" + shop_id + ");'" + followStatus;

                        //closing hide - btn 
                        nstr += "</div>";
                        //   closing box-li box-hover
                        nstr += "</div>";
                    }
                    catch (Exception ex) { nstr = ""; }
                    str += nstr;
                }

                //closing box-ul
                str += "</div>";
            }
            //closing interestbox
            str += "</div>";
            return str;
        }

        protected void CustomValidatorFollowCount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int count=0;
            FamFollow f = new FamFollow(Passport);
            DataTable dt = f.GetUserFollow(Passport.UserID);

            foreach(DataRow dr1 in dt.Rows)
            {
                if(Convert.ToInt32(dr1["status"].ToString()) == 1)
                {
                    count++;
                }
            }

            //count = dt.Rows.Count;
            if (count >= 3)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        private void SetStep(string pageStep)
        {
            FamOption op = new FamOption(Passport);
            op.SetValue(FamoBlock.Const_Option_Usr_StepsRegister + Passport.UserID.ToString(), pageStep);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Page.Validate();
            //CustomValidatorFollowCount.Validate();
            SetStep("/user/profile");
            Response.Redirect("/user/profile");
        }

        [WebMethod]
        public static bool unFollow(int shopID)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            FamFollow f = new FamFollow(pass);
            int followID = 0;
            DataTable dt1 = f.GetShopFollow(pass.UserID, shopID);
            foreach (DataRow dr1 in dt1.Rows)
            {
                followID = Convert.ToInt32(dr1["folw_id"].ToString());
            }

            FamFollow f1 = new FamFollow(pass, followID);
            f1.Status = FamoBlock.enmFollowStatus.Cancle;
            return f1.Update();
        }

        [WebMethod]
        public static bool follow(int shopID)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            FamFollow f = new FamFollow(pass);
            int followID = 0;
            DataTable dt1 = f.GetShopFollow(pass.UserID, shopID);
            foreach (DataRow dr1 in dt1.Rows)
            {
                followID = Convert.ToInt32(dr1["folw_id"].ToString());
            }

            if (followID > 0)
            {
                FamFollow f1 = new FamFollow(pass, followID);
                f1.Status = FamoBlock.enmFollowStatus.Follow;
                return f1.Update();
            }
            else
            {
                FamFollow f1 = new FamFollow(pass);
                f1.Status = FamoBlock.enmFollowStatus.Follow;
                f1.User.ID = pass.UserID;
                f1.Shop.ID = shopID;
                f1.Save();

                return true;
            }
        }


    }
}