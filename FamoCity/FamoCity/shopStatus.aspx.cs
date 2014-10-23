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
    public partial class shopStatus : System.Web.UI.Page
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
                    Session["target"] = "Shop/edit/Status";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/Status";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Shop)
                Response.Redirect("/login");
            else
            {
                FamUserStatus FUS = new FamUserStatus(Passport);
                DataTable dt = FUS.GetAllUserStatus(Passport.UserID);
                if (dt.Rows.Count > 0)
                    Id = Convert.ToInt32(dt.Rows[0]["stat_id"]);
           }

            if (!IsPostBack)
            {
                ShowUserData(Id);
            }
        }

        private void ShowUserData(int Id)
        {
            FamUserStatus FUS = new FamUserStatus(Passport,Id);
            DataTable dt = FUS.GetAllUserStatus(Passport.UserID);
            if (dt.Rows.Count > 0)
            FUS.ID =Convert.ToInt32(dt.Rows[0]["stat_id"]);
            txtDescriptipn.Text = FUS.Status;
       }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FamUserStatus FUS;
            if (Id > 0)
                FUS = new FamUserStatus(Passport, Id);
            else
            FUS = new FamUserStatus(Passport);

            FUS.Status = txtDescriptipn.Text;
            FUS.User.ID = Passport.UserID;

            if (Id > 0)
            {

                FUS.Update();
            }
            else
            {
                FUS.Save();
            }
        }

    }
}