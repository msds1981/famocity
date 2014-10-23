using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class admChrCloth : System.Web.UI.Page
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
                    Session["target"] = "/admin/Charcloth";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Charcloth";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                txtName.Focus();
                FamOption fo = new FamOption(Passport);
                lblSaveFile.Text = fo.GetValue(FamoBlock.Const_Option_Unity_Character);

              
                showgroup();
              
                if (id > 0)
                {
                    showCharColthData(id);
                }
            }
        }

        private void showCharColthData(int id)
        {
            FamChrClothes fcth = new FamChrClothes(Passport, id);
            ddlGroup.SelectedValue = fcth.Group.ID.ToString();
            txtName.Text = fcth.Name;
            txtObjCode.Text = Convert.ToInt32(fcth.ObjectCode).ToString();
            txtObjecId.Text = fcth.ObjectId.ToString();
            lblShowFile.Text = fcth.File.Path;
        }

        private void showgroup()
        {
            FamChrGroup Fcr = new FamChrGroup(Passport);
            ddlGroup.DataSource = Fcr.GetChrGroups();
            ddlGroup.DataValueField = "cgrp_id";
            ddlGroup.DataTextField = "name";
            ddlGroup.DataBind();
            ddlGroup.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlGroup.SelectedValue = "0";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamChrClothes fcg = new FamChrClothes(Passport, id);
            fcg.Group.ID = Convert.ToInt32(ddlGroup.SelectedValue);
            fcg.Name = txtName.Text;
            fcg.ObjectCode = (FamoBlock.enmObjectCode)Convert.ToInt32(txtObjCode.Text);
            fcg.ObjectId = Convert.ToInt32(txtObjecId.Text);
 
            int fid = ClassMain.UploadFile(fuCharCloth, lblSaveFile.Text, FamoBlock.enmObjectCode.CharacterGroup, id);
            if (fid > 0)
                fcg.File.ID = fid;

            if (id > 0)
            {
                fcg.Update();
            }
            else
            {
                id = fcg.Save();
                fcg.File.ObjectId = id;
                fcg.File.Update();
                Response.Redirect("/admin/Charcloth/" + id);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Charcloth");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            FamChrClothes fcg = new FamChrClothes(Passport, id);
           

            if (id > 0)
            {
                fcg.Delete();
                Response.Redirect("admin/CharclothShow");
            }
           
        }

    }
}