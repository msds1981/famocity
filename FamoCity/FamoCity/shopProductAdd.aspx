<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopProductAdd.aspx.cs" Inherits="FamoCity.shopProductAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery/jquery.MultiFile.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuProd', 'submnuProdAdd');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function deleteImg(fid) {
            var ans = confirm('Are you sure you want to delete?');
            if (ans) {
                $.ajax({
                    type: "POST",
                    url: 'shopProductAdd.aspx/deleteImg',
                    data: "{'fid':'" + fid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d > 0) {
                            alert("Deleted Successfilly");
                            var parent = document.getElementById("ulp");
                            var child = document.getElementById("f" + fid);
                            parent.removeChild(child);
                        }
                    },
                    error: AjaxFailed
                });
            }
        }
        function AjaxFailed(result) {
            alert(result.status + ' ' + result.statusText);
        }
    </script>
    <div id="content-outer">
        <!-- start content -->
        <div id="content">
            <div id="page-heading">
                <h1>
                    معلومات المنتج</h1>
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
                                                    <asp:Label ID="lblName" runat="server" Text="اسم المنتج :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="inp-form"></asp:TextBox>
                                                </td>
                                                <th valign="top">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtName"> <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="lblVCorrectEmail" runat="server" Text="أدخل اسم المنتج"></asp:Label></div></asp:RequiredFieldValidator>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblDescription" runat="server" Text="وصف المنتج :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-textarea" Rows="4"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblBrand" runat="server" Text="الماركة :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="inp-form">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblBrice" runat="server" Text="السعر :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtPrice" runat="server" CssClass="inp-form"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrice" runat="server" ControlToValidate="txtPrice"> <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label1" runat="server" Text="أدخل السعر"></asp:Label></div></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrice"
                                                        ValidationExpression="^(?:[1-9]\d*|0)?(?:\.\d+)?$"> <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label3" runat="server" Text="أدخل السعر بالأرقام"></asp:Label></div></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblDiscount" runat="server" Text="نسبة الخصم :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="inp-form"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorDiscount" runat="server"
                                                        ControlToValidate="txtDiscount" ValidationExpression="^(?:[1-9]\d*|0)?(?:\.\d+)?$"> <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label2" runat="server" Text="أدخل النسبة بصيغة رقم صحيح"></asp:Label></div></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblCountry" runat="server" Text="بلد الصنع :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="inp-form"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblSupplier" runat="server" Text="المورد :"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="txtSupplier" runat="server" CssClass="inp-form"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblImages" runat="server" Text="صور المنتج"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" class="multi" />
                                                </td>
                                                <td>
                                                بإمكانك رفع عدة صور
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                </th>
                                                <td>
                                                    &nbsp;
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
                                                    <asp:Button ID="btnDelete" runat="server" Text="حذف" CssClass="form-submit" OnClick="btnDelete_Click" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                        </table> 
                                        <asp:Literal ID="ltrImgs" runat="server"></asp:Literal>
                                 <%--       <div id="gallery_box">
                                            <ul>
                                                <li><a href="#">
                                                    <img src="images/mostafa/1.jpg" />
                                                </a></li>
                                                <li>
                                                    <img src="images/mostafa/2.jpg" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/3.jpg" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/4.png" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/1.jpg" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/2.jpg" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/3.jpg" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/4.png" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/1.jpg" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/2.jpg" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/3.jpg" />
                                                </li>
                                                <li>
                                                    <img src="images/mostafa/4.png" />
                                                </li>
                                            </ul>
                                        </div>--%>
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
