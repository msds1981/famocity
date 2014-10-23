<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="usrRegStep5.aspx.cs" Inherits="FamoCity.usrRegStep5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div class="ccontent size">
   <div id="fb-root"></div>
   <script src="http://connect.facebook.net/en_US/all.js">
   </script>
   <script>
       FB.init({
           appId: '452457478185290', cookie: true,
           status: true, xfbml: true
       });
       function FacebookInviteFriends() {
           FB.ui({ method: 'apprequests',
               message: 'see our nice game...'
           });
       }
   </script>
   <script type='text/javascript'>
       if (top.location != self.location) {
           top.location = self.location
       }
</script>
<a href='#' onclick="FacebookInviteFriends();"> Facebook Invite Friends</a>
        </div>
    </div>
</asp:Content>
