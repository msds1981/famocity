<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admUsers.aspx.cs" Inherits="FamoCity.admUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        عرض المستخدمين</h1>
    <asp:Button ID="btnAdd" runat="server" Text="اضافة مستخدم " OnClick="btnAdd_Click" />
    <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        OnPageIndexChanging="gvUser_PageIndexChanging">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="user_id" HeaderText="رقم المستخدم ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="username" HeaderText="اسم المستخدم">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="email" HeaderText="الايميل">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/admUser/{0}", Eval("user_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="LblMsg" runat="server"></asp:Label>
</asp:Content>
