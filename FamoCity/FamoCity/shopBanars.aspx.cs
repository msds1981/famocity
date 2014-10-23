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
    public partial class shopBanars : System.Web.UI.Page
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
                    Session["target"] = "/Shop/edit/Banars";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/Shop/edit/Banars";
                Response.Redirect("/login");
            }

            //
            FamShop s = new FamShop(Passport, Passport.ShopID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/login");

            DataTable dt = s.GetUserShops(Passport.UserID);
            if (dt.Rows.Count > 0)
                id = Convert.ToInt32(dt.Rows[0]["shop_id"]);

            if (!IsPostBack)
            {
                showImage();
            }

        }

        private void showImage()
        {

            FamBanar fb = new FamBanar(Passport);
            DataTable dt = fb.GetShopBanars(id);
            if (dt.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i == 1)
                        BindData(dt.Rows[0], txtDescFirstImage, ImgFirstLogo, hfFirstImage);
                    else if (i == 2)
                        BindData(dt.Rows[1], txtDescSecond, ImgSecondLogo, hfISecondmages);
                    else if (i == 3)
                        BindData(dt.Rows[2], txtDescThird, ImgThirdLogo, hfTthirdImages);
                    else if (i == 4)
                        BindData(dt.Rows[3], txtDescFord, ImgFourLogo, hfFourImage);
                    i++;
                }
            }

        }

        protected void btnAddBanarImage_Click(object sender, EventArgs e)
        {
            int v = 0;

            v = SaveImage(fuFirstImage, FamoBase.enmObjectCode.Banars, id);
            SaveBanar(id, v, txtDescFirstImage, hfFirstImage);

            v = SaveImage(fusecondImage, FamoBase.enmObjectCode.Banars, id);
            SaveBanar(id, v, txtDescSecond, hfISecondmages);

            v = SaveImage(fuThirdImage, FamoBase.enmObjectCode.Banars, id);
            SaveBanar(id, v, txtDescThird, hfTthirdImages);

            v = SaveImage(fuFourImage, FamoBase.enmObjectCode.Banars, id);
            SaveBanar(id, v, txtDescFord, hfFourImage);

        }

        private void BindData(DataRow Dr, TextBox TextDesc, Image Img, HiddenField hdnValue)
        {
            TextDesc.Text = Dr["description"].ToString();
            FamFile f = new FamFile(Passport, Convert.ToInt32(Dr["file_id"]));
            Img.ImageUrl = f.Path;
            hdnValue.Value = Dr["banar_id"].ToString();
        }

        private int SaveImage(FileUpload fu, FamoBase.enmObjectCode onbjcode, int objid)
        {
            string filePath = "";
            if (fu.HasFile && fu.PostedFile.ContentLength > 0)
            {
                filePath = ("~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
                fu.SaveAs(Server.MapPath(filePath));
            }
            else
            {
                //lblMsg.Text = "بجب عليك تحميل صورة ";
                return 0;
            }

            // حفظ ملف file
            FamFile fi = new FamFile(Passport);
            fi.UserID = Passport.UserID;
            fi.AlbumID = 0;
            fi.ObjectCode = onbjcode;
            fi.ObjectId = objid;
            fi.Path = filePath;
            int x = fi.Save();

            return x;
        }

        private int SaveBanar(int shopid, int fileid, TextBox text, HiddenField hdf)
        {

            if (hdf.Value == "0" && fileid == 0) return 0;

            FamBanar fb;
            if (hdf.Value == "0")
                fb = new FamBanar(Passport);
            else
                fb = new FamBanar(Passport, Convert.ToInt32(hdf.Value));
            fb.Shop.ID = shopid;
            if (fileid != 0) fb.File.ID = fileid;
            fb.Description = text.Text;


            if (hdf.Value == "0")
                fb.Save();
            else
                fb.Update();

            return 0;
        }
    }
}