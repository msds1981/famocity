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
    public partial class admTrigger : System.Web.UI.Page
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
                    Session["target"] = "/admin/Trigger";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Trigger";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                txtCode.Focus();
                FamOption fo = new FamOption(Passport);
                lblSaveFile.Text = fo.GetValue(FamoBlock.Const_Option_Unity_TriggerPath);

                showScene();
                if (id > 0)
                {
                    showScenceData(id);

                }
            }
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
   
        private void showScenceData(int id)
        {
            FamTrigger ftr = new FamTrigger(Passport, id);


            ddlScene.SelectedValue =ftr.Scene.ID.ToString();
            txtCode.Text = ftr.Code;
            txtVersion.Text = ftr.Version.ToString();
            lblShowFile.Text = ftr.File.Path;

            
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FamTrigger ft = new FamTrigger(Passport, id);
            ft.Scene.ID =Convert.ToInt32(ddlScene.SelectedValue);
            ft.Version =float.Parse(txtVersion.Text);
            ft.Code = txtCode.Text;

            ft.folder = Guid.NewGuid();
           
              
           
            //Upload File if HasFile=true
            int fid = SaveFile(fuTrigger, FamoBlock.enmObjectCode.Trigger, id);
            if (fid > 0)
            {
                

            }

            if (id > 0)
            {
                ft.Update();
                Response.Redirect("/admin/Triggers");
            }
            else
            {
                id = ft.Save();

                ft.File.ID = fid;

                if (id > 0)
                {

                    FamTrigger trgr = new FamTrigger(Passport, id);
                    FamScene scen = new FamScene(Passport, trgr.Scene.ID);
                    if (!Directory.Exists(Server.MapPath(".")+"/FamoFiles/" +scen.folder+"/"+ ft.folder.ToString()))
                    {
                        Directory.CreateDirectory(Server.MapPath(".")+"/FamoFiles/" + scen.folder + "/" + ft.folder.ToString());
                        Directory.CreateDirectory(Server.MapPath(".")+"/FamoFiles/" + scen.folder + "/" + ft.folder.ToString()+"/"+"Images");
                        Directory.CreateDirectory(Server.MapPath(".") + "/FamoFiles/" + scen.folder + "/" + ft.folder.ToString() + "/" + "Mob");
                        Directory.CreateDirectory(Server.MapPath(".") + "/FamoFiles/" + scen.folder + "/" + ft.folder.ToString() + "/" + "Desk");
                    }

                }
                UpdateFileID(ft.File.ID, id);
                Response.Redirect("/admin/Trigger/" + ft.ID);
               
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FamTrigger ft;
            if (id > 0)
                ft = new FamTrigger(Passport, id);

            else
                ft = new FamTrigger(Passport);

            if (id > 0)
            {
                ft.Delete();
                Response.Redirect("/admin/Triggers");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("/admin/Trigger");
           
        }
    }
}