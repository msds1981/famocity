<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admNews.aspx.cs" Inherits="FamoCity.admNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        الاخبار</h1>
    <asp:Label ID="Label1" runat="server" Text="اللغة"></asp:Label>
    <asp:DropDownList ID="ddlLang" runat="server" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged"
        AutoPostBack="True">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator3" ControlToValidate="ddlLang" Display="Dynamic"
        runat="server" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="lblcomVlang" runat="server" Text="الرجاء اختيار اللغة"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblDesc" runat="server" Text="وصف الخبر"></asp:Label>
    <asp:TextBox ID="txtDescription" runat="server" Height="125px" TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
        Display="Dynamic">
        <asp:Label ID="Label3" runat="server" Text="يرجى كتابةوصف للخبر"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="lblUserType" runat="server" Text="نوع المستخدم"></asp:Label>
    <asp:DropDownList ID="ddlUserType" runat="server">
        <asp:ListItem Value="0">-اختر من القائمة-</asp:ListItem>
        <asp:ListItem Value="1">مستخدم </asp:ListItem>
        <asp:ListItem Value="2">شركة</asp:ListItem>
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlUserType"
        Display="Dynamic" ErrorMessage="يرجى اختيار نوع المستخدم " Operator="NotEqual"
        ValueToCompare="0">
        <asp:Label ID="Label4" runat="server" Text="يرجى اختيار نوع المستخدم"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="حفظ" /><br />
    <br />
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;هل انت متاكد من الحذف؟&quot;)"
        Text="حذف " CausesValidation="False" /><br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="اختر اللغة  "></asp:Label>
    <asp:DropDownList ID="ddlGvlang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGvlang_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="btnNewNews" runat="server" OnClick="btnNewNews_Click" Text="اضافة جديدة " />
    <br />
    <br />
    <asp:GridView ID="gvNews" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="new_id" HeaderText="رقم الخبر ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="text" HeaderText="وصف الخبر ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle  />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/News/{0}", Eval("new_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle  />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
