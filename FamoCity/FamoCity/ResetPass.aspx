<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="ResetPass.aspx.cs" Inherits="FamoCity.ResetPass" %>
    <%@ Register TagPrefix="uc" TagName="Capcha" Src="~/userControls/Capatcha.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/forgetpassword.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/themes/base/jquery-ui.css" id="theme" />
<link rel="stylesheet" href="styles/Default/jquery.fileupload-ui.css" />
<%--<link rel="stylesheet" href="styles/Default/style.css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div class="ccontent size">
            <div class="bigbox boxsize-small">
                <form id="form1" runat="server">
                <div class="title-line">
                    طلب تغيير كلمة السر</div>
                <div class="form-register-r">
                    <div class=" form-line-r">
                        <div class="form-title-r">
                              <asp:Label ID="lblEmail" runat="server" Text="ادخل البريد الالكتروني "></asp:Label></div>
                              <asp:TextBox ID="txtEmail" runat="server" CssClass="form-field-r" ></asp:TextBox>
                     <%--   <input class="form-field-r" type="text" name="email" />--%>
                        <div class="validation">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtEmail" Display="Dynamic" 
            ErrorMessage="يرجى ادخال الايميل" ValidationGroup="VgRepassport"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtEmail" Display="Dynamic" 
            ErrorMessage="يرجى ادخال الايميل بطريقة صحيحة" 
            ValidationGroup="VgRepassport" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></div>
                    </div>
 
 
 
                    
                    <div class="captcha">
                    <uc:Capcha ID="Capcha1" runat="server" />
                    </div>
                <div class=" form-label-cancel">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:Label ID="lblCap" runat="server" Text=""></asp:Label>
                    </div>
                    
                    <div class=" form-label-r">
                     <asp:Button ID="btnsend" runat="server" onclick="btnsend_Click" Text="ارسال" 
            ValidationGroup="VgRepassport" CssClass="css_btn_class"/>
                     <%--   <a href="#" class="css_btn_class">ارسال</a>--%>
                    </div>
 
 
 
                </div>
<%-- <div id="fileupload">
    <form action="FileTransferHandler.ashx" method="post" enctype="multipart/form-data">
        <div class="fileupload-buttonbar">
            <label class="fileinput-button">
                <span>Add files...</span>
                <input type="file" name="files[]" multiple="multiple" />
            </label>
            <button type="button" class="delete button">Delete all files</button>
			<div class="fileupload-progressbar"></div>
        </div>
    </form>
    <div class="fileupload-content">
        <table class="files"></table>
    </div>
    <div id="filesUploaded" style="display:none">
   </div>
</div>
<script id="template-upload" type="text/x-jquery-tmpl">
    <tr class="template-upload{{if error}} ui-state-error{{/if}}">
        <td class="preview"></td>
        <td class="name">${name}</td>
        <td class="size">${sizef}</td>
        {{if error}}
            <td class="error" colspan="2">Error:
                {{if error === 'maxFileSize'}}File is too big
                {{else error === 'minFileSize'}}File is too small
                {{else error === 'acceptFileTypes'}}Filetype not allowed
                {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                {{else}}${error}
                {{/if}}
            </td>
        {{else}}
            <td class="progress"><div></div></td>
            <td class="start"><button>Start</button></td>
        {{/if}}
        <td class="cancel"><button>Cancel</button></td>
    </tr>
</script>
<script id="template-download" type="text/x-jquery-tmpl">
    <tr class="template-download{{if error}} ui-state-error{{/if}}">
        {{if error}}
            <td></td>
            <td class="name">${name}</td>
            <td class="size">${sizef}</td>
            <td class="error" colspan="2">Error:
                {{if error === 1}}File exceeds upload_max_filesize (php.ini directive)
                {{else error === 2}}File exceeds MAX_FILE_SIZE (HTML form directive)
                {{else error === 3}}File was only partially uploaded
                {{else error === 4}}No File was uploaded
                {{else error === 5}}Missing a temporary folder
                {{else error === 6}}Failed to write file to disk
                {{else error === 7}}File upload stopped by extension
                {{else error === 'maxFileSize'}}File is too big
                {{else error === 'minFileSize'}}File is too small
                {{else error === 'acceptFileTypes'}}Filetype not allowed
                {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                {{else error === 'uploadedBytes'}}Uploaded bytes exceed file size
                {{else error === 'emptyResult'}}Empty file upload result
                {{else}}${error}
                {{/if}}
            </td>
        {{else}}
            <td class="preview">
                {{if thumbnail_url}}
                    <a href="${url}" target="_blank"><img src="${thumbnail_url}"></a>
                {{/if}}
            </td>
            <td class="name">
                <a href="${url}"{{if thumbnail_url}} target="_blank"{{/if}}>${name}</a>
            </td>
            <td class="size">${sizef}</td>
            <td colspan="2"></td>
        {{/if}}
        <td class="delete">
            <button data-type="${delete_type}" data-url="${delete_url}">Delete</button>
        </td>
    </tr>
</script>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/jquery-ui.min.js"></script>
<script src="//ajax.aspnetcdn.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>
<script src="scripts/Default/jquery.iframe-transport.js"></script>
<script src="scripts/Default/jquery.fileupload.js"></script>
<script src="scripts/Default/jquery.fileupload-ui.js"></script>
<script src="scripts/Default/application.js"></script>
--%>                </form>
            </div>
        </div>
    </div>
</asp:Content>
