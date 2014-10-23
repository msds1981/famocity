<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="usrRegStep3.aspx.cs" Inherits="FamoCity.usrRegStep3" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/step3.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ccontent">
        <div class="meter">
            <ol>
                <li class="active"><span class="step">1</span> <span class="stage">انشاء حساب</span>
                </li>
                <li class="active"><span class="step">2</span> <span class="stage">معلومات شخصية</span>
                </li>
                <li class="active"><span class="step">3</span> <span class="stage">صورة شخصية</span>
                </li>
                <li><span class="step">4</span> <span class="stage">الاهتمامات</span> </li>
                <li><span class="step">5</span> <span class="stage">دعوة الاصدقاء</span> </li>
            </ol>
        </div>
        <div class="bigbox boxsize-small">
            <form id="form1" runat="server">
            <div class="title-line">
                صورة شخصية</div>
            <div class="form-register-r">
                <div class=" form-line-r">
                    <div class="form-title-r">
                        <%--<asp:Label ID="lblmsg" runat="server"></asp:Label>--%>
                    </div>
                    <%--<asp:FileUpload ID="fuImage" runat="server" class="form-field-r" />--%>
                    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
                    <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="fileUploaded"
                        TemporaryFolder="~/files_upload/" MultipleFileSelection="Disabled" AllowedFileExtensions="jpeg,jpg,png,gif"
                        OnClientValidationFailed="validationFail" MaxFileInputsCount="1" />
                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                        <div id="exFileList" class="file-list">
                            <%--<strong>Uploaded files in the target folder:</strong>--%>
                        </div>
                        <script type="text/javascript">
          //<![CDATA[
                            function validationFail(sender, args) {
                                alert("المساحة القصوى للصورة ٤ م ب من امتداد jpg,png");
                            }

                            function fileUploaded(sender, args) {
                                var name = args.get_fileName();
                                var path = "~/files_upload/" + name;
                                document.getElementById("imgDisplay").src = path;
                            }
          //]]>
                        </script>
                    </telerik:RadScriptBlock>
                    <%--     <input class="form-field-r" type="file" name="email" />--%>
                    <%--                        <div class="validation">
                            خط احمر</div>--%>
                </div>
                <asp:Label class=" form-label-x" ID="lblError" runat="server" Text="المساحة القصوى للصورة ٤ م ب من امتداد jpg,png"></asp:Label>
                <div class="photo-up transparent">
                    <img id="imgDisplay" alt="" height="119px" width="107px" />
                    <%-- <img src="#" />--%></div>
                <div class=" form-label-cancel">
                    <asp:Button ID="btnIgnore" runat="server" Text="تجاهل" class="css_btn_class" OnClick="btnIgnore_Click" />
                    <%--   <a href="#" class="css_btn_class">تجاهل</a>--%>
                </div>
                <div class=" form-label-r">
                </div>
                <div class=" form-label-r">
                    <asp:Button ID="btnNext" runat="server" Text="التالي" class="css_btn_class" OnClick="btnNext_Click" />
                    <%--   <a href="#" class="css_btn_class">التالي</a>--%>
                </div>
            </div>
            </form>
        </div>
    </div>
</asp:Content>
