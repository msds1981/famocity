<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admUserAdd.aspx.cs" Inherits="FamoCity.admUserAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#573550"></asp:Label>
    <asp:Label ID="lblUserGroup" runat="server" Text="المجموعة "></asp:Label><br />
    <br />
    <asp:DropDownList ID="ddlUserGroup" runat="server">
    </asp:DropDownList>
    <asp:CompareValidator ID="cvUserGroup" runat="server" ControlToValidate="ddlUserGroup"
        Display="Dynamic" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="Label1" runat="server" Text="يرجى اختيار المجموعة"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblUserName" runat="server" Text="اسم المستخدم"></asp:Label>
    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvAddUser" runat="server" ControlToValidate="txtUserName"
        Display="Dynamic">
        <asp:Label ID="Label3" runat="server" Text="يرجى ادخال اسم المستخدم"></asp:Label>
    </asp:RequiredFieldValidator>
    <asp:CustomValidator ID="CvUserName" runat="server" OnServerValidate="CvUserName_ServerValidate"
        ControlToValidate="txtUserName" Display="Dynamic">
        <asp:Label ID="Label16" runat="server" Text="اسم المستخدم موجود سابقا"></asp:Label>
    </asp:CustomValidator><br />
    <br />
    <asp:Label ID="lblFirstName" runat="server" Text="الاسم الاول"></asp:Label>
    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
        Display="Dynamic" ErrorMessage="يرجى ادخال الاسم الاول">
        <asp:Label ID="Label4" runat="server" Text="يرجى ادخال الاسم الاول"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="lblLastName" runat="server" Text="الاسم الاخير "></asp:Label>
    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
        Display="Dynamic">
        <asp:Label ID="Label5" runat="server" Text="يرجى ادخال الاسم الاخير"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:HiddenField ID="hfd" runat="server" />
    <br />
    <br />
    <asp:Button ID="btnEditPass" runat="server" Text="تعديل كلمة المرور" Visible="false"
        OnClientClick="ChangePass();return false;" /><br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="كلمة المرور "></asp:Label>
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="" ControlToValidate="txtPassword"
        Display="Dynamic" OnServerValidate="CustomValidator1_ServerValidate">
        <asp:Label ID="lblmsgva" runat="server" Text=""></asp:Label>
    </asp:CustomValidator>
    <asp:Label ID="Label12" runat="server" Text="اعادة كلمة المرور "></asp:Label>
    <asp:TextBox ID="txtreapetPassword" runat="server" TextMode="Password"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblEmail" runat="server" Text="البريد الالكتروني"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
        Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
        <asp:Label ID="Label14" runat="server" Text="يرجى كتابة الايميل بطريقة صحيحة"></asp:Label>
    </asp:RegularExpressionValidator><br />
    <br />
    <asp:Label ID="lblUserStutas" runat="server" Text="حالة المستخدم "></asp:Label>
    <asp:DropDownList ID="ddlUserStutas" runat="server">
        <asp:ListItem Value="0">- اختر م القائمة</asp:ListItem>
        <asp:ListItem Value="1">غير نشط</asp:ListItem>
        <asp:ListItem Value="2">نشط</asp:ListItem>
        <asp:ListItem Value="3">محضور</asp:ListItem>
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlUserStutas"
        Display="Dynamic" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="Label8" runat="server" Text="يرجى اختيار  حالة الستخدم "></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblUserType" runat="server" Text="نوع المستخدم "></asp:Label>
    <asp:DropDownList ID="ddlUserType" runat="server">
        <asp:ListItem Value="0">- اختر من القائمة -</asp:ListItem>
        <asp:ListItem Value="1">User</asp:ListItem>
        <asp:ListItem Value="2">Shop</asp:ListItem>
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlUserType"
        Display="Dynamic" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="Label7" runat="server" Text="يرجى اختيار  نوع المستخدم"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblRegisDate" runat="server" Text="تاريخ التسجيل"></asp:Label>
    <asp:TextBox ID="txtRegisDate" runat="server" Style="margin-bottom: 0px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegisDate"
        Display="Dynamic">
        <asp:Label ID="Label9" runat="server" Text="يرجى كتابة تاريخ التسجيل"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="lblBirthDate" runat="server" Text="تاريخ الميلاد"></asp:Label>
    <asp:TextBox ID="txtBirthDate" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblGender" runat="server" Text="الجنس"></asp:Label>
    <asp:DropDownList ID="ddlGender" runat="server">
        <asp:ListItem Value="0">- اختر من القائمة -</asp:ListItem>
        <asp:ListItem Value="1">ذكر </asp:ListItem>
        <asp:ListItem Value="2">انثى </asp:ListItem>
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlGender"
        Display="Dynamic" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblLang" runat="server" Text="اللغة "></asp:Label>
    <asp:DropDownList ID="ddlLang" runat="server">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlLang"
        Display="Dynamic" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="Label6" runat="server" Text="يرجى اختيار  اللغة "></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="حفظ" /><br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="حذف" OnClientClick="return confirm(&quot;هل انت متاكد م الحذف&quot;)"
        CausesValidation="False" /><br />
    <br />
</asp:Content>
