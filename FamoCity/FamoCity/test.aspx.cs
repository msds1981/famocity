using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.IO;
using System.Web.Services;

namespace FamoCity
{
    public partial class test : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.Write(Request["p"].ToString());
        }
       /* private int SaveImage(FileUpload fu, FamoBase.enmObjectCode onbjcode, int objid)
        {

            string filePath = "";
            try
            {
                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        filePath = Server.MapPath("~/files_upload/") + DateTime.Now.Ticks + Path.GetExtension(hpf.FileName);
                        hpf.SaveAs(filePath);
                        //Response.Write("<b>File: </b>" + hpf.FileName + "  <b>Size:</b> " +
                        //    hpf.ContentLength + "  <b>Type:</b> " + hpf.ContentType + " Uploaded Successfully <br/>");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        
            // حفظ ملف file
            FamFile fi = new FamFile(Passport);
            fi.UserID = Passport.UserID;
            fi.AlbumID = 0;
            fi.ObjectCode = onbjcode;
            fi.Name = fu.FileName;
            fi.FileType = FamoBlock.enmFileType.Photo;
            fi.ObjectId = objid;
            fi.Path = filePath;
            int x = fi.Save();
            return x;
        }*/
    }
}