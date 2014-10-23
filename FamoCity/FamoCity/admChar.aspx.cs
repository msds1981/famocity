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
    public partial class admChar : System.Web.UI.Page
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
                    Session["target"] = "/admin/Char";
                    Response.Redirect("/login.aspx");
                }
            }
            else
            {
                Session["target"] = "/admin/Char";
                Response.Redirect("/login.aspx");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("login.aspx");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                txtName.Focus();
                FamOption fo = new FamOption(Passport);
                lblSaveFile.Text = fo.GetValue(FamoBlock.Const_Option_Unity_Character);


                if (id > 0)
                {
                    showCharData(id);
                }
            }
        }

        private void showCharData(int id)
        {
            FamCharacter fcr = new FamCharacter(Passport, id);

            int gender = Convert.ToInt32(fcr.Gender);
            ddlGender.SelectedValue = gender.ToString();
            txtName.Text = fcr.Name;
            lblShowFile.Text = fcr.File.Path;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamCharacter fcr = new FamCharacter(Passport, id);
            fcr.Gender = (FamoBlock.enmGender)Convert.ToInt32(ddlGender.SelectedValue);
            fcr.Name = txtName.Text;
            int fid = SaveFile(fuFile, FamoBlock.enmObjectCode.Character, id);
              if (fid > 0)
                fcr.File.ID = fid;

            if (id > 0)
            {
             bool x=  fcr.Update();
             Response.Redirect("/admin/Chargroup");
            }
            else
            {
                id = fcr.Save();
                UpdateFileID(fcr.File.ID, id);
                Response.Redirect("/admin/Chargrop/" + fcr.ID);

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
                lblMsg.Text = "بجب عليك تحميل صورة ";
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
            FamCharacter ft;
            if (id > 0)
                ft = new FamCharacter(Passport, id);

            else
                ft = new FamCharacter(Passport);

            if (id > 0)
            {
                ft.Delete();
                Response.Redirect("admCharShow.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("/admin/Char");

        }

       


        }
    }
