<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploader.aspx.cs" Inherits="FamoCity.uploader" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                     <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="fileUploaded"
               MultipleFileSelection="Disabled" 
        AllowedFileExtensions="jpeg,jpg,png,gif" 
        onclientvalidationfailed="validationFail" TemporaryFolder="~/files_upload/" ManualUpload="false" />
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
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    </div>
    </form>
</body>
</html>
