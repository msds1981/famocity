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
    public partial class admNews : System.Web.UI.Page
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
                    Session["target"] = "/admin/News";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/News";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);


            if (!IsPostBack)
            {
                ClassMain.FillLanguage(ddlLang);
                ClassMain.FillLanguageselected(ddlGvlang);
             
                bindgvlist();
                if (id > 0)
                    bindDataNews(id);

            }
        }
        private void bindgvlist()
        {
            FamNews fl = new FamNews(Passport);
            gvNews.DataSource = fl.GetNewsByLanguage((Convert.ToInt32(ddlGvlang.SelectedValue)));
            gvNews.DataBind();
        }

        private void bindDataNews(int id)
        {
            FamNews fn = new FamNews(Passport, id);
            DataTable dt = fn.GetNews(id);
            if (dt.Rows.Count > 0)
            {
                fn.ID = Convert.ToInt32(dt.Rows[0]["new_id"]);
                txtDescription.Text = fn.Description;
                ddlLang.SelectedValue = fn.Language.ID.ToString();
                int target = Convert.ToInt32(fn.Target);
                ddlUserType.SelectedValue = target.ToString();
            }
        }
     
    
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamNews fn;
            if (id > 0)
                fn = new FamNews(Passport, id);
            else
                fn = new FamNews(Passport);

            fn.Description = txtDescription.Text;
            fn.Language.ID = Convert.ToInt32(ddlLang.SelectedValue);
            fn.Target = (FamoBlock.enmUserType)Convert.ToInt32(ddlUserType.SelectedValue);


            if (id > 0)
            {
                fn.Update();
                bindgvlist();
            }
            else
            {
                fn.Save();
                bindgvlist();
                Response.Redirect("/admin/News/" + fn.ID);
            }

        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            FamNews fm = new FamNews(Passport);

            if (id > 0 && Convert.ToInt32(ddlLang.SelectedValue) > 0)
            {
                DataTable dt = fm.GetNews(id, Convert.ToInt32(ddlLang.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    txtDescription.Text = dt.Rows[0]["text"].ToString();
                    ddlUserType.SelectedValue = dt.Rows[0]["target"].ToString();
                }
                else
                {

                    txtDescription.Text = "";
                    ddlUserType.SelectedValue = "0";
                }
            }
            bindgvlist();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FamNews fn;
            if (id > 0)
                fn = new FamNews(Passport, id);
            else
                fn = new FamNews(Passport);

            if (id > 0)
            {
                fn.Delete();
                bindgvlist();
                Response.Redirect("/admin/News");
             
            }

        }

        protected void gvNews_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNews.PageIndex = e.NewPageIndex;
            bindgvlist(); ;

        }


        protected void ddlGvlang_SelectedIndexChanged(object sender, EventArgs e)
        {
            FamNews fm = new FamNews(Passport);

            if (Convert.ToInt32(ddlGvlang.SelectedValue) > 0)
            {
                DataTable dt = fm.GetNewsByLanguage((Convert.ToInt32(ddlGvlang.SelectedValue)));
                if (dt.Rows.Count > 0)
                {
                    gvNews.DataSource = dt;
                    gvNews.DataBind();


                }
                else

                    bindgvlist();
               }


        }

        protected void btnNewNews_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/News");
        }


    }
}