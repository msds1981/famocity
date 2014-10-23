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
    public partial class shopMainProd2 : System.Web.UI.Page
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

            if (pass.Connection == null) {
                pass = new FamoPassport(ClassMain.ConnectionString);
                Session["passport"] = pass;
            }

            //if (Page.RouteData.Values["id"] != null)
            if (Request["id"] != null)
                productID = Convert.ToInt32(Request["id"]);
            else
                Response.Redirect("/main");
            //productID = Convert.ToInt32(ClassMain.DecryptParameter(pID.Replace(" ", "+")));//getProductID(pID);
            // if (productID == 0) Response.Redirect("Main");

            getImage();
            showInfo(productID);
            setPics(productID);

            //<img src="/images/amrdiab.jpg" />

        }
        private void getImage()
        {
            FamUser fu = new FamUser(pass, pass.UserID);
            string fpath="";
            if (fu.File.Path != null)
                fpath=fu.File.Path.Replace("~", "");
            ltrimg.Text = "<img src='" + ClassMain.CheckIfImageExist(fpath, fu.Gender) + "' />";
        }
        private void BindReqComment()
        {
            FamComment comment = new FamComment(pass);
            DataTable dt = comment.GetComments((int)FamoBlock.enmObjectCode.Products, productID);

            foreach (DataRow dr in dt.Rows)
            {
                dr["file_path"] = ClassMain.CheckIfImageExist(dr["file_path"].ToString().Replace("~", ""), (FamoBlock.enmGender)Convert.ToInt32(dr["gender"]));
            }
            rpcomment.DataSource = dt;
            rpcomment.DataBind();
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