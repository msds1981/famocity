<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admChar.aspx.cs" Inherits="FamoCity.admChar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
      
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Charcters</h1>
    <asp:Label ID="lblLang" runat="server" Text="الاسم "></asp:Label>
    <asp:TextBox ID="txtName" runat="server" Height="28px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvdescrActivity" runat="server" ControlToValidate="txtName"
        Display="Dynamic">
        <asp:Label ID="lblActivitydes" runat="server" Text="الرجاء ادخال الاسم"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="lblactivity" runat="server" Text="الملف"></asp:Label>
    <asp:FileUpload ID="fuFile" runat="server" /><br />
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
    <br />
    <asp:Label ID="lblfile" runat="server" Text="امتداد ملف الحفظ"></asp:Label><br />
    <br />
    <asp:Label ID="lblShowFile" runat="server"></asp:Label><br />
    <br />
    <asp:Label ID="lblactivityDescription" runat="server" Text="الجنس"></asp:Label>
    <asp:DropDownList ID="ddlGender" runat="server">
        <asp:ListItem Value="0">-كلاهما -</asp:ListItem>
        <asp:ListItem Value="1">ذكر</asp:ListItem>
        <asp:ListItem Value="2">انثى</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;هل انت متاكد من الحذف&quot;)"
        Text="حذف " CausesValidation="False" /><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" ValidationGroup="vgActivity" /><br />
    <br />
    <asp:Label ID="lblSaveFile" runat="server"></asp:Label><br />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="اضافة جديدة "
        CausesValidation="False" />
</asp:Content>
