using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FamoLibrary;
using System.Data;
using System.IO;

namespace FamoCity
{
    /// <summary>
    /// Summary description for hdlProduct
    /// </summary>
    public class hdlProduct : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        FamoPassport Pass;
        public void ProcessRequest(HttpContext context)
        {
            Pass = (FamoPassport)context.Session["passport"];

            int pid = 0;
            if (context.Request.QueryString["pid"] != null)
            {
                try
                {
                    pid = Convert.ToInt32(context.Request.QueryString["pid"]);
                }
                catch (Exception ex) { pid = 0; }
            }

            //context.Response.ContentType = "text/plain";
            context.Response.Write(getProductData(pid));
        }

        private string getProductData(int pid) {
            //this function moved from shopMainProd.aspx
            String html = "";
            FamFile b = new FamFile(Pass);
            DataTable dt = b.GetFiles(FamoBlock.enmObjectCode.Products, pid);
            int i = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FamProduct p = new FamProduct(Pass, pid);
                    String imgPath;
                    int fileID = Convert.ToInt32(dr["file_id"].ToString());
                    FamFile f = new FamFile(Pass, fileID);
                    imgPath = f.Path.Substring(1);
                    string path = HttpContext.Current.Server.MapPath(imgPath);
                    if (!File.Exists(path))
                    {
                        imgPath = "/images/black.png";
                    }
                    html += "<li><div class='button'><img src='";
                    html += imgPath;
                    html += "' /><p><span class='title'>";
                    html += p.Name + " Image: " + i;
                    html += "</span></p></div><a href='" + imgPath + "'></a><a href='#' target='_blank'></a></li>";
                    i++;
                }
            }
            return html;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}