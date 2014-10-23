<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admCharGrop.aspx.cs" Inherits="FamoCity.admCharGrop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Charactor Group</h1>
    <asp:Label ID="lblChar" runat="server" Text="Character"></asp:Label>
    <asp:DropDownList ID="ddlChar" runat="server">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ddlChar" Display="Dynamic"
        runat="server" Operator="NotEqual" ValueToCompare="0" ValidationGroup="vgChrGroup">
        <asp:Label ID="lblcomVlang" runat="server" Text="الرجاء الاختيار"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblName" runat="server" Text="Name "></asp:Label>
    <asp:TextBox ID="txtName" runat="server" Height="28px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
        Display="Dynamic" ValidationGroup="vgChrGroup">
        <asp:Label ID="Label2" runat="server" Text="الرجاء كتابة الاسم"></asp:Label>
    </asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label ID="lblFile" runat="server" Text="File "></asp:Label>
    <asp:FileUpload ID="fuChar" runat="server" /><br />
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
    <br />
    <asp:Label ID="lblextfile" runat="server" Text="مسار الملف "></asp:Label>
    <asp:Label ID="lblShowFile" runat="server"></asp:Label><br />
    <br />
    <asp:Label ID="lblCam" runat="server" Text="Cam"></asp:Label>
    <asp:TextBox ID="txtCam" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvCharGroup" runat="server" ControlToValidate="txtCam"
        Display="Dynamic" ValidationGroup="vgChrGroup">
        <asp:Label ID="Label7" runat="server" Text="الرجاء ادخال رقم"></asp:Label>
    </asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="revCharGroup" runat="server" ControlToValidate="txtCam"
        Display="Dynamic" ValidationExpression="^\d+$" ValidationGroup="vgChrGroup">
        <asp:Label ID="Label3" runat="server" Text="الرجاء ادخال رقم"></asp:Label>
    </asp:RegularExpressionValidator><br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="اضافة جديدة"
        CausesValidation="False" /><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" ValidationGroup="vgChrGroup" /><br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="مجلد الحفظ "></asp:Label>
    <asp:Label ID="lblSaveFile" runat="server"></asp:Label><br />
    <br />
    <asp:Label ID="Label5" runat="server" Text="Groups"></asp:Label>
    <asp:DropDownList ID="ddlGroups" runat="server">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlGroups"
        Display="Dynamic" ForeColor="Red" Operator="NotEqual" ValueToCompare="0" ValidationGroup="vgchrDetails">*</asp:CompareValidator><br />
    <br />
    <asp:Label ID="Label6" runat="server" Text="Flag"></asp:Label>
    <asp:DropDownList ID="ddlFlag" runat="server">
        <asp:ListItem Value="0">- اختر من القائمة -</asp:ListItem>
        <asp:ListItem Value="1">متوافق </asp:ListItem>
        <asp:ListItem Value="2">متعارض</asp:ListItem>
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlFlag"
        Display="Dynamic" ForeColor="Red" Operator="NotEqual" ValueToCompare="0" ValidationGroup="vgchrDetails">*</asp:CompareValidator><br />
    <br />
    <asp:Button ID="btnAddto" runat="server" OnClick="btnAddto_Click" Text="اضافة القائمة"
        ValidationGroup="vgchrDetails" /><br />
    <br />
    <asp:GridView ID="gvCharDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCharDetails_RowCommand">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="grpd_id" HeaderText="رقم المجموعة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="flag_text" HeaderText="العلاقة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="المجموعة ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField HeaderText="حذف ">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Eval("grpd_id") %>'
                        CommandName="cmd" ToolTip="تنزيل" OnClientClick="return confirm(&quot;هل انت متاكد من المحذف&quot;)">حذف </asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
