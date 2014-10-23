<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.master" AutoEventWireup="true"
    CodeBehind="shopBasic2.aspx.cs" Inherits="FamoCity.shopBasic2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUser"
                        runat="server" ValidationGroup="VgUser" CssClass="validd" ErrorMessage="يرجى كتابة اسم المستخدم">
                    </asp:RequiredFieldValidator>
                </li>
                <li><span class="title_cms">البريد الإلكتروني</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtEmail" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEmail"
                        runat="server" ValidationGroup="VgUser" CssClass="validd" ErrorMessage="يرجى كتابة البريد الإلكتروني"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                        ValidationGroup="VgUser" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        CssClass="validd" Display="Dynamic" ErrorMessage="يرجى كتابة البريد بصيغة :someone@something.com"></asp:RegularExpressionValidator>
                </li>
                <li><span class="title_cms">الاسم الأول</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtFirstName" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFirstName"
                        runat="server" ValidationGroup="VgUser" CssClass="validd" ErrorMessage="يرجى كتابة الاسم الأول">
                    </asp:RequiredFieldValidator>
                </li>
                <li><span class="title_cms">الاسم الأخير</span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtLastName" runat="server" class="random_input"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtLastName"
                        runat="server" ValidationGroup="VgUser" CssClass="validd" ErrorMessage="يرجى كتابة الاسم الأخير">
                    </asp:RequiredFieldValidator>
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
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlDay"
                        ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual" CssClass="validd"
                        ErrorMessage="يرجى اختيار يوم الميلاد"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlMonth"
                        ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual" CssClass="validd"
                        ErrorMessage="يرجى اختيار شهر الميلاد"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlYear"
                        ValidationGroup="VgUser" ValueToCompare="0" Operator="NotEqual" CssClass="validd"
                        ErrorMessage="يرجى اختيار سنة الميلاد"></asp:CompareValidator>
                </li>
                <li><span class="title_cms">الجنسية</span>
                    <div class="cms_input">
                        <asp:DropDownList ID="ddlNationality" runat="server">
                        </asp:DropDownList>
                    </div>
                </li>
                <li>
                    <asp:Button ID="Button1" runat="server" Text="حفظ" OnClick="Button1_Click" ValidationGroup="VgUser" />
                </li>
            </ul>
            </form>
        </div>
    </div>
</asp:Content>
