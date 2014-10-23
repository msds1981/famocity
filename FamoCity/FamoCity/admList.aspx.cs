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
    public partial class admList : System.Web.UI.Page
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
                    Session["target"] = "/admin/list/" + type;
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/admin/list/" + type;
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);

            if (!IsPostBack)
            {
                ClassMain.FillLanguage(ddlLang);
                ClassMain.FillLanguageselected(ddlLangSelect);
                bindgvlist();
                txtName.Focus();


                if (id > 0)
                bindDataList(type, id);
                bindgvlist();
            }

        }
        private void bindgvlist()
        {

            FamList fl = new FamList(Passport);

            //gvList.DataSource = fl.GetListsLanguageByLanguage(Convert.ToInt32(ddlLangSelect.SelectedValue));
            //gvList.DataBind();
            gvList.DataSource = fl.GetListsByName(type,Convert.ToInt32(ddlLangSelect.SelectedValue));
            gvList.DataBind();
        }
        //private string getListType(string ParamType)
        //{
        //    string s="" ;
        //    switch (ParamType)
        //    {
        //        case "nationality":
        //            s = FamoBlock.Const_List_Nationality;
        //            break;
        //        case "country":
        //            s = FamoBlock.Const_List_Country;
        //            break;
        //        case "pages":
        //            s = FamoBlock.Const_List_PagesFeed;
        //            break;
        //    }
        //    return s;
        //}
        private void bindDataList(string type, int id)
        {
            FamList fl = new FamList(Passport, id);
            DataTable dt = fl.GetList(id);
            if (dt.Rows.Count > 0)
            {
                fl.ID = Convert.ToInt32(dt.Rows[0]["list_id"]);
                txtName.Text = fl.Text;
                ddlLang.SelectedValue = fl.language.ID.ToString();

            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamList li;
            if (id > 0)
                li = new FamList(Passport, id);
            else
                li = new FamList(Passport);

            li.Text = txtName.Text;
            li.language.ID = Convert.ToInt32(ddlLang.SelectedValue);
            li.Name = type;


            if (id > 0)
            {
                li.Update();
                Response.Redirect("/admin/List/" + type + "/" + li.ID);
            }
            else  
            {
                li.Save();
                Response.Redirect("/admin/List/" + type +"/"+ li.ID);
            }
       
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {

            FamList fm = new FamList(Passport);

            if (id > 0 && Convert.ToInt32(ddlLang.SelectedValue) > 0)
            {
                DataTable dt = fm.GetListsLanguage(id, Convert.ToInt32(ddlLangSelect.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["text"].ToString();
                  
                }
                else
                {

                    txtName.Text = "";
                    // ddlList.SelectedValue = "0";
                }
            }
        }

        protected void btnListDelete_Click(object sender, EventArgs e)
        {
            string type = RouteData.Values["type"].ToString();
            FamList li;
            if (id > 0)
                li = new FamList(Passport, id);
            else
                li = new FamList(Passport);
            
            if (id > 0)

                bindDataList(type, id);
            {
                li.Delete();
                Response.Redirect("/admin/List/" + type);
            }

        }

        protected void btnNewList_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/List/" + type);
        }

        protected void ddlLangSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            FamList fm = new FamList(Passport);

            if (Convert.ToInt32(ddlLangSelect.SelectedValue) > 0)
            {
                DataTable dt = fm.GetListsByName(type ,Convert.ToInt32(ddlLangSelect.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    gvList.DataSource = dt;
                    gvList.DataBind();

                }
                else

                    bindgvlist();
            }
        }
    }
}