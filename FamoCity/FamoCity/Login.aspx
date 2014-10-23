<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="FamoCity.Login1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/login.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div id="content">--%>
        <div class="ccontent size">
            <div class="bigbox boxsize-small">
                <form id="form1" runat="server">
                <div class="title-line">
                    تسجيل الدخول</div>
                <div class="form-register-r">
                    <div class=" form-line-r">
                        <div class="form-title-r">
                            البريد الالكتروني</div>
                        <asp:TextBox ID="txtEmail" CssClass="form-field-r" runat="server"></asp:TextBox>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="يجب كتابة البريد الالكتروني"></asp:RequiredFieldValidator></div>

                            
                    </div>
                    <div class="validation">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$" ValidationGroup="grpLogin" ErrorMessage="email invalid"></asp:RegularExpressionValidator>
                    </div>
                    <div class="validation">
                        <asp:CustomValidator ID="CustomValidator1" runat="server" 
                            ValidationGroup="grpLogin" ControlToValidate="txtEmail" 
                            ErrorMessage="not found email" 
                            onservervalidate="CustomValidator1_ServerValidate" ></asp:CustomValidator>
                        
                    </div>
                    <div class=" form-line-r">
                        <div class="form-title-r">
                            كلمة السر</div>
                        <asp:TextBox ID="txtPassword" CssClass="form-field-r" runat="server" TextMode="Password"></asp:TextBox>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" ErrorMessage="يجب كتابة كلمة المرور"></asp:RequiredFieldValidator></div>
                    </div>
                    <div class=" form-label-cancel">
                    <div class="validation" style="width:220px;">
                        <asp:Label ID="Lblmsg" runat="server"></asp:Label></div>
                    </div>
                    <div class=" form-label-r">
                        <asp:LinkButton ID="lnkForgetPassword" runat="server" 
                            OnClick="lnkForgetPassword_Click" CausesValidation="False">نسيت كلمة المرور</asp:LinkButton>
                        
                        <asp:Button ID="btnLogin" runat="server" Text="دخول"  CssClass="css_btn_class" 
                            ValidationGroup="grpLogin" onclick="btnLogin_Click"/>
                    </div>
                </div>
                </form>
            </div>
        </div>
    <%--</div><div class="clr"></div>--%>
</asp:Content>



