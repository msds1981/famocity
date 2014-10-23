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
    public partial class admPermissionadd : System.Web.UI.Page
    {
        private int nodesCount;
        private int[] nodesID;
        private int[] nodesParentID;
        private string[] nodesText;
        int id = 0;
        FamoPassport Passport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] != null)
            {
                Passport = (FamoPassport)Session["passport"];
                if (!Passport.Logged)
                {
                    Session["target"] = "/admin/admPermation";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/admin/admPermation";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (RouteData.Values["id"] != null)
                id = Convert.ToInt32(RouteData.Values["id"]);
            if (!IsPostBack)
            {
                ClassMain.FillLanguage(ddlLang);
               
                ShowParent();

                FillTree(ref trePerm, 0);
                if (id > 0)
                {
                    showUserPermissionData(id);

                }
            }
        }

    


        private void ShowParent()
        {
            FamPermission Fp = new FamPermission(Passport);
            ddlParent.DataSource = Fp.GetPermissionsByEndNode(false, Passport.Language_ID, "title");
            ddlParent.DataValueField = "perm_id";
            ddlParent.DataTextField = "text";
            ddlParent.DataBind();
            ddlParent.Items.Add(new ListItem("Root", "0"));
            ddlParent.SelectedValue = "0";
        }
        private void showUserPermissionData(int id)
        {
            FamPermission fp = new FamPermission(Passport, id);
            ddlLang.SelectedValue = fp.Language.ID.ToString();
            txtPermation.Text = fp.Title;
            ddlParent.SelectedValue = fp.Parent.ToString();
        }

        private void FillTree(ref TreeView tree, int MasterID = 0)//this function to Fill TreeView With Nodes
        {
            nodesCount = 0;
            nodesID = null;
            nodesParentID = null;
            nodesText = null;
            //() As String
            trePerm.Nodes.Clear();

            FamPermission ins = new FamPermission(Passport);


            DataTable DT = new DataTable();

            DT = ins.GetPermissionsByLanguage(Passport.Language_ID, "title");

            //() As String
            try
            {
                foreach (DataRow drTree in DT.Rows)
                {
                    Array.Resize(ref nodesID, nodesCount + 1);
                    nodesID[nodesCount] = Convert.ToInt32(drTree["perm_id"]);
                    Array.Resize(ref nodesParentID, nodesCount + 1);
                    nodesParentID[nodesCount] = Convert.ToInt32(drTree["parent_id"]);
                    Array.Resize(ref nodesText, nodesCount + 1);
                    nodesText[nodesCount] = "<a href='/admin/admPermation/" + Convert.ToInt32(drTree["perm_id"]) + "'>" + Convert.ToString(drTree["text"]) + "</>";
                    nodesCount += 1;
                }
                RecurTree(tree, null, MasterID);

            }
            catch (Exception ex) { }
        }
        private void RecurTree(TreeView tree, TreeNode parentNode, int parentID) // this function call it self and create the nods
        {
            for (int i = 0; i <= nodesCount - 1; i++)
            {

                if (nodesParentID[i] == parentID)
                {
                    TreeNode tmpNode = new TreeNode(nodesText[i], nodesID[i].ToString());
                    tmpNode.Checked = false;
                    tmpNode.SelectAction = TreeNodeSelectAction.None;

                    RecurTree(tree, tmpNode, nodesID[i]);
                    if (parentNode == null)
                    {
                        tree.Nodes.Add(tmpNode);
                    }
                    else
                    {
                        parentNode.ChildNodes.Add(tmpNode);

                    }
                }

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FamPermission fp;
            if (id > 0)
                fp = new FamPermission(Passport, id);
            else
                fp = new FamPermission(Passport);

            fp.Language.ID = Convert.ToInt32(ddlLang.SelectedValue);
            fp.Title = txtPermation.Text;
            fp.Parent = Convert.ToInt32(ddlParent.SelectedValue);

            if (id > 0)
            {

                fp.Update();
                FillTree(ref trePerm, 0);
            }

            else
            {
                fp.Save();
                txtPermation.Text = "";
                ddlLang.SelectedValue = "0";
                ddlParent.SelectedValue = "0";
                FillTree(ref trePerm, 0);

            }
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {

            FamPermission fp = new FamPermission(Passport);

            if (id > 0 && Convert.ToInt32(ddlLang.SelectedValue) > 0)
            {
                DataTable dt = fp.GetPermissions(id, Convert.ToInt32(ddlLang.SelectedValue));
                if (dt.Rows.Count > 0)
                {

                    txtPermation.Text = dt.Rows[0]["text"].ToString();
                    ddlParent.SelectedValue = dt.Rows[0]["parent_id"].ToString();

                }

                else
                {
                    txtPermation.Text = "";
                    ddlParent.SelectedValue = "0";

                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FamPermission fp;
            if (id > 0)
                fp = new FamPermission(Passport, id);
            else
                fp = new FamPermission(Passport);

            if (id > 0)
            {

                fp.Delete();
                txtPermation.Text = "";
                ddlParent.SelectedValue = "0";
                ddlLang.SelectedValue = "0";
                FillTree(ref trePerm, 0);
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/admPermation");
        }
    }
}