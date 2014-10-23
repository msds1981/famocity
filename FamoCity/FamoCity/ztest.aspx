
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ztest.aspx.cs" Inherits="FamoCity.ztest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function addURL(element)
        {
            $(element).attr('href', function() {
                return this.href + '&cylnders=12';
            });
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:button runat="server" text="Button" onclick="Unnamed1_Click" /><br /><br /><br /><br />
        <a onclick="addURL(this)" href="/search-results/?var1=red&var2=leather">Click this</a>

    </div>
    </form>
</body>
</html>
