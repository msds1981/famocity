using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using System.Web.UI.HtmlControls;

namespace FamoCity
{
    public partial class admUserAdd : System.Web.UI.Page
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
                    Session["target"] = "/admin/admUser";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/admUser";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
            {
                id = Convert.ToInt32(RouteData.Values["id"]);
                btnEditPass.Visible = true;
                hfd.Value = "1";
            }
            else
            {
                btnEditPass.Visible = false;
                hfd.Value = "0";

            }

            if (!IsPostBack)
            {
                showUserGroup();
                ClassMain.FillLanguage(ddlLang);

                if (id > 0)
                {
                    showUserDate(id);
                }
                else
                    lblTitle.Text = "اضافةالمستخدم";

            }
        }
        private void showUserDate(int id)
        {
            FamUser fu = new FamUser(Passport, id);
            txtUserName.Text = fu.UserName;
            ddlUserGroup.SelectedValue = fu.Group.ID.ToString();
            txtFirstName.Text = fu.FirstName;
            txtLastName.Text = fu.LastName;
            txtEmail.Text = fu.Email;
            txtPassword.Text = fu.Password;
            txtreapetPassword.Text = fu.Password;
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
            lblTitle.Text = "تعديل المستخدم";


        }

        private void showUserGroup()
        {
            FamGroup Fg = new FamGroup(Passport);
            ddlUserGroup.DataSource = Fg.GetGroups();
            ddlUserGroup.DataValueField = "grp_id";
            ddlUserGroup.DataTextField = "name";
            ddlUserGroup.DataBind();
            ddlUserGroup.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlUserGroup.SelectedValue = "0";
        }




        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;
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
            fu.RegisterDate = Convert.ToDateTime(txtRegisDate.Text);
            fu.Gender = (FamoBlock.enmGender)Convert.ToInt32(ddlGender.SelectedValue);
            fu.LanguageID = Convert.ToInt32(ddlLang.SelectedValue);
            fu.Group.ID = Convert.ToInt32(ddlUserGroup.SelectedValue);


            if (id > 0)
            {
                if (hfd.Value == "1")
                    fu.Password = txtPassword.Text;
                fu.Update();
                Response.Redirect("/admin/admUsers");
            }

            else
            {
                fu.Password = txtPassword.Text;
                fu.Save();
                Response.Redirect("/admin/admUser/" + fu.ID);
                lblMsg.Text = "تم الحفظ بنجاح في قاعدة البيانات ... قابلة للتعديل والحذف";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FamUser fu;
            if (id > 0)
                fu = new FamUser(Passport, id);

            else
                fu = new FamUser(Passport);

            if (id > 0)
            {

                fu.Delete();
                Response.Redirect("/admin/admUsers");
            }
        }

        protected void CvUserName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!IsValid) return;
            FamUser FU = new FamUser(Passport);
            DataTable dt = FU.GetUserByName(txtUserName.Text);
            if (dt.Rows.Count > 0)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!IsValid) return;
            FamUser FU = new FamUser(Passport);

            if (hfd.Value == "1")
            {
                if (FU.Password == "")
                {
                    lblmsgva.Text = "يرجى ادخال كلمة المرور";
                    args.IsValid = false;
                }
                else if (txtPassword.Text != txtreapetPassword.Text)
                {
                    lblmsgva.Text = "كلمة المرور ليست متطابقة";
                    args.IsValid = false;
                }

            }
            else
                args.IsValid = true;

        }
    }
}