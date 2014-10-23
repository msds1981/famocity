<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpMain.Master" AutoEventWireup="true" CodeBehind="usrProfile.aspx.cs" Inherits="FamoCity.usrProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/js/javako.js"></script>
    <script src="/js/edit-button.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSave1").click(function () {
                var fname = $("#<%=txtFirstName.ClientID%>").val();
                var lname = $("#<%=txtLastName.ClientID%>").val();
                $("#span1").html("لم يتم الحفظ");
                if (fname == "") {

                    $("#lblPer").html("ادخل الاسم الاول");
                    return false;
                }
                if (lname == "") {

                    $("#lblPer").html("ادخل الاسم الثاني");
                    return false;
                }

                //                 (fireAjax("/Methods.aspx/SaveUserFirstName", "{'firstname':'" + fname + "','lastname':'" + lname + "'}"))
                //                 $("#").html("تم التعديل على البيانات  بنجاح ");
                //                 });
                //                 });

                $.ajax({
                    type: "POST",
                    url: "/Methods.aspx/SaveUserFirstName",
                    data: "{'firstname':'" + fname + "','lastname':'" + lname + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d > 0) {
                            $("#lblPer").html("تم تعديل البيانات بنجاح");
                        }
                        $("#span1").html("تم الحفظ");
                    }

                });
            });

            $("#btnUpdatUser").click(function () {
                var Uname = $("#<%=txtUserUpdate.ClientID%>").val();
                $("#span2").html("لم يتم الحفظ");
                if (Uname == "") {

                    $("#lblUser").html("ادخل اسم المستخدم");
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: "/Methods.aspx/SaveUser",
                    data: "{'User':'" + Uname + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
//                        var letters = /^[A-Za-z]+$/;
//                        if (Uname.value.match(letters)) {
//                            return true;
//                        }
//                        else
//                        {
//                            $("#lblUser").html("اكتب الحروف باللغة الانجليزية ");
//                            return false;
//                        }
                        if (data.d > 0) {
                            $("#lblUser").html("تم تعديل البيانات بنجاح");
                        }
                        $("#span2").html("تم الحفظ");
                    }
                });
            });


            $("#btnAddress").click(function () {
                var Tddress = $("#<%=txtAddress.ClientID%>").val();
                var Dcounty = $("#<%=ddlContry.ClientID%>").val();
                var Tcity = $("#<%=txtCity.ClientID%>").val();
                var Dnationality = $("#<%=ddlNationlity.ClientID%>").val();
                var Box = $("#<%=txtBox.ClientID%>").val();

                $("#span3").html("لم يتم الحفظ");

                $.ajax({
                    type: "POST",
                    url: "/Methods.aspx/SaveAddress",
                    data: "{'address':'" + Tddress + "','country':'" + Dcounty + "','city':'" + Tcity + "','nationality':'" + Dnationality + "','bx':'" + Box + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d > 0) {
                            $("#lblAdress").html("تم تعديل البيانات بنجاح");

                        }
                        $("#span3").html("تم الحفظ");
                    }
                });
            });

            $("#btnSaveLang").click(function () {
                var TLang = $("#<%=ddlLang.ClientID%>").val();
                $("#span4").html("لم يتم الحفظ");
                if (TLang.SelectedValue == "0") {
                    $("#lblLang").html("الرجاء اختيار اللغة  ");
                    return false;
                } else


                    $.ajax({
                        type: "POST",
                        url: "/Methods.aspx/SaveLang",
                        data: "{'lang':'" + TLang + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d > 0) {
                                $("#lblLang").html("تم تعديل البيانات بنجاح");
                            }
                            $("#span4").html("تم الحفظ");
                        }
                    });
            });




            $("#btnSaveStatus").click(function () {
                var Tstate = $("#<%=txtStatus.ClientID%>").val();
                $("#span5").html("لم يتم الحفظ");
                if (Tstate == "") {

                    $("#lblStatus").html("ادخل الحالة");
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: "/Methods.aspx/SaveState",
                    data: "{'State':'" + Tstate + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d > 0) {
                            $("#lblStatus").html("تم تعديل البيانات بنجاح");
                        }
                        $("#span5").html("تم الحفظ");
                    }
                });
            });


            $("#btnSavePass").click(function () {
                var TCurrPss = $("#<%=txtCurrentPass.ClientID%>").val();
                var TNewPss = $("#<%=txtNewPass.ClientID%>").val();
                var TReapPss = $("#<%=txtReapetPass.ClientID%>").val();
                $("#span6").html("لم يتم الحفظ");

                if (TCurrPss == "") {

                    $("#lblmsg").html("ادخل كلمة المرور الحالية");
                    return false;
                }
                else if (TNewPss == "") {

                    $("#lblmsg").html("ادخل كلمة المرور الجديدة");
                    return false;
                }
                else if (TReapPss != TNewPss) {

                    $("#lblmsg").html("كلمة المرور ليست متطابقة");
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: "/Methods.aspx/SavePass",
                    data: "{'currpass':'" + TCurrPss + "','newpass':'" + TNewPss + "','reapetpass':'" + TReapPss + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d > 0) {
                            $("#lblmsg").html("تم تعديل البيانات بنجاح");
                            return false;
                        }
                        $("#span6").html("تم الحفظ");
                    }
                });
            });


            $("#btnSaveEmail").click(function () {
                var TEmail = $("#<%=txtEmail.ClientID%>").val();
                $("#Span7").html("لم يتم الحفظ");
                if (TEmail == " ") {
                    $("#ltsEmail").html("ادخل البريد الالكتروني");
                    return false;
                }
                else

                    $.ajax({
                        type: "POST",
                        url: "/Methods.aspx/SaveEmail",
                        data: "{'email':'" + TEmail + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d > 0) {
                                $("#ltsEmail").html("تم تعديل البيانات بنجاح");
                            }
                            $("#Span7").html("تم الحفظ");
                        }
                    });
            });

            $("#btnSaveDate").click(function () {

                var Dday = $("#<%=ddlDay.ClientID%>").val();
                var DMonth = $("#<%=ddlMonth.ClientID%>").val();
                var DYear = $("#<%=ddlYear.ClientID%>").val();
                $("#span8").html("لم يتم الحفظ");
                if (DMonth.SelectedValue == "0") {

                    $("#lblBirthDate").html("يرجى ادخال شهرا الميلاد  ");
                }
                if (DYear.SelectedValue == "0") {

                    $("#lblBirthDate").html("يرجى ادخال سنة الميلاد  ");
                }
                if (Dday.SelectedValue == "0") {

                    $("#lblBirthDate").html("يرجى ادخال يوم الميلاد ");
                }

                $.ajax({
                    type: "POST",
                    url: "/Methods.aspx/SaveBithDate",
                    data: "{'day':'" + Dday + "','month':'" + DMonth + "','year':'" + DYear + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d > 0) {
                            $("#lblBirthDate").html("تم التعديل على البيانات  بنجاح ");
                        }
                        $("#span8").html("تم الحفظ");
                    }
                });
            });
        });
     
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- START HEADER SEPERATOR -->
    <div id="header_seperator">
    </div>
    <!-- END HEADER SEPERATOR -->
    <!-- GRID CONTAINER START -->
    <div class="gridContainer clearfix">
        <!-- MAIN CONTENT START -->
        <div id="main_content">
		<div class="tabs_all"><!-- Tabs Start -->
		<div id="innerpage">
				<!--Right Start -->
                <div id="sidebarright">
		            <ul>
                        <li class="arrow_box">
				            <a href="#">خيارات عامة
				                <div class="tab_icon"><img src="images/amr_diab.png" /></div>
				            </a>
				        </li>
                        <li class="arrow_box">
				            <a href="#">خيارات الخصوصية
				                <div class="tab_icon"><img src="images/amr_diab.png" /></div>
				            </a>
				        </li>
