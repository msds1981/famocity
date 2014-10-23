<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.master" AutoEventWireup="true"
    CodeBehind="shopProductAdd2.aspx.cs" Inherits="FamoCity.shopProductAdd2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">
    <div class="cms_page_title">
        <span>معلومات المتجر</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
            <form id="form" runat="server">
            <script type="text/javascript">
                    function deleteP() {
                     var pid=<%=id%>;
                        var ans = confirm("هل انت متأكد من حذف المنتج " + pid + "؟");
                        if (ans) {
                            $.ajax({
                                type: "POST",
                                url: '/shopProductAdd2.aspx/deleteP',
                                data: "{'pid':'"+pid+"'}" ,
                                async: false,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    if (msg.d > 0) {
                                       // alert("Product deleted successfuly");
                                       window.location("/shop/edit/prods");
                                    }
                                },
                                error: AjaxFailed
                            });
                        }
                    }

                    function AjaxFailed(result) {
                        alert(result.status + ' ' + result.statusText);
                    }

                    function deleteImg(fid) {
                        var ans = confirm('هل انت متأكد من حذف صورة المنتج ؟');
                        if (ans) {
                            $.ajax({
                                type: "POST",
                                url: '/shopProductAdd2.aspx/deleteImg',
                                data: "{'fid':'"+fid+"'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    if (msg.d > 0) {
                                        //alert("Image deleted successfuly");
                                        $("#f" + fid).fadeOut("slow");

                                    }
                                },
                                error: AjaxFailed
                            });
                        }
                    }

            </script>
            <%--            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
            <ul class="innput">
                <li><span class="title_cms">
                    <asp:Label ID="lblName" runat="server" Text="اسم المنتج :"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtName" runat="server" CssClass="random_input"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                        runat="server" ErrorMessage="أدخل اسم المنتج" CssClass="validd">
                    </asp:RequiredFieldValidator>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblDescription" runat="server" Text="وصف المنتج :"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="random_input" Rows="4"
                            TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDescription"
                        runat="server" ErrorMessage="اكتب وصف المنتج" CssClass="validd">
                    </asp:RequiredFieldValidator>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblBrand" runat="server" Text="الماركة :"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:DropDownList ID="ddlBrand" runat="server" CssClass="random_input">
                        </asp:DropDownList>
                    </div>
                    <span class="validd"></span></li>
                <li><span class="title_cms">
                    <asp:Label ID="lblBrice" runat="server" Text="السعر :"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="random_input"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPrice"
                        runat="server" ErrorMessage="اكتب وصف المنتج" CssClass="validd">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rePrice" runat="server" ControlToValidate="txtPrice"
                        ErrorMessage="ادخل السعر بالارقام " ValidationExpression="^(?:[1-9]\d*|0)?(?:\.\d+)?$">     </asp:RegularExpressionValidator>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblDiscount" runat="server" Text="نسبة الخصم :"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="random_input"></asp:TextBox>
                    </div>
                    <asp:RegularExpressionValidator ID="revDescount" runat="server" ControlToValidate="txtDiscount"
                        ValidationGroup="VgCompany" ValidationExpression="^(?:[1-9]\d*|0)?(?:\.\d+)?$"
                        ErrorMessage="ادخل النسبة بصيغة رقم صحيح " CssClass="validd">
                    </asp:RegularExpressionValidator>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblCountry" runat="server" Text="بلد الصنع :"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="random_input"></asp:TextBox>
                    </div>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblSupplier" runat="server" Text="المورد :"></asp:Label></span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtSupplier" runat="server" CssClass="random_input"></asp:TextBox>
                    </div>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblActiv" runat="server" Text="النشاط :"></asp:Label></span>
                    <div class="cms_input">
                        <asp:DropDownList ID="ddlActiv" runat="server" CssClass="random_input">
                        </asp:DropDownList>
                    </div>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblImages" runat="server" Text="صور المنتج"></asp:Label>
                </span>
                    <div class="cms_input">
                        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="fileUploaded"
                            MultipleFileSelection="Automatic" AllowedFileExtensions="jpeg,jpg,png,gif" OnClientValidationFailed="validationFail"
                            TemporaryFolder="~/files_upload/" />
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <div id="exFileList" class="file-list">
                                <div class="valid">اختر ملف للرفع</div>
                            </div>
                            <script type="text/javascript">
          //<![CDATA[
                                function validationFail(sender, args) {
                                    alert("error occurred while uploading image !!");
                                }
                                var fileList = null,
                                fileListUL = null;
                                function fileUploaded(sender, args) {

                                    var name = args.get_fileName(),
                                    li = document.createElement("li");

                                    if (fileList == null) {
                                        fileList = document.getElementById("exFileList");
                                        fileListUL = document.createElement("ul");
                                        fileList.appendChild(fileListUL);

                                        fileList.style.display = "block";
                                    }

                                    li.innerHTML = name;
                                    fileListUL.appendChild(li);
                                }
          //]]>
                            </script>
                        </telerik:RadScriptBlock>
                    </div>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="يجب رفع صورة واحدة على الاقل"
                        CssClass="validd"></asp:CustomValidator>
                </li>
                <li>
                    <asp:Literal ID="ltrImgs" runat="server"></asp:Literal>
                </li>
                <li>
                    <asp:Button ID="btnSave" runat="server" Text=" حفظ" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="حذف" OnClientClick="deleteP()" />
                </li>
            </ul>
            </form>
        </div>
    </div>
</asp:Content>
