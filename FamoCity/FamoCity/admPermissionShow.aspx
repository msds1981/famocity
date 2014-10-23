<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admPermissionShow.aspx.cs" Inherits="FamoCity.admPermissionShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        المجموعات
    </h1>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="اضافة جديدة "
        CausesValidation="False" />
    <asp:TreeView ID="trePerm" runat="server" ShowLines="True" SkipLinkText="google.com"
        Font-Bold="False" Font-Size="10pt">
        <Nodes>
            <asp:TreeNode Text="New Node" Value="New Node">
                <asp:TreeNode NavigateUrl="~/admPermissionadd.aspx?id={0}" Text="New Node" Value="New Node">
                </asp:TreeNode>
            </asp:TreeNode>
        </Nodes>
    </asp:TreeView>
</asp:Content>
