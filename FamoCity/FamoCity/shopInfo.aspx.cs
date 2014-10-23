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
    public partial class shopInfo : System.Web.UI.Page
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
                    Session["target"] = "Shop/edit/Info";
                    Response.Redirect("/Login");
                }

            }
            else
            {
                Session["target"] = "Shop/edit/Info";
                Response.Redirect("/Login");
            }

            FamShop s = new FamShop(Passport, Passport.ShopID);
            if (s.Owner.ID != Passport.UserID)
                Response.Redirect("/Login");
            else
            {
                DataTable dt = s.GetUserShops(Passport.UserID);
                if (dt.Rows.Count > 0)
                {
                    Id = Convert.ToInt32(dt.Rows[0]["shop_id"]);
                }
            }

            if (!IsPostBack)
            {
                BindCountry();
                showDataCompny(Convert.ToInt32(Id));
            }

        }

        private void showDataCompny(int Shopid)
        {
            // استعراض بيانات الشركة
            FamShop FS = new FamShop(Passport, Shopid);
            if (FS.ID > 0)
            {
                txtCompany.Text = FS.Name;
                txtAboutCompany.Text = FS.About;
                txtWebName.Text = FS.WebName;
                //استعراض عنواين الشركة
                FamContactProfile FCP = new FamContactProfile(Passport, Id);
                DataTable dt = FCP.GetContacts((int)FamoBlock.enmObjectCode.Shops, Id);

                if (dt.Rows.Count > 0)
                {
                    txtCompanyAddress.Text = dt.Rows[0]["address"].ToString();
                    txtEmail.Text = dt.Rows[0]["email"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["country"].ToString();
                }
                // استعراض بيانات الموبايل 
                ShowPhoneData(ref txtJawal, FamoBlock.enmPhoneType.Mobile);
                // بيانات الفاكس
                ShowPhoneData(ref txtFax, FamoBlock.enmPhoneType.Fax);
                // بيانات الهاتف
                ShowPhoneData(ref txtPhone, FamoBlock.enmPhoneType.Phone);
            }

        }

        private void ShowPhoneData(ref TextBox Text, FamoBlock.enmPhoneType type)
        {
            FamPhoneProfile FPP = new FamPhoneProfile(Passport);

            DataTable dt = FPP.GetPhoneProfiles(Convert.ToInt32(FamoBlock.enmObjectCode.Shops), Id, type);
            if (dt.Rows.Count > 0)
                Text.Text = dt.Rows[0]["number"].ToString();

        }


        private void BindCountry()
        {
            FamList FL = new FamList(Passport);
            ddlCountry.DataSource = FL.GetListsByName(FamoLibrary.FamoBlock.Const_List_Country);
            ddlCountry.DataValueField = "list_id";
            ddlCountry.DataTextField = "text";
            ddlCountry.DataBind();
            ddlCountry.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlCountry.SelectedValue = "0";
        }
        protected bool checkAvailability(string webName)
        {
            bool availability = true;
            FamShop shp = ClassMain.GetShop(txtWebName.Text);
            if (shp.ID > 0)
            {
                availability = false;
            }

            return availability;
        }
        protected void addIntoCompany_Click(object sender, EventArgs e)
        {
            // حفظ اسم الشركة وتفاصيلها 
            bool save;
            if (checkAvailability(txtWebName.Text))
            {
                save = true;
            }
            else
            {
                FamShop org = new FamShop(Passport, Passport.ShopID);
                if (org.WebName.Equals(txtWebName.Text))
                {
                    save = true;
                }
                else
                {
                    save = false;
                }
            }
            if (save)
            {
                FamShop FS = new FamShop(Passport);

                FS.ID = Id;
                FS.Name = txtCompany.Text;
                FS.About = txtAboutCompany.Text;
                if (FS.Update())
                {

                    // حفظ بيانات عنوان الشركة
                    FamContactProfile FCP = new FamContactProfile(Passport);
                    DataTable dt = FCP.GetContacts((int)FamoBlock.enmObjectCode.Shops, Id);
                    if (dt.Rows.Count > 0)
                    {
                        FCP.ID = Convert.ToInt32(dt.Rows[0]["contact_id"]);
                        FCP.Email = txtEmail.Text;
                        FCP.Country.ID = int.Parse(ddlCountry.SelectedValue);
                        FCP.Address = txtCompanyAddress.Text;
                        FCP.Update();
                    }
                    else
                    {
                        FamContactProfile FCPs = new FamContactProfile(Passport);
                        FCPs.Email = txtEmail.Text;
                        FCPs.Country.ID = int.Parse(ddlCountry.SelectedValue);
                        FCPs.ObjectCode = FamoBlock.enmObjectCode.Shops;
                        FCPs.ObjectId = Id;
                        FCP.Address = txtCompanyAddress.Text;
                        FCPs.Save();
                    }
                    // حفظ بيانات الموبايل 
                    SavePhoneData(txtJawal, FamoBlock.enmPhoneType.Mobile);
                    // حفظ بيانات الفاكس 
                    SavePhoneData(txtFax, FamoBlock.enmPhoneType.Fax);
                    // حفظ بيانات الهاتف
                    SavePhoneData(txtPhone, FamoBlock.enmPhoneType.Phone);
                }
            }
        }

        private void SavePhoneData(TextBox Text, FamoBlock.enmPhoneType type)
        {
            FamPhoneProfile FPP = new FamPhoneProfile(Passport);

            DataTable dt = FPP.GetPhoneProfiles(Convert.ToInt32(FamoBlock.enmObjectCode.Shops), Id, type);
            if (dt.Rows.Count > 0)
            {
                //update
                FPP.ID = Convert.ToInt32(dt.Rows[0]["phon_id"]);
                FPP.Number = Text.Text;
                FPP.Update();
            }
            else
            {
                //save
                FamPhoneProfile FPPs = new FamPhoneProfile(Passport);

                FPPs.Number = Text.Text;
                FPPs.ObjectCode = FamoBlock.enmObjectCode.Shops;
                FPPs.ObjectId = Id;
                FPPs.Type = type;

                FPPs.Save();
            }
        }
        protected void validationLabel()
        {
            if (txtWebName.Text.Trim().Equals(""))
            {
                lblAvailable.Text = "الرجاء اختيار الإسم";
                lblAvailable.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                if (checkAvailability(txtWebName.Text))
                {
                    lblAvailable.Text = "متوفر";
                    lblAvailable.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblAvailable.Text = "غير متوفر";
                    lblAvailable.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void btnAvailable_Click(object sender, EventArgs e)
        {
            validationLabel();
        }
    }
}