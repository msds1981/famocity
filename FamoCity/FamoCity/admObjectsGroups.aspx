<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admObjectsGroups.aspx.cs" Inherits="FamoCity.admObjectsGroups" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            width: 156px;
        }
    </style>
     <style type="text/css">
  
         div.centered      { width: 467px; }
div.columns       { width: 936px;  
margin: 0 auto;
             height: 32px;
         }
div.columns div   { width: 233px; 
height: 30px; 
float: left; }
div.grey          { background-color: #cccccc;text-align:center }
div.red           { background-color: White; text-align:center}
div.clear         { clear: both;
             height: 8px;
             width: 463px;
         }
         .style2
         {
             height: 56px;
         }
         .style3
         {
             width: 156px;
             height: 56px;
         }
   </style>
</head>
<body>

    <form id="form1" runat="server">
   
   
            <div id="page-heading">
                <h1>
                   Objects</h1>
            </div>


          
                                              
                                                <br />
                                              
                                                    <asp:Label ID="lblScene" runat="server" Text="Scene"></asp:Label>
                                              
                                                    

                                                      <asp:DropDownList ID="ddlScene" runat="server" AutoPostBack="True" 
                                                          onselectedindexchanged="ddlScene_SelectedIndexChanged">
                                                  </asp:DropDownList>

                                                 


                                               

                                                 <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                    ControlToValidate="ddlTrigger" Display="Dynamic" 
                                                    ErrorMessage="الرجاء  الاختيار " Operator="NotEqual" ValueToCompare="0"  >
                                                   
                                                      
                                                            <asp:Label ID="Label6" runat="server" Text="الرجاء  الاختيار"></asp:Label>
                                                    
                                                    
                                                    </asp:CompareValidator>

                                                    <br />

                                               
                                                
                                                  <asp:Label ID="LblTrigger" runat="server" Text="Trigger"></asp:Label>
                                              
                                                  
                                                   <asp:DropDownList ID="ddlTrigger" runat="server" 
                                                        onselectedindexchanged="ddlTrigger_SelectedIndexChanged" 
                                                        AutoPostBack="True">
                                                     </asp:DropDownList>
                                                 
                                               

                                                  <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                                    ControlToValidate="ddlScene" Display="Dynamic" 
                                                    Operator="NotEqual" 
                                                    ValueToCompare="0" >
                                                   
                                                   
                                                     <asp:Label ID="Label7" runat="server" Text="الرجاء اختيار  الحدث"></asp:Label>
                                                    
                                                    </asp:CompareValidator>

                                                    <br />

                                                    <asp:Label ID="Label1" runat="server" Text="PlatForm"></asp:Label>
                                              
                                                  
                                                   <asp:DropDownList ID="ddlplatorm" runat="server" 
                                                        AutoPostBack="True" 
                onselectedindexchanged="ddlplatorm_SelectedIndexChanged">
                                                        <asp:ListItem Value="Desk" Text="Desk"></asp:ListItem>
                                                          <asp:ListItem Value="Mob" Text="Mob"></asp:ListItem>

                                                     </asp:DropDownList>
                                                 
                                               

                                                  <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                                    ControlToValidate="ddlplatorm" Display="Dynamic" 
                                                    Operator="NotEqual" 
                                                    ValueToCompare="" >
                                                   
                                                   
                                                     <asp:Label ID="Label2" runat="server" Text="الرجاء اختيار  منصة العمل"></asp:Label>
                                                    
                                                    </asp:CompareValidator>
                                         <br />
                                            
                                             <asp:Label ID="lblUpFile" runat="server" Text="File"></asp:Label>
                                                
                        <br />
                        path:=<asp:Label ID="prefexpath" runat="server" Text="C:\inetpub\vhosts\fameway.com\httpdocs\_upload"></asp:Label>
    <asp:TextBox ID="localfilesfolder" runat="server"></asp:TextBox>
    <br />
         <asp:Label ID="foundpath" runat="server"></asp:Label>
    <br />
                                                   <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                
                                            
                                                <br />


                                                
                                                    <asp:Label ID="lblfile" runat="server" Text="امتداد حفظ الملف"></asp:Label>
                                                
                                                 <asp:Label ID="lblSaveFile" runat="server"></asp:Label>
                                                

                                              
                                                
                                           



                                           <br />


                                         
                                          <br />

                                              
                                               
                                           <br />

                                                
                                                    <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="حفظ" CssClass="form-submit"
                                                  />
                                                    


     
       
            <br />

      <div class='columns'>
              <div class='red' >id</div>
               <div class='red'>Code</div>
                <div class='red'>Description</div>
               <div class='red'> edit</div>
               </div>


     <asp:Literal ID="objectslist" runat="server"></asp:Literal>
   


    </form>
   </body>
</html>
