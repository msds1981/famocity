<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usrMain.aspx.cs" Inherits="FamoCity.usrMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Famocity</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>

    <link href="/newfiles/css/boilerplate.css" rel="stylesheet" type="text/css" />
    <link href="/newfiles/css/MQ.css" rel="stylesheet" type="text/css" />
    <link href="/newfiles/css/index.css" rel="stylesheet" type="text/css" />
    <!-- Orignal Jquery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <%--<script src="/newfiles/js/jquery-1.10.2.js" type="text/javascript"></script>--%>
    <%--<script src="/newfiles/js/jquery-1.10.2.min.js" type="text/javascript"></script>--%>
    <!-- Notifications drop down -->
    <%--<link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/themes/base/jquery-ui.css" id="theme" />--%>
    <link href="/css/uploader/jquery-ui.1.8.13.css" rel="stylesheet" type="text/css" />
    <link href="/css/uploader/jquery.fileupload-ui.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/uploader/style.css" rel="stylesheet" type="text/css" />--%>
    <script src="/newfiles/js/notification.js" type="text/javascript"></script>
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
    <script src="/newfiles/js/SettingScript.js" type="text/javascript"></script>
    <script src="/js/Chat.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        

        $(document).ready(function () {
            //friendship page actions
            /*   $(".lioptions").html(function () {
            var img = $(this).attr("imgorg");
            $(this).html("<img src='" + img + "'/>");
            });
            $(".lioptions").mouseenter(function () {
            var imgover = $(this).attr("imgover");
            $(this).html("<img src='" + imgover + "'/>");
            });
            $(".lioptions").mouseleave(function () {
            var imgorg = $(this).attr("imgorg");
            $(this).html("<img src='" + imgorg + "'/>");
            });*/
            //$(".lioptions").mouseout();
            
        });

        function sticky_relocate() {
            var window_top = $(window).scrollTop();
            var div_top = $('#sticky-anchor').offset().top;
            if (window_top > div_top - 53) {
                $('#sticky').addClass('stick');
            } else {
                $('#sticky').removeClass('stick');
            }
        }

        $(function () {
            $(window).scroll(sticky_relocate);
            sticky_relocate();
            
        });

    </script>
    <script type="text/javascript">
        /*$(document).ready(function () {
        //alert($(window).width() + "");
        var result = $(window).width() - 640;
        //alert(result+"");
        $(".tpc").click(function () {
        $(".left_hidden_panel").show('slow').animate({
        width: "640px"
        }, 600, function () {
        });
        $(".right_panel").show('slow').animate({
        width: result + "px"
        }, 500, function () {
        $('.right_panel').masonry({
        itemSelector: '.tpc',
        isOriginLeft: false,
        isAnimated: true
        });
        });
        });

        $(".left_hidden_panel").click(function () {
        $(".left_hidden_panel").show('slow').animate({
        width: "0%"
        }, 600, function () {
        $(this).hide();
        });
        $(".right_panel").show('slow').animate({
        width: "100%"
        }, 500, function () {
        $('.right_panel').masonry({
        itemSelector: '.tpc',
        isOriginLeft: false,
        isAnimated: true
        });
        });
        });

        });
        */



    </script>
    <!-- Scrollbar -->
    <script src="/newfiles/js/jquery.mCustomScrollbar.min.js" type="text/javascript"></script>
    <script src="/newfiles/js/jquery.mousewheel.min.js" type="text/javascript"></script>
    <!-- Custom scrollbars CSS -->
    <link href="/newfiles/css/jquery.mCustomScrollbar.css" rel="stylesheet" />





     <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="/newfiles/css/boilerplate.css" rel="stylesheet" type="text/css" />
    <link href="/newfiles/css/MQ.css" rel="stylesheet" type="text/css" />
    <link href="/newfiles/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/uploader/jquery-ui.1.8.13.css" rel="stylesheet" type="text/css" />
    <link href="/css/uploader/jquery.fileupload-ui.css" rel="stylesheet" type="text/css" />
  <%--  <script src="/js/Chat.js" type="text/javascript"></script>--%>

    <link href="/css/uploader/jquery-ui.1.8.13.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnPgNo" runat="server" Value="0" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnUsrNo" runat="server" Value="0" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnlasttpcid" runat="server" Value="0" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnfirsttpcid" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnpguser" runat="server" Value="0" ClientIDMode="Static" />
    </form>
    <!-- Top Header Start -->
    <div id="logo">
        <%--<img src="/newfiles/images/logo.png" alt="Logo" title="FamoCity" />--%>
        
        <asp:Literal ID="famologo" runat="server"></asp:Literal></div>
    <div id="Notification">
        <div id="notify_list">
            <ul class="noti_ul">
                <li class="go-Store">
                    <a href="/shop">السوق</a>
                    </li>
                <li class="add_topic"><a id="button_add_topic" href="#">
                    <img src="/newfiles/images/add_topic.png" /></a></li>
                <li class="friends_noti" onclick="ChangeZero(3);">
                    <asp:Label ID="lblCount" CssClass="" runat="server" Text=""></asp:Label>
                    <%-- <span class="notired">34</span>--%>
                    <a href="#">
                        <img src="/newfiles/images/friends_icon.png" /></a>
                    <div class="drpdwn_friends listdown">
                        <div class="noti_arrow arrow_frnd">
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
                                            <div class="friend_item_pic"><a href='<%#Eval("link") %>'>
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
                <li class="alerts_noti"  onclick="ChangeZero(2)">
                <span class="notiredEvent " id="notiredEvent110" runat="server"></span> 
                <a href="#">
                    <img src="/newfiles/images/alerts_icon.png" /></a>
                    <div class="drpdwn_alerts listdown">
                        <div class="noti_arrow arrow_alert">
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
                

                 <li class="msgs_noti" onclick="ChangeZero(1);">
                 <asp:Label ID="msgcountLable" CssClass="" runat="server" Text=""></asp:Label>
                
              
                    
                     <a href="#">
                    <img src="/newfiles/images/msgs_icon.png" /></a>
                    <div class="drpdwn_msgs listdown">
                        <div class="noti_arrow arrow_msg">
                        </div>
                        <!-- Msgs Content Start -->
                        <div class="msg_container_title">
                            الرسائل الخاصة
                        </div>
                        <div class="msgs_part_frame">
                            <ul>
                                <%--<li><a href="#" class="inboxactive">(99) البريد الوارد</a></li>--%>
                                <%--<li><a href="#">(120) الرسائل الاخري</a></li>--%>
                                <li><a href="/chat/">ارسال رسالة جديدة</a></li>
                            </ul>
                        </div>
                        <div class="msg_items_con">
                            <ul>
                                <asp:Literal ID="MessageList" runat="server"></asp:Literal>
                               <%-- <li>
                                    <div class="msg_item">
                                        <div class="msg_sender_name">
                                            محمد مصطفى صلاح</div>
                                        <div class="msg_content">
                                            انا جايلك النهاردة استنانى ضرورى متنساش</div>
                                        <div class="msg_time">
                                            الثلاثاء 25 نوفمبر</div>
                                    </div>
                                    <div class="msg_item_pic">
                                        <img src="/newfiles/images/prof_pic.jpg" /></div>
                                </li>--%>
                                <%--<li>
                                    <div class="msg_item">
                                        <div class="msg_sender_name">
                                            محمد مصطفى صلاح</div>
                                        <div class="msg_content">
                                            انا جايلك النهاردة استنانى ضرورى متنساش</div>
                                        <div class="msg_time">
                                            الثلاثاء 25 نوفمبر</div>
                                    </div>
                                    <div class="msg_item_pic">
                                        <img src="/newfiles/images/prof_pic.jpg" /></div>
                                </li>
                                <li>
                                    <div class="msg_item">
                                        <div class="msg_sender_name">
                                            محمد مصطفى صلاح</div>
                                        <div class="msg_content">
                                            انا جايلك النهاردة استنانى ضرورى متنساش</div>
                                        <div class="msg_time">
                                            الثلاثاء 25 نوفمبر</div>
                                    </div>
                                    <div class="msg_item_pic">
                                        <img src="/newfiles/images/prof_pic.jpg" /></div>
                                </li>
                                <li>
                                    <div class="msg_item">
                                        <div class="msg_sender_name">
                                            محمد مصطفى صلاح</div>
                                        <div class="msg_content">
                                            انا جايلك النهاردة استنانى ضرورى متنساش</div>
                                        <div class="msg_time">
                                            الثلاثاء 25 نوفمبر</div>
                                    </div>
                                    <div class="msg_item_pic">
                                        <img src="/newfiles/images/prof_pic.jpg" /></div>
                                </li>--%>
                            </ul>
                        </div>
                        <div class="more_msgs">
                            <a href="/chat/">مشاهدة باقى الرسائل</a></div>
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
    <div class="left_hidden_panel">
        <div class="sidebarmenu-search">
            <input type="text" name="sidebar-search" value="اكتب كلمة البحث" class="sidebar-search" />
        </div>

        <!--الصورة الشخصية والاسم-->
        <asp:Literal ID="ltrTabUserPhoto" runat="server"></asp:Literal>
        
        <div class="sidebar-main-title">
            القائمة الرئيسية
        </div>

        <!-- القائمة الرئيسية -->
        <asp:Literal ID="ltrTabMainMenu" runat="server"></asp:Literal>

        <!-- Left Hidden Panel End -->
    </div>
    <!-- Div For All Content Start -->
    <div id="page_content">
        <asp:Literal ID="ltrPageContent" runat="server" ClientIDMode="Static"></asp:Literal>
        <!-- Center Header Start -->
        <!-- Navbar Start -->
        <!-- Topics Start -->
        <!-- Div For All Content End -->






    </div>
    

    <!-- Hidden Add Topic Div Start -->
    <div id="modal">
        <div class="heading">
            اضافة موضوع جديد
        </div>
        <div class="topic_content">
           <%-- <div class="temp">--%>
                <textarea id="txtTopic"> </textarea>
                <input id="show_youtube" type="text" value="ضع عنوان فيديو اليوتيوب هنا ......."
                    class="form-field-r">
            <%--</div>--%>
            <div id="fileupload">
                <div class="fileupload-content un-ui-corner-bottom un-ui-widget-content">
                    <div class="files">
                    </div>
                </div>
                <div id="filesUploaded" style="display: none">
                </div>
                <form action="/FileTransferHandler.ashx" method="post" enctype="multipart/form-data">
                <div class="fileupload-buttonbar btnandlode">
                    <button title="مسح جميع الملفات المختارة" type="button" class="delete button deleteico deleteico_m">
                    </button>
                    <label class="fileinput-button addico addico_m">
                        <span class="hide_all" title="">اضافة ملف</span>
                        <input id="btnUploadFilesTpc" type="file" name="files[]" multiple="multiple" />
                    </label>
                    <a href="javascript:void(0);" class="youtube_btn" title="مشاركة مقطع فيديو من اليوتيوب">
                    </a>
                    <div class="fileupload-progressbar" style="/*width: 546px;*/ height: 16px; border-radius: 3px;
                        margin-top: 5px; margin-right: 13px;">
                    </div>
                </div>
                </form>
                <div id="btnSendTopic" class="topic_send">
                    <input id="btnNewTopic" type="button" name="add_topic" value="مشاركة" onclick='AddTopic(document.getElementById("txtTopic"),document.getElementById("show_youtube"));'
                        class="add_tpc_submit">
                </div>
            </div>
            <script id="template-upload" type="text/x-jquery-tmpl">
                <span class="template-view template-upload{{if error}} ui-state-error{{/if}}">
                    <div class="preview preview_ico"></div>
                    <div class="name name-view">${name}</div>
                    <div class="size size-view">${sizef}</div>
                    <div class="cancel"><button>Cancel</button></div>
                    {{if error}}
                        <div class="error error-view" colspan="2">Error:
                            {{if error === 'maxFileSize'}}File is too big
                            {{else error === 'minFileSize'}}File is too small
                            {{else error === 'acceptFileTypes'}}Filetype not allowed
                            {{else error === 'maxNumberOfFiles'}}Max number of files
                            {{else}}${error}
                            {{/if}}
                        </div>
                        
                    {{else}}
                        <div class="progress progress-view"><div></div></div>
                        <div class="start"><button>Start</button></div>
                    {{/if}}
                        
                    
                </span>
            </script>
            <script id="template-download" type="text/x-jquery-tmpl">
                <span class="template-view template-download{{if error}} ui-state-error{{/if}}">
                    {{if error}}
                        <div class="preview_ico"></div>
                        <div class="name">${name}</div>
                        <div class="size">${sizef}</div>
                        <div class="error error-view" colspan="2">Error:
                            {{if error === 1}}File exceeds upload_max_filesize (php.ini directive)
                            {{else error === 2}}File exceeds MAX_FILE_SIZE (HTML form directive)
                            {{else error === 3}}File was only partially uploaded
                            {{else error === 4}}No File was uploaded
                            {{else error === 5}}Missing a temporary folder
                            {{else error === 6}}Failed to write file to disk
                            {{else error === 7}}File upload stopped by extension
                            {{else error === 'maxFileSize'}}File is too big
                            {{else error === 'minFileSize'}}File is too small
                            {{else error === 'acceptFileTypes'}}Filetype not allowed
                            {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                            {{else error === 'uploadedBytes'}}Uploaded bytes exceed file size
                            {{else error === 'emptyResult'}}Empty file upload result
                            {{else}}${error}
                            {{/if}}
                        </div>
                    {{else}}
                        <div class="preview preview_ico">
                            {{if thumbnail_url}}
                                <a href="${url}" target="_blank"><img src="${thumbnail_url}"></a>
                            {{/if}}
                        </div>
                        <div class="name">
                            <a href="${url}"{{if thumbnail_url}} title="${name}" target="_blank"{{/if}}>${name}</a>
                        </div>
                        <div class="size">${sizef}</div>
                        <div colspan="2"></div>
                    {{/if}}
                    <div class="delete">
                        <button data-type="${delete_type}" data-url="${delete_url}">Delete</button>

                    </div>
                </span>

            </script>

        
        </div>

    </div>

    <!-- New Message -->
    <div id="newChatMsg">
        <div class="heading">
            رسالة جديدة
        </div>
        <div>
        
        <input type="text" value="" id="msgReciver"  class="fsearchcss"/>
        <input type="hidden" value="0" id="msgReciverid" />
         <input type="hidden" value="0" id="myid"  runat="server"/>

        </div>
        <div class="topic_content">
            <textarea id="msgtextchat"> </textarea>
                
            
            <div id="Div1" class="topic_send">
                <input id="btnSendChatMsg" type="button" name="add_topic" value="إرسال" onclick='sendmessagepopup()'
                    class="add_tpc_submit">    
            </div> 
        </div>
    </div>
    <!-- End New Message -->
    <!-- POP UP VIEW START-->
    <div class="popup">
        <div class='tpc-view'>
            <div id="pnlTopic" class="tpc-view-head">
                <a href="javascript:void(0);" onclick="ClosePopup(&quot;#pnlTopic&quot;);">
                    <img class="closepopup" src="/newfiles/images/close.gif"></a></div>
            <!-- content load from server side -->
        </div>
    </div>
    

    <!-- move photo from album to another -->
      <div id="moveimage">
        <div class='moving_content'>
            <div>نقل صورة الى البوم اخر</div>
            <div id="moving_list">
                <select id="albumlist">
                </select>
            </div>
            <div class="movingbuttons">
                <input type="hidden" id="hdnfileId" value="0" />
                <input type="button" id="movetheimage" value="نقل" class="moving_buttons" onclick="MoveToAlbum();"/>
                <input type="button" id="cancelmoveimage" value="الغاء"  class="moving_buttons" onclick="CloseMovingAlbum();"/>
            </div>
        </div>
    </div>


    <!--jQuery-->
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>--%>
    <!-- uploader -->
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/jquery-ui.min.js"></script>
    <%--<script src="//ajax.aspnetcdn.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>--%>
    <script src="/js/uploader/jquery.tmpl.min.js" type="text/javascript"></script>
    <script src="/js/uploader/jquery.iframe-transport.js"></script>
    <script src="/js/uploader/jquery.fileupload.js"></script>
    <script src="/js/uploader/jquery.fileupload-ui.js"></script>
    <script src="/js/uploader/application.js"></script>
    <script src="/newfiles/js/jquery.add_topic.js"></script>
    <script src="/newfiles/js/masonry.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#button_add_topic').click(function (e) { // Button which will activate our modal
                OpenPost();
            });
            $('#new-photo').live('click', function (e) { // Add new photo
                OpenPost();
            });
            function OpenPost() {
                RemoveFilesOfUploader();
                $('#modal').reveal({ // The item which will be opened with reveal
                    animation: 'fade',                   // fade, fadeAndPop, none
                    animationspeed: 600,                       // how fast animtions are
                    closeonbackgroundclick: true,              // if you click background will modal close?
                    dismissmodalclass: 'close'    // the class of a button or element that will close an open modal
                });

                return false;
            }


            $('.sep-icon-2ico').live("click", function (e) {

                $('#newChatMsg').reveal({
                    animation: 'fade',
                    animationspeed: 600,
                    closeonbackgroundclick: true,
                    dismissmodalclass: 'close'
                });

                return false;
            });


            $('.imgmoving').live("click", function (e) { // Button which will activate our modal
                $('#moveimage').reveal({
                    animation: 'fade',
                    animationspeed: 600,
                    closeonbackgroundclick: true,
                    dismissmodalclass: 'close'
                });
                $("#hdnfileId").val($(this).attr("fileid"));
                $(".drpdwn_photos").css("display", "none");
                return false;
            });







            $('#lnkChangePohto').click(function (e) { // 
                $('#photouser').reveal({ // The item which will be opened with reveal
                    animation: 'fade',                   // fade, fadeAndPop, none
                    animationspeed: 600,                       // how fast animtions are
                    closeonbackgroundclick: true,              // if you click background will modal close?
                    dismissmodalclass: 'close'    // the class of a button or element that will close an open modal
                });
                return false;
            });

            /*jQuery('#btnUploadFilesTpc').each(function (index) {
            var uploader1 = new qq.FileUploader({
            onComplete: function (id, filename, responseJSON) {
            alert('test');
            }
            });
            });*/
            //            $(".closepopup").click(function () {
            //                $('#pnlTopic').trigger('reveal:close');
            //            });

            /*$('.tpc').click(function (e) { // Button which will activate our modal

            $('.popup').reveal({ // The item which will be opened with reveal
            animation: 'fade',                   // fade, fadeAndPop, none
            animationspeed: 600,                       // how fast animtions are
            closeonbackgroundclick: true,              // if you click background will modal close?
            dismissmodalclass: 'close'    // the class of a button or element that will close an open modal
            });

            return false;
            });*/
            $("#lnkmasonry").click(function () {
                $('.right_panel').masonry({
                    itemSelector: '.tpc',
                    isOriginLeft: false
                });
            });
        });

        $(function () {
            $('.right_panel').masonry({
                itemSelector: '.tpc',
                isOriginLeft: false
            });
            //var $boxes = $('<div class="box"/><div class="box"/><div class="box"/>');
            //$('.right_panel').append($boxes).masonry('appended', $boxes);

        });
//        function MessagePopup() {
//            $('#newChatMsg').reveal({
//                animation: 'fade',                  
//                animationspeed: 600,                      
//                closeonbackgroundclick: true,              
//                dismissmodalclass: 'close'   
//            });
//        }

    </script>
    <script src="/newfiles/js/jquery.mCustomScrollbar.concat.min.js"></script>
    <%--<script>
        (function ($) {
            $(window).load(function () {
                $("#content_1").mCustomScrollbar({
                    autoHideScrollbar: true,
                    theme: "dark-thin"
                });
            });
        })(jQuery);
    </script>--%>
    


</body>
</html>
