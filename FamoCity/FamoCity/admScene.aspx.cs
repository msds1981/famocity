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
    public partial class admScene : System.Web.UI.Page
    {
        int id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/Scene";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Scene";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                FamOption fo = new FamOption(Passport);
                lblSaveFile.Text = fo.GetValue(FamoBlock.Const_Option_Unity_ScenePath);
                ClassMain.FillLanguageselected(ddlLang);
              
                showObjectCode();
                txtName.Focus();
                if (id > 0)
                {
                    showScenceData(id);

                }
            }
        }
   
        private void showObjectCode()
        {
            FamIndex Fi = new FamIndex(Passport);
            ddlObjectCode.DataSource = Fi.GetIndexsByLanguage("enmObjectCode", Passport.Language_ID);
            ddlObjectCode.DataValueField = "index_id";
            ddlObjectCode.DataTextField = "text";
            ddlObjectCode.DataBind();
            
        }

        private void showScenceData(int id)
        {
            FamScene fs = new FamScene(Passport, id);
            ddlLang.SelectedValue = Convert.ToInt32(fs.Language.ID).ToString();//fs.Language.ID.ToString();
            txtName.Text = fs.Name;
            txtDesc.Text = fs.Description;
            txtCode.Text = fs.Code;
            ddlObjectCode.SelectedValue = Convert.ToInt32(fs.ObjectCode).ToString();
            txtOpjectId.Text = fs.ObjectId.ToString();
            txtVersion.Text = fs.Version.ToString();
            lblShowPath.Text = fs.File.Path;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FamScene fs = new FamScene(Passport, id);
            fs.Language.ID = Convert.ToInt32(ddlLang.SelectedValue);
            fs.Name = txtName.Text;
            fs.Description = txtDesc.Text;
            fs.Code = txtCode.Text;
            fs.ObjectCode = (FamoBlock.enmObjectCode)Convert.ToInt32(ddlObjectCode.SelectedValue);
            fs.ObjectId = Convert.ToInt32(txtOpjectId.Text);
            fs.Version = float.Parse(txtVersion.Text);
            fs.folder = Guid.NewGuid();
            int fid = SaveFile(fuScene, FamoBlock.enmObjectCode.Scene, id);
            if (fid > 0)
            {
                fs.File.ID = fid;
                
            }
            if (id > 0)
            {
                fs.Update();
                Response.Redirect("/admin/Scenes");
            }
            else
            {
                id = fs.Save();
                if (id > 0)
                {
                    FamScene scen = new FamScene(Passport, id);

                    if (!Directory.Exists(Server.MapPath(".")+ "/FamoFiles/" + scen.folder.ToString()))
                    {
                        Directory.CreateDirectory(Server.MapPath(".")+"/FamoFiles/" + scen.folder.ToString());
                    }
                }
                UpdateFileID(fs.File.ID, id);
                Response.Redirect("admin/Scene/" + fs.ID);
                lblmsg.Text = "تمت الاضافة بنجاح ";
            }
        }

        private void UpdateFileID(int file_id, int ObjID)
        {
            if (file_id > 0)
            {
                FamFile f = new FamFile(Passport, file_id);
                f.ObjectId = ObjID;
                f.Update();
            }
        }

        private int SaveFile(FileUpload fu, FamoBase.enmObjectCode onbjcode, int objid)
        {
            string filePath = "";
            if (fu.HasFile && fu.PostedFile.ContentLength > 0)
            {
                filePath = (lblSaveFile.Text + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
                fu.SaveAs(Server.MapPath(filePath));
            }
            else
            {
                lblmsg.Text = "بجب عليك تحميل صورة ";
                return 0;
            }

            FamFile fi = new FamFile(Passport);
            fi.UserID = Passport.UserID;
            fi.ObjectCode = onbjcode;
            fi.ObjectId = objid;
            fi.Path = filePath;
            fi.Name = Path.GetFileName(filePath);
            return fi.Save();
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {

            FamScene fs = new FamScene(Passport);

            if (id > 0 && Convert.ToInt32(ddlLang.SelectedValue) > 0)
            {
                DataTable dt = fs.Getscen(id, Convert.ToInt32(ddlLang.SelectedValue));
                if (dt.Rows.Count > 0)
                {

                    txtName.Text = dt.Rows[0]["text"].ToString();
                    txtDesc.Text = dt.Rows[1]["text"].ToString();
                }
                else
                {

                    txtName.Text = "";
                    txtDesc.Text = "";

                }
            }


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FamScene fa;
            if (id > 0)
                fa = new FamScene(Passport, id);

            else
                fa = new FamScene(Passport);

            if (id > 0)
            {
                fa.Delete();
                Response.Redirect("/admin/Scenes");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Scene");

        }
    }
}