<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopBrandAdd.aspx.cs" Inherits="FamoCity.shopBrandAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery/jquery.MultiFile.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuBrands', 'submnuBrandAdd');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-outer">
        <!-- start content -->
        <div id="content">
            <div id="page-heading">
                <h1>
                    معلومات المتجر</h1>
            </div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0" id="content-table">
                <tr>
                    <th rowspan="3" class="sized">
                        <img src="images/shared/side_shadowright.jpg" width="20" height="300" alt="" />
                    </th>
                    <th class="topleft">
                    </th>
                    <td id="tbl-border-top">
                        &nbsp;
                    </td>
                    <th class="topright">
                    </th>
                    <th rowspan="3" class="sized">
                        <img src="images/shared/side_shadowleft.jpg" width="20" height="300" alt="" />
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
                                                    <asp:Label ID="lblBrandTitle" runat="server" Text="اسم الماركة :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="inp-form"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblBrandDescription" runat="server" Text="الوصف :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtDescriptipn" runat="server" CssClass="inp-form" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblLogo" runat="server" Text="الشعار :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:FileUpload ID="fulImage" runat="server" />
                                                </td>
                                                <td>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td>
                                                    <asp:Image ID="imgLogo" runat="server" />
                                                    <%--<asp:FileUpload ID="FileUpload1" runat="server" class="multi" />--%> </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td>
                                                    <asp:Label ID="lblValidation" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">
                                                    <asp:Button ID="btnSave" runat="server" Text="" CssClass="form-submit" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnDelete" runat="server" Text="حذف" CssClass="form-submit" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                        OnClick="btnDelete_Click" />
                                                </td>
                                                <td valign="top">
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
                                                        شرح الاستخدا شرح الاستخدا شرح الاستخدا شرح الاستخدا شرح الاستخدا شرح الاستخدا شرح
                                                        الاستخدا
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
