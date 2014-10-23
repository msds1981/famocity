<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="newPass.aspx.cs" Inherits="FamoCity.newPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/newpassword.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div class="ccontent size">
            <div class="bigbox boxsize-small">
                <form id="form1" runat="server">
                <div class="title-line">
                    أنشاء كلمة سر جديدة</div>
                <div class="form-register-r">
                    <div class=" form-line-r">
                        <div class="form-title-r">
                             <asp:Label ID="lblNewPasswore" runat="server" Text="كلمة السر الجديدة"></asp:Label></div>
                        
                        <asp:TextBox ID="txtNewPassword" runat="server" class="form-field-r" 
                            TextMode="Password"></asp:TextBox>
        
                      <%--  <input class="form-field-r" type="text" name="email" />--%>
                        <div class="validation">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtNewPassword" Display="Dynamic" 
            ErrorMessage="يرجى  ادخال كلمة السر الجديدة" ValidationGroup="VgNewPassword"></asp:RequiredFieldValidator>
                           
                           </div>
                    </div>
                    <div class=" form-line-r">
                        <div class="form-title-r">
                         <asp:Label ID="lblRebeatPassword" runat="server" Text="تكرار كلمةالمرور"></asp:Label></div>

                              <asp:TextBox ID="txtRepeatPassword" runat="server" class="form-field-r" 
                            TextMode="Password"></asp:TextBox>
                     <%--   <input class="form-field-r" type="text" name="email" />--%>
                        <div class="validation">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtNewPassword" ControlToValidate="txtRepeatPassword" 
            Display="Dynamic" ErrorMessage="كلمةالمرور ليست متطابقة" 
            ValidationGroup="VgNewPassword"></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtRepeatPassword" Display="Dynamic" 
            ErrorMessage="يرجى تكرار كلمة المرور الجديدة" ValidationGroup="VgNewPassword"></asp:RequiredFieldValidator>
                           
                           </div>
                    </div>
                    <div class=" form-label-cancel">
                    </div>
                    <div class=" form-label-r">
                        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="موافق" 
            ValidationGroup="VgNewPassword" class="css_btn_class" />
                     <%--   <a href="#" class="css_btn_class">موافق</a>--%>
                    </div>
                </div>
                <asp:HiddenField ID="hdn" runat="server" Value="0" />
                </form>
            </div>
        </div>
    </div>
</asp:Content>
