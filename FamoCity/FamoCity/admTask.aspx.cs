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
    public partial class admTask : System.Web.UI.Page
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
                    Session["target"] = "admTask.aspx";
                    Response.Redirect("login.aspx");
                }

            }
            else
            {
                Session["target"] = "admTask.aspx";
                Response.Redirect("login.aspx");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("login.aspx");

            if (Request["id"] != null)
                id = Convert.ToInt32(Request["id"]);
          
            if (!IsPostBack)
            {
                showDataGd(Passport.Language_ID);
                ClassMain.FillLanguage(ddlLang);
                ClassMain.FillLanguage(ddlOrdLang);
                if (id > 0)

                    showdatatask(id);

            }
        }

        private void showDataGd(int lang)
        {
            FamTask ft = new FamTask(Passport);

            gvTask.DataSource = ft.GetTaskByLanguage(lang,"name");
            gvTask.DataBind();
        }

        private void showdatatask(int id)
        {
            FamTask ft = new FamTask(Passport, id);
            ddlLang.SelectedValue = ft.Language.ID.ToString();
            txtTaskName.Text = ft.Name;
            txtTaskDesc.Text = ft.Desciption;
            txtGroupName.Text = ft.Group;

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamTask ft = new FamTask(Passport, id);

            ft.Language.ID = Convert.ToInt32(ddlLang.SelectedValue);
            ft.Name = txtTaskName.Text;
            ft.Desciption = txtTaskDesc.Text;
            ft.Group = txtGroupName.Text;

            // Upload File if HasFile=true
            int fid = SaveFile(fuImage, FamoBlock.enmObjectCode.Taskes, id);
            if (fid > 0)
                ft.File.ID = fid;


            if (id > 0)
            {
                ft.Update();

            }
            else
            {
                id = ft.Save();

                UpdateFileID(ft.File.ID, id);
                lblmsg.Text = "تم الحفظ في قاعدة البيانات بنجاح ";
                txtGroupName.Text = "";
                txtTaskDesc.Text = "";
                txtTaskName.Text = "";
                ddlLang.SelectedValue = "0";

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
                filePath = ("~/files_upload/" + DateTime.Now.Ticks + Path.GetExtension(fu.FileName));
                fu.SaveAs(Server.MapPath(filePath));
            }
            else
            {
                //lblmsg.Text = "بجب عليك تحميل صورة ";
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
            FamTask fm = new FamTask(Passport);

            if (id > 0 && Convert.ToInt32(ddlLang.SelectedValue) > 0)
            {
                DataTable dt = fm.GetTask(id, Convert.ToInt32(ddlLang.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    txtTaskName.Text = dt.Rows[0]["text"].ToString();
                    txtTaskDesc.Text = dt.Rows[1]["text"].ToString();
                }
                else
                {
                    txtTaskName.Text = "";
                    txtTaskDesc.Text = "";
                }

            }
        }

        protected void ddlOrdLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            // txtAativity.Text = "";
            FamTask fm = new FamTask(Passport);
            int i=Convert.ToInt32(ddlOrdLang.SelectedValue);
            if (i > 0)
            {
                showDataGd(i);

            }
        }
    }
}
