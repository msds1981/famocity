using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mUtilities;
using FamoLibrary;
using System.Data;

namespace FamoCity
{
    public partial class shopVideo : System.Web.UI.Page
    {
        int id=0;
        string key = "";
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/Video";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/Video";
                Response.Redirect("/login");
            }

            FamShop s = new FamShop(Passport, Passport.UserID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/login");
            else
            {
                DataTable dt = s.GetUserShops(Passport.UserID);
                if (dt.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dt.Rows[0]["shop_id"]);
                    key = FamoBase.Const_Option_Shp_VideoIntro + id.ToString();
                }
            }

            ltrVideo.Text = ClassMain.GetVideoYoutubeEmbed(key, 540, 260);
        }
    
        protected void btnSaveKey_Click(object sender, EventArgs e)
        {
            FamOption op = new FamOption(Passport);
            op.SetValue(key, txtLink.Text);
        }
    }
}