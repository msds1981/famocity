<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginCms.aspx.cs" Inherits="FamoCity.LoginCms" %>

    <div class="cms_page_title">
        <span>تسجيل الدخول</span>
    </div>
    <!-- -->
    <div id="inside_left_content_cms">
        <div class="input_styles">
            <form id="form" runat="server">
            <ul class="innput">
                <li><span class="title_cms">
                    <asp:Label ID="lbl1" runat="server" Text="البريد الإلكتروني"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtEmail" CssClass="random_input" runat="server"></asp:TextBox>
                    </div>
                    
                </li>
                <li><span class="title_cms">
                    <asp:Label ID="lbl2" runat="server" Text="كلمة المرور"></asp:Label>
                </span>
                    <div class="cms_input">
                        <asp:TextBox ID="txtPassword" CssClass="random_input" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label ID="Lblmsg" runat="server" Class="validd"></asp:Label></div>
                </li>
                <li>
                    <input id="Button1" type="button" value="button" />
                </li>
            </ul>
            </form>
        </div>
    </div>

