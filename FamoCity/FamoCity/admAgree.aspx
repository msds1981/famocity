<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admAgree.aspx.cs" Inherits="FamoCity.admAgree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h1>
        الاتفاقية
    </h1>
    <asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ddlLang" Display="Dynamic"
        runat="server" Operator="NotEqual" ValueToCompare="0">
        <asp:Label ID="lblcomVlang" runat="server" Text="الرجاء اختيار اللغة"></asp:Label>
    </asp:CompareValidator><br />
    <br />
    <asp:Label ID="lblgreating" runat="server" Text="الاتفاقية  "></asp:Label>
    <asp:TextBox ID="txtAgreement" runat="server" Height="175px" TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAgreement"
        Display="Dynamic">
        <asp:Label ID="Label3" runat="server" Text="الرجاء كتابة الاتفاقية"></asp:Label>
        <br />
        <br />
    </asp:RequiredFieldValidator><br />
    <br />
    <asp:Button ID="btnAddLang" runat="server" OnClick="btnAddLang_Click" Style="height: 26px"
        Text="حفظ" /><br />
    <br />
    <asp:Button ID="btnDeleteArgeement" runat="server" OnClick="btnDeleteArgeement_Click"
        OnClientClick="return confirm(&quot;هل انت متاكد من الحذف ؟&quot;)" Text="حذف"
        CausesValidation="False" /><br />
    <br />
    <asp:Label ID="lblLang" runat="server" Text="اللغة "></asp:Label>
    <asp:DropDownList ID="ddlgvLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgvLang_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="btnNewAgree" runat="server" OnClick="btnNewAgree_Click" Text="اضافة جديدة "
        CausesValidation="False" />
    <br />
    <br />
    <br />
    <br />
    <asp:GridView ID="gvAgreement" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle />
        <Columns>
            <asp:BoundField DataField="agre_id" HeaderText="رقم الاتفاقية ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="type" HeaderText="نوع الاتفاقية ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="text" HeaderText="الاتفاقية">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField HeaderText="تعديل ">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" title="تعديل" runat="server" NavigateUrl='<%# string.Format("/admin/Agree/{0}/{1}",Eval("type"), Eval("agre_id")) %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                <ItemStyle />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            لاتوجد بيانات&nbsp; ناتجة من الاستعلام
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
