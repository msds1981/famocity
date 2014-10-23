﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn1.Master" AutoEventWireup="true"
    CodeBehind="admTrigger.aspx.cs" Inherits="FamoCity.admTrigger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnu3D', 'subTrig');
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="content-outer">
                <!-- start content -->
                <div id="content">
                    <div id="page-heading">
                        <h1>
                            Add Trigger</h1>
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
                                                        <th>
                                                            <asp:Label ID="lblCode" runat="server" Text="الكود "></asp:Label>
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtCode" runat="server" CssClass="inp-form"></asp:TextBox>
                                                            <%--  <input type="text" class="inp-form" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode"
                                                                Display="Dynamic" ErrorMessage="الرجاء ادخال الكود ">
                                                                <div class="error-left">
                                                                </div>
                                                                <div class="error-inner">
                                                                    <asp:Label ID="Label3" runat="server" Text="الرجاء اختيار اللغة"></asp:Label></div>
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            <asp:Label ID="lblVersion" runat="server" Text="Version"></asp:Label>
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtVersion" runat="server" CssClass="inp-form"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvVersion" runat="server" ErrorMessage="الرجاء ادخال Version"
                                                                ControlToValidate="txtVersion">
                                </div>
                                <div class="error-inner">
                                    <asp:Label ID="Label4" runat="server" Text="الرجاء ادخال Version"></asp:Label></div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtVersion"
                                    Display="Dynamic" ValidationExpression="(^\d{0,3}[.]?\d{0,2}$)">
                </div>
                <div class="error-inner">
                    <asp:Label ID="Label5" runat="server" Text="يرجى ادخال رقم  بطريقة صحيحة"></asp:Label></div>
                </asp:RegularExpressionValidator> </td> </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblScene" runat="server" Text="Scene"></asp:Label>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlScene" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CompareValidator ID="cvScene" runat="server" ControlToValidate="ddlScene" Display="Dynamic"
                            ErrorMessage="الرجاء اختيار Scene" Operator="NotEqual" ValueToCompare="0">
                            <div class="error-left">
                            </div>
                            <div class="error-inner">
                                <asp:Label ID="Label6" runat="server" Text="الرجاء اختيار Scene"></asp:Label></div>
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblFile" runat="server" Text="File"></asp:Label>
                    </th>
                    <td>
                        <asp:FileUpload ID="fuTrigger" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblExtention" runat="server" Text="امتداد حفظ الملف "></asp:Label>
                    </th>
                    <td>
                        <asp:Label ID="lblShowFile" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>

              

                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td valign="top">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="حفظ " />
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;هل انت متاكد من الحذف &quot;)"
                            Text="حذف" CausesValidation="False" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                        <asp:Label ID="lblExtentionfile" runat="server" Text="ملف الحفظ"></asp:Label>
                    </th>
                    <td>
                        <asp:Label ID="lblSaveFile" runat="server"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td valign="top">
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="اضافة جديدة "
                            CausesValidation="False" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <th colspan="3">
                        &nbsp;
                    </th>
                </tr>
                <tr>
                    <th colspan="3">
                        &nbsp;
                    </th>
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
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <hr />
    <br />
    <br />
</asp:Content>