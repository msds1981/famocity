<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admOption.aspx.cs" Inherits="FamoCity.admOption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        الخيارات
    </h1>
    <asp:Label ID="lblLnag" runat="server" Text="اللغة الرسمية "></asp:Label>
    <asp:DropDownList ID="ddlLang" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="lblTitle" runat="server" Text="عنوان المقدمة "></asp:Label>
    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblVideoInto" runat="server" Text="فيديو"></asp:Label>
    <asp:TextBox ID="txtVideoInto" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblactivityDescription" runat="server" Text="تفاصيل "></asp:Label>
    <asp:TextBox ID="txtInto" runat="server" Height="51px" TextMode="MultiLine"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblgustuser" runat="server" Text="اسم المستخدم الضيف"></asp:Label>
    <asp:TextBox ID="txtgustuser" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblguatpassword" runat="server" Text="كلمة مرور المستخدم الضيف"></asp:Label>
    <asp:TextBox ID="txtgustpassword" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblUserCreate" runat="server" Text="السماح للمستخدمين بإنشاء حساب"></asp:Label>
    <asp:RadioButton ID="rdTrueCreateUser" runat="server" Text="نعم " Checked="True"
        GroupName="vgCreateUser" />
    <asp:RadioButton ID="rdFalseCreateUser" runat="server" Text="لا" GroupName="vgCreateUser" /><br />
    <br />
    <asp:Label ID="lblshopcreated" runat="server" Text="السماح للشركات بانشاء حساب"></asp:Label>
    <asp:RadioButton ID="rdTrueCreateCompaby" runat="server" Checked="True" Text="نعم "
        GroupName="vgCreateCompany" />
    <asp:RadioButton ID="rdFalseCreateCompany" runat="server" Text="لا" GroupName="vgCreateCompany" /><br />
    <br />
    <asp:Label ID="lblUserslogin" runat="server" Text="السماح للمستخدمين بتسجيل الدخول"></asp:Label>
    <asp:RadioButton ID="rdTrueRegisrCompany" runat="server" Checked="True" Text="نعم "
        GroupName="vgCompanyRegis" />
    <asp:RadioButton ID="rdFalseCompanyRegist" runat="server" Text="لا" Style="direction: ltr"
        GroupName="vgCompanyRegis" /><br />
    <br />
    <asp:Label ID="lblshoplogin" runat="server" Text="السماح للشركات بتسجيل الدخول "></asp:Label>
    <asp:RadioButton ID="rdTrueRegisrUser" runat="server" Checked="True" Text="نعم "
        GroupName="vgShopRegis" />
    <asp:RadioButton ID="rdFalseUserRegist" runat="server" Text="لا" GroupName="vgShopRegis" /><br />
    <br />
    <asp:Label ID="lblshop3dlogin" runat="server" Text="تسجيل دخول لشركات 3D"></asp:Label>
    <asp:RadioButton ID="rdTrueRegisrUser0" runat="server" Checked="True" GroupName="gnlogincomany3d"
        Text="نعم " />
    <asp:RadioButton ID="rdFalseCompany3D" runat="server" GroupName="gnlogincomany3d"
        Text="لا" /><br />
    <br />
    <asp:Label ID="lblusers3dloging" runat="server" Text="تسجيل دخول للمستخدمين 3D"></asp:Label>
    <asp:RadioButton ID="rdTrueUser3D" runat="server" Checked="True" GroupName="gnuserslogin3d"
        Text="نعم " />
    <asp:RadioButton ID="rdFalseUser3D" runat="server" GroupName="gnuserslogin3d" Text="لا" /><br />
    <br />
    <asp:Label ID="lblscenefolder" runat="server" Text="Scene Folder"></asp:Label>
    <asp:TextBox ID="TxtScene" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="lbltriggerfolder" runat="server" Text="Trigger Folder"></asp:Label>
    <asp:TextBox ID="txtTrigger" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblobjectfolder" runat="server" Text="ObjectFolder"></asp:Label>
    <asp:TextBox ID="txtObject" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="lblcharcterfolder" runat="server" Text="CharcterFolder"></asp:Label>
    <asp:TextBox ID="txtChar" runat="server"></asp:TextBox><br />
    <br />
    <asp:Button ID="btnadd" runat="server" OnClick="Button1_Click" Text="حفظ" /><br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
    <br />
    الصفة الاولى<br />
    <br />
    التسجيل<br />
    <br />
    <asp:Label ID="lblCompanyCreate" runat="server" Text="السماح للشركات بإنشاء حساب"></asp:Label><br />
    <br />
    <asp:Label ID="lblUserRegist" runat="server" Text="السماح للمستخدم بالتسجيل "></asp:Label><br />
    <br />
    <asp:Label ID="lblCompanyRegist" runat="server" Text="للسماح للشركات بتسجيل للدخول"></asp:Label>
    3D
</asp:Content>
