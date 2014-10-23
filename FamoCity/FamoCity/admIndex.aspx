<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admIndex.aspx.cs" Inherits="FamoCity.admIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Add Endex</h1>
    <asp:Label ID="lblLang" runat="server" Text="اللغة "></asp:Label>
    <asp:DropDownList ID="ddlLang" runat="server" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged"
        AutoPostBack="True">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlLang"
        Display="Dynamic" ErrorMessage="يرجى اختيار اللغة " Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="lblcomVlang" runat="server" Text="الرجاء اختيار اللغة"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblName" runat="server" Text="EnmName"></asp:Label>
    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
        Display="Dynamic" ErrorMessage="يرجى ادخال EnmName">
        <asp:Label ID="Label1" runat="server" Text="الرجاء كتابة النشاط"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="lblValue" runat="server" Text="EnmValue"></asp:Label>
    <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValue"
        Display="Dynamic">
        <asp:Label ID="lblActivitydes" runat="server" Text="يرجى ادخال القيمة"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="lblNameT" runat="server" Text="Name"></asp:Label>
    <asp:TextBox ID="TxtNameT" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtNameT"
        Display="Dynamic">
        <asp:Label ID="Label3" runat="server" Text="يرجى ادخال الاسم"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="حفظ" /><br />
    <br />
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;هل انت متاكد من حذف السجل &quot;)"
        Text="حذف" CausesValidation="False" /><br />
    <br />
    <asp:Button ID="btnAddIndex" runat="server" OnClick="btnAddIndex_Click" Text="اضافة جديدة"
        CausesValidation="False" />
</asp:Content>
