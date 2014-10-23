<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" Inherits="FamoCity.Default2" %>

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
            top:34px;
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
            <!-- <<<----- -->
            <!-- login box -->
            <div class="momo" id="login">
                <div class="form_box">
                    <div class="form_top">
                        <div class="form_top_all">
                            <asp:Image ID="Image1" runat="server" CssClass="img_login" />
                            <asp:TextBox ID="txtlogemail" ClientIDMode="Static" runat="server" CssClass="email_login email_icon"
                                value="" AutoCompleteType="Email"></asp:TextBox>
                      
                            <asp:TextBox ID="txtlogpassword" ClientIDMode="Static" runat="server" class="email_login password_icon"
                                TextMode="Password" value=""></asp:TextBox>
                        
                            <div class="form_v">
                                <asp:RequiredFieldValidator ID="rfvemail" runat="server" ErrorMessage="يجب كتابة البريد الالكتروني"
                                    ControlToValidate="txtlogemail" Display="Dynamic" ValidationGroup="grpLogin"></asp:RequiredFieldValidator>
                              
                                <asp:Label ID="lblmsg" runat="server" Style="display: none;" Text=""></asp:Label>
                                <asp:RegularExpressionValidator ID="regexpemail" runat="server" ErrorMessage="ادخل الايميل بطريقة صحيح"
                                    ControlToValidate="txtlogemail" ValidationGroup="grpLogin" Display="Dynamic"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <%--   <span></span>--%>
                                <asp:RequiredFieldValidator ID="rfvpassword" runat="server" ErrorMessage="" ControlToValidate="txtlogpassword"
                                    Display="Dynamic" ValidationGroup="grpLogin"></asp:RequiredFieldValidator>
                                <%--  <span></span>--%>
                            </div>
                            <div class="link_box">
                                <span>
                                    <asp:Button ID="btnlogin" runat="server" Text="دخول" CssClass="link_box_btn" ValidationGroup="grpLogin"
                                        OnClick="btnlogin_Click" /></span> <span><span>
                                            <asp:CheckBox ID="chklogin" runat="server" />
                                            البقاء متصلا </span>
                                            <span><a href="#" class="flip" addcls="flipped4">نسيت كلمة المرور؟</a></span>
                                            <span><a href="#" class="flip" addcls="flipped" style="left: 0px; opacity: 1;">إنشاء
                                                حساب جديد</a></span> </span>
                            </div>
                        </div>
                    </div>
                    <div class="form_shadow">
                        <img src="newfiles/images/gbs.png" /></div>
                </div>
                <!-- end form_box -->
            </div>
            <!-- end momo -->
            <!-- register box -->
            <div class="momo" id="recover">
                <div class="form_box">
                    <div class="form_top">
                        <div class="form_top_all">
                            <a href="#" class="regester_user_btn flip" addcls="flipped2" style="left: 0px; opacity: 1;">
                                إنشاء حساب مستخدم</a> <a href="#" class="regester_shop_btn flip" addcls="flipped3">إنشـــاء
                                    حســـاب متجـــر</a> <a href="#" class="back_login_btn flip" addcls="flipped" style="left: 0px;
                                        opacity: 1;">تسجيـــل الدخــول</a>
                        </div>
                    </div>
                    <div class="form_shadow">
                        <img src="newfiles/images/gbs.png" /></div>
                </div>
                <!-- end form_box -->
            </div>
            <!-- end momo -->
            <!-- register1 box -->
            <div class="momo" id="regist">
                <div class="form_box">
                    <div class="form_top">
                        <div class="form_top_all">
                            <img id="loaderuser" class="loader" src="/images/loadinfo.net.gif" style="display:none;"/>
                            <asp:TextBox ID="txtRegUser" ClientIDMode="Static" runat="server" CssClass="text_mox_regist m_t_user"
                                value=""></asp:TextBox>
                            
                            <div class="form_v">
                               <span id="MsgEmailRegistrar"></span>
                        
                            </div>
                            <asp:TextBox ID="txtPassUser" ClientIDMode="Static" runat="server" CssClass="text_mox_regist"
                                value="" TextMode="Password"></asp:TextBox>
                        
                            <div class="form_v">
                            
                                  <span id="MsgPassUser"></span>
                            </div>
                            <asp:TextBox ID="txtRePassUser" ClientIDMode="Static" runat="server" CssClass="text_mox_regist"
                                value="" TextMode="Password"></asp:TextBox>
                         
                            <div class="form_v">
                               
                                <span id="MsgRePassUser"></span>
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
                                    <asp:Button ID="btnsaveUser" runat="server" Text="حفظ" ValidationGroup="vgRegistarUser"
                                        CssClass="link_box_btn"  OnClientClick="return checkUserRegister()" OnClick="btnsaveUser_Click"  /><%--<a href="#" class="link_box_btn">تسجيل</a>--%></span>
                                <span><span>
                                    <asp:CheckBox ID="chkGreeting" runat="server" ValidationGroup="vgRegistarUser" Checked="false" />اوافق
                                    على الشروط</span>
                                 
                                    <span><a href="#" class="flip" addcls="flipped">الخيارات السابقة</a></span>
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
            <!-- shop box -->
            <div class="momo" id="reshop">
                <div class="form_box">
                    <div class="form_top">
                        <div class="form_top_all">
                            <asp:TextBox ID="txtEmailShop" runat="server" CssClass="text_mox_regist m_t_shop"
                                value="" ClientIDMode="Static"></asp:TextBox>
                         
                            <div class="form_v">
                             
                                <span id="MsEmail"></span>
                           
                            </div>
                            <asp:TextBox ID="TxtPassShop" runat="server" CssClass="text_mox_regist" value=""
                                ClientIDMode="Static" TextMode="Password"></asp:TextBox>
                        
                            <div class="form_v">
                                
                                <span id="MsgPass"></span>
                            </div>
                            <asp:TextBox ID="TxtRePassShop" runat="server" class="text_mox_regist" value="" TextMode="Password"
                                onChange="checkPasswordMatch();" ClientIDMode="Static"></asp:TextBox>
                            <%--<input name="" type="text" id="" class="text_mox_regist" value="تأكيد كلمة المرور"
                                onfocus="if (this.value == 'تأكيد كلمة المرور') {this.value = '';}" onblur="if (this.value == '') {this.value = 'تأكيد كلمة المرور';}">--%>
                            <div class="form_v">
                                <span id="MsRepass"></span>
                            </div>
                            <asp:TextBox ID="TxtNameShop" runat="server" class="text_mox_regist" ValidationGroup="VGshopName"
                                ClientIDMode="Static" value=""></asp:TextBox>
                            <%-- <input name="" type="text" id="" class="text_mox_regist" value="أسم المتجر" onfocus="if (this.value == 'أسم المتجر') {this.value = '';}"
                                onblur="if (this.value == '') {this.value = 'أسم المتجر';}">--%>
                            <div class="form_v">
                              
                                <span id="MsShopName"></span>
                                <%--<asp:Label ID="lblmsgshop" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>--%>
                                <span id="MsShopAgree"></span>
                            </div>
                            <div class="link_box">
                                <span>
                                    <asp:Button ID="btnEnterShop" runat="server" Text="تسجيل" CssClass="link_box_btn"
                                        OnClientClick="return checkShopRegister()" OnClick="btnEnterShop_Click" ValidationGroup="VGshopName" /></span>
                                <%--    <span><a href="#" class="link_box_btn">دخـــول</a></span>--%>
                                <span><span>
                                    <asp:CheckBox ID="ChkBoxShop" runat="server" Text="اوافق على الشروط"  />
                                </span><span><a href="#" class="flip" addcls="flipped">الخيارات السابقة</a></span>
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
            <!-- forget password box -->
            <div class="momo" id="fpass">
                <div class="form_box">
                    <div class="form_top">
                        <div class="form_top_all">
                            <p class="login_box_title m_t_forget">
                                طلــب تغييـــر كلمــة المــرور</p>
                            <p class="login_box_text">
                                إذا كنت ترغب في تغيير كلمة المرور ،قم بأدخال البريد الالكتروني الخاص بحسابك وتابع
                                الخطوات</p>
                            <p class="login_box_text">
                                <a href="#" class="flip" addcls="">العودة الي تسجيل الدخول او إنشاء حساب جديد من <span>
                                    هنا</span></a></p>
                            <img id="Imgfpass" class="loader loadpass" src="/images/loadinfo.net.gif" style="display:none;"/>
                            <asp:TextBox ID="txtReEmail" ClientIDMode="Static" runat="server" CssClass="text_mox_regist"
                                value=""></asp:TextBox>
                       
                            <div class="form_v">
                                <span id="msgResntEmail"></span>
                              
                            </div>
                            <div class="link_box">
                                <span>
                                    <input id="btnReEmail" type="button" value="ارسل"  onclick="checkResentEmail();" class="link_box_btn" addcls="flipped5" />
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
            <!-- forget password box -->
            <div class="momo" id="success">
                <div class="form_box">
                    <div class="form_top">
                        <div class="form_top_all">
                            <img class="m_t_trou" src="newfiles/images/truo.png" />
                            <p class="login_box_title">
                                تــم ارســال الطلـــب بنجـــاح</p>
                            <p class="login_box_text">
                                <a href="#" class="flip" addcls="flipped5">م ارسال الطلب بنجاح ، الرجاء مراجع بريدك الالكتروني
                                    من <span>هنا</span></a></p>
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
