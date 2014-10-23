using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class admLang : System.Web.UI.Page
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
                    Session["target"] = "/admin/Lang";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Lang";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                txtLangauge.Focus();
                bindgvlang();
                if (id > 0)
                    showLanguage(id);
            }
        }

        private void bindgvlang()
        {
            FamLanguage fl = new FamLanguage(Passport);
            GvLang.DataSource = fl.GetLanguages();
            GvLang.DataBind();
        }

        private void showLanguage(int id)
        {
            FamLanguage fl = new FamLanguage(Passport, id);

            if (fl.ID > 0)
            {
                txtLangauge.Text = fl.Name;
                txtCode.Text = fl.Code;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FamLanguage fl;
            if (id > 0)
                fl = new FamLanguage(Passport, id);
            else
                fl = new FamLanguage(Passport);

            fl.Name = txtLangauge.Text;
            fl.Code = txtCode.Text;

            if (id > 0)
            {
                fl.Update();
                bindgvlang();
                lblMsg.Text = "تم التعديل بنجاح";
            }
            else
            {
                fl.Save();
                bindgvlang();
                lblMsg.Text = "تم الحفظ بنجاح ";
                Response.Redirect("/admin/Lang/" + fl.ID);
               
            }
          
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FamLanguage fl;
            if (id > 0)
            {
                fl = new FamLanguage(Passport, id);
               
            }
            else
            {
                fl = new FamLanguage(Passport);
                
            }
            
            if (id > 0)
            {
               

                fl.Delete();
                Response.Redirect("/admin/Lang");
                bindgvlang();
            }
          
           

        }

        protected void addNewLang_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Lang");
        }
    }
}
