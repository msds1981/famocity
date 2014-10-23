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
    public partial class admObjectsGroups : System.Web.UI.Page
    {
        FamoPassport Passport;
        int UploadType =0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UploadType = Convert.ToInt32(Request["UType"].ToString());
            }
            catch (Exception ex) { UploadType = 0; }
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
            if (!IsPostBack)
            {
                showScene();
                showObjectCode();
               
            }
        }
        protected void saveNewObjects()
        { int[] fil;


          //  int[] fil = ClassMain.UploadUnityFiles(ddlplatorm.SelectedValue, Passport, Convert.ToInt32(ddlTrigger.SelectedValue), fuFile, FamoBlock.enmObjectCode.Objects, 0);
        if (Directory.Exists(prefexpath.Text.Replace("\\", "/") + "/" + localfilesfolder.Text))
            fil = ClassMain.UploadUnityFilesByfixedpath(prefexpath.Text.Replace("\\", "/") + "/" + localfilesfolder.Text.ToString(), ddlplatorm.SelectedValue, Passport, Convert.ToInt32(ddlTrigger.SelectedValue), FamoBlock.enmObjectCode.Objects, 0);
        else
            foundpath.Text = "المسار غير موجود";
           

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
            if (Convert.ToInt32(ddlScene.SelectedValue) > 0)
                ddlTrigger.DataSource = Ft.GetScenTrigger(Convert.ToInt32(ddlScene.SelectedValue));
            else
                ddlTrigger.DataSource = Ft.GetTriggers();
            ddlTrigger.DataValueField = "trg_id";
            ddlTrigger.DataTextField = "code";
            ddlTrigger.DataBind();
            ddlTrigger.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlTrigger.SelectedValue = "0";
        }
        protected void ddlScene_SelectedIndexChanged(object sender, EventArgs e)
        {
            showTrigger();


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            saveNewObjects();
            FillGrade(ddlplatorm.SelectedItem.ToString());
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void ddlTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrade(ddlplatorm.SelectedItem.ToString());
            //ddlplatorm.SelectedValue = "";
            FamTrigger trg = new FamTrigger(Passport, Convert.ToInt32(ddlTrigger.SelectedValue));
            FamScene scen = new FamScene(Passport, trg.Scene.ID);
            lblSaveFile.Text = "The Files Will save in " + "FamoFiles/" +scen.folder+"/"+ trg.folder;
        }


        void FillGrade(string platform)
        {

    //        <div class='columns'>
    //    <div class='red' >Column 1</div>
    //    <div class='red'>Column 2</div>
    //    <div class='red'>Column 3</div>
    //    <div class='red'> <input type='button' value='Edit' /></div>
    //</div>



    //<div class="clear"></div>

            string platform1 = ddlplatorm.SelectedItem.Value;
            FamObject objct = new FamObject(Passport);
            DataTable dt = new DataTable();
            string str = "";
            dt=objct.GetTriggerObjects(Convert.ToInt32(ddlTrigger.SelectedValue),platform1);
            foreach (DataRow dr in dt.Rows)
            {
                str =str+ "<div class='columns'>";
                str=str+"<div class='red' >"+dr["ojct_id"]+"</div>";
                str = str + "<div class='red'>" + dr["code"] + "</div>";
                str=str+"<div class='red'>"+dr["description"]+"</div>";
                str = str + "<div class='red'> <a href='admin/Object/" + dr["ojct_id"] + "' id=" + dr["ojct_id"] + "   >edit</a></div>";
                str = str + "</div";

            }
            objectslist.Text = str;
        }

        protected void ddlplatorm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrade("");

        }

    }
}