using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;

namespace FamoCity
{
    public partial class admGroups : System.Web.UI.Page
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
                    Session["target"] = "/admin/groups";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/groups";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                txtName.Focus();
                bindgvGroups();
                if (id > 0)
                    bindDataList(id);
            }
        }
        private void bindgvGroups()
        {
            FamGroup fm = new FamGroup(Passport);
            gvGroups.DataSource = fm.GetGroups();
            gvGroups.DataBind();
        }
        private void bindDataList(int id)
        {
            FamGroup fm = new FamGroup(Passport, id);

            if (fm.ID > 0)
            {
                txtName.Text = fm.Name;
                int status = Convert.ToInt32(fm.Status);
                ddlStatus.SelectedValue = status.ToString();
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FamGroup fm;
            if (id > 0)
                fm = new FamGroup(Passport, id);
            else
                fm = new FamGroup(Passport);

            if (id > 0)
            {
                fm.Delete();
                Response.Redirect("/admin/groups");
            }


        }

 

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            FamGroup fm;
            if (id > 0)
                fm = new FamGroup(Passport, id);
            else
                fm = new FamGroup(Passport);
            txtName.Focus();
            fm.Name = txtName.Text;
            fm.Status = (FamoBlock.enmGroupStatus)Convert.ToInt32(ddlStatus.SelectedValue);


            if (id > 0)
            {

                fm.Update();
            }
            else
            {
                fm.Save();
                Response.Redirect("/admin/groups/" + fm.ID);
            }
            bindgvGroups();


        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/groups");
        }
    }
}