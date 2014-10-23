using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.IO;
using System.Web.Services;
using System.Data;

namespace FamoCity
{
    public partial class shopBrandAdd2 : System.Web.UI.Page
    {
        FamoPassport Passport;
        //      public int id;
        int fileID;
        protected int id { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];

                if (!Passport.Logged)
                {
                    Session["target"] = "/Shop/edit/Brands";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/Shop/edit/Brands";
                Response.Redirect("/login");
            }

            FamShop s = new FamShop(Passport, Passport.ShopID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/login");

            if (Page.RouteData.Values["id"] != null)
            {
                try
                {
                    id = Convert.ToInt32(Page.RouteData.Values["id"].ToString().Trim('{', '}'));
                }
                catch (Exception ex)
                {
                    id = 0;
                }
            }

            if (!IsPostBack)
            {
                if (id > 0)
                    showBrandData(id);
                else
                    btnDelete.Visible = false;
            }
        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (id > 0)
        //    {
        //        FamBrand b = new FamBrand(Passport, id);
        //        b.Delete();
        //        Response.Redirect("/shop/edit/brands");
        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (id > 0)
                updateBrand(id);
            else
                saveNewBrand();
        }

        protected void showBrandData(int id)
        {
            FamBrand b = new FamBrand(Passport, id);
            txtTitle.Text = b.Title;
            txtDescriptipn.Text = b.Description;
            imgLogo.ImageUrl = b.File.Path;
        }

        protected void updateBrand(int id)
        {
            FamBrand b = new FamBrand(Passport, id);
            b.Title = txtTitle.Text;
            b.Description = txtDescriptipn.Text;
            b.Update();
            int[] f = ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Brands, b.ID);
            if (f.Length > 0) fileID = f[0];
            if (fileID > 0)
            {
                b.File.ID = fileID;
                b.Update();
            }
        }
        protected void saveNewBrand()
        {
            FamBrand b = new FamBrand(Passport);
            b.Title = txtTitle.Text;
            b.Description = txtDescriptipn.Text;
            b.Shop.ID = Passport.ShopID;
            int id = b.Save();
            int[] f = ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Brands, id);
            if (f.Length > 0) fileID = f[0];
            if (fileID > 0)
            {
                b.File.ID = fileID;
                b.Update();
            }
        }

        [WebMethod]
        public static bool deleteBrand(int bid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamBrand b = new FamBrand(pass, bid);
            return b.Delete();
        }
    }
}