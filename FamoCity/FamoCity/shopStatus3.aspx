<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopStatus3.aspx.cs" Inherits="FamoCity.shopStatus3" %>
    <div class="cms_page_title">
        <span>معلومات المتجر</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
            <form id="form" runat="server">
            <ul class="innput">
                <li><span class="title_cms">
                    <asp:Label ID="lblBrandDescription" runat="server" Text="حالة المستخدم"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtDescriptipn" runat="server" CssClass="random_input" MaxLength="100"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RfStatus" ControlToValidate="txtDescriptipn" runat="server"
                        ErrorMessage=" الرجاء كتابة حالة المستخدم" CssClass="validd" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Button ID="btnSave" runat="server" Text="حفظ" OnClick="btnSave_Click" />
                </li>
            </ul>
            </form>
        </div>
    </div>
