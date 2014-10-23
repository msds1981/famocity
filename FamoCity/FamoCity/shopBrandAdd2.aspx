<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.master" AutoEventWireup="true"
    CodeBehind="shopBrandAdd2.aspx.cs" Inherits="FamoCity.shopBrandAdd2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">



    <div class="cms_page_title">
        <span>تعديل ماركة</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
        <form id="form" runat="server">
                        <script type="text/javascript">

                            function deleteB() {
                            var bid=<%=id%>;
                                var ans = confirm('هل انت متأكد من حذف الماركة ؟');
                                if (ans) {
                                    $.ajax({
                                        type: "POST",
                                        url: '/shopBrandAdd2.aspx/deleteBrand',
                                        data: "{'bid':'"+bid+"'}" ,
                                        async: false,
                                        contentType: "application/json; charset=utf-8",
                                       dataType: "json",
                                        success: function (msg) {
                                            if (msg.d > 0) {
                                               // alert("Brand deleted successfuly");

                                            }
                                        },
                                        error: AjaxFailed
                                    });
                                }
                            }

                            function AjaxFailed(result) {
                                alert(result.status + ' ' + result.statusText);
                            }

    </script>
            <ul class="innput">
                <li><span class="title_cms">اسم الماركة</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="random_input"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" class="validd" runat="server" ValidationGroup="title" ErrorMessage="ادخل اسم الماركة" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                     </li>
                <li><span class="title_cms">الوصف</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtDescriptipn" runat="server" Rows="4" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <span class="validd"></span>
                    </li>
                <li><span class="title_cms">الشعار</span>
                    <div class="cms_input">
                    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
                        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="fileUploaded"
               MultipleFileSelection="Disabled" 
        AllowedFileExtensions="jpeg,jpg,png,gif" 
        onclientvalidationfailed="validationFail" TemporaryFolder="~/files_upload/" MaxFileInputsCount="1"/>
        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <div id="exFileList" class="file-list">
               <div class="valid">اختر ملف للرفع</div>
          </div>
          <script type="text/javascript">



          //<![CDATA[
              function validationFail(sender, args) {
                  alert("fail");
              }
              var fileList = null,
                    fileListUL = null;

              function fileUploaded(sender, args) {
                 // alert("");
                 /* var name = args.get_fileName(),
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

              function OnClientFilesUploaded(sender, args) {
                 // $("<%= btnSave.ClientID%>").hide();
              }
          //]]>
          </script>
     </telerik:RadScriptBlock>
                        <%--<asp:FileUpload ID="fulImage" runat="server" />--%>
                        <%--<input type="file" name="file" id="file" />
                                    <script>                                        $('#file').customFileInput();</script>--%>
                    </div>
                    <span class="validd"></span></li>
                <li>
                    <asp:Image ID="imgLogo" runat="server" />
                </li>
                <li>
                    <div class="cms_input">
                        <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="form-submit" OnClick="btnSave_Click" ValidationGroup="title"/>
                        <asp:Button ID="btnDelete" runat="server" Text="حذف" CssClass="form-submit" OnClientClick="deleteB()"  />
                    </div>
                </li>
            </ul>
            </form>
        </div>
    </div>
</asp:Content>
