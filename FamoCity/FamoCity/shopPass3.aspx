<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopPass3.aspx.cs" Inherits="FamoCity.shopPass3" %>
    <div class="cms_page_title">
        <span>معلومات المتجر</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
        <form id="form" runat="server">
            <ul class="innput">
                <li><span class="title_cms">
                    <asp:Label ID="lblOldPassword" runat="server" Text=" كلمة المرور القديمة"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtOldPassword" runat="server" class="random_input" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOldPassword"
                        runat="server" ValidationGroup="VgNewPassword" ErrorMessage="يرجى كتابة كلمة المرور القديمة"
                        CssClass="validd">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="csvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                        ErrorMessage="كلمةالمرور القديمة خاطئة " OnServerValidate="csvOldPassword_ServerValidate"
                        ValidationGroup="VgNewPassword" CssClass="validd">
                    </asp:CustomValidator>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblNewPassword" runat="server" Text="  كلمةالمرور الجديد"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtNewPassword" runat="server" class="random_input" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNewPassword"
                        runat="server" ValidationGroup="VgNewPassword" ErrorMessage=" يرجى كتابة كلمةالمرورالجديدة"
                        CssClass="validd" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblRepeatPassword" runat="server" Text="اعادة كلمة المرور الجديدة"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtRepeatPassword" runat="server" class="random_input" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRepeatPassword"
                        runat="server" ValidationGroup="VgNewPassword" ErrorMessage="يرجى كتابة كلمة المرور القديمة"
                        CssClass="validd">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Button ID="btnSave" runat="server" Text="حفظ" ValidationGroup="VgNewPassword"
                        OnClick="btnSave_Click" />
                </li>
                <li>
                    <asp:Literal ID="ltrError" runat="server"></asp:Literal>
                </li>
            </ul>
            </form>
        </div>
    </div>