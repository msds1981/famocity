<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmn2.Master" AutoEventWireup="true"
    CodeBehind="admTask.aspx.cs" Inherits="FamoCity.admTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label ID="lblLang" runat="server" Text="اللغة "></asp:Label>
    <asp:CompareValidator ID="cmvLang" runat="server" ControlToValidate="ddlLang" Display="Dynamic"
        ErrorMessage="يرجى اختيار اللغة" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator><br />
    <br />
    <asp:TextBox ID="txtTaskName" runat="server"></asp:TextBox>
    <asp:Label ID="lblTask" runat="server" Text="اسم المهمة "></asp:Label>
    <asp:RequiredFieldValidator ID="rfvtaskName" runat="server" ControlToValidate="txtTaskName"
        Display="Dynamic" ErrorMessage="يرجى ادخال اسم المهمة"></asp:RequiredFieldValidator><br />
    <br />
    <asp:TextBox ID="txtTaskDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
    <asp:Label ID="lblDesc" runat="server" Text="وصف المهمة "></asp:Label>
    <asp:RequiredFieldValidator ID="rfvtaskdesc" runat="server" ControlToValidate="txtTaskDesc"
        Display="Dynamic" ErrorMessage="يرجى كتابة وصف المهمة "></asp:RequiredFieldValidator><br />
    <br />
    <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
    <asp:Label ID="lblGroupName" runat="server" Text="اسم المجموعة "></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGroupName"
        Display="Dynamic" ErrorMessage="يرجى كتابة اسم المجموعة"></asp:RequiredFieldValidator><br />
    <br />
    <asp:FileUpload ID="fuImage" runat="server" />
    <asp:Label ID="lblImage" runat="server" Text="الصورة "></asp:Label><br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="حفظ" /><br />
    <br />
    <asp:Label ID="lblmsg" runat="server"></asp:Label>
    <asp:DropDownList ID="ddlOrdLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdLang_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:GridView ID="gvTask" runat="server" AutoGenerateColumns="False" EmptyDataText="عفوا لا توجد بيانات في قاعدة البيانات ">
        <Columns>
            <asp:BoundField DataField="text" HeaderText="اسم المهمة " />
            <asp:BoundField DataField="grp" HeaderText="المجموعة " />
            <asp:HyperLinkField DataNavigateUrlFields="tsk_id" DataNavigateUrlFormatString="admTask.aspx?id={0}"
                HeaderText="تعديل " Text="تعديل " />
        </Columns>
    </asp:GridView>
</asp:Content>
