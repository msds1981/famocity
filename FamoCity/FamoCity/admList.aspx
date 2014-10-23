<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admList.aspx.cs" Inherits="FamoCity.admList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        القوائم</h1>
    <asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged"
        AppendDataBoundItems="false">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ddlLang" Display="Dynamic"
        runat="server" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="lblcomVlang" runat="server" Text="الرجاء اختيار اللغة"></asp:Label>
    </asp:CompareValidator>
    <asp:Label ID="lblName" runat="server" Text="النص"></asp:Label><br />
    <br />
    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
        Display="Dynamic">
        <asp:Label ID="Label4" runat="server" Text="من فضلك اكتب النص"></asp:Label>
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="حفظ" /><br />
    <br />
    <asp:Button ID="btnListDelete" runat="server" OnClick="btnListDelete_Click" OnClientClick="return confirm(&quot;هل انت متاكد من حذف السجل&quot;)"
        Text="حذف " CausesValidation="False" /><br />
    <br />
    <asp:Button ID="btnNewList" runat="server" OnClick="btnNewList_Click" Text="اضافة جديدة "
        CausesValidation="False" /><br />
    <br />
    <asp:Label ID="lblName0" runat="server" Text="اختر اللغة من القائمة "></asp:Label>
    <asp:DropDownList ID="ddlLangSelect" runat="server" AppendDataBoundItems="false"
        AutoPostBack="True" OnSelectedIndexChanged="ddlLangSelect_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="list_id" HeaderText="الرقم ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="text" HeaderText="الاسم">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/List/{0}/{1}",Eval("name"), Eval("list_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label5" runat="server" Text="لا توجد بيانات في قاعدة البيانات "></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
