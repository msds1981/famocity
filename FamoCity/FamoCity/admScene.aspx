<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn1.Master" AutoEventWireup="true" CodeBehind="admScene.aspx.cs" Inherits="FamoCity.admScene" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     $(document).ready(function () {
         ActiveSubMnu('mnu3D', 'subScene');
     });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
   <div id="content-outer">
        <!-- start content -->
        <div id="content">
            <div id="page-heading">
                <h1>
                 Scene</h1>
            </div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0" id="content-table">
                <tr>
                    <th rowspan="3" class="sized">
                        <img src="images/shared/side_shadowright.jpg" width="20" height="300" alt="" />
                    </th>
                    <th class="topleft">
                    </th>
                    <td id="tbl-border-top">
                        &nbsp;
                    </td>
                    <th class="topright">
                    </th>
                    <th rowspan="3" class="sized">
                        <img src="images/shared/side_shadowleft.jpg" width="20" height="300" alt="" />
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
                                                <th valign="top">
                                                    <asp:Label ID="Label1" runat="server" Text="اللغة"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="True" class="selectdate-n"
                                                        OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                  
                                                </td>
                                                <td>
                                                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ddlLang" Display="Dynamic"
                                                        runat="server" Operator="NotEqual" ValueToCompare="0" >
                                                        <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="lblcomVlang" runat="server" Text="الرجاء اختيار اللغة"></asp:Label></div>
                                                    </asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th >
                                                    <asp:Label ID="lblName" runat="server" Text="الاسم "></asp:Label>
                                                </th>
                                                <td>
                                                     <asp:TextBox ID="txtName" runat="server" CssClass="inp-form"></asp:TextBox>
                                                 
                                                </td>
                                                <td>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                 ControlToValidate="txtName" Display="Dynamic"  >
                                                  <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label3" runat="server" Text="يرجى ادخال الاسم"></asp:Label></div>
                                                 
                                                 </asp:RequiredFieldValidator>


                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <th >
                                                     <asp:Label ID="lblDesc" runat="server" Text="الوصف"></asp:Label>
                                                </th>
                                                <td>

                                                  <asp:TextBox ID="txtDesc" runat="server" Height="53px" TextMode="MultiLine" ></asp:TextBox>

                                                  
                                                </td>
                                                <td>

                                                 <asp:RequiredFieldValidator ID="rfvDescr" runat="server" 
                                                    ControlToValidate="txtDesc" Display="Dynamic" 
                                                   >
                                                    <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label4" runat="server" Text="يرجى ادخال الوصف"></asp:Label></div>
                                                                                                                                                            
                                                    </asp:RequiredFieldValidator>
                                                    
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <th >
                                                   <asp:Label ID="LblObjectCode" runat="server" Text="ObjectCode"></asp:Label></th>
                                                <td>

                                                     <asp:DropDownList ID="ddlObjectCode" runat="server">
                                                     </asp:DropDownList></td>
                                                <td>

                                                    <asp:CompareValidator ID="CompareValidator2" ControlToValidate="ddlObjectCode" Display="Dynamic"
                                                        runat="server" Operator="NotEqual" ValueToCompare="0" >
                                                        <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label8" runat="server" Text="الرجاء الاختيار من القائمة "></asp:Label></div>
                                                    </asp:CompareValidator></td>
                                            </tr>
                                            <tr>
                                                <th >
                                                    <asp:Label ID="lblObjectId" runat="server" Text="ObjectId"></asp:Label></th>
                                                <td>

                                                 <asp:TextBox ID="txtOpjectId" runat="server" CssClass="inp-form"></asp:TextBox></th></td>
                                                <td>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                        ControlToValidate="txtOpjectId"   >
                                                        <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label7" runat="server" Text="يرجى ادخال"></asp:Label></div> 
                                                        
                                                                                                                
                                                        </asp:RequiredFieldValidator>



                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 

                                                        runat="server" ControlToValidate="txtOpjectId"
                                                        Display="Dynamic" 
                                                        ValidationExpression="(^\d{0,3}[.]?\d{0,2}$)"
                                                       >
                                                         <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label5" runat="server" Text="يرجى ادخال رقم  بطريقة صحيحة"></asp:Label></div>
                                                        
                                                        
                                                        </asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                               <th >
                                                     <asp:Label ID="lblVersion" runat="server" Text="Version"></asp:Label>
                                           <td>

                                                 <asp:TextBox ID="txtVersion" runat="server" CssClass="inp-form"></asp:TextBox></td>
                                                <td>

                                                 <asp:RequiredFieldValidator ID="rfvVersion" runat="server" 
                                                        ControlToValidate="txtVersion" ErrorMessage="يرجى ادخال "  >
                                                        <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label6" runat="server" Text="يرجى ادخال"></asp:Label></div> 
                                                        
                                                                                                                
                                                        </asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                               <th >
                                                  <asp:Label ID="lblCode" runat="server" Text="الكود "></asp:Label><td>

                                                  
                                                 <asp:TextBox ID="txtCode" runat="server" CssClass="inp-form"></asp:TextBox></td>
                                                <td>

                                                 <asp:RequiredFieldValidator ID="rfvCode" runat="server" 
                                                        ControlToValidate="txtCode" Display="Dynamic" 
                                                      >
                                                        
                                                        <div class="error-left">
                                                        </div>
                                                        <div class="error-inner">
                                                            <asp:Label ID="Label2" runat="server" Text="يرجى ادخال الكود "></asp:Label></div> 
                                                        
                                                        </asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                               <th >
                                                      &nbsp;<td>

                                                      <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
                                              </th>  <td>

                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                               <th >
                                                      &nbsp;</th><td>

                                                  <asp:FileUpload ID="fuScene" runat="server" /></td>
                                                <td>

                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                               <th >
                                                      <asp:Label ID="lblExtention" runat="server" Text="امتداد الملف"></asp:Label>
                                                      <td>

                                                  <asp:Label ID="lblShowPath" runat="server"></asp:Label></td>
                                               </th> <td>

                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">

                                                     <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="حفظ"  />
                                                   
                                                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                                            onclientclick="return confirm(&quot;هل انت متاكد من الحذف &quot;)" 
                                                         Text="حذف" CausesValidation="False" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblfile" runat="server" Text="ملف الحفظ"></asp:Label>
                                                </th>
                                                <td valign="top">

                                                     <asp:Label ID="lblSaveFile" runat="server" Text="Label"></asp:Label></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th colspan="3">
                                                   <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                                     Text="اضافة جديدة " CausesValidation="False" />
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
        <!--  end content -->
        <div class="clear">
            &nbsp;</div>
    </div> 
    
    
    </ContentTemplate>
    </asp:UpdatePanel>

 
   

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

        <br />
     
</asp:Content>
