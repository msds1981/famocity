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
    public partial class admAgree : System.Web.UI.Page
    {
        int id = 0;
        string type;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                if (RouteData.Values["type"] != null)
                    type = RouteData.Values["type"].ToString();
                else
                    Response.Redirect("/admin/main");

                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/Agree/" + type;
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/Agree/" + type;
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
              
               
                ClassMain.FillLanguageselected(ddlgvLang);
                ClassMain.FillLanguage(ddlLang);
              
              
                txtAgreement.Focus();
                if (id > 0 )
                showAgreenent(type, id);
                bindGvArgeement();
               
            }   
         
        }

        private void bindGvArgeement()
        {

            FamAgreement fa = new FamAgreement(Passport);
            gvAgreement.DataSource = fa.GetAgreemnets(getType(type), Convert.ToInt32(ddlgvLang.SelectedValue));
            gvAgreement.DataBind();
        }

        private FamoBlock.enmUserType getType(string type) {
                 FamAgreement fa = new FamAgreement(Passport);

                 if (type.ToLower() == "shop" || type.ToLower() == "2")
                     return FamoBlock.enmUserType.Shop;
                 else if (type.ToLower() == "user" || type.ToLower() == "1")
                     return FamoBlock.enmUserType.User;
            return 0;
        }

        private void showAgreenent(string type,int id)
        {
            FamAgreement fg = new FamAgreement(Passport, id);
            //DataTable dt = fg.GetAgreement(id, Convert.ToInt32(type));
            //if (dt.Rows.Count > 0)
            //{
                //ddlLang.SelectedValue = dt.Rows[0]["LanguageID"].ToString();
                //txtAgreement.Text = dt.Rows[0]["Text"].ToString();
            
            //}

                ddlLang.SelectedValue = fg.LanguageID.ToString();
                txtAgreement.Text = fg.Text;
          
          
        }

        protected void btnAddLang_Click(object sender, EventArgs e)
        {
            FamAgreement fm;
            if (id > 0)
                fm = new FamAgreement(Passport, id);
            else
                fm = new FamAgreement(Passport);
            fm.LanguageID = Convert.ToInt32(ddlLang.SelectedValue);
            if((type.ToLower() == "shop"))
            fm.Type =FamoBlock.enmUserType.Shop ;
             else if (type.ToLower() == "user" )
                  fm.Type =FamoBlock.enmUserType.User ;
       
            fm.Text = txtAgreement.Text;
            if (id > 0)
            {

                fm.Update();
                bindGvArgeement();
                Response.Redirect("/admin/Agree/" + type + "/" + fm.ID);
            }

            else
            {
                fm.Save();
                bindGvArgeement();
                Response.Redirect("/admin/Agree/" + type + "/" + fm.ID);

            }
          

        }

        protected void btnDeleteArgeement_Click(object sender, EventArgs e)
        {
            FamAgreement fm;
            if (id > 0)
                fm = new FamAgreement(Passport, id);

            else
                fm = new FamAgreement(Passport);

            if (id > 0)
            {

               fm.Delete();
               bindGvArgeement();
               Response.Redirect("/admin/Agree" + type);
            }

        }

        protected void gvAgreement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAgreement.PageIndex = e.NewPageIndex;
            bindGvArgeement();
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtAgreement.Text = "";
            FamAgreement fm = new FamAgreement(Passport);

            if (id > 0 && Convert.ToInt32(ddlLang.SelectedValue) > 0)
            {
                DataTable dt = fm.GetAgreements(id, Convert.ToInt32(ddlLang.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    txtAgreement.Text = dt.Rows[0]["text"].ToString();
                  
                }
                else
                {
                    txtAgreement.Text = "";
                }

            }
        }

        protected void btnNewAgree_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Agree" + "/" + type);
        }

        protected void ddlgvLang_SelectedIndexChanged(object sender, EventArgs e)
        {
           
             FamAgreement fm = new FamAgreement(Passport);
            if (Convert.ToInt32(ddlgvLang.SelectedValue) > 0)
            {
                DataTable dt = fm.GetAgreemnets(getType(type), Convert.ToInt32(ddlgvLang.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    gvAgreement.DataSource = dt;
                    gvAgreement.DataBind();
                }
                else
                    bindGvArgeement();
               }

             }

      
    }
}