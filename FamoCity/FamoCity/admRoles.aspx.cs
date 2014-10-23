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
    public partial class admRoles : System.Web.UI.Page
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
                    Session["target"] = "/admin/admRoles";
                    Response.Redirect("/login");
                }
            }
            else
            {
                Session["target"] = "/admin/admRoles";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");
            FamGroup fg = new FamGroup(Passport);
            DataTable dt = fg.GetGroups();
            if (dt.Rows.Count > 0)

                id = Convert.ToInt32(dt.Rows[0]["grp_id"]);
            if (!IsPostBack)
            {

                showgroup();
                FillTree(ref trePerm, 0);
                showRoles(id);
            }
        }
        private void showRoles(int id)
        {


            FamRole fr = new FamRole(Passport);

            ddlGroup.SelectedValue = fr.Group.ID.ToString();

            DataTable dt = fr.GetRoles();
            if (dt.Rows.Count > 0)

                for (int i = 0; i < dt.Rows.Count-1; i++)
                {
                    foreach (TreeNode abc in trePerm.Nodes)
                    {
                        if (abc.Value == dt.Rows[i]["rol_id"].ToString())
                            abc.Selected = true;
                          abc.Checked = fr.Deserv;
                    }

                }
        }

        private void showgroup()
        {

            FamGroup Fg = new FamGroup(Passport);

            ddlGroup.DataSource = Fg.GetGroups();
            ddlGroup.DataValueField = "grp_id";
            ddlGroup.DataTextField = "name";
            ddlGroup.DataBind();
            ddlGroup.Items.Add(new ListItem("- اختر من القائمة -", "0"));
            ddlGroup.SelectedValue = "0";
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
            //  EscNeedsDAL Acc = new EscNeedsDAL(pass);

            DataTable DT = new DataTable();
            // DT = Acc.getNeeds();
            DT = ins.GetPermissionsByLanguage(Passport.Language_ID, "title");

            //() As String
            try
            {
                foreach (DataRow drTree in DT.Rows)
                {
                    Array.Resize(ref nodesID, nodesCount + 1);
                    nodesID[nodesCount] = Convert.ToInt32(drTree["perm_id"]);
                    //nodesID[nodesCount] = Convert.ToInt32(drTree["need_id"]);
                    Array.Resize(ref nodesParentID, nodesCount + 1);
                    nodesParentID[nodesCount] = Convert.ToInt32(drTree["parent_id"]);
                    Array.Resize(ref nodesText, nodesCount + 1);
                    nodesText[nodesCount] = Convert.ToString(drTree["text"]) + "</>";
                    nodesCount += 1;
                }
                RecurTree(tree, null, MasterID);

            }
            catch (Exception exx) { }
        }
        private void RecurTree(TreeView tree, TreeNode parentNode, int parentID)// this function call it self and create the nods
        {
            for (int i = 0; i <= nodesCount - 1; i++)
            {

                if (nodesParentID[i] == parentID)
                {
                    TreeNode tmpNode = new TreeNode(nodesText[i], nodesID[i].ToString());
                    //tmpNode.Checked = false;

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
            //Node(0);
         FamRole fr = new FamRole(Passport);

            int grpid = Convert.ToInt32(ddlGroup.SelectedValue);
            DataTable dt = fr.GetRoles();
            int i = 0;
                foreach (TreeNode abc in trePerm.SelectedNode.ChildNodes)
                {
                    DataTable dtg = fr.GetRolesByGroup(grpid, Convert.ToInt32(abc.Value));
                    if (dtg.Rows.Count > 0)
                        fr = new FamRole(Passport, Convert.ToInt32(dt.Rows[0]["rol_id"]));
                    else
                        fr = new FamRole(Passport);
                    fr.Permission.ID = Convert.ToInt32(abc.Value);
                    fr.Group.ID = Convert.ToInt32(ddlGroup.SelectedValue);
                    fr.Deserv = abc.Checked;
                    i++;
                    if (fr.ID > 0)
                        fr.Update();
                    else
                        fr.Save();
                }
            
        }
        int i = 0;

        protected void btnNewActivity_Click(object sender, EventArgs e)
        {

        }
        //private void Node(int id) {
        //    i++;
            
        //    foreach (TreeNode abc in trePerm.Nodes[id].ChildNodes)
        //    {
        //        Node(i);
        //        Console.WriteLine(abc.Value);
        //    }
        //}

        //private void Save() { 
        
        //}
    }

}
