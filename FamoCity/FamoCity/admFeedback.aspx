<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admFeedback.aspx.cs" Inherits="FamoCity.admFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        الملاحظات
    </h1>
    <asp:GridView ID="gvFeedBack" runat="server" AutoGenerateColumns="False" OnRowCommand="gvFeedBack_RowCommand">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="subject" HeaderText="الموضوع ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="username" HeaderText="اسم المستخدم">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="FullName" HeaderText="الاسم الكامل">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="note" HeaderText="الملاحظات">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تنزيل الملفات ">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkDown" runat="server" CommandArgument='<%# Eval("file_path") %>'
                        CommandName="cmddonwn" ToolTip="تنزيل">تنزيل </asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle Width="20%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="حذف ">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkDelete" runat="server" CommandArgument='<%# Eval("feed_id") %>'
                        CommandName="cmddelete" ToolTip="حذف" OnClientClick="return confirm(&quot;هل انت متاكد من المحذف&quot;)">حذف </asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle Width="20%" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
