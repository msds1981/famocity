using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FamoLibrary;
using System.Web.Services;

namespace FamoCity
{
    public partial class shopMainProducts : System.Web.UI.Page
    {
        FamoPassport pass;
        public static int shopID;
        int brandID;
        string shopname;
        protected void Page_Load(object sender, EventArgs e)
        {
            string bID = null;

            if (Session["passport"] == null)
            {
                pass = new FamoPassport(ClassMain.ConnectionString);
                Session["passport"] = pass;
            }
            else
                pass = (FamoPassport)Session["passport"];

            //string shID = Request.QueryString["p"].ToString();
            //shopID = Convert.ToInt32(ClassMain.DecryptParameter(shID.Replace(" ", "+")));
            FamShop shp;
            if(RouteData.Values["shopname"]!=null)
            {
                shp = ClassMain.GetShop(RouteData.Values["shopname"].ToString());
            }
            else
            {
                shp = new FamShop(pass);
            }

            if (shp.ID == 0)
                Response.Redirect("Main");
            else
                shopname = shp.WebName;

            try
            {
                bID = RouteData.Values["brand"].ToString();
            }
            catch (Exception ex)
            {
                showProducts(shp.ID);
                composeSideBar(shp.ID);
            }
            if (bID != null)
            {

                brandID = Convert.ToInt32(RouteData.Values["brand"].ToString().Trim('{','}'));
                if (brandID == 0) Response.Redirect("Main");
                showProducts(shp.ID, brandID);
                composeSideBar(shp.ID);
            }
        }

        protected void composeSideBar(int shopID)
        {
            //right side bar
            string code = "";
            FamBrand b = new FamBrand(pass);
            DataTable dt = b.GetShopBrands(shopID);
            foreach (DataRow dr in dt.Rows)
            {
                FamShop shp = ClassMain.GetShop(shopID.ToString());
                int brandID = Convert.ToInt32(dr["brand_id"].ToString());
                //string relodeLink = "Shop/{" +shp.WebName + "}/products/{" + brandID.ToString() + "}";
                string relodeLink = "/shop/" + shopname + "/products/" + brandID.ToString();
                FamBrand cb = new FamBrand(pass, brandID);
                code += "<a href=" + relodeLink + ">";
                code += "<div id='full_column'>";

                code += "<div class='column_left'><div class='product_title'>";
                code += cb.Title;               
                //code += "</div><div class='views_numbers'><ul><li>166 <span><img src='/images/views_icon.png' /></span></li><li>245 <span><img src='/images/numbers_icon.png' /></span></li></ul></div></div><div class='column_right'>";

                //remove comment in above line, and remove the next line bellow
                code += "</div><div class='views_numbers'></div></div><div class='column_right'>";
                string path = "";
                if (cb.File.ID > 0)
                    path = cb.File.Path.Replace("~", "");
                else
                    path = "";
                code += "<img src=" + path + " /></div></div></a>";
            }
            ltrSideBar.Text = code;
        }
        protected void showProducts(int shopID)
        {
            FamProduct p = new FamProduct(pass);
            DataTable dt = p.GetLatestProducts(shopID);
            ltrBoxes.Text = ClassMain.buildProductCode(dt, pass);
        }
        protected void showProducts(int shopID, int brandID)
        {
            FamProduct p = new FamProduct(pass);
            DataTable dt = p.GetLatestProductsBasedOnBrand(shopID, brandID);
            ltrBoxes.Text = ClassMain.buildProductCode(dt, pass);
        }
        [WebMethod]
        public static string updatePage()
        {
            string code = "";
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamProduct p = new FamProduct(pass);
            DataTable dt = p.GetNextLatestProducts(shopID);

            code = ClassMain.buildProductCode(dt, pass);
            return code;
        }

    }
}