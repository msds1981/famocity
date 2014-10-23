<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="FamoCity.Message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="/Messages/Styles/CSS/Styles.css" />
    <link type="text/css" rel="stylesheet" href="/Messages/Styles/CSS/jquery.mCustomScrollbar.css" />
    <script type="text/javascript" src="/Messages/Scripts/jquery-1.10.2.js"></script>
    <%-- <script type="text/javascript" src="Messages/Scripts/jquery-ui-1.10.4.custom.js"></script>--%>
    <script src="/Messages/Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script src="/Messages/Scripts/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
    <script src="/Messages/Scripts/AngelScript.js" type="text/javascript"></script>
    <script src="/js/Chat.js" type="text/javascript"></script>
    <link href="/newfiles/css/desktop.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/screen.css" rel="stylesheet" type="text/css" />--%>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="/newfiles/css/boilerplate.css" rel="stylesheet" type="text/css" />
    <link href="/newfiles/css/MQ.css" rel="stylesheet" type="text/css" />
    <link href="/newfiles/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/uploader/jquery-ui.1.8.13.css" rel="stylesheet" type="text/css" />
    <link href="/css/uploader/jquery.fileupload-ui.css" rel="stylesheet" type="text/css" />
    <link href="Messages/Styles/CSS/responsive.css" rel="stylesheet" type="text/css" />
    <%--  <link href="/css/uploader/jquery-ui.1.8.13.css" rel="stylesheet" type="text/css" />
    <link href="/css/uploader/jquery.fileupload-ui.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="css/uploader/style.css" rel="stylesheet" type="text/css" />--%>
    <%-- <script src="/newfiles/js/notification.js" type="text/javascript"></script>
    <script src="/newfiles/js/respond.min.js" type="text/javascript"></script>
    
    <!-- social functions -->
    <script src="/js/social_load.js" type="text/javascript"></script>
    <script src="/js/social.js" type="text/javascript"></script>
    <script src="/js/social_albums.js" type="text/javascript"></script>
    <!-- scripts of Menus -->
    <script src="/newfiles/js/toolmenu.js" type="text/javascript"></script>
    <script src="/newfiles/js/topicmenu.js" type="text/javascript"></script>
    <script src="/newfiles/js/album-option.js" type="text/javascript"></script>
    <script src="/newfiles/js/photos-option.js" type="text/javascript"></script>
    <script src="/newfiles/js/video-option.js" type="text/javascript"></script>
    <script src="/newfiles/js/SettingScript.js" type="text/javascript"></script>--%>
    <script type="text/javascript">





        $(document).ready(function () {
            $(".newMessgae").css("display", "none");
            // BuildUserInfoSide();

            /***  work only on desktop  ***/


            BuildConversationList();


            $(".ui-helper-hidden-accessible").html("");


        });
            
    </script>
    <%--<script>

       
//            for (var i = 0; i < friend.length; i++) {
//                //alert("true");
//                var itemfrienddiv = document.createElement("div");
//                itemfrienddiv.className = "item itemFriend";
//                itemfrienddiv.id = "itemfrnd_" + friend[i].linkid;
//                itemfrienddiv.setAttribute('linkid', friend[i].linkid);
//                //image of friend
//                var img = new Image();
//                img.src = friend[i].urlimage;
//                itemfrienddiv.appendChild(img);
//                //info of friend
//                var infodiv = document.createElement("div");
//                infodiv.className = "info";
//                itemfrienddiv.appendChild(infodiv);

//                var Atag = document.createElement("a");
//                Atag.href = friend[i].url;
//                infodiv.appendChild(Atag);
//                //name of friend
//                var namefriendh3 = document.createElement('h3');
//                var namefriend = document.createTextNode(friend[i].firstname + ' ' + friend[i].lastname);
//                namefriendh3.appendChild(namefriend);

//                Atag.appendChild(namefriendh3);


//                //Cencel buttom
//                var CancelAtag = document.createElement("a");
//                CancelAtag.className = "cancel";
//                var cencelRout = '/CencelFrendRepuest?link_id=' + friend[i].linkid;
//                CancelAtag.onclick = function () { AjaxProfile.cencel(cencelRout); };
//                var cencelText = document.createTextNode(response.CencelText);
//                CancelAtag.appendChild(cencelText);
//                // document.getElementsByClassName("cancel").onclick = "AjaxProfile.cencel('/CencelFrendRepuest?link_id=" + friend[i].linkid + "')";

//                //click("AjaxProfile.cencel('/CencelFrendRepuest?link_id=" + friend[i].linkid + "')")
//                itemfrienddiv.appendChild(CancelAtag);
//                //accept buttom
//                var AcceptAtag = document.createElement("a");
//                AcceptAtag.className = "accept";
//                var acceptText = document.createTextNode(response.AcceptText);
//                //url from RouteProvider.cs
//                var Acceptrout = "/AcceptFrendRepuest?link_id=" + friend[i].linkid;
//                AcceptAtag.onclick = function () { AjaxProfile.accept(Acceptrout); };

