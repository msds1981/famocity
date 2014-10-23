using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using System.Data;
using System.Text.RegularExpressions;
namespace FamoCity
{
    public partial class usrRegStep2 : System.Web.UI.Page
    {
        int id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged || Passport.UserType != FamoBlock.enmUserType.User) Response.Redirect("/login");
                id = Passport.UserID;
            }
            else {
                Response.Redirect("/login");

            }
            if (!IsPostBack)
            {
                BindCountry();
                if(Passport.Logged)
                loadData();
            }
        }

        protected void loadData()
        {
            // Name
            FamUser FU = new FamUser(Passport, id);
            txtFirsName.Text = FU.FirstName;
            txtLastName.Text = FU.LastName;

            //Phone data
            FamPhoneProfile FPF = new FamPhoneProfile(Passport);
            int objCode = (int)FamoBlock.enmObjectCode.Users;
            DataTable dt = FPF.GetPhoneProfiles(objCode, id, FamoBlock.enmPhoneType.Mobile);
            foreach (DataRow dr in dt.Rows)
            {
                txtNumberPhone.Text = dr["number"].ToString();
            }

            //Country Data
            FamContactProfile fp = new FamContactProfile(Passport);
            DataTable dt1 = fp.GetContacts(objCode, id);
            foreach (DataRow dr in dt1.Rows)
            {
                ddlCountry.SelectedValue = dr["country"].ToString();
            }
        }

        protected void LinkNext_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return; 

            FamUser FU = new FamUser(Passport,id);
          
            FU.FirstName = txtFirsName.Text;
            FU.LastName = txtLastName.Text;

            FU.Update();
            
            //حفظ بيانات الجوال
            FamPhoneProfile FPF = new FamPhoneProfile(Passport);
            FPF.Number = txtNumberPhone.Text;
            FPF.Type = FamoBlock.enmPhoneType.Mobile;
            FPF.ObjectCode = FamoBlock.enmObjectCode.Users;
            FPF.ObjectId = id;
            FPF.Save();

           // حفظ الدولة
            FamContactProfile fp = new FamContactProfile(Passport);
            fp.Country.ID = Convert.ToInt32(ddlCountry.SelectedValue);
            fp.ObjectCode = FamoBlock.enmObjectCode.Users;
            fp.ObjectId = id;
            fp.Save();

            SetStep("/step3");
            Response.Redirect("/step3");
        }

        private void SetStep(string pageStep)
        {
            FamOption op = new FamOption(Passport);
            op.SetValue(FamoBlock.Const_Option_Usr_StepsRegister + Passport.UserID.ToString(), pageStep);
        }

        private void BindCountry()
        {
            FamList FL = new FamList(Passport);
            ddlCountry.DataSource = FL.GetListsByName(FamoLibrary.FamoBlock.Const_List_Country,Passport.Language_ID);
            ddlCountry.DataValueField = "list_id";
            ddlCountry.DataTextField = "text";
            ddlCountry.DataBind();
            ddlCountry.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlCountry.SelectedValue = "0";
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            /*
            Regex regexObj = new Regex(@"^\+\d");//-\d{3}\-\d{3}\-\d{4}$

            if (regexObj.IsMatch(txtNumberPhone.Text))
            {
                string formattedPhoneNumber =
                    regexObj.Replace(txtNumberPhone.Text, "($1) $2-$3");
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }*/
        }
    }
}