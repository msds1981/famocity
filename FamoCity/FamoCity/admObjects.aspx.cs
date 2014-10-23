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
    public partial class admObjects : System.Web.UI.Page
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
                    Session["target"] = "admin/Object";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Object";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                FamOption fo = new FamOption(Passport);
                //lblShowFile.Text = fo.GetValue(FamoBlock.Const_Option_Unity_ObjectPath);
               
                showScene();
                showObjectCode();
                objName.Focus();
                if (id > 0)
                {
                    showoObjectData(id);

                }
            }
        }

        private void showoObjectData(int id)
        {
            FamObject fo = new FamObject(Passport, id);
            FamTrigger trgr = new FamTrigger(Passport, fo.Trigger.ID);
            ddlTrigger.SelectedValue = Convert.ToInt32(fo.Trigger.ID).ToString();
            ddlScene.SelectedValue = Convert.ToInt32(fo.Scene.ID).ToString();
            Versionlable.Text = fo.Version.ToString() ;
            txtCodeLable.Text = fo.Code;
            objName.Text = fo.name;
            objDescription.Text = fo.description;
            ddlScene.SelectedValue = trgr.Scene.ID.ToString();
            showTrigger();
            ddlTrigger.SelectedValue = trgr.ID.ToString();
            MapAddress.Text = fo.map;
            //ddlObjectCode.SelectedValue = Convert.ToInt32(fo.ObjectCode).ToString();
            //txtObjId.Text = fo.ObjectId.ToString();
            lblSaveFile.Text = fo.File.Path;
            lblfileimage.Text = fo.Image.Path;
            ObjImage.ImageUrl = fo.Image.Path;
        }

        private void showObjectCode()
        {
            FamIndex Fi = new FamIndex(Passport);

            //ddlObjectCode.DataSource = Fi.GetIndexsByLanguage("enmObjectCode", Passport.Language_ID);
          
            //ddlObjectCode.DataValueField = "index_id";
            //ddlObjectCode.DataTextField = "text";
            //ddlObjectCode.DataBind();
            //ddlObjectCode.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            //ddlObjectCode.SelectedValue = "0";
        }

        private void showScene()
        {
            FamScene Fs = new FamScene(Passport);
            ddlScene.DataSource = Fs.Getscens();
         
            ddlScene.DataValueField = "scn_id";
            ddlScene.DataTextField = "code";
            ddlScene.DataBind();
            ddlScene.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlScene.SelectedValue = "0";
        }

        private void showTrigger()
        {
            FamTrigger Ft = new FamTrigger(Passport);
            if(Convert.ToInt32(ddlScene.SelectedValue)>0)
            ddlTrigger.DataSource = Ft.GetScenTrigger(Convert.ToInt32(ddlScene.SelectedValue));   
            else
                ddlTrigger.DataSource = Ft.GetTriggers();   
            ddlTrigger.DataValueField = "trg_id";
            ddlTrigger.DataTextField = "code";
            ddlTrigger.DataBind();
            ddlTrigger.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlTrigger.SelectedValue = "0";
        }


        private void showFolder()
        {
            FamTrigger Ft = new FamTrigger(Passport);
            //if (Convert.ToInt32(ddlScene.SelectedValue) > 0)
            //    ddlFolder.DataSource = Ft.GetScenTrigger(Convert.ToInt32(ddlScene.SelectedValue));
            //else
            //    ddlFolder.DataSource = Ft.GetTriggers();
            //ddlFolder.DataValueField = "trg_id";
            //ddlFolder.DataTextField = "code";
            //ddlFolder.DataBind();
            //ddlFolder.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            //ddlFolder.SelectedValue = "0";
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamObject fo = new FamObject(Passport, id);
           // fo.Trigger.ID = Convert.ToInt32(ddlTrigger.SelectedValue);
            //fo.Scene.ID = Convert.ToInt32(ddlScene.SelectedValue);
            fo.name =objName.Text;
            fo.description = objDescription.Text;
            fo.map = MapAddress.Text;
            FamTrigger fobj = new FamTrigger(Passport, Convert.ToInt32(ddlTrigger.SelectedValue));
            FamScene fsen = new FamScene(Passport, fobj.Scene.ID);
           //fo.Image.ID
            //Upload File if HasFile=true
            //int fid = SaveFile(fobj.folder.ToString(), fuFile, FamoBlock.enmObjectCode.Objects, id);
            //if (fid > 0)
            //    fo.File.ID = fid;

            int[] img = ClassMain.UploadImage(fsen.folder.ToString()+"/"+ fobj.folder.ToString() + "/Images", ImageUploader, FamoBlock.enmObjectCode.Objects, 0);// SaveFile(ImageUploader, FamoBlock.enmObjectCode.Objects, id);
            if (img[0] > 0)
                fo.Image.ID = img[0];
            ObjImage.ImageUrl = fo.Image.Path;

            if (id > 0)
            {
                fo.Update();
                Response.Redirect("/admin/Object/" + fo.ID);
            }
            else
            {
                id = fo.Save();

                UpdateFileID(fo.File.ID, id);
              
                Response.Redirect("/admin/Object/" + fo.ID); 
                //lblmsg.Text = "تم الحفظ في قاعدة البيانات بنجاح ";
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
                //filePath = (lblShowFile.Text + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
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



        private int SaveFile(string Folder,FileUpload fu, FamoBase.enmObjectCode onbjcode, int objid)
        {
            if(!Directory.Exists(Server.MapPath(".")+"/"+"FamoFiles/"+Folder))
                Directory.CreateDirectory(Server.MapPath(".") + "FamoFiles/" + "/" + Folder);
            string filePath = "";
            if (fu.HasFile && fu.PostedFile.ContentLength > 0)
            {
                filePath = Folder + "/" + fu.FileName ;
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FamObject fo;
            if (id > 0)
                fo = new FamObject(Passport, id);

            else
                fo = new FamObject(Passport);

            if (id > 0)
            {

                fo.Delete();
                Response.Redirect("/admin/Objectshow");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Object");
        }

        protected void ddlScene_SelectedIndexChanged(object sender, EventArgs e)
        {
            showTrigger();

            
        }

        //protected void Createfolder_Click(object sender, EventArgs e)
        //{
        //    if (!Directory.Exists(HttpContext.Current.Server.MapPath(".") + "/"+folder.Text))
        //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(".") + "/" + folder.Text);
        //    foreach (string direct in Directory.GetDirectories(HttpContext.Current.Server.MapPath(".")))
        //        //Directory.GetDirectories();
        //        DirecoriesList.Items.Add(direct);
        //}

       
    }
}