using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using System.IO;

namespace FamoCity
{
    public partial class shopLogo : System.Web.UI.Page
    {
        int id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/Logo";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/Logo";
                Response.Redirect("/login");
            }

            FamShop s = new FamShop(Passport, Passport.ShopID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/login");
            else
            {
                DataTable dt = s.GetUserShops(Passport.UserID);
                if (dt.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dt.Rows[0]["shop_id"]);
                }
            }
            if (!IsPostBack)
                showlogo();
        }
        private void showlogo()
        {

            FamShop shp = new FamShop(Passport, id);
            if (shp.File.ID > 0)
            {
                ImgLogo.ImageUrl = shp.File.Path;
                hdfSaveImage.Value = shp.File.ID.ToString();
            }
        }

        protected void btnAddImage_Click(object sender, EventArgs e)
        {

            hdfSaveImage.Value = SaveFile(fuImage, FamoBase.enmObjectCode.Shops, id).ToString();
        }

        private int SaveFile(FileUpload fu, FamoBase.enmObjectCode onbjcode, int objid)
        {
            string filePath = "";
            if (fu.HasFile && fu.PostedFile.ContentLength > 0)
            {
                filePath = ("~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
                fu.SaveAs(Server.MapPath(filePath));
            }
            else
            {
                lblmsg.Text = "بجب عليك تحميل صورة ";
                return 0;
            }
            FamFile fi = new FamFile(Passport);

            fi.UserID = Passport.UserID;
            fi.AlbumID = 0;
            //fi.file_name = fuImage.FileName;
            fi.ObjectCode = onbjcode;
            fi.ObjectId = objid;
            fi.Path = filePath;
            int x = fi.Save();
            hdfSaveImage.Value = x.ToString();
            if (x > 0)
            {
                FamShop s = new FamShop(Passport, id);
                s.File.ID = x;
                s.Update();
            }

            return 0;

        }
    }
}