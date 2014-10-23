<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admCharGroupShow.aspx.cs" Inherits="FamoCity.admCharGroupShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Show Endex</h1>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="اضافة جديدة " />
    <asp:GridView ID="gvCharGroup" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="cgrp_id" HeaderText="الرقم">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="اسم المجموعة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/Chargrp/{0}", Eval("cgrp_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
