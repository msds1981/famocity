using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using FamoLibrary;

namespace FamoCity
{
    public partial class shopMap : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/Map";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/Map";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Shop)
                Response.Redirect("/login");

        }

        protected void btnAddImage_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static bool hasPosition()
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            string position;
            FamOption op = new FamOption(pass);
            position = op.GetValue(FamoBlock.Const_Option_Shp_Map + pass.ShopID);
            if (position == "")
                return false;
            else
                return true;
        }

        [WebMethod]
        public static bool saveLocation(double lat, double lng)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamOption op = new FamOption(pass);
            op.SetValue(FamoBlock.Const_Option_Shp_Map+pass.ShopID, lat + "-" + lng);
            return true;
        }

        [WebMethod]
        public static double getLocationLat()
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            string position;
            FamOption op = new FamOption(pass);
            position = op.GetValue(FamoBlock.Const_Option_Shp_Map + pass.ShopID);
            double lat;
            String[] pos = position.Split('-');
            lat = Convert.ToDouble(pos[0].ToString());
            return lat;
        }

        [WebMethod]
        public static double getLocationLng()
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            string position;
            FamOption op = new FamOption(pass);
            position = op.GetValue(FamoBlock.Const_Option_Shp_Map + pass.ShopID);
            double lng;
            String[] pos = position.Split('-');
            lng = Convert.ToDouble(pos[1].ToString());
            return lng;
        }
        
    }
}