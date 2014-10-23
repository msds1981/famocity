<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admChrCloth.aspx.cs" Inherits="FamoCity.admChrCloth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
  

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Add Char Clothes</h1>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
        Display="Dynamic">
        <asp:Label ID="Label4" runat="server" Text="الرجاء اختيار الاسم"></asp:Label>
    </asp:RequiredFieldValidator>
    <asp:Label ID="lblChar" runat="server" Text="group"></asp:Label>
    <asp:DropDownList ID="ddlGroup" runat="server" >
    </asp:DropDownList>
    <br />
    <br />
    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ddlGroup" Display="Dynamic"
        runat="server" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="lblcomVlang" runat="server" Text="الرجاء الاختيار"></asp:Label>
    </asp:CompareValidator>
    <asp:Label ID="lblObjCode" runat="server" Text="ObjCode"></asp:Label>
    <asp:TextBox ID="txtObjCode" runat="server" Height="28px" ></asp:TextBox><br />
    <br />
    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtObjCode"
        Display="Dynamic">
        <asp:Label ID="Label2" runat="server" Text="الرجاء كتابة رقم"></asp:Label>
    </asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
        ControlToValidate="txtObjCode" ValidationExpression="^\d+$">
        <asp:Label ID="Label8" runat="server" Text="الرجاء ادخال ارقام فقط"></asp:Label>
    </asp:RegularExpressionValidator>
    <asp:Label ID="lblObjectId" runat="server" Text="ObjId"></asp:Label>
    <asp:TextBox ID="txtObjecId" runat="server" ></asp:TextBox><br />
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtObjecId"
        Display="Dynamic">
        <asp:Label ID="Label9" runat="server" Text="الرجاء كتابة رقم"></asp:Label>
    </asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
        ControlToValidate="txtObjecId" ValidationExpression="^\d+$">
        <asp:Label ID="Label10" runat="server" Text="الرجاء ادخال ارقام فقط"></asp:Label>
    </asp:RegularExpressionValidator>
    <asp:Label ID="lblFile" runat="server" Text="File "></asp:Label>
    <asp:FileUpload ID="fuCharCloth" runat="server" /><br />
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:Label ID="lblShowFile" runat="server"></asp:Label><br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="اضافة جديدة" /><br />
    <br />
    <asp:Button ID="Button2" runat="server" Text="Button" /><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"  />
</asp:Content>
