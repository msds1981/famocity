<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admObjectsProd.aspx.cs" Inherits="FamoCity.admObjectsProd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    اسم المعرض
    <asp:DropDownList ID="ShopList" runat="server" >
    </asp:DropDownList>
    <br />
    اسم المنتج
    <asp:TextBox ID="ProductName" runat="server"></asp:TextBox>
    <br />
    الوصف
    <asp:TextBox ID="Description" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    السعر القديم
    <asp:TextBox ID="OldPrice" runat="server"></asp:TextBox>
    <br />
    السعر الجديد
    <asp:TextBox ID="NewPrice" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="Category" runat="server">
    </asp:DropDownList>
    <br />
    رفع صور المنتج

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

    <br /><br />
    Object Files
    <br />
                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload2" OnClientFileUploaded="fileUploaded"
                            MultipleFileSelection="Automatic" AllowedFileExtensions="unity3d" OnClientValidationFailed="validationFail"
                            TemporaryFolder="~/files_upload/" />
                        <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                            <div id="Div1" class="file-list">
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
                        اختر السين
                        <br />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:DropDownList ID="Scenes" runat="server" AutoPostBack="True" 
            onselectedindexchanged="Scenes_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
    اسم التريقر
        <asp:DropDownList ID="Trigger" runat="server">
        </asp:DropDownList>
    
    </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    الفيرجن
    <asp:TextBox ID="Version" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="SaveButton" runat="server" Text="حفظ" 
        onclick="SaveButton_Click" />
    <br />
    <br />
    
    
    <script type="text/javascript">
        function OnClientEntryAdding(sender, eventArgs) {
            //$telerik.$("#WorldMap").css('background-image', 'url(Images/' + eventArgs.get_entry().get_value() + '.png)');
            // alert("");
            alert(sender.toString());
        }
         
    </script>
    
 
    <telerik:RadDropDownTree ID="RadDropDownTree1" runat="server" DefaultMessage="Select location" Skin="Default"
        EnableFiltering="true" FilterSettings-Highlight="Matches" OnClientEntryAdding="OnClientEntryAdding">
    </telerik:RadDropDownTree>
    
    
    
    </form>
</body>
</html>
