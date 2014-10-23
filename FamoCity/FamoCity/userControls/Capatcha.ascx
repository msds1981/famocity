<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Capatcha.ascx.cs" Inherits="Services.Capatcha" %>


  <%--  <asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">--%>
   <p>اكتب محتوى الصورة</p>
    <asp:Image ID="Image1" runat="server" ImageUrl="../CImage.aspx"/>
 
    <asp:TextBox ID="txtimgcode" runat="server"></asp:TextBox>

<%--</asp:Content>--%>

    