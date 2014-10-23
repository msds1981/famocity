<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admGroups.aspx.cs" Inherits="FamoCity.admGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        مجموعة المستخدمين</h1>
    <asp:Label ID="Label5" runat="server"></asp:Label>
    <asp:Label ID="lblName" runat="server" Text="اسم المجموعة"></asp:Label>
    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvGroups" runat="server" ControlToValidate="txtName"
        Display="Dynamic" ValidationGroup="vgGroup">
        <asp:Label ID="Label3" runat="server" Text="الرجاء اختيار اسم المجموعة"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="lblactivity" runat="server" Text="الحالة "></asp:Label>
    <asp:DropDownList ID="ddlStatus" runat="server">
        <asp:ListItem Value="0">-اختر من القائمة -</asp:ListItem>
        <asp:ListItem Value="1">غير نشط</asp:ListItem>
        <asp:ListItem Value="2">نشط</asp:ListItem>
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlStatus"
        Display="Dynamic" ErrorMessage="الرجاء اختيار الحالة للمجموعة " Operator="NotEqual"
        ValueToCompare="0" ValidationGroup="vgGroup">
        <br />
        <br />
              <asp:Label ID="Label4" runat="server" Text="الرجاء اختيار الحالة للمجموعة"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" ValidationGroup="vgGroup" /><br />
    <br />
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;هل انت متاكد من الحذف&quot;)"
        Text="حذف " /><br />
    <br />
    <asp:Button ID="btnNew" runat="server" OnClientClick="ShowDialog();" Text="اضافة جديدة"
        CausesValidation="False" OnClick="btnNew_Click" /><br />
    <br />
    <asp:GridView ID="gvGroups" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="grp_id" HeaderText="رقم المجموعة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="اسم المجموعة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/groups/{0}", Eval("grp_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
