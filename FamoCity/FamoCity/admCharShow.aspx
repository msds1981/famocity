<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admCharShow.aspx.cs" Inherits="FamoCity.admCharShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Characters</h1>
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="اضافة جديدة" />
    <asp:GridView ID="gvObject" runat="server" AutoGenerateColumns="False" EmptyDataText="قاعدة البيانات فارغة من البيانات ">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="chr_id" HeaderText="الرقم ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="الاسم ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/Char/{0}", Eval("chr_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
