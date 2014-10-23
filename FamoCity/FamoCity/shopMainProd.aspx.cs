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
    public partial class shopMainProd : System.Web.UI.Page
    {
        FamoPassport pass;
        int productID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null)
            {
                pass = new FamoPassport(ClassMain.ConnectionString);
                Session["passport"] = pass;
            }
            else
                pass = (FamoPassport)Session["passport"];


            if (Page.RouteData.Values["id"] != null)
                productID = Convert.ToInt32(Page.RouteData.Values["id"]);
            else
                Response.Redirect("/main");
            //productID = Convert.ToInt32(ClassMain.DecryptParameter(pID.Replace(" ", "+")));//getProductID(pID);
            // if (productID == 0) Response.Redirect("Main");
            showInfo(productID);
            setPics(productID);

        }

        //product details
        protected void showInfo(int pID)
        {
            FamProduct p = new FamProduct(pass, pID);
            lblPName.Text = p.Name;
            lblPPrice.Text = p.NewPrice.ToString();
            lblPBrand.Text = p.Brand.Title;
            lblInfo.Text = p.Description;

        }
        //products pictuers
        protected void setPics(int prID)
        {
            String html = "";
            FamFile b = new FamFile(pass);
            DataTable dt = b.GetFiles(FamoBlock.enmObjectCode.Products, prID);
            int i = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FamProduct p = new FamProduct(pass, prID);
                    String imgPath;
                    int fileID = Convert.ToInt32(dr["file_id"].ToString());
                    FamFile f = new FamFile(pass, fileID);
                    imgPath = f.Path.Substring(1);
                    string path = MapPath(imgPath);
                    if (!File.Exists(path))
                    {
                        imgPath = "images/black.png";
                    }
                    html += "<li><div class='button'><img src='";
                    html += imgPath;
                    html += "' /><p><span class='title'>";
                    html += p.Name + " Image: " + i;
                    html += "</span></p></div><a href='" + imgPath + "'></a><a href='#' target='_blank'></a></li>";
                    i++;
                }
            }
            ltrBaner.Text = html;
        }
    }
}
