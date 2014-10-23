<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopPass.aspx.cs" Inherits="FamoCity.shopPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuSett', 'submnuPass');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-outer">
        <!-- start content -->
        <div id="content">
            <div id="page-heading">
                <h1>
                    <asp:Label ID="lblHeader" runat="server" Text="تغيير كلمة المرور"></asp:Label></h1>
            </div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0" id="content-table">
                <tr>
                    <th rowspan="3" class="sized">
                        <img src="/images/shared/side_shadowright.jpg" width="20" height="300" alt="" />
                    </th>
                    <th class="topleft">
                    </th>
                    <td id="tbl-border-top">
                        &nbsp;
                    </td>
                    <th class="topright">
                    </th>
                    <th rowspan="3" class="sized">
                        <img src="/images/shared/side_shadowleft.jpg" width="20" height="300" alt="" />
                    </th>
                </tr>
                <tr>
                    <td id="tbl-border-left">
                    </td>
                    <td>
                        <!--  start content-table-inner -->
                        <div id="content-table-inner">
                            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                <tr valign="top">
                                    <td>
                                        <!-- start id-form -->
                                        <table border="0" cellpadding="0" cellspacing="0" id="id-form">
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblOldPassword" runat="server" Text=" كلمة المرور القديمة"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtOldPassword" runat="server" class="inp-form" 
                                                        TextMode="Password"></asp:TextBox>
                                                    <%--  <input type="text" class="inp-form" />--%>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOldPassword"
                                                        runat="server" ValidationGroup="VgNewPassword">
                                                        <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="lblValOldPassword" runat="server" Text="يرجى كتابة كلمة المرور القديمة"></asp:Label></div>
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblNewPassword" runat="server" Text="  كلمةالمرور الجديد"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtNewPassword" runat="server" class="inp-form" 
                                                        TextMode="Password"></asp:TextBox>
                                                    <%--     <input type="text" class="inp-form" />--%>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNewPassword"
                                                        runat="server" ValidationGroup="VgNewPassword">
                                                        <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="lblValNewPassword" runat="server" Text="يرجى كتابة كلمةالمرورالجديدة"></asp:Label></div>
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblRepeatPassword" runat="server" Text="اعادة كلمة المرور الجديدة"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtRepeatPassword" runat="server" class="inp-form" 
                                                        TextMode="Password"></asp:TextBox>
                                                    <%-- <input type="text" class="inp-form" />--%>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRepeatPassword"
                                                        runat="server" ValidationGroup="VgNewPassword">
                                                        <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="lblVaiRepeatPassword" runat="server" Text="يرجى اعادة كلمة المرور الجديدة"></asp:Label></div>
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" ValidationGroup="VgNewPassword"
                                                        class="form-submit" />
                                                    <%--  <input type="button" value="" class="form-submit" />--%>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="ltrError" runat="server"></asp:Literal>
                                                    <%--<asp:Label ID="lblmsg" runat="server"></asp:Label>--%>
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
                        <!--  end content-table-inner  -->
                    </td>
                    <td id="tbl-border-right">
                    </td>
                </tr>
                <tr>
                    <th class="sized bottomleft">
                    </th>
                    <td id="tbl-border-bottom">
                        &nbsp;
                    </td>
                    <th class="sized bottomright">
                    </th>
                </tr>
            </table>
            <div class="clear">
                &nbsp;</div>
        </div>
        <!--  end content -->
        <div class="clear">
            &nbsp;</div>
    </div>
</asp:Content>
