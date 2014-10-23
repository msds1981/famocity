<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admObjectShow.aspx.cs" Inherits="FamoCity.admObjectShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Objects</h1>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="اضافة " />
    <asp:GridView ID="gvObject" runat="server" AutoGenerateColumns="False" EmptyDataText="قاعدة البيانات فارغة من البيانات ">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="code" HeaderText="الكود ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" runat="server" NavigateUrl='<%# string.Format("admin/Object/{0}", Eval("ojct_id")) %>'
                        title="تعديل"></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
