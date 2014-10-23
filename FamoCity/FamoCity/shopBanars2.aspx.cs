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
    public partial class shopBanars2 : System.Web.UI.Page
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
                    Session["target"] = "Shop/edit/banars";
                    Response.Redirect("/Login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/banars";
                Response.Redirect("/Login");
            }
            FamShop s = new FamShop(Passport, Passport.ShopID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/Login");

            if (!IsPostBack)
            {
                showimages();
            }

        }

        private void showimages() {
            //عرض صور المنتج هنا
            ltrImgs.Text = " <div id='gallery_box'><ul id='ulp'>";
            FamBanar b = new FamBanar(Passport);
            
            foreach (DataRow dr in b.GetShopBanars(Passport.ShopID).Rows)
            {
                int banarid = Convert.ToInt32(dr["banar_id"].ToString());
                string href = "javascript:deleteImg(" + banarid + "); return false;";
                ltrImgs.Text += "<li id='f" + banarid + "' onclick='deleteImg(" + banarid + ")'><a href='#'>";
                ltrImgs.Text += "<img src='" + dr["file_path"].ToString().Replace("~", "") + "'/>";
                ltrImgs.Text += "</a></li>";
            }
            ltrImgs.Text += "</ul></div>";
        }

        protected void addIntoCompany_Click1(object sender, EventArgs e)
        {
            int[] f = ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Banars, 0);
            for (int i = 0; i < f.Length; i++) { 
                FamBanar fb = new FamBanar(Passport);
                fb.Shop.ID = Passport.ShopID;
                fb.File.ID = f[i];
                int id = fb.Save();
                fb.File.ObjectId = id;
                fb.File.Update();
            }
        }
    }
}