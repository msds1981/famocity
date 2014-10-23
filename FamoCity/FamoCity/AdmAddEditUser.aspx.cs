using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class AdmAddEditUser : System.Web.UI.Page
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
                    Session["target"] = "AdmAddEditUser.aspx";
                    Response.Redirect("login.aspx");
                }

            }
            else
            {
                Session["target"] = "AdmAddEditUser.aspx";
                Response.Redirect("login.aspx");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("login.aspx");

            if (Request["id"] != null)
                id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {

                showlan();
                if (id > 0)
                {
                    showUserDate(id);

                }

            }
        }
        private void showUserDate(int id)
        {
            FamUser fu = new FamUser(Passport, id);
            txtUserName.Text = fu.UserName;
            txtFirstName.Text = fu.FirstName;
            txtLastName.Text = fu.LastName;
            txtEmail.Text = fu.Email;
            int userstutas = Convert.ToInt32(fu.Status);
            ddlUserStutas.SelectedValue = userstutas.ToString();
            int type = Convert.ToInt32(fu.Type);
            ddlUserType.SelectedValue = type.ToString();
            txtBirthDate.Text = fu.BirthDate;
            txtRegisDate.Text = fu.RegisterDate.ToString();
            int gender = Convert.ToInt32(fu.Gender);
            ddlGender.SelectedValue = gender.ToString();
            int matrialstutas = Convert.ToInt32(fu.MaritalState);
            ddlLang.SelectedValue = fu.LanguageID.ToString();

        }
        private void showlan()
        {

            FamLanguage Fm = new FamLanguage(Passport);
            ddlLang.DataSource = Fm.GetLanguages();
            ddlLang.DataValueField = "lang_id";
            ddlLang.DataTextField = "name";
            ddlLang.DataBind();
            ddlLang.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlLang.SelectedValue = "0";
        }
    

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamUser fu;
            if (id > 0)
                fu = new FamUser(Passport, id);
            else
                fu = new FamUser(Passport);
            fu.UserName = txtUserName.Text;
            fu.FirstName = txtFirstName.Text;
            fu.LastName = txtLastName.Text;
            fu.Email = txtEmail.Text;
            fu.Status = (FamoBlock.enmUserStatus)Convert.ToInt32(ddlUserStutas.SelectedValue);
            fu.Type = (FamoBlock.enmUserType)Convert.ToInt32(ddlUserType.SelectedValue);
            fu.BirthDate = txtBirthDate.Text;
            fu.RegisterDate = DateTime.Parse(txtRegisDate.Text);
            fu.Gender = (FamoBlock.enmGender)Convert.ToInt32(ddlGender.SelectedValue);
            fu.LanguageID = Convert.ToInt32(ddlLang.SelectedValue);
            if (id > 0)
            {

                fu.Update();
                Response.Redirect("AdmUsers.aspx");
            }

            else
            {
                fu.Save();


            }

        }



        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            FamUser fu;
            if (id > 0)
                fu = new FamUser(Passport, id);

            else
                fu = new FamUser(Passport);

            if (id > 0)
            {

                fu.Delete();
                Response.Redirect("AdmUsers.aspx");
            }
        }
    }
}