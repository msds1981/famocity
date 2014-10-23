using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.IO;
using System.Data;

namespace FamoCity
{
    public partial class usrRegStep3 : System.Web.UI.Page
    {
        int id = 0;
        FamoPassport Passport;
        protected string UploadFolderPath = "~/files_upload/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                //un comment this line
                if (!Passport.Logged || Passport.UserType != FamoBlock.enmUserType.User) Response.Redirect("/login");
            }
            else {
                Response.Redirect("/login");
            }
            id = Passport.UserID;
        }
        //protected void FileUploadComplete(object sender, EventArgs e)
        //{
        //    string filename = System.IO.Path.GetFileName(AsyncUpload1.FileName);
        //    string ext = filename.Substring(filename.LastIndexOf(".") + 1);

        //    if (ext != "jpg" && ext != "gif" ) { lblError.Text = "invalid Extension"; return; }
            
        //    AsyncUpload1.SaveAs(Server.MapPath(this.UploadFolderPath) + filename);
        //}
        protected void btnIgnore_Click(object sender, EventArgs e)
        {
            Response.Redirect("/step4");
        }
        private void SetStep(string pageStep)
        {
            FamOption op = new FamOption(Passport);
            op.SetValue(FamoBlock.Const_Option_Usr_StepsRegister + Passport.UserID.ToString(), pageStep);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = AsyncUpload1.UploadedFiles[0].FileName;
                string ext = filename.Substring(filename.LastIndexOf(".") + 1);
                int fileID=0;
                FamUser u = new FamUser(Passport, id);
                int[] f = ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Users, id);
                if (f.Length > 0) fileID = f[0];
                if (fileID > 0)
                {
                    u.File.ID = fileID;
                    u.Update();
                    SetStep("/step4");
                    Response.Redirect("/step4");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "الرجاء اختيار صورة العرض";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }       
    }
}