<%--                        <li class="arrow_box">
				            <a href="#">تعطيل الاوضاع
				                <div class="tab_icon"><img src="images/amr_diab.png" /></div>
				            </a>
				        </li>
                        <li class="arrow_box">
				            <a href="#">تجربة شىء اخر
				                <div class="tab_icon"><img src="images/amr_diab.png" /></div>
				            </a>
				        </li>
                        <li class="arrow_box">
				            <a href="#">بعض الاغراض
				                <div class="tab_icon"><img src="images/amr_diab.png" /></div>
				            </a>
				        </li>
				        <li class="arrow_box">
				            <a href="#">خدمات عامة
				                <div class="tab_icon"><img src="images/amr_diab.png" /></div>
				            </a>
				        </li>--%>
                    </ul><!--Right End -->
                </div><!-- END sidebarright -->
                <form id="form" runat="server">
		    <div id="tabs">
            <ul>
            <!-- Start articel 1 -->
			 <!--Tab one-->
            <li>
            <!-- Start tabcontent -->
            <div class="tabcontent"><!-- Context Start-->
                <div class="context" id="butt">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="txtName" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title"> <asp:Literal ID="Literal1" runat="server" Text="الإسم الشخصي"></asp:Literal> </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                                
                            </div>
                            <div class="spann">
                                  <asp:Literal ID="ltfirstname" runat="server" Text="الاسم الاول"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                <asp:TextBox ID="txtFirstName" runat="server" Text=""></asp:TextBox>
                            </div>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                             
                            </div>
                            <div class="spann">
                             <asp:Literal ID="ltlastname" runat="server" Text="الاسم الثاني"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                <asp:TextBox ID="txtLastName" runat="server" Text=""></asp:TextBox>
                            </div>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                          <p> <span id="lblPer" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                                <span id="span1"></span>
                                <input id="btnSave1" type="button" value="حفظ"/>
                                <input type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div><!-- Context End --> 


                       <div class="context" id="Div2">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="ltrUserName" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title">
                     <asp:Literal ID="lttitleUser" runat="server" Text="اسم المستخدم"></asp:Literal> 
                     </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                              
                            </div>
                              <div class="spann">
                            <asp:Literal ID="ltuser" runat="server" Text="اسم المستخدم"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                <asp:TextBox ID="txtUserUpdate" runat="server" Text=""></asp:TextBox>
                            </div>
                          
                        </div>
                    </div><!-- Con End-->


                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                          <p> <span id="lblUser" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                               <div class="detailDiv"><!-- Con Start-->
                              <div class="con_all">
                               <div class="save_info">
                                 <span id="span2"></span>
                                <input id="btnUpdatUser" type="button" value="حفظ"/>
                                <input type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div>



          

               <div class="context" id="tabAddress">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="ltrAddress" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title">
                     <asp:Literal ID="ltrtitleAddress" runat="server" Text="العنوان"></asp:Literal> 
                     </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                              
                            </div>
                              <div class="spann">
                            <asp:Literal ID="ltUpdateAddress" runat="server" Text="العنوان"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                <asp:TextBox ID="txtAddress" runat="server" Text=""></asp:TextBox>
                            </div>
                          
                        </div>
                    </div><!-- Con End-->


                      <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                            
                            </div>
                             <div class="spann">
                              <asp:Literal ID="ltContry" runat="server" Text="الدولة"></asp:Literal> 
                            </div>
                            <div class="inputt">
                               <asp:DropDownList ID="ddlContry" runat="server">
                                </asp:DropDownList>
                            </div>
                           
                        </div>
                    </div><!-- Con End-->



                      <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                              
                            </div>
                             <div class="spann">
                            <asp:Literal ID="ltCity" runat="server" Text="المدينة"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                 <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                    </div><!-- Con End-->


                      
                      <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                             
                            </div>
                            <div class="spann">
                                <asp:Literal ID="ltNationality" runat="server" Text="الجنسية"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                <asp:DropDownList ID="ddlNationlity" runat="server">
                                </asp:DropDownList>
                            </div>
                            
                        </div>
                    </div>



                      <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                             
                            </div>
                            <div class="spann">
                              <asp:Literal ID="ltBox" runat="server" Text="صندوق البريد"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                   <asp:TextBox ID="txtBox" runat="server"></asp:TextBox>
                            </div>
                            
                        </div>
                    </div><!-- Con End-->

                        <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                             <p> <span id="lblAdress" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                 
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                                   <span id="span3"></span>
                                <input id="btnAddress" type="button" value="حفظ"/>
                                <input type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div>

                    <div class="context" id="tabLang">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="ltLang" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title">
                     <asp:Literal ID="lttitleLang" runat="server" Text="اللغة"></asp:Literal> 
                     </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                             
                            </div>
                             <div class="spann">
                              <asp:Literal ID="ltrlang" runat="server" Text="اللغة"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                <asp:DropDownList ID="ddlLang" runat="server">
                                </asp:DropDownList>
                            </div>
                           
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                            </div>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <p> <span id="lblLang" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                                 <span id="span4"></span>
                                <input id="btnSaveLang" type="button" value="حفظ"/>
                                <input id="btnCanceleLang" type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div><!-- Context End --> 

                  <div class="context" id="tavStatus">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="ltStatus" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title">
                     <asp:Literal ID="lttitlestatus" runat="server" Text="الحالة"></asp:Literal> 
                     </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                       
                            </div>
                             <div class="spann">
                                <asp:Literal ID="ltrstatus" runat="server" Text="الحالة"></asp:Literal> 
                            </div>
                            <div class="inputt">
                                <asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                            </div>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                        <p> <span id="lblStatus" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                                <span id="Span5" style="color:red" ></span>
                                <input id="btnSaveStatus" type="button" value="حفظ"/>
                                <input id="btnCancleStatus" type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div><!-- Context End --> 

                  <div class="context" id="Div3">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="lblPhoto" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title">
                     <asp:Literal ID="Literal5" runat="server" Text="الصورة الشخصية"></asp:Literal> 
                     </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                       
                            </div>
                             <div class="spann">
                                <asp:Literal ID="Literal6" runat="server" Text="مسار الصورة"></asp:Literal> 
                            </div>
                            <div class="inputt">

                            </div>
                           
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                            </div>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                        <p> <span id="Span9" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                                <span id="Span10" style="color:red" ></span>
                                <input id="Button1" type="button" value="حفظ"/>
                                <input id="Button2" type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div><!-- Context End --> 

            </div><!-- End tabcontent -->
           </li>  <!--End Tab one-->

        <!---------------------------------------------------->



            <!--Tab Tow-->
                
            <!-- Start tabcontent -->
            <li>
            <div class="tabcontent"><!-- Context Start-->
                <div class="context" id="tabPass">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="LtCurrentPass" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title"> 
                    <asp:Literal ID="Literal3" runat="server" Text="كلمة المرور"></asp:Literal> </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                                
                            </div>                            
                            <div class="spann">
                               <asp:Literal ID="ltTitleCurrPass" runat="server" Text="كلمة المرور الحالة"></asp:Literal>
                            </div>

                            <div class="inputt">
                                <asp:TextBox ID="txtCurrentPass" runat="server" Text="" TextMode="Password"></asp:TextBox>
                            </div>

                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                              
                            </div>
                            <div class="spann">
                               <asp:Literal ID="Literal2" runat="server" Text=" كلمةالمرور الجديدة"></asp:Literal>
                            </div>
                            <div class="inputt">
                                <asp:TextBox ID="txtNewPass" runat="server" Text="" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                              
                            </div>
                            <div class="spann">
                               <asp:Literal ID="ltreaperPass" runat="server" Text=" اعادة كلمةالمرور"></asp:Literal>
                            </div>
                            <div class="inputt">
                                <asp:TextBox ID="txtReapetPass" runat="server" Text="" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div><!-- Con End-->
                   <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                           <p> <span id="lblmsg" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                                <span id="Span6" style="color:red" ></span>
                                <input id="btnSavePass" type="button" value="حفظ" />
                                <input type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div><!-- Context End -->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                          
                        </div>
                    </div><!-- Con End--> 

                 <div class="context" id="tabEmail">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="ltsEmail" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title"> 
                    <asp:Literal ID="ltTitltEmail" runat="server" Text="البريد الالكتروني"></asp:Literal> </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                              
                            </div>
                              <div class="spann">
                               <asp:Literal ID="ltEmail" runat="server" Text="البريد الالكتروني"></asp:Literal>
                            </div>
                            <div class="inputt">
                                <asp:TextBox ID="txtEmail" runat="server" Text=""></asp:TextBox>
                            </div>
                          
                        </div>
                    </div><!-- Con End-->
                  
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                             <p> <span id="lblEmail" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                                <span id="Span7"></span>
                                <input type="button" value="حفظ" onclick="return btnSaveEmail_onclick()" 
                                    id="btnSaveEmail" />
                                <input type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div>

                   <div class="context" id="Div1">
                    <div class="edit_button">
                        <a href="#" class="button" > تعديل </a>
                    </div>		
                    <div class="edit_name">
                        <asp:Literal ID="ltshowdate" runat="server" ></asp:Literal>
                    </div>
                    <div class="edit_title"> 
                    <asp:Literal ID="ltTitleDate" runat="server" Text="تاريخ الميلاد"></asp:Literal> </div>
                    <div class="detailDiv"> <!-- Con Start-->
                        <div class="con_all">
                            <div class="validd">
                            </div>
                            <div class="inputt">
                     <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="false" CssClass="selectdate-y">
                   </asp:DropDownList>
                    <div class="validd">
                                </div>
                                <div class="spann">
                               <asp:Literal ID="ltDate" runat="server" Text="تاريخ الميلاد"></asp:Literal>
                            </div>
                     <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="false" CssClass="selectdate-m">
                    
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                    </asp:DropDownList>
                     <div class="validd">
                    </div>
                        <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="false" CssClass="selectdate-m">
                     
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="26">26</asp:ListItem>
                        <asp:ListItem Value="27">27</asp:ListItem>
                        <asp:ListItem Value="28">28</asp:ListItem>
                        <asp:ListItem Value="29">29</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="31">31</asp:ListItem>
                    </asp:DropDownList>
                     
                            </div>
                           
                            
                        </div>
                    </div><!-- Con End-->
                  
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                        <p> <span id="lblBirthDate" style="color:red" ></span></p>
                        </div>
                    </div><!-- Con End-->
                    <div class="detailDiv"><!-- Con Start-->
                        <div class="con_all">
                            <div class="save_info">
                                <span id="span8"></span>
                                
                                <input id="btnSaveDate" type="button" value="حفظ"/>
                                <input type="button" value="الغاء" Class="btn_cancel"/>
                            </div>
                        </div>
                    </div><!-- Con End-->
                </div>

              
            </div><!-- End tabcontent -->
           </li>

            <!--EndTab Tow-->



         <!------------------------------------------------->






        </ul>
        </div>
        </form>
        <!-- end tabs -->
		</div><!-- END innerpage -->
		
		</div><!-- Tabs End -->
		
        </div><!-- MAIN CONTENT END -->

        <div class="clear">
        </div>
    </div>
    <!-- GRID CONTAINER END -->

</asp:Content>
