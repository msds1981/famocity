<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="FamoCity.test" %>

<%@ Register src="userControls/Capatcha.ascx" tagname="Capatcha" tagprefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery.js"></script>
</head>
<body>
    <form id="form1" runat="server">
                         <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="fileUploaded"
                            MultipleFileSelection="Automatic" AllowedFileExtensions="jpeg,jpg,png,gif" OnClientValidationFailed="validationFail"
                            TemporaryFolder="~/files_upload/unity3d/" />
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
                                /*
                                    var name = args.get_fileName(),
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
   </form>
</body>
</html>
