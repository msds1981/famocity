<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.Master" AutoEventWireup="true"
    CodeBehind="shopInfo2.aspx.cs" Inherits="FamoCity.shopInfo2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">
    <div class="cms_page_title">
        <span>معلومات المتجر</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
            <form id="form" runat="server">
            <ul class="innput">
                <li><span class="title_cms">اسم المتجر</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtCompany" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCompany"
                        runat="server" ValidationGroup="VgCompany" ErrorMessage="الرجاء كتابة اسم المتجر"
                        CssClass="validd">
                    </asp:RequiredFieldValidator>
                </li>
                <li><span class="title_cms">اسم الصفحة</span>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="cms_input">
                                <asp:TextBox ID="txtWebName" runat="server" class="random_input"></asp:TextBox>
                                <asp:Label ID="lblAvailable" runat="server" Text=""></asp:Label>
                                <asp:Button ID="btnAvailable" runat="server" Text="التحقق من توفر الإسم" OnClick="btnAvailable_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtWebName"
                        ValidationGroup="VgCompany" ValidationExpression="^[a-z0-9_-]{3,15}$" ErrorMessage="يمكن أن يتكون الإسم من أرقام و حروف انجليزية و ’-’ و ’_’ فقط"
                        CssClass="validd" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtWebName"
                        runat="server" ValidationGroup="VgCompany" ErrorMessage=" الرجاء كتابة اسم للصفحة"
                        CssClass="validd" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="csvPages" runat="server" ErrorMessage="اسم الصفحة موجود سابقا"
                        ControlToValidate="txtWebName" ValidationGroup="VgCompany" CssClass="validd"
                        Display="Dynamic" OnServerValidate="csvPages_ServerValidate">
                    </asp:CustomValidator>
                </li>
                <li><span class="title_cms">عنوان المتجر</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtCompanyAddress" runat="server" Height="64px" TextMode="MultiLine"
                            class="random_input"></asp:TextBox>
                    </div>
                    <span class="validd"></span></li>
                <li><span class="title_cms">عن المتجر</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtAboutCompany" runat="server" Height="64px" TextMode="MultiLine"
                            class="random_input"></asp:TextBox>
                    </div>
                    <span class="validd"></span></li>
                <li><span class="title_cms">رقم الفاكس</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtFax" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFax"
                        ValidationGroup="VgCompany" ValidationExpression="^\d+$" ErrorMessage="ادخل رقم الفاكس بطريقة صحيحة"
                        CssClass="validd">
                    </asp:RegularExpressionValidator>
                </li>
                <li><span class="title_cms">رقم الجوال</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtJawal" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtJawal"
                        ValidationGroup="VgCompany" ValidationExpression="^\d+$" ErrorMessage="ادخل رقم الجوال بطريقة صحيحة"
                        CssClass="validd" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtJawal"
                        runat="server" ValidationGroup="VgCompany" ErrorMessage="الرجاء كتابة رقم الجوال"
                        CssClass="validd" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="csvPhone" runat="server" ErrorMessage="رقم الجوال موجود  سابقا"
                        ControlToValidate="txtJawal" ValidationGroup="VgCompany" CssClass="validd" Display="Dynamic"
                        OnServerValidate="csvPhone_ServerValidate">
                    
                    
                    </asp:CustomValidator>
                </li>
                <li><span class="title_cms">الإيميل الإلكتروني</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtEmail" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                        ValidationGroup="VgCompany" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ErrorMessage="ادخل الايميل بطريقة صحيحة" CssClass="validd" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtEmail"
                        runat="server" ValidationGroup="VgCompany" ErrorMessage="الرجاء كتابة الإيميل"
                        CssClass="validd" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="csvEmail" runat="server" ErrorMessage="الايميل موجود سابقا"
                        ControlToValidate="txtEmail" ValidationGroup="VgCompany" CssClass="validd" Display="Dynamic"
                        OnServerValidate="csvEmail_ServerValidate">
                    
                    </asp:CustomValidator>
                </li>
                <li><span class="title_cms">رقم الهاتف</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtPhone" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhone"
                        ValidationGroup="VgCompany" ValidationExpression="^\d+$" ErrorMessage="ادخل رقم الهاتف بطريقة صحيحة"
                        CssClass="validd" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </li>
                <li><span class="title_cms">الدولة</span>
                    <div class="cms_input">
                        <asp:DropDownList ID="ddlCountry" runat="server">
                        </asp:DropDownList>
                    </div>
                    <span class="validd"></span></li>
                <li>
                    <asp:Button ID="addIntoCompany" runat="server" Text="حفظ" ValidationGroup="VgCompany"
                        OnClick="addIntoCompany_Click1" />
                </li>
            </ul>
            </form>
        </div>
    </div>
</asp:Content>
