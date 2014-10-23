<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admTriggerShow.aspx.cs" Inherits="FamoCity.admTriggerShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Trigger</h1>
    <asp:GridView ID="gvTrigger" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="code" HeaderText="الكود ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/Trigger/{0}", Eval("trg_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle ForeColor="White" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
