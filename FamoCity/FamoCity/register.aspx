<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="FamoCity.register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FamoCity Social Network</title>
    
    <link href="newfiles/css/fleep.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="newfiles/js/jquery-1.10.2.min.js"></script>
    <script src="/newfiles/js/fleep.js" type="text/javascript"></script>
    <script src="/js/jquery.watermark.js" type="text/javascript"></script>
    <script src="/newfiles/js/shop-register.js" type="text/javascript"></script>
    <script src="/newfiles/js/user-register.js" type="text/javascript"></script>
    <script src="/newfiles/js/forgetpass.js" type="text/javascript"></script>
    <script type="text/javascript">


        $(document).ready(function () {
            $.watermark.options = {
                className: 'watermark',
                useNative: false,
                hideBeforeUnload: false
            };

            $('#txtFirstName').watermark('الاسم الأول');
            $('#txtLastName').watermark('الاسم الأخير');
            $('#txtlogemail').watermark('البريد الإلكتروني');
            $('#txtlogpassword').watermark('كلمة المرور');
            $('#txtReEmail').watermark('البريد الإلكتروني');
            $('#txtRegUser').watermark('البريد الإلكتروني');
            $('#txtPassUser').watermark('كلمة المرور');
            $('#txtRePassUser').watermark('اعد كلمة المرور');
            $('#txtEmailShop').watermark('البريد الالكتروني');
            $('#txtEmailShop').watermark('البريد الالكتروني');
            $('#TxtPassShop').watermark('كلمةالمرور');
            $('#TxtRePassShop').watermark('اعادة كلمة المرور');
            $('#TxtNameShop').watermark('اسم المتجر');
            
        });
    </script>
    <style type="text/css">
        .watermark
        {
            color: #fff;
        }
        
        .loader
        {
            position:absolute;
            top:163px;
            left:38px;
            }
        .loadpass
        {
            top:233px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bg">
    </div>
    <div class="bg2">
    </div>
    <div class="bg3">
    </div>
    <div class="form_wrapper">
        <img src="newfiles/images/famo_logo.png" />
        <div class="loginWrapper">
            <!-- register1 box -->
            <div class="momo" >
                <div class="form_box">
                    <div class="form_top">
                        <div class="form_top_all">
                            <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" CssClass="text_mox_regist"
                                value="" style="margin-top:28px;"></asp:TextBox>
                            <div class="form_v">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="يجب كتابة الإسم" ControlToValidate="txtFirstName" ValidationGroup="regGrp"></asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox ID="txtLastName" ClientIDMode="Static" runat="server" CssClass="text_mox_regist"
                                value=""></asp:TextBox>
                            <div class="form_v" style="height:0px;"></div>
                            <img id="loaderuser" class="loader" src="/images/loadinfo.net.gif" style="display:none;"/>
                            <asp:TextBox ID="txtRegUser" ClientIDMode="Static" runat="server" CssClass="text_mox_regist m_t_user"
                                value=""></asp:TextBox>
                            
                            <div class="form_v">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="يجب كتابة الإيميل" ControlToValidate="txtRegUser" ValidationGroup="regGrp"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRegUser" ValidationGroup="regGrp" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" >
                                </asp:RegularExpressionValidator>
                            </div>
                            <asp:TextBox ID="txtPassUser" ClientIDMode="Static" runat="server" CssClass="text_mox_regist"
                                value="" TextMode="Password"></asp:TextBox>
                        
                            <div class="form_v">
                            
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="يجب كتابة كلمة المرور" ControlToValidate="txtPassUser" ValidationGroup="regGrp"></asp:RequiredFieldValidator>
                            </div>
                            <div class="link_box">
                                <p class="text_mox_title">
                                    تاريخ الميلاد</p>
                                <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="false" CssClass="selectdate-y"
                                    ValidationGroup="VgRegistor">
                                </asp:DropDownList>
                               
                                <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="false" CssClass="selectdate-m s-mp"
                                    ValidationGroup="VgRegistor">
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
                               
                                <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="false" CssClass="selectdate-m s-mp"
                                    ValidationGroup="VgRegistor">
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
                               
                            </div>
                            <div class="form_v">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="يجب اختيار تاريخ الميلاد" ControlToValidate="ddlYear" Operator="NotEqual" ValidationGroup="regGrp" ValueToCompare="0"></asp:CompareValidator>
                            </div>
                            <div class="link_box">
                                <p class="text_mox_title">
                                    الجنس</p>
                                <div class="redio_box_r ">
                                    <asp:RadioButton ID="rdbMan" runat="server" Text="ذكر" class="text_mox_rr" Checked="True"
                                        GroupName="gender" />
                              
                                    <asp:RadioButton ID="rdbFemal" runat="server" Text="انثى" CssClass="text_mox_rr"
                                        GroupName="gender" />
                                 
                                </div>
                            </div>
                            <div class="link_box">
                                <span>
                                    <asp:Button ID="btnsaveUser" runat="server" Text="حفظ"  ValidationGroup="regGrp"
                                        CssClass="link_box_btn" OnClick="btnsaveUser_Click"  /><%--<a href="#" class="link_box_btn">تسجيل</a>--%></span>
                                <span style="margin-top:8px;">
                                <span>
                                    <asp:CheckBox ID="chkGreeting" runat="server" ValidationGroup="vgRegistarUser" Checked="false" />اوافق
                                    على الشروط</span>
                                 
                                    <%--<span><a href="#" class="flip" addcls="flipped">الخيارات السابقة</a></span>--%>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="form_shadow">
                        <img src="newfiles/images/gbs.png" /></div>
                </div>
                <!-- end form_box -->
            </div>
            <!-- end momo -->
        </div>
        <!-- end loginwrapper -->
    </div>
    <!-- end form_wrapper -->
    <asp:HiddenField ID="hdnAgree" runat="server" Value="0" />
    </form>
</body>
</html>
