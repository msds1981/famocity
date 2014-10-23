<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.master" AutoEventWireup="true"
    CodeBehind="shopVideo2.aspx.cs" Inherits="FamoCity.shopVideo2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">
    <div class="cms_page_title">
        <span>رفع الفيديو</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
         <form id="form" runat="server">
            <ul class="innput">
                <li>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="ارفع الملف"
                        class="validd" ControlToValidate="txtLink" ValidationGroup="vidValidation"></asp:RequiredFieldValidator>
                    <div class="cms_input">
                        <asp:TextBox ID="txtLink" runat="server" CssClass="random_input"></asp:TextBox></div>
                    <span class="title_cms">ارفع ملف التحميل </span></li>
                <li>
                    <asp:Literal ID="ltrVideo" runat="server"></asp:Literal>
                </li>
                <li>
                    <asp:Button ID="Button1" runat="server" CssClass="form-submit" Text="حفظ" ValidationGroup="vidValidation"
                        OnClick="Button1_Click" />
                </li>
            </ul>
            </form>
        </div>
    </div>
</asp:Content>
