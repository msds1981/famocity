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
    public partial class shopProductAdd2 : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected int id { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];

                if (!Passport.Logged)
                {
                    Session["target"] = "/Shop/edit/Prods";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/Shop/edit/Prods";
                Response.Redirect("/login");
            }

            if (RouteData.Values["id"] != null)
            {
                try
                {
                    id = Convert.ToInt32(RouteData.Values["id"].ToString().Trim('{','}'));
                }
                catch (Exception ex) { id = 0; }
            }

            FamShop s = new FamShop(Passport, Passport.ShopID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/login");

            if (!IsPostBack)
            {
                ddlShowBrands();
                ClassMain.FillActivities(ddlActiv, "- اختر نشاط -");
                if (id > 0)
                    showProductData(id);
                else
                   btnDelete.Visible = false;
            }
        }
        protected void showProductData(int id)
        {
            FamProduct p = new FamProduct(Passport, id);
            if (p.Shop.ID != Passport.ShopID) Response.Redirect("/login");
            txtName.Text = p.Name;
            txtDescription.Text = p.Description;
            ddlBrand.SelectedValue = p.Brand.ID.ToString();
            txtPrice.Text = p.NewPrice.ToString();
            txtDiscount.Text = p.Discount.ToString();
            txtCountry.Text = p.MadeIn;
            txtSupplier.Text = p.Supllier;
            ddlActiv.SelectedValue = p.Activity.ID.ToString();
            ShowImages();
        }

        private void ShowImages() { 
            //عرض صور المنتج هنا
            ltrImgs.Text = " <div id='gallery_box'><ul id='ulp'>";
            FamFile f = new FamFile(Passport);
            foreach (DataRow dr in f.GetFiles(FamoBlock.enmObjectCode.Products, id).Rows)
            {
                int fileID = Convert.ToInt32(dr["file_id"].ToString());
                string href = "javascript:deleteImg(" + fileID + "); return false;";
                ltrImgs.Text += "<li id='f" + fileID + "' onclick='deleteImg(" + fileID + ")'><a href='#'>";
                //ltrImgs.Text += "<img src='/thumb.aspx?image=" + dr["file_path"].ToString().Replace("~", "") + "&size=136'/>";
                ltrImgs.Text += "<img src='" + dr["file_path"].ToString().Replace("~", "") + "'/>";
                ltrImgs.Text += "</a></li>";
            }
            ltrImgs.Text += "</ul></div>";
        
        }

        protected void ddlShowBrands()
        {
            FamBrand b = new FamBrand(Passport);

            DataTable dt = b.GetShopBrands(Passport.ShopID);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ddlBrand.DataSource = ds.Tables["FamBrand"];
            ddlBrand.DataTextField = "Title";
            ddlBrand.DataValueField = "brand_id";
            ddlBrand.DataSource = dt;
            ddlBrand.DataBind();

            ddlBrand.Items.Insert(0, new ListItem("-اختر من القائمة-", "0"));
            ddlBrand.SelectedIndex = 0;

        }

        protected bool updateProduct(int id)
        {
            if (!IsValid) return false;
            FamProduct p = new FamProduct(Passport, id);
            p.Name = txtName.Text;
            p.Description = txtDescription.Text;
            p.Brand.ID = Convert.ToInt32(ddlBrand.SelectedValue);
            p.NewPrice = Convert.ToDecimal(txtPrice.Text);
            p.Discount = Convert.ToDecimal(txtDiscount.Text);
            p.MadeIn = txtCountry.Text;
            p.Supllier = txtSupplier.Text;
            p.Activity.ID = Convert.ToInt16(ddlActiv.SelectedValue);
            p.Update();
            ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Products, p.ID);
            return true;
        }

        protected int saveNewProduct()
        {
            if (!IsValid) return 0;
            FamProduct p = new FamProduct(Passport);
            p.Name = txtName.Text;
            p.Description = txtDescription.Text;
            p.Brand.ID = Convert.ToInt32(ddlBrand.SelectedValue);
            p.NewPrice = Convert.ToDecimal(txtPrice.Text);
            p.Discount = Convert.ToDecimal((txtDiscount.Text.Trim() == "") ? "0" : txtDiscount.Text);
            p.Shop.ID = Passport.ShopID;
            p.MadeIn = txtCountry.Text;
            p.Supllier = txtSupplier.Text;
            p.Activity.ID = Convert.ToInt16(ddlActiv.SelectedValue);
            int Id = p.Save();
            ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Products, Id);
            return Id;

        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (id > 0)
        //    {
        //        FamProduct p = new FamProduct(Passport, id);
        //        p.Delete();
        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                updateProduct(id);
                ShowImages();
            }
            else
            {
                id = saveNewProduct();
                if (id > 0)
                {
                    Response.Redirect("/shop/edit/prods");
                }
            }
            //ClassMain.UploadMultiFiles(AsyncUpload1,FamoBlock.enmObjectCode.Products, Convert.ToInt32(id))[0];
        }

        [WebMethod]
        public static bool deleteImg(int fid)
        {
            //delete image of product via ajax
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamFile f = new FamFile(pass, fid);
            return f.Delete();
        }

        [WebMethod]
        public static bool deleteP(int pid)
        {
            //delete product via ajax
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamProduct p = new FamProduct(pass, pid);
            return p.Delete();
        }

    }
}
