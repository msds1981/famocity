<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admLang.aspx.cs" Inherits="FamoCity.admLang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        اللغات</h1>
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#573550"></asp:Label>
    <asp:TextBox ID="txtLangauge" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLangauge"
        Display="Dynamic">
        <asp:Label ID="Label3" runat="server" Text="الرجاء كتابة اللغة"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNameActivity" runat="server" ControlToValidate="txtCode"
        Display="Dynamic">
        <asp:Label ID="Label2" runat="server" Text="الرجاء كتابة الرمز "></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
        OnClientClick="return confirm(&quot;هل انت متاكد الحذف&quot;)" Text="حذف" /><br />
    <br />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="حفظ" /><br />
    <br />
    <asp:Button ID="addNewLang" runat="server" OnClick="addNewLang_Click" Text="اضافة جديدة "
        CausesValidation="False" /><br />
    <br />
    <asp:GridView ID="GvLang" runat="server" AutoGenerateColumns="False" EmptyDataText="قاعدة البيانات فارغة ">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="lang_id" HeaderText="الرقم">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="اللغة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="code" HeaderText="الرمز ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/Lang/{0}", Eval("lang_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <FooterStyle ForeColor="White" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
