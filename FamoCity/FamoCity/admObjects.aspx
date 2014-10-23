<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn1.Master" AutoEventWireup="true" CodeBehind="admObjects.aspx.cs" Inherits="FamoCity.admObjects" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       $(document).ready(function () {
           ActiveSubMnu('mnu3D', 'subObjects');
       });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <%--<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   
    <div id="content-outer">
        <!-- start content -->
        <div id="content">
            <div id="page-heading">
                <h1>
                   Objects</h1>
            </div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0" id="content-table">
                <tr>
                    <th rowspan="3" class="sized">
                        <img src="/images/shared/side_shadowright.jpg" width="20" height="300" alt="" />
                    </th>
                    <th class="topleft">
                    </th>
                    <td id="tbl-border-top">
                        &nbsp;
                    </td>
                    <th class="topright">
                    </th>
                    <th rowspan="3" class="sized">
                        <img src="/images/shared/side_shadowleft.jpg" width="20" height="300" alt="" />
                    </th>
                </tr>
                <tr>
                    <td id="tbl-border-left">
                    </td>
                    <td>
                        <!--  start content-table-inner -->
                        <div id="content-table-inner">
                            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                <tr valign="top">
                                    <td>
                                        <!-- start id-form -->
                                        <table border="0" cellpadding="0" cellspacing="0" id="id-form">
                                            <tr>
                                                <th >
                                                   <asp:Label ID="lblCode" runat="server" Text="Code"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:Label ID="txtCodeLable" runat="server" Text="Label"></asp:Label>
                                                   <%--  <asp:TextBox ID="txtCode" runat="server" CssClass="inp-form"></asp:TextBox>--%>
                                                </td>
                                               <%-- <td>
                                                   <asp:RequiredFieldValidator ID="rfvCode" runat="server" 
                                                        ControlToValidate="txtCode" Display="Dynamic" 
                                                       >
                                                           <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label3" runat="server" Text="الرجاء ادخال الكود"></asp:Label></div>
                                                        
                                                        
                                                        
                                                        </asp:RequiredFieldValidator>
                                                </td>--%>
                                            </tr>

                                             <tr>
                                                <th >
                                                   <asp:Label ID="Label12" runat="server" Text="Name"></asp:Label>
                                                </th>
                                                <td>
                                                     <asp:TextBox ID="objName" runat="server" CssClass="inp-form"></asp:TextBox>
                                                </td>
                                                <td>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                        ControlToValidate="objName" Display="Dynamic" 
                                                       >
                                                           <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label13" runat="server" Text="الرجاء ادخال الاسم"></asp:Label></div>
                                                        
                                                        
                                                        
                                                        </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <th >
                                                   <asp:Label ID="Label14" runat="server" Text="Description"></asp:Label>
                                                </th>
                                                <td>
                                                     <asp:TextBox ID="objDescription" runat="server" CssClass="inp-form"></asp:TextBox>
                                                </td>
                                                <td>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                        ControlToValidate="objDescription" Display="Dynamic" 
                                                       >
                                                           <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label15" runat="server" Text="الرجاء ادخال الوصف"></asp:Label></div>
                                                        
                                                        
                                                        
                                                        </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <th >
                                                   <asp:Label ID="lblVersion" runat="server" Text="Version"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:Label ID="Versionlable" runat="server" Text="Label"></asp:Label>
                                                 <%--  <asp:TextBox ID="txtVersion" runat="server" CssClass="inp-form"></asp:TextBox>--%>
                                                
                                                    <%--     <input type="text" class="inp-form" />--%>
                                                </td>
                                              <%--  <td>

                                                 <asp:RequiredFieldValidator ID="rfvVersion" runat="server" 
                                                        ControlToValidate="txtVersion" Display="Dynamic" >
                                                        <div class="error-left">
                                                         </div>
                                                          <div class="error-inner">
                                                         <asp:Label ID="Label4" runat="server" Text="الرجاء ادخال Version"></asp:Label></div>
        
        
                                                        </asp:RequiredFieldValidator>

                                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                                        runat="server" ControlToValidate="txtVersion"    Display="Dynamic" 
                                                        ErrorMessage="يرجى ادخال رقم  بطريقة صحيحة" ValidationExpression="(^\d{0,3}[.]?\d{0,2}$)" >
                                                       </div>
                                                       <div class="error-inner">
                                                        <asp:Label ID="Label5" runat="server" Text="يرجى ادخال رقم  بطريقة صحيحة"></asp:Label></div>    
                         
                                                      </asp:RegularExpressionValidator>

   
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <th >
                                                    <asp:Label ID="lblScene" runat="server" Text="Scene"></asp:Label>
                                                </th>
                                                <td>
                                                    

                                                      <asp:DropDownList ID="ddlScene" runat="server" AutoPostBack="True" 
                                                          onselectedindexchanged="ddlScene_SelectedIndexChanged" Enabled="False">
                                                  </asp:DropDownList>




                                                </td>
                                                <td>

                                                 <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                    ControlToValidate="ddlTrigger" Display="Dynamic" 
                                                    ErrorMessage="الرجاء  الاختيار " Operator="NotEqual" ValueToCompare="0"  >
                                                    <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label6" runat="server" Text="الرجاء  الاختيار"></asp:Label></div>
                                                    
                                                    
                                                    </asp:CompareValidator>

                                                </td>
                                            </tr>
                                            <tr>
                                                <th >
                                                
                                                  <asp:Label ID="LblTrigger" runat="server" Text="Trigger"></asp:Label>
                                                 </th>
                                                <td>
                                                
                                                  
                                                  
                                                   <asp:DropDownList ID="ddlTrigger" runat="server" Enabled="False">
    </asp:DropDownList>
                                                  </td>
                                                <td>

                                                  <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                                    ControlToValidate="ddlScene" Display="Dynamic" 
                                                    Operator="NotEqual" 
                                                    ValueToCompare="0" >
                                                    <div class="error-left">
                                                     </div>
                                                    <div class="error-inner">
                                                     <asp:Label ID="Label7" runat="server" Text="الرجاء اختيار  الحدث"></asp:Label></div>
                                                    
                                                    </asp:CompareValidator></td>
                                            </tr>
                                   <%--         <tr>
                                                <th >
                                             <asp:Label ID="lblUpFile" runat="server" Text="File"></asp:Label></th>
                                                <td>
                                                  <asp:FileUpload ID="fuFile" runat="server" /></td>
                                                <td>

                                                   <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
                                            </tr>--%>
                                         
                                            <%--<tr>
                                                <th >
                                                  <asp:Label ID="lblObjCode" runat="server" Text="ObjectCode"></asp:Label></th>
                                                <td>
                                                  <asp:DropDownList ID="ddlObjectCode" runat="server" Visible="False">
                                                   </asp:DropDownList></td>
                                                <td>
                                                                                               
                                                <asp:CompareValidator ID="CompareValidator3" runat="server"  ControlToValidate="ddlObjectCode" Display="Dynamic"       
                                                  ErrorMessage="الرجاء الاختيار " Operator="NotEqual" ValueToCompare="0" >
                                                  <div class="error-left">
                                                     </div>
                                                    <div class="error-inner">
                                                     <asp:Label ID="Label8" runat="server" Text="الرجاء الاختيار"></asp:Label></div>
                                                  
                                                  </asp:CompareValidator></td>
                                            </tr>--%>
                                            <%--<tr>
                                                <th >
                                                 <asp:Label ID="lblObjId" runat="server" Text="ObjectId"></asp:Label></th>
                                                <td>
                                                   <asp:TextBox ID="txtObjId" runat="server" CssClass="inp-form"></asp:TextBox></td>
                                                <td>
                                                                                               
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtObjId" Display="Dynamic"   >
                                                <div class="error-left">
                                                     </div>
                                                    <div class="error-inner">
                                                     <asp:Label ID="Label9" runat="server" Text="الرجاء الاختيار ObjectId"></asp:Label></div>
                                                                                                
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                                runat="server" ControlToValidate="txtObjId"
                                                Display="Dynamic" 
                                               ValidationExpression="^\d+$">
                                                
                                                <div class="error-left">
                                                     </div>
                                                    <div class="error-inner">
                                                     <asp:Label ID="Label10" runat="server" Text="يرجى ادخال رقم  بطريقة صحيحة"></asp:Label></div>
                                                
                                                </asp:RegularExpressionValidator></td>
                                            </tr>--%>


                                                <tr>
                    <th>
                        <asp:Label ID="Label11" runat="server" Text="موقعه على الخريطة  "></asp:Label>
                    </th>
                    <td>
                        <asp:TextBox ID="MapAddress" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                                            <%--<tr>
                                                <th >
                                                    <asp:Label ID="lblsavfile" runat="server" Text="مجلد حفظ الملف "></asp:Label>
                                                </th>
                                                <td>
                                                 <asp:Label ID="lblShowFile" runat="server"></asp:Label></td>

                                                <td>
                                                                                               
                                                    &nbsp;</td>
                                            </tr>--%>


                                             <%--<tr>
                                                <th >
                                                    <asp:Label ID="Label1" runat="server" Text="المجلد "></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:DropDownList ID="ddlFolder" runat="server">
                                                    </asp:DropDownList>

                                                </td>
                                                 <td>
                                                     &nbsp;</td>          
                                                          <td>
                                                              &nbsp;</td>            
                                                <td>
                                                    &nbsp;</td>                                      
                                                    <caption>
                                                        &nbsp;</caption>
                                    </td>
                                            </tr>--%>



                                               <%--<tr>
                    <th>
                        <asp:Label ID="Label2" runat="server" Text="تحميل الصورة "></asp:Label>
                    </th>
                    <td>

                        <asp:FileUpload ID="ImageUploader" runat="server"   />
                    </td>
                     <td>

                                                   <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
                    <td>
                        &nbsp;
                    </td>
                </tr>--%>



                                            <tr>
                                                <th >
                                             <asp:Label ID="lblUpFile" runat="server" Text="File"></asp:Label></th>
                                                <td>
                                                <%--  <asp:FileUpload ID="fuFile" runat="server" />--%>
                                                    <telerik:RadAsyncUpload runat="server" ID="ImageUploader" OnClientFileUploaded="fileUploaded"
                            MultipleFileSelection="Automatic" AllowedFileExtensions="jpg,bmp,png,jpeg" OnClientValidationFailed="validationFail"
                            TemporaryFolder="~/files_upload/" MaxFileInputsCount="1" />

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
                                                  </td>
                                                <td class="style1">

                                                   <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
                                            </tr>



               <tr>
                                                <th >
                                                    <asp:Label ID="lblfile" runat="server" Text="امتداد حفظ الملف"></asp:Label>
                                                </th>
                                                <td>
                                                 <asp:Label ID="lblSaveFile" runat="server"></asp:Label></td>
                                                <td>

                                                    &nbsp;</td>
                                            </tr>


                                               <tr>
                                                <th >
                                                    <asp:Label ID="lblfileimage" runat="server" Text="امتداد حفظ الصورة"></asp:Label>
                                                </th>
                                                <td>
                                                 <asp:Label ID="lblSaveimage" runat="server"></asp:Label></td>
                                                <td>

                                                    &nbsp;</td>
                                            </tr>





                                              <tr>
                                                <th >
                                                    <asp:Label ID="Label1" runat="server" Text="الصورة"></asp:Label>
                                                </th>
                                                <td>

                                                    <asp:Image ID="ObjImage" runat="server" />
                                                 
                                                 </td>
                                                <td>

                                                    &nbsp;</td>
                                            </tr>

                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">

                                                    <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="حفظ" CssClass="form-submit"
                                                  />
                                                     <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                                                onclientclick="return confirm(&quot;هل انت متاكد من حذف السجل ؟&quot;)" 
                                                                Text="حذف " />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="3">
                                                   <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                                   Text="اضافة جديدة " />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="3">
                                                    &nbsp;</th>
                                            </tr>
                                        </table>
                                        <!-- end id-form  -->
                                    </td>
                                    <td>
                                        <!--  start related-activities -->
                                        <div id="related-activities">
                                            <!--  start related-act-top -->
                                            <div id="related-act-top">
                                                <img src="/images/forms/header_related_act.gif" width="271" height="43" alt="" />
                                            </div>
                                            <!-- end related-act-top -->
                                            <!--  start related-act-bottom -->
                                            <div id="related-act-bottom">
                                                <!--  start related-act-inner -->
                                                <div id="related-act-inner">
                                                    <div class="left">
                                                        <a href="">
                                                            <img src="/images/forms/icon_plus.gif" width="21" height="21" alt="" /></a></div>
                                                    <div class="right">
                                                        <h5>
                                                            شرح الاستخدام</h5>
                                                        <ul class="greyarrow">
                                                            <li></li>
                                                            <li></li>
                                                        </ul>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                                <!-- end related-act-inner -->
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- end related-act-bottom -->
                                        </div>
                                        <!-- end related-activities -->
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="/images/shared/blank.gif" width="695" height="1" alt="blank" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <div class="clear">
                            </div>
                        </div>
                        <!--  end content-table-inner  -->
                    </td>
                    <td id="tbl-border-right">
                    </td>
                </tr>
                <tr>
                    <th class="sized bottomleft">
                    </th>
                    <td id="tbl-border-bottom">
                        &nbsp;
                    </td>
                    <th class="sized bottomright">
                    </th>
                </tr>
            </table>
            <div class="clear">
                &nbsp;</div>
        </div>
   
   </ContentTemplate>
   
    </asp:UpdatePanel>


        <!--  end content -->
        <div class="clear">
            &nbsp;</div>
    
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <hr />
    <br />
    <br />







    <p>
    <br />
 
   
    
</p>
<p>
 
   
   
</p>
<p>
   
   
   
</p>
<p>
    
   
   
</p>
<p>
   
&nbsp;
   
</p>
<p>
   
</p>
<p>
    
   
</p>
<p>
  
  
    
</p>
<p>
   
</p>
<p>
   
   
    
</p>
</asp:Content>
