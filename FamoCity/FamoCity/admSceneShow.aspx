<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admSceneShow.aspx.cs" Inherits="FamoCity.admSceneShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Previo Scene</h1>
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="اضافة " />
    <asp:GridView ID="gvScene" runat="server" AutoGenerateColumns="False" EmptyDataText="قاعدة البيانات فارغة ">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="code" HeaderText="الكود ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="version" HeaderText="version">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="obj_code" HeaderText="obj_code">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="obj_id" HeaderText="obj_id">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/Scene/{0}", Eval("scn_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
