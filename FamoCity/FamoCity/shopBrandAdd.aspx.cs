using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.IO;
namespace FamoCity
{
    public partial class shopBrandAdd : System.Web.UI.Page
    {
        FamoPassport Passport;
        String id;
        int fileID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                id = Page.RouteData.Values["id"].ToString();
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/Brand";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/Brand";
                Response.Redirect("/login");
            }

            FamShop s = new FamShop(Passport, Passport.UserID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/login");

            if (id != null)
            {
                int intID = Convert.ToInt32(id);
                showBrandData(intID);
            }
            else
            {
                btnDelete.Visible = false;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                int intID = Convert.ToInt32(id);
                FamBrand b = new FamBrand(Passport, intID);
                b.Delete();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                int intID = Convert.ToInt32(id);
                updateBrand(intID);
            }
            else
            {
                saveNewBrand();
            }
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
            fileID = ClassMain.UploadImage(fulImage, FamoBlock.enmObjectCode.Brands, b.ID);
            b.File.ID = fileID;
            b.File.Update();
            b.Update();
        }
        protected void saveNewBrand()
        {
            FamBrand b = new FamBrand(Passport);
            b.Title = txtTitle.Text;
            b.Description = txtDescriptipn.Text;
            b.Save();
            //lblBrandTitle.Text = b.ID + "";
            fileID = ClassMain.UploadImage(fulImage, FamoBlock.enmObjectCode.Brands, b.ID);
            //lblLogo.Text = "" + fileID;
            b.File.ID = fileID;
            b.Update();
        }
    }
}