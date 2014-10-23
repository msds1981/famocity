<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="Feedback.aspx.cs" Inherits="FamoCity.Feedback" %>
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/feedback.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ccontent">
    <div class="bigbox boxsize-small">
        <form id="form1" runat="server">
            <div class="title-line">
                الشكاوي والاقتراحات</div>
            <div class="form-register-r">
                

                <div class=" form-line-r">
                    <div class="form-title-r">
                            <asp:Label ID="lblSubject" runat="server" Text="الموضوع "></asp:Label></div>
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-field-r"></asp:TextBox>
                    <div class="validation">
                    </div>
                </div>

                <div class=" form-line-r form-padding-right">
                    <div class="form-title-r">
                            <asp:Label ID="lblPages" runat="server" Text="الصفحة "></asp:Label></div>
                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="selectdate-n">
                            </asp:DropDownList>
                    <div class="validation">
                    </div>
                </div>
                
                <div class=" form-line-r">
                    <div class="form-title-r">
                            <asp:Label ID="lblDescription" runat="server" Text="وصف المشكلة "></asp:Label></div>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-field-r" Height="50px" 
                    TextMode="MultiLine"></asp:TextBox>
                    <div class="validation">
                    </div>
                </div>
                
                <div class=" form-line-r form-padding-right">
                    <div class="form-title-r">
                        <asp:Label ID="lblFilde" runat="server" Text="تحميل الملف "></asp:Label></div>

                    <div style="float:right; width:217;">
                        <telerik:radscriptmanager runat="server" ID="RadScriptManager1" />
                        <telerik:radasyncupload runat="server" ID="AsyncUpload1" MultipleFileSelection="Disabled"
                            AllowedFileExtensions="jpeg,jpg,png,gif,rar,zip" OnClientValidationFailed="validationFail"
                            TemporaryFolder="~/files_upload/" MaxFileInputsCount="1" />
                        <telerik:radscriptblock ID="RadScriptBlock1" runat="server">
                        <script type="text/javascript">
                            function validationFail(sender, args) {
                                alert("الملفات المسموحة من امتداد : jpeg, jpg, png, gif, rar, zip");
                            }
                        </script>
                        </telerik:radscriptblock>
                        <div id="exFileList" class="file-list"></div>
                    </div>
                      
                    <div class="validation">
                    </div>
                </div>
                
                <div class=" form-label-cancel">
                </div>
                <div class=" form-label-r">
                </div>
                <div class=" form-label-r">
                    <asp:Button ID="btnRegistar" runat="server" Text="تسجيل"
                    ValidationGroup="VgRegistor" class="css_btn_class" 
                    onclick="btnRegistar_Click" />
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                </div>


               
                

            </div>
        </form>
    </div>
</div>
</asp:Content>
