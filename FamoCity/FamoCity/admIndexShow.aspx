<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admIndexShow.aspx.cs" Inherits="FamoCity.admIndexShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Show Endex</h1>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="اضافة جديدة" />
    <asp:GridView ID="gvIndex" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="enm_name" HeaderText="Enm Name">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="enm_value" HeaderText="Enm Value">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" runat="server" NavigateUrl='<%# string.Format("/admin/Index/{0}", Eval("index_id")) %>'
                        title="تعديل"></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
