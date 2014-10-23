<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.master" AutoEventWireup="true" CodeBehind="shopBanars2.aspx.cs" Inherits="FamoCity.shopBanars2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">
    <div class="cms_page_title">
        <span>معلومات المتجر</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
            <form id="form" runat="server">
            <ul class="innput">
                <li><span class="title_cms">اختر الصور المراد رفعها</span>
                    <div class="cms_input">
                        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="fileUploaded"
               MultipleFileSelection="Automatic" 
        AllowedFileExtensions="jpeg,jpg,png,gif" 
        onclientvalidationfailed="validationFail" TemporaryFolder="~/files_upload/" />
        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <div id="exFileList" class="file-list">
               <%--<strong>Uploaded files in the target folder:</strong>--%>
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
                    
                </li>
                <asp:Literal ID="ltrImgs" runat="server"></asp:Literal>
	<!-- Mostafa Start -->
<%--	<div id="gallery_box">
	<ul>
	
	<li>
	<a href="#">
	<img src="images/mostafa/1.jpg" />
	</a>
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
	</div>
--%>	<!-- Mostafa End -->
                <li>
                    <asp:Button ID="addIntoCompany" runat="server" Text="حفظ" ValidationGroup="VgCompany"
                        OnClick="addIntoCompany_Click1" />
                </li>
            </ul>
            </form>
        </div>
    </div>
</asp:Content>
