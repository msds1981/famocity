<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admChrClothShow.aspx.cs" Inherits="FamoCity.admChrClothShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Char Clothes</h1>
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="اضافة جديدة" />
    <asp:GridView ID="gvObject" runat="server" AutoGenerateColumns="False" EmptyDataText="قاعدة البيانات فارغة من البيانات ">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="clth_id" HeaderText="الرقم ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="الاسم ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/Charcloth/{0}", Eval("clth_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
