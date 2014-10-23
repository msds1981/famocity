<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="msg.aspx.cs" Inherits="FamoCity.msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/forgetpassword.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div class="ccontent size">
            <div class="bigbox boxsize-small">
                <asp:Label ID="lblMsg" runat="server" Text="Label" style="float:right;direction:rtl;"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
