using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using System.IO;
using System.Web.Services;

namespace FamoCity
{
    public partial class shopProductAdd : System.Web.UI.Page
    {
        FamoPassport Passport;
        String id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/Product";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/Product";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Shop)
                Response.Redirect("/login");
            else
            {
                //if (!IsPostBack)
                //{
                //    ddlShowBrands();
                //}
                if (RouteData.Values["id"] != null)
                {
                    id = RouteData.Values["id"].ToString();
                    if (!IsPostBack)
                    {
                        ddlShowBrands();
                        int intID = Convert.ToInt32(id);
                        showProductData(intID);

                    }

                }
                else
                {
                    if (!IsPostBack)
                    {
                        ddlShowBrands();
                    }
                    btnDelete.Visible = false;
                }

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
            //عرض صور المنتج هنا
            ltrImgs.Text = " <div id='gallery_box'><ul id='ulp'>";
            FamFile f = new FamFile(Passport);
            foreach (DataRow dr in f.GetFiles(FamoBlock.enmObjectCode.Products, id).Rows)
            {
                int fileID = Convert.ToInt32(dr["file_id"].ToString());
                string href = "javascript:deleteImg(" + fileID + "); return false;";
                ltrImgs.Text += "<li id='f" + fileID + "' onclick='deleteImg(" + fileID + ")'><a href='#'>";
                ltrImgs.Text += "<img src='" + dr["file_path"].ToString().Replace("~", "") + "'/>";
                ltrImgs.Text += "</a></li>";
            }
            ltrImgs.Text += "</ul></div>";
        }

        protected void ddlShowBrands()
        {
            FamBrand b = new FamBrand(Passport);

            DataTable dt = b.GetBrands();
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
            FamProduct p = new FamProduct(Passport, id);
            p.Name = txtName.Text;
            p.Description = txtDescription.Text;
            p.Brand.ID = Convert.ToInt32(ddlBrand.SelectedValue);
            p.NewPrice = Convert.ToDecimal(txtPrice.Text);
            p.Discount = Convert.ToDecimal(txtDiscount.Text);
            p.MadeIn = txtCountry.Text;
            p.Supllier = txtSupplier.Text;
            return p.Update();

        }
        protected int saveNewProduct()
        {
            FamProduct p = new FamProduct(Passport);
            p.Name = txtName.Text;
            p.Description = txtDescription.Text;
            p.Brand.ID = Convert.ToInt32(ddlBrand.SelectedValue);
            p.NewPrice = Convert.ToDecimal(txtPrice.Text);
            p.Discount = Convert.ToDecimal((txtDiscount.Text.Trim() == "") ? "0" : txtDiscount.Text);
            p.Shop.ID = Passport.ShopID;
            p.MadeIn = txtCountry.Text;
            p.Supllier = txtSupplier.Text;
            return p.Save();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                int intID = Convert.ToInt32(id);
                FamProduct p = new FamProduct(Passport, intID);
                p.Delete();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                int intID = Convert.ToInt32(id);
                if (updateProduct(intID))
                {
                    clearBoxes();
                }
            }
            else
            {
                id = saveNewProduct().ToString();
                if (Convert.ToInt32(id) > 0)
                {
                    clearBoxes();
                }
            }
            ClassMain.UploadMultiFiles(FamoBlock.enmObjectCode.Products, Convert.ToInt32(id));
        }
        protected void clearBoxes()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtDiscount.Text = "";
            txtCountry.Text = "";
            txtSupplier.Text = "";
            ddlBrand.SelectedIndex = 0;
        }

        [WebMethod]
        public static bool deleteImg(int fid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamFile f = new FamFile(pass, fid);
            return f.Delete();
        }

    }
}