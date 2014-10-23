<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="usrRegStep2.aspx.cs" Inherits="FamoCity.usrRegStep2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/step2.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ccontent">
            <div class="meter">
                <ol>
                    <li class="active"><span class="step">1</span> <span class="stage">انشاء حساب</span>
                    </li>
                    <li class="active"><span class="step">2</span> <span class="stage">معلومات شخصية</span>
                    </li>
                    <li><span class="step">3</span> <span class="stage">صورة شخصية</span> </li>
                    <li><span class="step">4</span> <span class="stage">الاهتمامات</span> </li>
                    <li><span class="step">5</span> <span class="stage">دعوة الاصدقاء</span> </li>
                </ol>
            </div>
            <div class="bigbox boxsize-small">
                <form id="form1" runat="server">
                <div class="title-line">
                    معلومات شخصية</div>
                <div class="form-register-r">
                    <div class=" form-line-r">
                        <div class="form-title-r">
                            <asp:Label ID="lblFirstName" runat="server" Text="الاسم الاول"></asp:Label></div>
                        <asp:TextBox ID="txtFirsName" runat="server" CssClass="form-field-r"></asp:TextBox>
                        <%--  <input class="form-field-r" type="text" name="email" />--%>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirsName"
                                Display="Dynamic" ErrorMessage="يرجى كتابة اسمك الاول" ></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class=" form-line-r form-padding-right">
                        <div class="form-title-r">
                            <asp:Label ID="lblLastName" runat="server" Text="الاسم الاخير "></asp:Label></div>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-field-r"></asp:TextBox>
                        <%--    <input class="form-field-r" type="text" name="firstname" />--%>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName"
                                Display="Dynamic" ErrorMessage="يرجى كتابة الاسم الاخير" ></asp:RequiredFieldValidator></div>
                    </div>
                    <div class=" form-line-r">
                        <div class="form-title-r">
                            <asp:Label ID="lblCountry" runat="server" Text="الدولة"></asp:Label></div>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="selectdate-n">
                        </asp:DropDownList>
                        <%--
                        <select class="selectdate-n">
                            <option>مصر</option>
                            <option>Short Option</option>
                            <option>This Is A Longer Option</option>
                        </select>--%>
                        <div class="validation">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="الرجاء تحديد اسم الدولة"
                                Operator="GreaterThan" ValueToCompare="0" ControlToValidate="ddlCountry"></asp:CompareValidator></div>
                    </div>
                    <div class=" form-line-r form-padding-right">
                        
                    </div>
                    <div class=" form-line-r">
                        <div class="form-title-r">
                            <asp:Label ID="lblNumberPhone" runat="server" Text="رقم الجوال"></asp:Label></div>
                        <asp:TextBox ID="txtNumberPhone" runat="server" ValidationGroup="VgRegistor" MaxLength="14" CssClass="form-field-r"
                            ></asp:TextBox>
                        <%--   <input class="form-field-r" type="text" name="email" />--%>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNumberPhone"
                                Display="Dynamic" ErrorMessage="رقم الجوال" ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumberPhone"
                                Display="Dynamic" ErrorMessage="يرجى ادخال رقم التلفون بطريقة صحيحة" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ErrorMessage="invalid mobile number" 
                                onservervalidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class=" form-line-r form-padding-right">
                    <div class=" form-label-x">
                            مثال 966546670778</div>
                    </div>
                    <div class=" form-label-cancel">
                    </div>
                    <div class=" form-label-r">
                    </div>
                    <div class=" form-label-r">
                        <asp:LinkButton ID="LinkNext" runat="server" CssClass="css_btn_class" OnClick="LinkNext_Click">التالي </asp:LinkButton>
                    </div>
                </div>
                </form>
            </div>
        </div>
</asp:Content>
