<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admRoles.aspx.cs" Inherits="FamoCity.admRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
  

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        الادوار والوظائف</h1>
    <asp:Label ID="lblGroup" runat="server" Text="المجموعة"></asp:Label>
    <asp:DropDownList ID="ddlGroup" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnNewActivity" runat="server" OnClick="btnNewActivity_Click" Text="اضافة جديدة "
        CausesValidation="False" />
    <asp:TreeView ID="trePerm" runat="server" ShowCheckBoxes="All" ShowLines="True">
    </asp:TreeView>
</asp:Content>
