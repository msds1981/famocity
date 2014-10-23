<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admPermissionadd.aspx.cs" Inherits="FamoCity.admPermissionadd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        مجموعة المستخدمين</h1>
    <asp:Label ID="lblLang" runat="server" Text="اللغة "></asp:Label>
    <asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlLang"
        Display="Dynamic" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="Label1" runat="server" Text="الرجاء اختيار اللغة"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblPermation" runat="server" Text="اسم الصلاحية "></asp:Label>
    <asp:TextBox ID="txtPermation" runat="server"></asp:TextBox><br />
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPermation"
        Display="Dynamic">
        <asp:Label ID="Label4" runat="server" Text="الرجاء ادخال اسم الصلاحية"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="القائمةا لابن "></asp:Label>
    <asp:DropDownList ID="ddlParent" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="حفظ " /><br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" OnClientClick="return confirm(&quot;هل انت متاكد من الحذف&quot; )"
        Text="حذف" CausesValidation="False" /><br />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="اضافة جديدة"
        CausesValidation="False" />
    <br />
    <br />
    <br />
    <br />
    <asp:TreeView ID="trePerm" runat="server" ShowLines="True">
    </asp:TreeView>
</asp:Content>
