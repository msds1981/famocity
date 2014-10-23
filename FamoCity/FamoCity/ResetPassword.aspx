<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="FamoCity.ResetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:Label ID="lblEmail" runat="server" Text="ادخل الايميل "></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtEmail" Display="Dynamic" 
            ErrorMessage="يرجى ادخال الايميل" ValidationGroup="VgRepassport">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtEmail" Display="Dynamic" 
            ErrorMessage="يرجى ادخال الايميل بطريقة صحيحة" 
            ValidationGroup="VgRepassport" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
        <br />
        <asp:Button ID="btnsend" runat="server" onclick="btnsend_Click" Text="ارسال" 
            ValidationGroup="VgRepassport" />
    
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ValidationGroup="VgRepassport" />
    
    </div>
    </form>
</body>
</html>
