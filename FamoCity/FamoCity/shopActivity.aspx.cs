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
    public partial class shopActivity : System.Web.UI.Page
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
                    Session["target"] = "/Shop/edit/Activities";
                    Response.Redirect("/login");
                }
                
            }
            else
            {
                Session["target"] = "/Shop/edit/Activities";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Shop)
                Response.Redirect("/login");

            FamShop s = new FamShop(Passport);

            DataTable dt = s.GetUserShops(Passport.UserID);
            if (dt.Rows.Count > 0)
            {
                Id = Convert.ToInt32(dt.Rows[0]["shop_id"]);
            }


            if (!IsPostBack)
            {
                BindCompanyActivity();
                if (Id > 0)
                    showActivity(Id);
            }
        }
        private void BindCompanyActivity()
        {
            //هذه الدالة تقوم بتعبئة القائمة بالقيم من قاعدة البيانات 

            FamActivity Fa = new FamActivity(Passport);

            ckblActivity.DataSource = Fa.GetActivitiesByCol("name");
            ckblActivity.DataValueField = "actv_id";
            ckblActivity.DataTextField = "text";
            ckblActivity.DataBind();
        }
        private void showActivity(int Id)
        {
            FamShopInterest fs = new FamShopInterest(Passport);

            DataTable dt = fs.GetShopInterests(Id);
          
            if (dt.Rows.Count > 0)

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (ListItem abc in ckblActivity.Items)
                    {
                        if (abc.Value == dt.Rows[i]["actv_id"].ToString())
                            abc.Selected = true;
                    }
                }
               
        }
        protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            int cnt = 0;

            for (int i = 0; i < ckblActivity.Items.Count; i++)
            {
                if (ckblActivity.Items[i].Selected)
                {
                    cnt++;
                }
            }
            args.IsValid = (cnt == 0) ? false : true;
        }

        protected void Button1_Click(object sender, EventArgs e)
            
        {
            if (!IsValid)
                return;
            FamShopInterest FSI = new FamShopInterest(Passport);
             FSI.Clean(Id);
       
            foreach (ListItem abc in ckblActivity.Items)
            {
                if (abc.Selected)
                {
                    FSI.Shop.ID = Id;
                    FSI.Activity.ID = Convert.ToInt32(abc.Value);
                    FSI.Save();
                }
            }

        }
    }
}