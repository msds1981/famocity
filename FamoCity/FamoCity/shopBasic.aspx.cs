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
    public partial class shopBasic : System.Web.UI.Page
    {
        int Id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "Shop/edit/OwnerInfo";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/OwnerInfo";
                Response.Redirect("/login");
            }

            FamShop s = new FamShop(Passport, Passport.UserID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/login");

            Id = Passport.UserID;
            if (!IsPostBack)
            {
                FillYears();
                BindNationality();
                showDataUsers(Id);
            }
        }

        private void showDataUsers(int userId)
        {
            // استعراض بيانات المستخدم
            FamUser Fu = new FamUser(Passport, userId);

            if (Fu.ID > 0)
            {
                txtUser.Text = Fu.UserName;
                txtEmail.Text = Fu.Email;
                txtFirstName.Text = Fu.FirstName;
                txtLastName.Text = Fu.LastName;
                string[] d = Fu.BirthDate.Split(new char[] { '/' });
                ddlDay.SelectedValue = d[0];
                ddlMonth.SelectedValue = d[1];
                ddlYear.SelectedValue = d[2];
                //rdbFemal.Checked = Convert.ToBoolean(Fu.Gender);
                //rdbMan.Checked = Convert.ToBoolean(Fu.Gender);

                //استعراض الجنسية
                FamContactProfile FCP = new FamContactProfile(Passport, Id);
                DataTable dt = FCP.GetContacts((int)FamoBlock.enmObjectCode.Users, Id);

                if (dt.Rows.Count > 0)
                {
                    ddlNationality.SelectedValue = dt.Rows[0]["nationalty"].ToString();
                }

            }

        }

        private void FillYears()
        {
            ListItem itm = new ListItem("سنة", "0");
            ddlYear.Items.Add(itm);
            for (int i = 1950; i < 2000; i++)
            {
                itm = new ListItem(i.ToString(), i.ToString());
                ddlYear.Items.Add(itm);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //حفظ بيانات المستخدم 
            FamUser fu = new FamUser(Passport, Id);
            fu.UserName = txtUser.Text;
            fu.Email = txtEmail.Text;
            fu.FirstName = txtFirstName.Text;
            fu.LastName = txtLastName.Text;
            fu.BirthDate = ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
            //if (rdbMan.Checked)
            //    fu.Gender = FamoBlock.enmGender.Male;
            //else if (rdbFemal.Checked)
            //    fu.Gender = FamoBlock.enmGender.Female;
           bool b = fu.Update();

            if (b)
            {
                //حفظ الجنسية
                FamContactProfile fcp = new FamContactProfile(Passport);
                DataTable dt = fcp.GetContacts((int)FamoBlock.enmObjectCode.Users, Id);
                if (dt.Rows.Count > 0)
                {
                    fcp.ID = Convert.ToInt32(dt.Rows[0]["contact_id"]);
                    fcp.Nationality.ID = Convert.ToInt32(ddlNationality.SelectedValue);
                    fcp.Update();
                }
                else
                {
                    fcp.Nationality.ID = Convert.ToInt32(ddlNationality.SelectedValue);
                    fcp.ObjectCode = FamoBlock.enmObjectCode.Users;
                    fcp.ObjectId = Id;
                    fcp.Save();
                }
            }
        }

        private void BindNationality()
        {
            FamList FL = new FamList(Passport);
            ddlNationality.DataSource = FL.GetListsByName(FamoLibrary.FamoBlock.Const_List_Nationality);
            ddlNationality.DataValueField = "list_id";
            ddlNationality.DataTextField = "text";
            ddlNationality.DataBind();
            ddlNationality.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlNationality.SelectedValue = "0";
        }
    }
}
