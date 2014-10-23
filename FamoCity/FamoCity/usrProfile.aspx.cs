using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class usrProfile : System.Web.UI.Page
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
                    Session["target"] = "/user/profile";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/user/profile";
                Response.Redirect("/login");
            }

            //just user only
            if (Passport.UserType != FamoBlock.enmUserType.User)
                Response.Redirect("/login");

            //User/{username}/profile

            id = Passport.UserID;
            if (!IsPostBack)
            {
                if (id > 0)
                    FillYears();

                FillList(ddlContry, FamoBlock.Const_List_Country);
                FillList(ddlNationlity, FamoBlock.Const_List_Nationality);
                ClassMain.FillLanguage(ddlLang);
                ShowProfileUser(id);
            }
        }
        private void ShowProfileUser(int id)
        {

            FamUser fu = new FamUser(Passport, id);
            txtName.Text = fu.FullName;
            txtFirstName.Text = fu.FirstName;
            txtLastName.Text = fu.LastName;
            ltrUserName.Text = " www.e-smartcities.com/" + "  " + fu.UserName;
            txtUserUpdate.Text = fu.UserName;
            ddlLang.SelectedValue = fu.LanguageID.ToString();
            txtEmail.Text = fu.Email;
            ltsEmail.Text = fu.Email;
            ltshowdate.Text = fu.BirthDate;
            try
            {
                string[] d = fu.BirthDate.Split(new char[] { '/' });
                ddlDay.SelectedValue = d[0];
                ddlMonth.SelectedValue = d[1];
                ddlYear.SelectedValue = d[2];
            }
            catch (Exception ex) { }

            FamContactProfile cp = new FamContactProfile(Passport, id);
            ltrAddress.Text = cp.Address + " - " + cp.City + " - " + cp.Country.Text;
            ddlContry.SelectedValue = cp.Country.ID.ToString();
            ddlNationlity.SelectedValue = cp.Nationality.ID.ToString();
            txtAddress.Text = cp.Address;
            ltStatus.Text = cp.State;
            txtStatus.Text = cp.State;
            txtCity.Text = cp.City;
            txtBox.Text = cp.PoBox;

            FamLanguage fm = new FamLanguage(Passport, fu.LanguageID);
            ltLang.Text = fm.Name;
           }

        private void FillList(DropDownList ddl, string name)
        {
            FamList fcp = new FamList(Passport);
            ddl.DataSource = fcp.GetListsByName(name, Passport.Language_ID);
            ddl.DataValueField = "list_id";
            ddl.DataTextField = "text";
            ddl.DataBind();
            ddl.Items.Add(new ListItem("- اختر من القائمة -", "0"));
          

        }

        private void FillYears()
        {
            ListItem itm = new ListItem();
            ddlYear.Items.Add(itm);
            for (int i = 1950; i < 2000; i++)
            {
                itm = new ListItem(i.ToString(), i.ToString());
                ddlYear.Items.Add(itm);
            }
        }
    }
}