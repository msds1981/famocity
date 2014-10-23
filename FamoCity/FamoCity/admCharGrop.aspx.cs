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
    public partial class admCharGrop : System.Web.UI.Page
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
                    Session["target"] = "/admin/Chargroup";
                    Response.Redirect("/login.aspx");
                }

            }
            else
            {
                Session["target"] = "/admin/Chargroup";
                Response.Redirect("/login.aspx");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login.aspx");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                txtName.Focus();
                FamOption fo = new FamOption(Passport);
                lblSaveFile.Text = fo.GetValue(FamoBlock.Const_Option_Unity_Character);

                showChar();
                showgroup();
                showgroupDetails();
                if (id > 0)
                {
                    showCharData(id);
                    showgroupDetails();
                }
            }
        }

        private void showCharData(int id)
        {
            FamChrGroup fcg = new FamChrGroup(Passport, id);
            ddlChar.SelectedValue = fcg.Character.ID.ToString();
            txtName.Text = fcg.Name;
            txtCam.Text = fcg.CameraValue;
            lblShowFile.Text = fcg.File.Path;


        }

        private void showgroup()
        {
            FamChrGroup Fcr = new FamChrGroup(Passport);
            ddlGroups.DataSource = Fcr.GetChrGroups();
            ddlGroups.DataValueField = "cgrp_id";
            ddlGroups.DataTextField = "name";
            ddlGroups.DataBind();
            ddlGroups.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlGroups.SelectedValue = "0";
        }

        private void showChar()
        {
            FamCharacter Fcr = new FamCharacter(Passport);
            ddlChar.DataSource = Fcr.GetCharacters();
            ddlChar.DataValueField = "chr_id";
            ddlChar.DataTextField = "name";
            ddlChar.DataBind();
            ddlChar.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlChar.SelectedValue = "0";
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

            }
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamChrGroup fcg = new FamChrGroup(Passport, id);
            fcg.Character.ID = Convert.ToInt32(ddlChar.SelectedValue);
            fcg.Name = txtName.Text;
            fcg.CameraValue = txtCam.Text;

            int fid = ClassMain.UploadFile(fuChar, lblSaveFile.Text, FamoBlock.enmObjectCode.CharacterGroup, id);
            if (fid > 0)
             fcg.File.ID = fid;

            if (id > 0)
            {
              fcg.Update();
              showgroupDetails();   
            }
            else
            {
                id = fcg.Save();
                fcg.File.ObjectId = id;
                fcg.File.Update();
                Response.Redirect("/admin/Chargroup" + id);
                showgroupDetails();
            }
      
        }

        private void showgroupDetails()
        {
            FamChrGroupDetail fgd = new FamChrGroupDetail(Passport);
            gvCharDetails.DataSource = fgd.GetCharacterByGroup(id);
            gvCharDetails.DataBind();


        }

        protected void btnAddto_Click(object sender, EventArgs e)
        {
            FamChrGroupDetail fgd = new FamChrGroupDetail(Passport);
            fgd.Flag = (FamoBlock.enmGroupFlag)Convert.ToInt32(ddlFlag.SelectedValue);
            fgd.Relative.ID = Convert.ToInt32(ddlGroups.SelectedValue);
            fgd.Group.ID = id;
            fgd.Save();
            showgroupDetails();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Chargroup");
        }

        protected void gvCharDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string dll = e.CommandArgument.ToString();
            FamChrGroupDetail fgd = new FamChrGroupDetail(Passport,int.Parse( dll));
            fgd.Delete();
             showgroupDetails();

             
        }

    

     


    }



}
