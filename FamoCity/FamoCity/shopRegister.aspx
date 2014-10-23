<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="shopRegister.aspx.cs" Inherits="FamoCity.shopRegister" %>

<%@ Register src="userControls/Capatcha.ascx" tagname="Capatcha" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/shop-regiser.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div class="ccontent size">
            <div class="bigbox boxsize-small">
                <form id="form1" runat="server">
                <div class="title-line">
                    تسجيل حساب متجر</div>
                <div class="form-register-r">
                    <div class=" form-line-r">
                        <div class="form-title-r">
                            <asp:Label ID="lblEmail" runat="server" Text="البريد الالكتروني"></asp:Label></div>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-field-r" 
                            AutoCompleteType="Email"></asp:TextBox>
                        <%--  <input class="form-field-r" type="text" name="firstname" />--%>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                Display="Dynamic" ErrorMessage="يرجى ادخال البريد الالكتروني" ValidationGroup="VgRegistor"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                Display="Dynamic" ErrorMessage="اكتب الاميل بطريقةصحيحة" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="VgRegistor"></asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="csmEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                ErrorMessage="الايميل موجود سابقا" OnServerValidate="CustomValidator1_ServerValidate"
                                ValidationGroup="VgRegistor"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class=" form-line-r form-padding-right">
                        <div class="form-title-r">
                            <asp:Label ID="lblCompanyName" runat="server" Text="اسم المتجر"></asp:Label></div>
                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-field-r"></asp:TextBox>
                        <%-- <input class="form-field-r" type="text" name="email" />--%>
                        <div class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCompanyName"
                            Display="Dynamic" ErrorMessage="من فضلك اكتب اسم المتجر" ValidationGroup="VgRegistor"></asp:RequiredFieldValidator>
                            </div>
                    </div>
                </div>
                <div class=" form-line-r">
                    <div class="form-title-r">
                        <asp:Label ID="lblFirstName" runat="server" Text="الاسم الاول"></asp:Label></div>
                    <asp:TextBox ID="txtFirsName" runat="server" CssClass="form-field-r" 
                        AutoCompleteType="FirstName"></asp:TextBox>
                    <%--
                            <input class="form-field-r" type="text" name="firstname" />--%>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFirsName"
                            Display="Dynamic" ErrorMessage="يرجى كتابة اسمك الاول" ValidationGroup="VgRegistor"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class=" form-line-r form-padding-right">
                    <div class="form-title-r">
                        <asp:Label ID="lblLastName" runat="server" Text="الاسم الاخير "></asp:Label>
                    </div>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-field-r" 
                        AutoCompleteType="LastName"></asp:TextBox>
                    <%--  <input class="form-field-r" type="text" name="email" />--%>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName"
                            Display="Dynamic" ErrorMessage="اكتب الاسم الاخير" ValidationGroup="VgRegistor"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class=" form-line-r">
                    <div class="form-title-r">
                        <asp:Label ID="lblPassword" runat="server" Text="كلمة المرور "></asp:Label></div>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-field-r"></asp:TextBox>
                    <%--  <input class="form-field-r" type="text" name="firstname" />--%>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" ErrorMessage="يرجى ادخال كلمة المرور " ValidationGroup="VgRegistor"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class=" form-line-r form-padding-right">
                    <div class="form-title-r">
                        <asp:Label ID="lblRePassword" runat="server" Text="اعادة كلمة المرور"></asp:Label></div>
                    <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password" CssClass="form-field-r"></asp:TextBox>
                    <%-- <input class="form-field-r" type="text" name="email" />--%>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRePassword"
                            Display="Dynamic" ErrorMessage="يرجى اعادة كلمة المرور" ValidationGroup="VgRegistor"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtRePassword" ErrorMessage="اعد كلمة المرور بطريقة صحيحة"
                            ValidationGroup="VgRegistor"></asp:CompareValidator>
                    </div>
                </div>
                <div class=" form-line-r">
                    <div class="form-title-r">
                        <asp:Label ID="lblNumberPhone" runat="server" Text="رقم الجوال"></asp:Label></div>
                    <asp:TextBox ID="txtNumberPhone" runat="server" ValidationGroup="VgRegistor" CssClass="form-field-r"></asp:TextBox>
                    <%-- <input class="form-field-r" type="text" name="email" />--%>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNumberPhone"
                            Display="Dynamic" ErrorMessage="رقم الجوال" ValidationGroup="VgRegistor"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumberPhone"
                            Display="Dynamic" ErrorMessage="يرجى ادخال رقم التلفون بطريقة صحيحة" ValidationExpression="^\d+$"
                            ValidationGroup="VgRegistor"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class=" form-line-r form-padding-right">
                    <div class="form-title-r">
                        <asp:Label ID="lblNumberFax" runat="server" Text="رقم الفاكس"></asp:Label></div>
                    <asp:TextBox ID="txtNumberFax" runat="server" CssClass="form-field-r"></asp:TextBox>
                    <div class="validation">
                    </div>
                </div>
                <div class=" form-line-r">
                    <div class="form-title-r">
                        <asp:Label ID="Label3" runat="server" Text="اسم الصفحه"></asp:Label></div>
                    <asp:TextBox ID="txtWebName" runat="server" ValidationGroup="VgRegistor" 
                        CssClass="form-field-r" MaxLength="30"></asp:TextBox>
                    <%-- <select class="selectdate-n">
                                <option>مصر</option>
                                <option>Short Option</option>
                                <option>This Is A Longer Option</option>
                            </select>--%>
                    <div class="validation">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtWebName"
                        ValidationGroup="VgCompany" ValidationExpression="^[a-zA-Z0-9_-]{3,20}$" ErrorMessage="يمكن أن يتكون الإسم من أرقام و حروف انجليزية و ’-’ و ’_’ فقط"
                        CssClass="validd" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtWebName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" 
                            ErrorMessage="اسم الصفحة مكرر .. ادخل اسم اخر " ControlToValidate="txtWebName" 
                            onservervalidate="CustomValidator2_ServerValidate"  ValidationGroup="VgRegistor" ></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" 
                            ControlToValidate="txtWebName" ErrorMessage="يرجى التأكد من اسم المتجر"  
                            ValidationGroup="VgRegistor" onservervalidate="CustomValidator3_ServerValidate"></asp:CustomValidator>
                    </div>
                </div>
                <div class=" form-line-r form-padding-right">
                    <div class="form-title-r">
                        </div>
                    
                    <div class="validation">
                    </div>
                </div>
                <div class=" form-line-r">
                    <div class="form-title-r">
                        <asp:Label ID="lblDate" runat="server" Text="تاريخ الميلاد"></asp:Label></div>
                    <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="false" CssClass="selectdate-m">
                        <asp:ListItem Value="0">يوم </asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
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
                    <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="false" CssClass="selectdate-m">
                        <asp:ListItem Value="0">شهر </asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="false" CssClass="selectdate-y">
                    </asp:DropDownList>
                    <div class="validation">
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlDay"
                            ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual">
                            <div class="error-left">
                            </div>
                            <div class="error-inner">
                                <asp:Label ID="lblCDay" runat="server" Text="يرجى اختيار يوم الميلاد"></asp:Label>
                            </div>
                        </asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlMonth"
                            ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual">
                            <div class="error-left">
                            </div>
                            <div class="error-inner">
                                <asp:Label ID="lblCMonth" runat="server" Text="يرجى اختيار شهر الميلاد"></asp:Label>
                            </div>
                        </asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlYear"
                            ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual">
                            <div class="error-left">
                            </div>
                            <div class="error-inner">
                                <asp:Label ID="Label1" runat="server" Text="يرجى اختيار سنة الميلاد"></asp:Label>
                            </div>
                        </asp:CompareValidator>
                    </div>
                </div>
                <div class=" form-line-r form-padding-right">
                    <div class="form-title-r">
                        <asp:Label ID="lblCountry" runat="server" Text="الدولة"></asp:Label></div>
                    <asp:DropDownList ID="ddlCountry" runat="server" class="selectdate-n">
                    </asp:DropDownList>
                    <%--   <input class="form-radio-r" type="radio" name="email" /><span>ذكر</span>--%>
                    <div class="validation">
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlDay"
                            ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual">
                            <div class="error-left">
                            </div>
                            <div class="error-inner">
                                <asp:Label ID="Label2" runat="server" Text="يرجى اختيار اسم الدولة"></asp:Label>
                            </div>
                        </asp:CompareValidator>
                    </div>
                </div>
                <div class=" form-line-r">
                    <div class="form-title-r">
                        الجنس</div>
                    <asp:RadioButton ID="radMale" runat="server" Text="ذكر" GroupName="gender" 
                        CssClass="form-radio-r" Checked="True" />
                    <%-- <input class="form-radio-r" type="radio" name="email" /><span>انثى</span>--%>
                    <asp:RadioButton ID="radFemale" runat="server" Text="انثى" GroupName="gender" 
                        class="form-radio-r" EnableTheming="True" />
                    <%--   <input class="form-field-r" type="text" name="firstname" />--%>
                </div>
                <div class=" form-line-r form-padding-right">
                    <div class="form-title-r">
                        <asp:Label ID="lblCompanyName0" runat="server" Text="العنوان"></asp:Label></div>
                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" class="form-field-r"></asp:TextBox>
                    <%--   <a href="#" class="css_btn_class">تسجيل</a>--%>
                </div>
                <div class=" form-line-r-big">
                    <div class="form-title-r">
                        النشاط التجاري</div>
                    <asp:CheckBoxList ID="ckblActivity" runat="server" CssClass="form-title-r" RepeatColumns="2">
                    </asp:CheckBoxList>
                <div class="validation">
                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="يرجى تحديد نشاط الشركة"
                        OnServerValidate="CustomValidator1_ServerValidate1" ValidationGroup="VgRegistor"></asp:CustomValidator>
                </div>
                </div>
                <div class=" form-label-cancel">
                    <asp:HiddenField ID="hdnAgree" runat="server" Value="0" />
                    <asp:CheckBox ID="chkGreeting" runat="server" Text="اوافق على اتفاقية الموقع" />
                </div>
                <div class="captcha">
                    <uc1:Capatcha ID="Capatcha1" runat="server" />
                </div>
                <div class=" form-label-cancel">
                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                </div>
                <div class=" form-label-r">
                    <asp:Button ID="btnRegistar" runat="server" OnClick="btnRegistar_Click" Text="تسجيل"
                        ValidationGroup="VgRegistor" class="css_btn_class" />
                    <%--   <a href="#" class="css_btn_class">تسجيل</a>--%>
                </div>
                </form>
            </div>
            
        </div>
    </div>
</asp:Content>