//                AcceptAtag.appendChild(acceptText);
//                itemfrienddiv.appendChild(AcceptAtag);

//                var lastclass = $(".itemFriend").last();
//                $(lastclass).after(itemfrienddiv);
           // }
        

        </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="myid" runat="server" />
      <asp:HiddenField ID="LinkuserId" runat="server" />
    <!-- Top Header Start -->
    <div id="logo">
        <%--<img src="/newfiles/images/logo.png" alt="Logo" title="FamoCity" />--%>
        <asp:Literal ID="famologo" runat="server"></asp:Literal></div>
    <div id="Notification">
        <div id="notify_list">
            <ul class="noti_ul">
                <li class="add_topic"><a id="button_add_topic" href="#">
                    <img src="/newfiles/images/add_topic.png" /></a></li>
                <li class="friends_noti">
                    <asp:Label ID="lblCount" CssClass="" runat="server" Text=""></asp:Label>
                    <%-- <span class="notired">34</span>--%>
                    <a href="#">
                        <img src="/newfiles/images/friends_icon.png" /></a>
                    <div class="drpdwn_friends listdown">
                        <div class="noti_arrow">
                        </div>
                        <!-- Friends Content Start -->
                        <div class="friend_container_title">
                            طلبات الصداقة
                        </div>
                        <%--<div class="friends_part_frame">
                            <ul>
                                <li><a href="#" class="inboxactive">البحث عن الاصدقاء</a></li>
                                <li><a href="#">اعدادات الاصدقاء</a></li>
                            </ul>
                        </div>--%>
                        <div class="friend_items_con">
                            <ul>
                                <asp:Repeater ID="RreqFriends" runat="server">
                                    <ItemTemplate>
                                        <li id='liRequest_<%#Eval("link_id") %>'>
                                            <div class="friend_item">
                                                <div class="friend_sender_name">
                                                    <%#Eval("UserName")%></div>
                                                <div class="friend_confirm">
                                                    <a class="accept_friend" href="javascript:void(0);" onclick='AcceptFriendShip(<%#Eval("link_id") %>);'>
                                                        قبول</a> <span><a class="refuse_friend" href="javascript:void(0);" onclick='NotAllowRequest(<%#Eval("link_id") %>);'>
                                                            رفض</a></span>
                                                </div>
                                                <div class="clr">
                                                </div>
                                                <div class="friend_time">
                                                    صديق مشترك</div>
                                            </div>
                                            <div class="friend_item_pic">
                                                <a href='<%#Eval("link") %>'>
                                                    <img alt="" src='<%#Eval("user_file") %>' /></a></div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div class="more_friends">
                            <%-- <a href="/usrmain.aspx?uid=<%=userid %>&pg=3&tab=2">مشاهدة باقى الطلبات</a>--%>
                            <%--<a href='<%#Eval("MoreFriends") %>'  >مشاهدة باقى الطلبات</a>--%>
                            <asp:Literal ID="MoreFriends" runat="server"></asp:Literal>
                        </div>
                        <!-- Friends Content End -->
                    </div>
                </li>
                <li class="alerts_noti"><span class="notiredEvent " id="notiredEvent110" runat="server">
                </span><a href="#">
                    <img src="/newfiles/images/alerts_icon.png" /></a>
                    <div class="drpdwn_alerts listdown">
                        <div class="noti_arrow">
                        </div>
                        <!-- Alerts Content Start -->
                        <div class="alert_container_title">
                            الاشعارات
                        </div>
                        <div class="alerts_part_frame">
                            <%--<ul>
                                <li><a href="#" class="inboxactive">تحديد كمقروء</a></li>
                                <li><a href="#">اعدادات الاشعارات</a></li>
                            </ul>--%>
                        </div>
                        <div class="alert_items_con">
                            <ul>
                                <asp:Literal ID="Notificationlit" runat="server"></asp:Literal>
                            </ul>
                        </div>
                        <div class="more_alerts">
                            <a href="#">مشاهدة باقى الاشعارات</a></div>
                        <!-- Alerts Content End -->
                    </div>
                </li>
                <li class="msgs_noti">
                 <asp:Label ID="msgcountLable" CssClass="" runat="server" Text=""></asp:Label>  <a href="#">
                    <img src="/newfiles/images/msgs_icon.png" /></a>
                    <div class="drpdwn_msgs listdown">
                        <div class="noti_arrow">
                        </div>
                        <!-- Msgs Content Start -->
                        <div class="msg_container_title">
                            الرسائل الخاصة
                        </div>
                        <div class="msgs_part_frame">
                            <ul>
                                <li><a href="#" class="inboxactive">(99) البريد الوارد</a></li>
                                <li><a href="#">(120) الرسائل الاخري</a></li>
                                <li><a href="#">ارسال رسالة جديدة</a></li>
                            </ul>
                        </div>
                        <div class="msg_items_con">
                            <ul>
                                <asp:Literal ID="MessageList" runat="server"></asp:Literal>
                            </ul>
                        </div>
                        <div class="more_msgs">
                            <a href="#">مشاهدة باقى الرسائل</a></div>
                        <!-- Msgs Content End -->
                    </div>
                </li>
                <li class="settings_noti"><a href="javascript:void(0);">
                    <img src="/newfiles/images/setting_icon.png" /></a>
                    <div class="drpdwn_settings listdown">
                        <div class="noti_arrow">
                        </div>
                        <!-- User Setting Content start -->
                        <div class="prof_info">
                            <div class="prof_pic">
                                <asp:Literal ID="ltrImageSmall" runat="server"></asp:Literal></div>
                            <div class="prof_edit">
                                <div class="prof_name">
                                    <asp:Literal ID="ltrFullName" runat="server"></asp:Literal></div>
                                <div class="edit_prof">
                                    <a href="/shop/customer/info">تعديل الملف الشخصي</a>
                                    <%--<asp:HyperLink ID="lnkProfilePage" runat="server">تعديل الملف الشخصي</asp:HyperLink>--%>
                                </div>
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="setting_menu">
                            <ul>
                                <%--<li><a href="#">القوائم</a></li>
                                <li><a href="#">مساعدة</a></li>--%>
                                <li><a href="/shop/customer/info">الاعدادات</a></li>
                                <%-- <li><asp:HyperLink ID="lnkSettingPage" runat="server">الاعدادات</asp:HyperLink></li>--%>
                                <li><a href="javascript:void(0);" onclick="RenewHeaderImages();">اعادة ترتيب الصور</a></li>
                                <%--<li><a href="javascript:void(0);" id="lnkChangePohto">تغيير الصورة الشخصية</a></li>--%>
                                <%--<li><a href="javascript:void(0);" id="lnkmasonry">masonry</a></li>--%>
                                <li><a href="/feedback">لديك مشكلة ؟</a></li>
                                <li><a href="/logout">تسجيل الخروج</a></li>
                            </ul>
                        </div>
                    </div>
                    <!-- User Setting Content end -->
                </li>
                <li class="tab-menu"><a href="#" class="stngs_icon">
                    <img src="/newfiles/images/tablet/tab-menu.png" /></a> </li>
            </ul>
        </div>
        <!-- Notification End -->
    </div>
    <div class="gridContainer clearfix">
        <!-- Top Header Wrapper Start -->
        <div id="Top_Header_Wrapper">
            <!-- Notification Start -->
            <%--<div id="top_search">
                <input type="text" name="top_search" class="top_search_input" value="اكتب كلمة البحث" />
                <input type="submit" name="top_search_submit" class="top_search_submit" value="ابحث" />
            </div>--%>
            <!-- Top Header Wrapper End -->
        </div>
        <!-- Top Header End -->
    </div>
    <section class="page-messages">
        <div class="openMessages">
                <img src="/Messages/Styles/Images/menu-btn.png">
                الرسائل
            </div>
      <div class="ps-box-messages">
             <div class="bm-header">
                  
                </div>
             <div class="bm-search">
                  
                </div>
                <div class="bm-messages">
                   
                </div>
                <%--<div class="mb-messages-more">
                    <img src="Styles/Images/icon-msg-more.png" />
                    <span>تحميل رسائل أقدم</span>
                    <img src="Styles/Images/arrow-down-black.png" />
                </div>--%>
            </div>
             <div>
        <%--<a href="#" id="newMsg">رسالة جديدة</a>--%>
        <input type="button" id="newMsg" value="رسالة جديدة" />
        </div>
            <div class="ps-box-content">
            <div class="newMessgae"> <span>الى-</span> <input type="text" value="" id="toNewMsg" toid="0" /></div>
                <div class="ps-bc-messages">
                    
                    
                </div>
                <div class="ps-bc-input" fid="0">
                    <div class="ps-bc-input-submit"></div>
                    <div class="ps-bc-input-text">
                        <span class="ps-bc-input-submit-arrow"></span>
                        <textarea name="text" id="msgtextchat" placeholder="اكتب رسالتك هنا ..."></textarea>
                    </div>
                    <div class="ps-bc-input-tools">
                        <span class="up-photo"></span>
                        <input name="up-photo-msg" type="file" hidden="hidden" multiple="multiple" />
                        <span class="emoji"></span>
                        <div class="emoji-box">
                            <div class="ps-header">
                                <span class="ps-h-option active" data-box="ps-c-box-1">م</span>
                                <span class="ps-h-option" data-box="ps-c-box-2">ب</span>
                            </div>
                            <div class="ps-content">
                                <div class="ps-c-box ps-c-box-1" style="display: block;">
                                    <span class="emoji-face" data-code="1">1</span>
                                    <span class="emoji-face" data-code="2">2</span>
                                </div>
                                <div class="ps-c-box ps-c-box-2">
                                    <span class="emoji-face" data-code="3">3</span>
                                    <span class="emoji-face" data-code="4">4</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
