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
    public partial class admPermissionShow : System.Web.UI.Page
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
                    Session["target"] = "/admin/admPermissionshow";
                    Response.Redirect("/login");
                }

            }
            else
            {
                Session["target"] = "/admin/admPermissionshow";
                Response.Redirect("/login");
            }

            if (Passport.UserType != FamoBlock.enmUserType.Managment)
                Response.Redirect("/login");

            if (!IsPostBack)
            {
                FillTree(ref trePerm, 0);
            }


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
                    nodesText[nodesCount] = "<a href='/admin/admPermation/" + Convert.ToInt32(drTree["perm_id"]) + "'>" + Convert.ToString(drTree["text"]) + "</>";
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/admPermation");

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/admPermation");
        }
    }
}