<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopBanars.aspx.cs" Inherits="FamoCity.shopBanars" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuPages', 'submnuBanrs');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-outer">
        <!-- start content -->
        <div id="content">
            <div id="page-heading">
                <h1>
                    <asp:Label ID="lblHeader" runat="server" Text="البنرات"></asp:Label></h1>
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
                                                <th>
                                                    &nbsp;</th>


                                                <td style="width: 390px;">

                      <asp:Image ID="ImgFirstLogo" runat="server" Height="119px" Width="107px" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    صورة رقم ١ :
                                                </th>


                                                <td style="width: 390px;">

                                                    <asp:HiddenField ID="hfFirstImage" runat="server" value="0"/>

                                                    <asp:FileUpload ID="fuFirstImage" runat="server" CssClass="file_1" />
                                                    <br />
                                                </td>
                                                <td>
                                                    <div class="bubble-left">
                                                    </div>
                                                    <div class="bubble-inner">
                                                        JPEG, GIF 5MB max per image</div>
                                                    <div class="bubble-right">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    وصف الصورة &nbsp;</th>
                                                <td>
                                                     <asp:TextBox ID="txtDescFirstImage" runat="server" CssClass="form-textarea" 
                                                         Height="26px" TextMode="MultiLine"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;</th>
                                                <td>
                      <asp:Image ID="ImgSecondLogo" runat="server" Height="119px" Width="107px" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    صورة رقم ٢ :
                                                </th>
                                                <td>
                                                    <asp:HiddenField ID="hfISecondmages" runat="server" value="0"/>
                                                    <asp:FileUpload ID="fusecondImage" runat="server" CssClass="file_1" />
                                                </td>
                                                <td>
                                                    <div class="bubble-left">
                                                    </div>
                                                    <div class="bubble-inner">
                                                        JPEG, GIF 5MB max per image</div>
                                                    <div class="bubble-right">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;وصف الصورة الثانية &nbsp;</th>
                                                <td>
                                                     <asp:TextBox ID="txtDescSecond" runat="server" CssClass="form-textarea" 
                                                         Height="26px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;</th>
                                                <td>
                      <asp:Image ID="ImgThirdLogo" runat="server" Height="119px" Width="107px" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    صورة رقم ٣ :
                                                </th>
                                                <td>
                                                    <asp:HiddenField ID="hfTthirdImages" runat="server" value="0"/>
                                                    <asp:FileUpload ID="fuThirdImage" runat="server" CssClass="file_1" />
                                                </td>
                                                <td>
                                                    <div class="bubble-left">
                                                    </div>
                                                    <div class="bubble-inner">
                                                        JPEG, GIF 5MB max per image</div>
                                                    <div class="bubble-right">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    وصف الصورة الثالثة
                                                </th>
                                                <td>
                                                     <asp:TextBox ID="txtDescThird" runat="server" CssClass="form-textarea" 
                                                         Height="26px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;</th>
                                                <td>
                      <asp:Image ID="ImgFourLogo" runat="server" Height="119px" Width="107px" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    صورة رقم ٤ :
                                                </th>
                                                <td>
                                                    <asp:FileUpload ID="fuFourImage" runat="server" CssClass="file_1" />
                                                    <asp:HiddenField ID="hfFourImage" runat="server" value="0"/>
                                                </td>
                                                <td>
                                                    <div class="bubble-left">
                                                    </div>
                                                    <div class="bubble-inner">
                                                        JPEG, GIF 5MB max per image</div>
                                                    <div class="bubble-right">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    وصف الصورة الثالثة &nbsp;</th>
                                                <td valign="top">
                                                     <asp:TextBox ID="txtDescFord" runat="server" CssClass="form-textarea" 
                                                         Height="26px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">
                                                    &nbsp;</td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">
                                                    <asp:Button ID="btnAddBanarImage" runat="server" Text="" 
                                                        class="form-submit" onclick="btnAddBanarImage_Click" />
                                                    <%--  <input type="button" value="" class="form-submit" />--%>
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
