<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopBasic3.aspx.cs" Inherits="FamoCity.shopBasic3" %>
<script type="text/javascript">
    $(document).ready(function () {
            $("#btnSaveUser").click(function () {
                var Uname = $("#<%=txtUser.ClientID%>").val();
                var Email = $("#<%=txtEmail.ClientID%>").val();
                var Fname = $("#<%=txtFirstName.ClientID%>").val();
                var Lname = $("#<%=txtLastName.ClientID%>").val();
                var DdlDay = $("#<%=ddlDay.ClientID%>").val();
                var DdlMonth = $("#<%=ddlMonth.ClientID%>").val();
                var DdlYear = $("#<%=ddlYear.ClientID%>").val();
                var DdlNationality = $("#<%=ddlNationality.ClientID%>").val();
                
                $("#span1").html("لم يتم الحفظ");
                if (Uname == "") {

                    $("#lblmsg").html("ادخل اسم المستخدم");
                    return false;
                }
                if (Email == "") {

                    $("#lblmsg").html("ادخل البريد الالكتروني");
                    return false;
                }
                if (Fname == "") {

                    $("#lblmsg").html("ادخل الاسم الاول ");
                    return false;
                }
                if (Lname == "") {

                    $("#lblmsg").html("ادخل الاسم الثاني");
                    return false;
                }
//                if (!isValidEmailAddress(Email)) {
//                    $("#lblmsg").html("يرجى ادخال الايميل بطريقة صحيحة ");
//                 }

//                if (DdlDay == "") {

//                    $("#lblmsg").html("ادخل يوم الميلاد");
//                    return false;
//                }
//                if (DdlMonth.selectedvalue == "") {

//                    $("#lblmsg").html("ادخل شهر الميلاد");
//                    return false;
//                }
//                 if (DdlYear.selectedvalue == "") {

//                    $("#lblmsg").html("ادخل شهر الميلاد");
//                    return false;
//                }
//                 if (DdlNationality.selectedvalue == "") {

//                    $("#lblmsg").html("ادخل سنة الميلاد");
//                    return false;
//                }



                //                 (fireAjax("/Methods.aspx/SaveUserFirstName", "{'firstname':'" + fname + "','lastname':'" + lname + "'}"))
                //                 $("#").html("تم التعديل على البيانات  بنجاح ");
                //                 });
                //                 });

                $.ajax({
                    type: "POST",
                    url: "/shopBasic3.aspx/SaveUserInformation",
                    data: "{'username':'" + Uname + "','email':'" + Email + "','firstname':'" + Fname + "','lastname':'" + Lname + "','ddlDay':'" + DdlDay + "','ddlMonth':'" + DdlMonth + "','ddlYear':'" + DdlYear + "','ddlNationality':'" + DdlNationality + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d > 0) {
                            $("#lblmsg").html("تم تعديل البيانات بنجاح");
                        }
                        $("#span1").html("تم الحفظ");
                    }

                });
            });

        });
        function isValidEmailAddress(Email) {
            var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
            return pattern.test(emailAddress);
        };
</script>
    <div class="cms_page_title">
        <span>المعلومات الأساسية</span>
    </div>
    <div id="inside_left_content_cms">
        <div class="input_styles">
        <form id="form" runat="server">
            <ul class="innput">
                <li><span class="title_cms">اسم المستخدم</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtUser" runat="server" class="random_input"></asp:TextBox>
                    </div>
                   
                </li>
                <li><span class="title_cms">البريد الإلكتروني</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtEmail" runat="server" class="random_input"></asp:TextBox>
                    </div>
                   
                  <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                        ValidationGroup="VgUser" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        CssClass="validd" Display="Dynamic" ErrorMessage="يرجى كتابة البريد بصيغة :someone@something.com"></asp:RegularExpressionValidator>--%>
                </li>
                <li><span class="title_cms">الاسم الأول</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtFirstName" runat="server" class="random_input"></asp:TextBox>
                    </div>
                  
                </li>
                <li><span class="title_cms">الاسم الأخير</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtLastName" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    
                </li>
                <li><span class="title_cms">تاريخ الميلاد</span>
                    <div class="cms_input">
                        <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="false">
                            <asp:ListItem Value="0">يوم </asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="false">
                            <asp:ListItem Value="0">شهر </asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="false">
                        </asp:DropDownList>
                    </div>
                   <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlDay"
                        ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual" CssClass="validd"
                        ErrorMessage="يرجى اختيار يوم الميلاد"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlMonth"
                        ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual" CssClass="validd"
                        ErrorMessage="يرجى اختيار شهر الميلاد"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlYear"
                        ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual" CssClass="validd"
                        ErrorMessage="يرجى اختيار سنة الميلاد"></asp:CompareValidator>--%>
                </li>
                <li><span class="title_cms">الجنسية</span>
                    <div class="cms_input">
                        <asp:DropDownList ID="ddlNationality" runat="server">
                        </asp:DropDownList>
                    </div>
                </li>
                <li>
                     <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                        <p> <span id="lblmsg" class="validd"></span></p>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                            <span id="span1"></span>
                    <input id="btnSaveUser" type="button" value="حفظ"/>
                            </div>
                        </div>
                    </div><!-- Con End-->

                </li>
            </ul>
            </form>
        </div>
    </div>
    