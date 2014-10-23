<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopInfo.aspx.cs" Inherits="FamoCity.shopInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuPages', 'submnuInfo');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-table-inner">
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td>
                    <!-- start id-form -->
                    <table border="0" cellpadding="0" cellspacing="0" id="id-form">
                        <tr>
                            <th valign="top">
                                <asp:Label ID="lblName" runat="server" Text="اسم المتجر"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtCompany" runat="server" class="inp-form"></asp:TextBox>
                                <%--  <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCompany"
                                    runat="server" ValidationGroup="VgCompany">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="Label1" runat="server" Text="يرجى ادخال إسم المتجر"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                <asp:Label ID="lblWebName" runat="server" Text="الاسم المميز"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtWebName" runat="server" class="inp-form" ></asp:TextBox><asp:Label
                                    ID="lblAvailable" runat="server" Text=""></asp:Label>
                                </br><asp:Button ID="btnAvailable" runat="server" Text="التحقق من توفر الإسم" 
                                    onclick="btnAvailable_Click" /> <%--  <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtWebName"
                                    runat="server" ValidationGroup="VgCompany" >
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblRequired" runat="server" Text=" يرجى ادخال إسم المتجر المميز"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtWebName"
                                    ValidationGroup="VgCompany" ValidationExpression="^[a-z0-9_-]{3,15}$">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="Label2" runat="server" Text="يمكن أن يتكون الإسم من أرقام و حروف انجليزية و ’-’ و ’_’ فقط"></asp:Label>
                                    </div>
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                   <asp:Label ID="lblCompanyAddress" runat="server" Text="عنوان المتجر : "></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtCompanyAddress" runat="server" Height="64px" TextMode="MultiLine"
                                    class="form-textarea"></asp:TextBox>
                                <%--  <textarea rows="" cols="" class="form-textarea"></textarea>--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCompanyAddress"
                                    runat="server" ValidationGroup="VgCompany">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblValidCompanyAddress" runat="server" Text="يرجى ادخال عنوان المتجر"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                             <asp:Label ID="lblCompanyDetails" runat="server" Text="عن المتجر : "></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtAboutCompany" runat="server" Height="54px" TextMode="MultiLine"
                                    class="form-textarea"></asp:TextBox>
                                <%--       <textarea rows="" cols="" class="form-textarea"></textarea>--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAboutCompany"
                                    runat="server" ValidationGroup="VgCompany">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblDetailsNumber" runat="server" Text="يرجى تفاصيل عن الشركة"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                              <asp:Label ID="lblFaxNumber" runat="server" Text="رقم الفاكس  : "></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtFax" runat="server" class="inp-form"></asp:TextBox>
                                <%--  <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtFax"
                                    runat="server" ValidationGroup="VgCompany">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblValidFaxNumber" runat="server" Text="يرجى ادخال رقم الفاكس"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFax"
                                    ValidationGroup="VgCompany" ValidationExpression="^\d+$">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblCorrectNumber" runat="server" Text="ادخل رقم الفاكس بطريقة صحيحة"></asp:Label>
                                    </div>
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                  <asp:Label ID="lblJawalNumber" runat="server" Text="رقم الجوال  : "></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtJawal" runat="server" class="inp-form"></asp:TextBox>
                                <%-- <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtJawal"
                                    runat="server" ValidationGroup="VgCompany">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblValidJawalNumber" runat="server" Text="يرجى ادخال رقم الجوال"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtJawal"
                                    ValidationGroup="VgCompany" ValidationExpression="^\d+$">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblCorrectjawalNumber" runat="server" Text="ادخل رقم الجوال بطريقة صحيحة"></asp:Label>
                                    </div>
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                <asp:Label ID="lblEmail" runat="server" Text="البريد الالكتروني  : "></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" class="inp-form"></asp:TextBox>
                                <%--   <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtEmail"
                                    runat="server" ValidationGroup="VgCompany">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblValidEmail" runat="server" Text="يرجى ادخال الايميل"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                                    ValidationGroup="VgCompany" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblCorrectEmail" runat="server" Text="ادخل رقم الايميل بطريقة صحيحة"></asp:Label>
                                    </div>
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                   <asp:Label ID="lblPhoneNumber" runat="server" Text="رقم الهاتف  : "></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPhone" runat="server" class="inp-form"></asp:TextBox>
                                <%-- <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtPhone"
                                    runat="server" ValidationGroup="VgCompany">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblValidPhoneNumber" runat="server" Text="يرجى ادخال رقم الهاتف"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhone"
                                    ValidationGroup="VgCompany" ValidationExpression="^\d+$">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblCorrectPhone" runat="server" Text="ادخل رقم الهاتف بطريقة صحيحة"></asp:Label>
                                    </div>
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                               <asp:Label ID="lblCountry" runat="server" Text="الدولة  : "></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" class="styledselect_form_1">
                                </asp:DropDownList>
                                <%-- <select class="styledselect_form_1">
                                    <option value="">All</option>
                                    <option value="">Products</option>
                                    <option value="">Categories</option>
                                    <option value="">Clients</option>
                                    <option value="">News</option>
                                </select>--%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                &nbsp;
                            </th>
                            <td valign="top">
                                <asp:Button ID="addIntoCompany" runat="server" CssClass="form-submit" OnClick="addIntoCompany_Click"
                                    Text="" ValidationGroup="VgCompany" />
                                <%--<input type="button" value="" class="form-submit"/>--%>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <!-- end id-form  -->
                </td>
                <td>
                    <!--  start related-activities -->
                    <div id="related-activities">
                        <!--  start related-act-top -->
                        <div id="related-act-top">
                            <img src="/images/forms/header_related_act.gif" width="271" height="43" alt="" />
                        </div>
                        <!-- end related-act-top -->
                        <!--  start related-act-bottom -->
                        <div id="related-act-bottom">
                            <!--  start related-act-inner -->
                            <div id="related-act-inner">
                                <div class="left">
                                    <a href="">
                                        <img src="/images/forms/icon_plus.gif" width="21" height="21" alt="" /></a></div>
                                <div class="right">
                                    <h5>
                                        شرح الاستخدام</h5>
                                    <ul class="greyarrow">
                                        <li></li>
                                        <li></li>
                                    </ul>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <!-- end related-act-inner -->
                            <div class="clear">
                            </div>
                        </div>
                        <!-- end related-act-bottom -->
                    </div>
                    <!-- end related-activities -->
                </td>
            </tr>
            <tr>
                <td>
                    <img src="/images/shared/blank.gif" width="695" height="1" alt="blank" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div class="clear">
        </div>
    </div>
</asp:Content>
