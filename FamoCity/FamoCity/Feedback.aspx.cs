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
    public partial class Feedback : System.Web.UI.Page
    {
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null)
            {
                Passport = new FamoPassport(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStr1"].ConnectionString);
                Session["passport"] = Passport;
            }
            else
                Passport = (FamoPassport)Session["passport"];


            if (!IsPostBack)
            {
                showPages();
            }
        }
    
        private void showPages()
        {

            FamList Fl = new FamList(Passport);
            ddlPage.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlPage.DataSource = Fl.GetListsByName(FamoBlock.Const_List_PagesFeed);
            ddlPage.DataValueField = "list_id";
            ddlPage.DataTextField = "text";
            ddlPage.DataBind();

            // ddlLang.SelectedValue = "0";
        }

        protected void btnRegistar_Click(object sender, EventArgs e)
        {
            int fileID=0;
            if (!Passport.Logged) Passport.LoginWithoutRecordLogs("salwa.ksu@gmail.com", "a");
            FamFeedback ffb = new FamFeedback(Passport);
            ffb.Subject = txtSubject.Text;
            ffb.Note = txtDescription.Text;
            ffb.User.ID = Passport.UserID;
            ffb.Type.ID = Convert.ToInt32(ddlPage.SelectedValue);
            int i = ffb.Save();
            
            int[] f = ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Feedback, i);
            if (f.Length > 0) fileID = f[0];
            if (fileID > 0)
            {
                ffb.File.ID = fileID;
                ffb.Update();
            } 
        }
        //private int SaveFile(FileUpload fu, FamoBase.enmObjectCode onbjcode, int objid)
        //{
        //    string filePath = "";
        //    if (fu.HasFile && fu.PostedFile.ContentLength > 0)
        //    {
        //        filePath = ("~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
        //        fu.SaveAs(Server.MapPath(filePath));
        //    }
        //    else
        //        return 0;
        //    FamFile fi = new FamFile(Passport);
        //    fi.UserID = Passport.UserID;
        //    fi.ObjectCode = onbjcode;
        //    fi.ObjectId = objid;
        //    fi.Path = filePath;
        //    return fi.Save();
        //}
    }
}