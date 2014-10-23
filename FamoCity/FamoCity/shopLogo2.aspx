<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.master" AutoEventWireup="true"
    CodeBehind="shopLogo2.aspx.cs" Inherits="FamoCity.shopLogo2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">
    <div class="cms_page_title">
        <span>معلومات المتجر</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
        <form id="form" runat="server">
            <ul class="innput">
                <li><span class="title_cms">
                    <asp:Label ID="lblLogo" runat="server" Text="شعار الشركة"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:Image ID="ImgLogo" runat="server" Height="119px" Width="107px" class="random_input" />
                    </div>
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lblUpload" runat="server" Text="تحميل صورة جديده"></asp:Label>
                </span>
                    <div class="cms_input">
                        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="fileUploaded"
                                   MultipleFileSelection="Disabled" 
                            AllowedFileExtensions="jpeg,jpg,png,gif" 
                            onclientvalidationfailed="validationFail" TemporaryFolder="~/files_upload/" MaxFileInputsCount="1"/>
                            <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <div id="exFileList" class="file-list">
                                   <strong>Uploaded files in the target folder:</strong>
                              </div>
                              <script type="text/javascript">
                              //<![CDATA[
                                  function validationFail(sender, args) {
                                      alert("fail");
                                  }
                                  var fileList = null,
                                        fileListUL = null;

                                  function fileUploaded(sender, args) {
                                      /*var name = args.get_fileName(),
                                             li = document.createElement("li");

                                      if (fileList == null) {
                                          fileList = document.getElementById("exFileList");
                                          fileListUL = document.createElement("ul");
                                          fileList.appendChild(fileListUL);

                                          fileList.style.display = "block";
                                      }

                                      li.innerHTML = name;
                                      fileListUL.appendChild(li);*/
                                  }
                              //]]>
                              </script>
                         </telerik:RadScriptBlock>
                    </div>
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                </li>
                <li>
                    <div class="cms_input">
                        <asp:HiddenField ID="hdfSaveImage" runat="server" />
                    </div>
                </li>
                <li>
                    <asp:Button ID="btnAddImage" runat="server" OnClick="btnAddImage_Click" Text="حفظ" />
                </li>
            </ul>
        </form>
        </div>
    </div>
</asp:Content>
