<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn1.Master" AutoEventWireup="true" CodeBehind="AdmAddEditUser.aspx.cs" Inherits="FamoCity.AdmAddEditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    <asp:Label ID="lblUserName" runat="server" Text="اسم المستخدم"></asp:Label>
    <br />
    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
    <asp:Label ID="lblFirstName" runat="server" Text="الاسم الاول"></asp:Label>
    <br />
    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
    <asp:Label ID="lblLastName" runat="server" Text="الاسم الاخير "></asp:Label>
    <br />
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:Label ID="lblEmail" runat="server" Text="البريد الالكتروني"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlUserStutas" runat="server">
        <asp:ListItem Value="0">- اختر م القائمة</asp:ListItem>
        <asp:ListItem Value="1">غير نشط</asp:ListItem>
        <asp:ListItem Value="2">نشط</asp:ListItem>
        <asp:ListItem Value="3">محضور</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="lblUserStutas" runat="server" Text="حالة المستخدم "></asp:Label>
    <br />
    <asp:DropDownList ID="ddlUserType" runat="server">
        <asp:ListItem Value="0">- اختر من القائمة -</asp:ListItem>
        <asp:ListItem Value="1">User</asp:ListItem>
        <asp:ListItem Value="2">Shop</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="lblUserType" runat="server" Text="نوع المستخدم "></asp:Label>
    <br />
    <asp:TextBox ID="txtRegisDate" runat="server" style="margin-bottom: 0px"></asp:TextBox>
    <asp:Label ID="lblRegisDate" runat="server" Text="تاريخ التسجيل"></asp:Label>
    <br />
    <asp:TextBox ID="txtBirthDate" runat="server"></asp:TextBox>
    <asp:Label ID="lblBirthDate" runat="server" Text="تاريخ الميلاد"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlGender" runat="server">
        <asp:ListItem Value="0">- اختر من القائمة -</asp:ListItem>
        <asp:ListItem Value="1">ذكر </asp:ListItem>
        <asp:ListItem Value="2">انثى </asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="lblGender" runat="server" Text="الجنس"></asp:Label>
    <br />
    <br />
    <asp:DropDownList ID="ddlLang" runat="server">
    </asp:DropDownList>
    <asp:Label ID="lblLang" runat="server" Text="اللغة "></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="حفظ" />
    <asp:Button ID="btnDeleteUser" runat="server" onclick="btnDeleteUser_Click" Text="حذف" 
        onclientclick="return confirm(هل انت متاكد من الحذف)" />
    <br />
    <br />
    <br />
</asp:Content>
