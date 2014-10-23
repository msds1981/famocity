<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admActivity.aspx.cs" Inherits="FamoCity.admActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#573550"></asp:Label>
    <asp:Label ID="lblLang" runat="server" Text="اللغة"></asp:Label>
    <br />
    <br />
    <asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ddlLang" Display="Dynamic"
        runat="server" Operator="NotEqual" ValueToCompare="0" ValidationGroup="vgActivity">
        <asp:Label ID="lblcomVlang" runat="server" Text="الرجاء اختيار اللغة"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblactivity" runat="server" Text="النشاط "></asp:Label>
    <asp:TextBox ID="txtAativity" runat="server" Height="28px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNameActivity" runat="server" ControlToValidate="txtAativity"
        Display="Dynamic" ValidationGroup="vgActivity">
        <asp:Label ID="Label2" runat="server" Text="الرجاء كتابة النشاط"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="mainActivity" runat="server" Text="النشاط الرئيسي"></asp:Label>
    <asp:DropDownList ID="ddlMainActivity" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="lblactivityDescription" runat="server" Text="وصف النشاط "></asp:Label>
    <asp:TextBox ID="txtDescr" runat="server" Height="28px" TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvdescrActivity" runat="server" ControlToValidate="txtDescr"
        Display="Dynamic" ValidationGroup="vgActivity">
        <asp:Label ID="lblActivitydes" runat="server" Text="الرجاء كتابةوصف للنشاط"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" ValidationGroup="vgActivity" /><br />
    <br />
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;هل انت متاكد من الحذف&quot;)"
        Text="حذف " /><br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="النشاط "></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnNewActivity" runat="server" OnClick="btnNewActivity_Click" Text="اضافة جديدة " /><br />
    <br />
    <asp:DropDownList ID="ddlSelectLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectLang_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="lblRote" runat="server"></asp:Label>
    <asp:TreeView ID="tvActivity" runat="server" ShowLines="True">
    </asp:TreeView>
    <br />
    <br />
</asp:Content>
