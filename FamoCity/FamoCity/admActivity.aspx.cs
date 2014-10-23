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
    public partial class admActivity : System.Web.UI.Page
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
                    Session["target"] = "/admin/Activity";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Activity";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);

            if (!IsPostBack)
            {
                ClassMain.FillLanguageselected(ddlSelectLang);
                ClassMain.FillLanguage(ddlLang);

                txtAativity.Focus();

                showMainAativity();
                ClassMain.FillActivityTree(ref tvActivity, 0,Passport.Language_ID, true);

                if (id > 0)
                    showActivity(id);

            }
        }


        private void showActivity(int id)
        {
            FamActivity act = new FamActivity(Passport, id);
            txtAativity.Text = act.Name;
            ddlLang.SelectedValue = act.LanguageID.ToString();
            txtDescr.Text = act.Description;
            ddlMainActivity.SelectedValue = act.ParentID.ToString();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamActivity fa;
            if (id > 0)
                fa = new FamActivity(Passport, id);
            else
                fa = new FamActivity(Passport);

            fa.Name = txtAativity.Text;
            fa.LanguageID = Convert.ToInt32(ddlLang.SelectedValue);
            fa.Description = txtDescr.Text;
            fa.ParentID = Convert.ToInt32(ddlMainActivity.SelectedValue);

            if (id > 0)
            {
                fa.Update();
                lblMsg.Text = "تم  التعديل بنجاح ";

            }
            else
            {
                fa.Save();

                Response.Redirect("/admin/Activity/" + fa.ID);
            }

        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {

            FamActivity fa = new FamActivity(Passport);

            if (id > 0 && Convert.ToInt32(ddlLang.SelectedValue) > 0)
            {
                DataTable dt = fa.GetActivities(id, Convert.ToInt32(ddlLang.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    txtAativity.Text = dt.Rows[0]["text"].ToString();
                    txtDescr.Text = dt.Rows[1]["text"].ToString();
                }

                else
                {
                    txtAativity.Text = "";
                    txtDescr.Text = "";

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FamActivity fa;
            if (id > 0)
                fa = new FamActivity(Passport, id);
            else
                fa = new FamActivity(Passport);

            if (id > 0)
            {
                fa.Delete();
                lblMsg.Text = "تم الحذف بنجاح ";

                Response.Redirect("/admin/Activity");

            }
        }

        private void showgroup()
        {
            FamActivity fa = new FamActivity(Passport);
            ddlMainActivity.DataSource = fa.GetActivities();
            ddlMainActivity.DataValueField = "parent_id";
            ddlMainActivity.DataTextField = "text";
            ddlMainActivity.DataBind();
            ddlMainActivity.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlMainActivity.SelectedValue = "0";
        }

        private void showMainAativity()
        {
            FamActivity fa = new FamActivity(Passport);
            ddlMainActivity.DataSource = fa.GetActivitiesByLanguage(Passport.Language_ID, "name");
            ddlMainActivity.DataValueField = "actv_id";
            ddlMainActivity.DataTextField = "text";
            ddlMainActivity.DataBind();
            ddlMainActivity.Items.Add(new ListItem("-نشاط رئيسي -", "0"));
            ddlMainActivity.SelectedValue = "0";

        }

        protected void btnNewActivity_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Activity");
        }

        protected void ddlSelectLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            FamActivity fa = new FamActivity(Passport);
            if (Convert.ToInt32(ddlSelectLang.SelectedValue) > 0)
            {
                ClassMain.FillActivityTree(ref tvActivity, 0,Convert.ToInt32( ddlSelectLang.SelectedValue), true);
               
            }

        }
    }
}