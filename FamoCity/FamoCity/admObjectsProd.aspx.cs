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
    public partial class admObjectsProd : System.Web.UI.Page
    {

        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "admin/Object";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/admin/Object";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");
            if (!IsPostBack)
            {
                FillShopList();
                FillScenes();
                FillActivityList();
            }


            if (!IsPostBack)
            {
                RadDropDownTree1.DataFieldID = "ID";
                RadDropDownTree1.DataFieldParentID = "ParentID";
                RadDropDownTree1.DataValueField = "Value";
                RadDropDownTree1.DataTextField = "Text";
                RadDropDownTree1.DataSource = GetData();
                RadDropDownTree1.DataBind();

                
            }
   
        }
        public DataTable GetData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("ParentID");
            table.Columns.Add("Value");
            table.Columns.Add("Text");

            table.Rows.Add(new String[] { "1", null, "World_Continents", "World Continents" });
            table.Rows.Add(new String[] { "2", null, "World_Oceans", "World Oceans" });

            table.Rows.Add(new String[] { "3", "1", "Asia", "Asia" });
            table.Rows.Add(new String[] { "4", "1", "Africa", "Africa" });
            table.Rows.Add(new String[] { "5", "1", "Australia", "Australia" });
            table.Rows.Add(new String[] { "6", "1", "Europe", "Europe" });
            table.Rows.Add(new String[] { "7", "1", "North_America", "North America" });
            table.Rows.Add(new String[] { "8", "1", "South_America", "South America" });

            table.Rows.Add(new String[] { "9", "2", "Arctic_Ocean", "Arctic Ocean" });
            table.Rows.Add(new String[] { "10", "2", "Atlantic_Ocean", "Atlantic Ocean" });
            table.Rows.Add(new String[] { "11", "2", "Indian_Ocean", "Indian Ocean" });
            table.Rows.Add(new String[] { "12", "2", "Pacific_Ocean", "Pacific Ocean" });
            table.Rows.Add(new String[] { "13", "2", "South_Ocean", "SouthOcean" });

            return table;
        }    
        protected int saveNewProduct()
        {

            //if (!IsValid) return 0;
            FamProduct p = new FamProduct(Passport);
            p.Name = ProductName.Text;
            p.Description = Description.Text;
            p.Catagory.ID = Convert.ToInt32(Category.Text);
            // p.Brand.ID = Convert.ToInt32(ddlBrand.SelectedValue);
            p.NewPrice = Convert.ToDecimal(NewPrice.Text);
            p.OldPrice = Convert.ToInt32(OldPrice.Text);
            //    p.Discount = Convert.ToDecimal((txtDiscount.Text.Trim() == "") ? "0" : txtDiscount.Text);
            p.Shop.ID = Convert.ToInt32(ShopList.SelectedValue);// Passport.ShopID;

            p.UseIn3d = true;
            int Id = p.Save();
            ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Products, Id);

            FamShop shop = new FamShop(Passport, Convert.ToInt32(ShopList.SelectedValue));
            int[] fil = ClassMain.UploadImage(shop.folder.ToString(), AsyncUpload2, FamoBlock.enmObjectCode.Objects, 0);
            // FamObject fob = new FamObject(Passport, objid);
            foreach (int filid in fil)
            {
                FamFile objfiles = new FamFile(Passport, filid);
                FamObject fobj = new FamObject(Passport);
                fobj.Trigger.ID = Convert.ToInt32(Trigger.SelectedValue);
                fobj.Version = (float)Convert.ToDecimal(Version.Text);
                fobj.File.ID = filid;
                fobj.ObjectCode = FamoBlock.enmObjectCode.Products;
                fobj.ObjectId = Id;
                fobj.Code = objfiles.Name;
                int objid = fobj.Save();

            }
            return Id;
        }
        void FillShopList()
        {
            ClassMain.FillShop(ShopList);
        }

        void FillActivityList()
        {
            ClassMain.FillActivities(Category);
        }

        void FillTriggerList()
        {
            ClassMain.FillTrigger(Convert.ToInt32(Scenes.SelectedValue), Trigger);
        }

        void FillScenes()
        {
            ClassMain.FillScenes(Scenes);
        }

        protected void Scenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Scenes.SelectedValue) > 0)
                FillTriggerList();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            saveNewProduct();
        }
    }
}