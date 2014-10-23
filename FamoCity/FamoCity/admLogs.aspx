<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admLogs.aspx.cs" Inherits="FamoCity.admLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        سجلات الدخول</h1>
    <asp:Label ID="lblSearch" runat="server" Text="بحث عن طريق الاسم "></asp:Label>
    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" OnClick="btnSearch_Click" Text="بحث " />
    <asp:GridView ID="gvLog" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        EmptyDataText="لاتوجد بيانات في قاعدة البيانات " OnPageIndexChanging="gvLog_PageIndexChanging">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="grp_name" HeaderText="اسم المجموعة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="FullName" HeaderText="اسم المستخدم">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="platform" HeaderText="platform">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="ip" HeaderText="العنوان ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="log_time" HeaderText="الزمن ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="status_text" HeaderText="الحالة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
</asp:Content>
