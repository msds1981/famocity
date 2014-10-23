<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopBasic.aspx.cs" Inherits="FamoCity.shopBasic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuSett', 'submnuBasic');
        });
    </script>
    <style type="text/css">
        .style1
        {
            height: 44px;
        }
    </style>
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
                                <asp:Label ID="lblUserName" runat="server" Text=": اسم المستخدم "></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtUser" runat="server" CssClass="inp-form"></asp:TextBox>
                            
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUser"
                                    runat="server" ValidationGroup="VgUser">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblVUserName" runat="server" Text="يرجى كتابة اسم المستخدم"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                <asp:Label ID="lblEmail" runat="server" Text=": البريد الالكتروني"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="inp-form"></asp:TextBox>
                                <%--  <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmail"
                                    runat="server" ValidationGroup="VgUser">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="Label2" runat="server" Text="يرجى كتابة الايميل "></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                                    ValidationGroup="VgUser" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblVCorrectEmail" runat="server" Text="ادخل الايميل بطريقة صحيحة"></asp:Label>
                                    </div>
                                </asp:RegularExpressionValidator>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                <asp:Label ID="lblFirstName" runat="server" Text=" :الاسم الاول"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="inp-form"></asp:TextBox>
                                <%--   <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFirstName"
                                    runat="server" ValidationGroup="VgUser">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblVFirstName" runat="server" Text="يرجى كتابة الاسم الاول"></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                <asp:Label ID="lblLastName" runat="server" Text=" : الاسم الاخير "></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="inp-form"></asp:TextBox>
                                <%--  <input type="text" class="inp-form" />--%>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtLastName"
                                    runat="server" ValidationGroup="VgUser">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblVLastName" runat="server" Text="يرجى كتابة الاسم الاخير  "></asp:Label>
                                    </div>
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                <asp:Label ID="Label5" runat="server" Text=": تاريخ الميلاد"></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="false" CssClass="styledselect_form_1">
                                    <asp:ListItem Value="0">يوم </asp:ListItem>
                                    <asp:ListItem Value="01">01</asp:ListItem>
                                    <asp:ListItem Value="02">02</asp:ListItem>
                                    <asp:ListItem Value="03">03</asp:ListItem>
                                    <asp:ListItem Value="04">04</asp:ListItem>
                                    <asp:ListItem Value="05">05</asp:ListItem>
                                    <asp:ListItem Value="06">06</asp:ListItem>
                                    <asp:ListItem Value="07">07</asp:ListItem>
                                    <asp:ListItem Value="08">08</asp:ListItem>
                                    <asp:ListItem Value="09">09</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="21">21</asp:ListItem>
                                    <asp:ListItem Value="22">22</asp:ListItem>
                                    <asp:ListItem Value="23">23</asp:ListItem>
                                    <asp:ListItem Value="24">24</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="26">26</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="28">28</asp:ListItem>
                                    <asp:ListItem Value="29">29</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="31">31</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="false" CssClass="styledselect_form_1">
                                    <asp:ListItem Value="0">شهر </asp:ListItem>
                                    <asp:ListItem Value="01">01</asp:ListItem>
                                    <asp:ListItem Value="02">02</asp:ListItem>
                                    <asp:ListItem Value="03">03</asp:ListItem>
                                    <asp:ListItem Value="04">04</asp:ListItem>
                                    <asp:ListItem Value="05">05</asp:ListItem>
                                    <asp:ListItem Value="06">06</asp:ListItem>
                                    <asp:ListItem Value="07">07</asp:ListItem>
                                    <asp:ListItem Value="08">08</asp:ListItem>
                                    <asp:ListItem Value="09">09</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="false" CssClass="styledselect_form_1">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlDay"
                                    ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblCDay" runat="server" Text="يرجى اختيار يوم الميلاد"></asp:Label>
                                    </div>
                                </asp:CompareValidator>
                                 <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlMonth"
                                    ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="lblCMonth" runat="server" Text="يرجى اختيار شهر الميلاد"></asp:Label>
                                    </div>
                                </asp:CompareValidator>
                                 <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlYear"
                                    ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual">
                                    <div class="error-left">
                                    </div>
                                    <div class="error-inner">
                                        <asp:Label ID="Label1" runat="server" Text="يرجى اختيار سنة الميلاد"></asp:Label>
                                    </div>
                                </asp:CompareValidator>

                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                <asp:Label ID="lblNationalty" runat="server" Text=": الجنسية"></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlNationality" runat="server" CssClass="styledselect_form_1">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <th class="style1">
                                &nbsp;
                            </th>
                            <td valign="top" class="style1">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="form-submit"
                                    ValidationGroup="VgUser" />
                                <%-- <input type="button" value="" class="form-submit" />--%>
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
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
                            <img src="images/forms/header_related_act.gif" width="271" height="43" alt="" />
                        </div>
                        <!-- end related-act-top -->
                        <!--  start related-act-bottom -->
                        <div id="related-act-bottom">
                            <!--  start related-act-inner -->
                            <div id="related-act-inner">
                                <div class="left">
                                    <a href="">
                                        <img src="images/forms/icon_plus.gif" width="21" height="21" alt="" /></a></div>
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
                    <img src="images/shared/blank.gif" width="695" height="1" alt="blank" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div class="clear">
        </div>
    </div>
</asp:Content>
