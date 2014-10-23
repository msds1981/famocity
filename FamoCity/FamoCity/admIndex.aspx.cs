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
    public partial class admIndex : System.Web.UI.Page
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
                    Session["target"] = "/admin/Index";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Index";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                txtName.Focus();
                ClassMain.FillLanguage(ddlLang);
             
                if (id > 0)
                {
                    showIndexDate(id);
                }

            }
    }
  
        private void showIndexDate(int id)
        {
            FamIndex fi = new FamIndex(Passport, id);
            ddlLang.SelectedValue = fi.language.ID.ToString();
            txtName.Text = fi.EnmName;
            txtValue.Text =Convert.ToInt32(fi.EnmValue).ToString();
            TxtNameT.Text = fi.Name;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamIndex fi;
            if (id > 0)
                fi = new FamIndex(Passport, id);
            else
                fi = new FamIndex(Passport);

            fi.language.ID = Convert.ToInt32(ddlLang.SelectedValue);
            fi.EnmName = txtName.Text;
            fi.EnmValue =Convert.ToInt32(txtValue.Text);
            fi.Name = TxtNameT.Text;
            if (id > 0)
            {
                fi.Update();
                Response.Redirect("/admin/Index");
            }
            else
            {
                fi.Save();
                Response.Redirect("/admin/Index/" + fi.ID);
            }
            
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            FamIndex fm = new FamIndex(Passport);

            if (id > 0 && Convert.ToInt32(ddlLang.SelectedValue) > 0)
            {
                DataTable dt = fm.GetIndex(id, Convert.ToInt32(ddlLang.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    TxtNameT.Text = dt.Rows[0]["text"].ToString();
                   
                }
                else
                {
                    TxtNameT.Text = "";
                  

                }

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FamIndex fi;
            if (id > 0)
                fi = new FamIndex(Passport, id);
            else
                fi = new FamIndex(Passport);

            if (id > 0)
            {
                fi.Delete();
                Response.Redirect("/admin/CharIndexs");
            }
           
        }

        protected void btnAddIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Index");
          
        }
}}