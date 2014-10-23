//
var imgLoadSmall = "/newfiles/images/load-small.gif";
var imgLoaderAjax = "/images/ajax-loader2.gif";
var imgTagLoadSmall = "<img class='ImgLoadSmall' src='" + imgLoadSmall + "'/>";
var imgTagLoadAjax = "<img src='/images/ajax-loader2.gif'/>";
var imgTagLoadAjaxCenter = "<img src='/images/ajax-loader2.gif' class='content-insider imgloadercenter'/>";
var tempvar = "";
var commInterval; //setInterval for comments
var keepContent; //btnSendTopic keep content temprary for use later -- AddTopic function
var timeoutMasnory;
var timeoutNewTopics;
var timeoutFirstId;
var clrTimer = 0;





/*******************************************************************************/
/*   we need event to detect width changing for desktop, tablet and mobile     */
/*******************************************************************************/







$(document).ready(function () {
    $.ajaxSetup({ type: 'POST', dataType: 'json', contentType: 'application/json', data: {} });
    //show/hide youtube input share 
    $(".youtube_btn").click(function () {
        $("#show_youtube").toggle();
    });


    $(".maxevent").click(function () {


        CallEventDetails($(".maxevent").attr("ids"));
    });

    //PreparePage(1);









    //    $('.search').live('keyup.autocomplete', function () {
    //        $(this).autocomplete({
    //            source: 'url.php'
    //        });
    //    });


    // AutoCompleate For any Searching
    /*  $(function () {

    $("#fsearch").live('keyup', function () {
    $(this).autocomplete({
    source: function (request, response) {
    $.ajax({
    url: "/Methods.aspx/AutoComplete",
    data: "{ 'mail': '" + request.term + "' }",
    dataType: "json",
    type: "POST",
    contentType: "application/json; charset=utf-8",
    dataFilter: function (data) { return data; },
    success: function (data) {
    response($.map(data.d, function (item) {
    //alert(item.Name);
    return {
    value: item.ID,
    label: item.Name,
    imgsrc: item.PhotoPath,
    countr: item.Country,
    Link:item.Link
    }
    })) //ajax
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
    //alert(textStatus);
    }
    }); //source
    },
    select: function (event, ui) {
    //alert(ui.item.value + " , " + ui.item.label + " " + ui.item.name);

    },
    minLength: 3
    })
    .data("autocomplete")._renderItem = function (ul, item) {
    //alert(item.imgsrc+"  img");
    return $("<li class='ssss'></li>")
    .data("item.autocomplete", item)
    .append("<img src='" + item.imgsrc + "' />")
    .append("<span id='" + item.value + "' class ='m3rad_info' style='direction:rtl' >" +item.label + "</span>")
    .appendTo(ul);
    };


    ; //autocomplete
    }); //live
    }); //function
    */





    $(function () {



        $("#fsearch").live('keyup', function () {
            // var h = $(".red-active").parent();
            // alert($(this).val());
            var are = $(".red-active").attr("area");
            // alert(are);
            //if ($(this).val().length < 4) return;
           // $("#friends_request_blocks").html(imgTagLoadAjaxCenter);
            var mnubarloader = $("#loader-mnubar");
            mnubarloader.html(imgTagLoadAjax);
            $.ajax({
                url: "/Methods.aspx/SearchFriendsRelation",
                data: "{ 'mail': '" + $(this).val() + "','area':'" + are + "','lastid':'0' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //alert(data.d);
                    mnubarloader.html("");
                    //$("#friends_request_blocks").html("");
                    $("#friends_request_blocks").html(data.d);

                }
            }); //ajax

        }); //live
    }); //function







    //        alert("d");
    //        $("#fsearch").live('keyup.autocomplete', function () {
    //            $(this).autocomplete({
    //                source: function (request, response) {
    //                   
    //                },
    //                minLength: 2
    //            }); // autocomplete
    //        }); //live




});

//function PreparePage(pg) {
//    alert(pg);
//}


//التمهيد لعرض الصفحة المطلوبه
function PreparePage(userid, pg) {
    //$(".gridContainer:last").next().remove();
    //$("#hdnPgNo").val(pg);
    //$(".gridContainer:last").after(pg);

    if (pg == 1)    //Topics
        LoadTopicsArea(userid);
    else if (pg == 2)   //Albums
        LoadAlbumsArea(userid);
    else if (pg == 3)   //Friends
        LoadFriendsArea(userid);
    else if (pg == 6)   //settings
        LoadSettingArea(userid);
    else if (pg == 4)   //my shops
        LoadShopsArea(userid);

    //current user's page
    $("#hdnpguser").val(userid);
}

$(function () {
    SwitchTopicIntervals(); //turn on intervals
    //alert("ready");
    //getCountOfNewTopics();

    //getCountOfNewTopics

    //get first topic_id by looking every 5 sec.
    //timeoutFirstId = setTimeout(getFirstTopicID, 5000);      /***  work only on desktop  ***/

    timeoutNewTopics = setInterval(getCountOfNewTopics, 10000); //load newest topics     /***  work only on desktop  ***/
    timeoutMasnory = setTimeout(PrepareMasnory, 3000); //resort tpoics one time
    timeoutNewTopics = setInterval(LoadNewstNotification, 10000);
    // timeoutNewTopics = setInterval(NotReadingCount, 10000);
    timeoutNewTopics = setInterval(GetNotiCount, 10000);


});



function SwitchTopicIntervals() {
    //تشغيل العدادات المسئولة عن قراءة عدد التعليقات والاعجاب وعدم الاعجاب
    setInterval(RefreshCommentsCount, 5000); //refresh the count of comments box
    setInterval(RefreshLikesCount, 5000); //refresh the count of likes box
    setInterval(RefreshUnLikesCount, 5000); //refresh the count of unlikes box

}

//call function of new comment
function onKeyPressNewComment(e, text, topid) {
    // alert(text);

    var key = window.event ? e.keyCode : e.which;
    if (key == 13) {
        //alert(text.value);
        AddComment(text, topid);
        return false;
    }
    else
        return true;

}

//تحميل صفحة شخص بالكامل
function LoadPage(uid) {

    $(".content-insider").remove(); //حذف المحتوى تمهيدا لإضافة محتوى جديد
    $(".gridContainer:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: "",
        data: "{'userid':'" + uid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var p = new Array();
                $(".content-insider").remove(); //remove loader
                p = data.d;
                $(".gridContainer:last").after(p[2]);
                //user's page url
                window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1]);

            }
        }
    });
}

//تحميل محتويات الصفحات في الصفحة الرئيسية
//albums,shops and settings
function LoadAreas(method, param) {

    $(".content-insider").remove(); //حذف المحتوى تمهيدا لإضافة محتوى جديد
    $(".gridContainer:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: method,
        data: param,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var p = new Array();
                $(".content-insider").remove(); //remove loader
                p = data.d;
                $(".gridContainer:last").after(p[2]);
                window.history.pushState("string", "Title", p[1]);
            }
        }
    });
}

//تحميل محتويات المقالات في الصفحة
function LoadTopicsArea(userid) {

    $(".content-insider").remove(); //حذف المحتوى تمهيدا لإضافة محتوى جديد
    $(".gridContainer:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LoadTopicsArea",
        data: "{'userid':'" + userid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $(".content-insider").remove(); //remove loader
            if (data.d != "") {
                var p = new Array();
                p = data.d;

                //render the topics area after statistic bar
                $(".gridContainer:last").after(p[2]);

                //find first topic id for looking about the newest topics 
                $("#hdnfirsttpcid").val(getFirstTopicID());


                //put URL of user's page 
                window.history.pushState("string", "Title", p[1]);


                PrepareMasnory();
                setTimeout(PrepareMasnory, 4000); //resort tpoics one time
                //$(".right_panel").imagesLoaded(function () {
                //                    
                //});

                //alert("usrmain.aspx?uid=" + p[0] + "&pg=" + p[1]);
                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1]);

            }
        },
        error: function () {
            $(".content-insider").remove();
        }
    });
}

//تحميل محتويات البومات الصور في الصفحة
function LoadAlbumsArea(userid) {
    //  alert("LoadAlbumsArea");
    LoadAreas("/Methods.aspx/LoadPhotosArea", "{'userid':'" + userid + "'}");
}

//تحميل محتويات الاعدادات في الصفحة
function LoadSettingArea(userid) {
    LoadAreas("/Methods.aspx/LoadSettingArea", "{'userid':'" + userid + "'}");
    //$('#fileupload2').fileupload();
}

//تحميل محتويات متاجري في الصفحة
function LoadShopsArea(userid) {
    LoadAreas("/Methods.aspx/LoadShopArea", "{'userid':'" + userid + "'}");
}

//تحميل محتويات اصدقائي في الصفحة
function LoadFriendsArea(userid) {
    //if there is no topics tags then exit

    $(".content-insider").remove(); //حذف المحتوى تمهيدا لإضافة محتوى جديد
    $(".gridContainer:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LoadFriendsArea",
        data: "{'userid':'" + userid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var p = new Array();
                p = data.d;
                $(".content-insider").remove(); //remove loader
                $(".gridContainer:last").after(p[3]);
                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1] + "&tab=" + p[2]);
                window.history.pushState("string", "Title", p[1]);
            }
        }
    });
}

/***********************************************/
//دوال تحديث عدد التعليقات والاعجاب وعدم الاعجاب 
//محتاجه الى ان تتوحد جميع الدوال في دالة واحدة
/***********************************************/

function RefreshCommentsCount() {
    //this function called from setInterval for refresh counters of comments
    $(".commcounts").each(function (index) {
        var cuurval = $(this).html(); //current value
        var currid = $(this).attr("id");
        var topid = $(this).attr("tpcid");
        $.ajax({
            type: "POST",
            url: "/Methods.aspx/getCountCommentsOfTopic",
            data: "{'topid':'" + topid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d != "") {
                    if (cuurval != data.d) {
                        $("#" + currid).html(data.d);
                        $("#" + currid).parent().parent().fadeTo(300, 0.1).fadeTo(500, 1.0);
                    }
                }
            }
        });
    });
}
function RefreshLikesCount() {
    //this function called from setInterval for refresh counters of likes
    //alert("RefreshLikesCount");
    $(".likesCount").each(function (index) {
        var cuurval = $(this).html(); //current value
        var currid = $(this).attr("id");
        var topid = $(this).attr("tpcid");
        //alert(topid);
        $.ajax({
            type: "POST",
            url: "/Methods.aspx/GetLikesCountOfTopic",
            data: "{'objid':'" + topid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //alert(data.d + "");
                if (data.d != "") {
                    //alert(cuurval + "");
                    if (cuurval != data.d) {
                        $("#" + currid).html(data.d);
                        $("#" + currid).parent().parent().fadeTo(300, 0.1).fadeTo(500, 1.0);
                    }
                }
            }
        });
    });
}
function RefreshUnLikesCount() {
    //this function called from setInterval for refresh counters of likes
    //alert("RefreshLikesCount");
    $(".unlikesCount").each(function (index) {
        var cuurval = $(this).html(); //current value
        var currid = $(this).attr("id");
        var topid = $(this).attr("tpcid");
        //alert(topid);
        $.ajax({
            type: "POST",
            url: "/Methods.aspx/GetUnLikesCountOfTopic",
            data: "{'objid':'" + topid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //alert(data.d + "");
                if (data.d != "") {

                    if (cuurval != data.d) {
                        $("#" + currid).html(data.d);
                        $("#" + currid).parent().parent().fadeTo(300, 0.1).fadeTo(500, 1.0);
                    }
                }
            }
        });
    });
}

function LoadMoreComments(divsee, topid) {
    //called from link tage 'seemode'
    tempvar = $(divsee).html();
    var lastid = $(".tpc-comment-view-box").eq(-2).attr("comid"); //last comment
    $(divsee).html(imgTagLoadSmall);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/SeeMoreComments",
        data: "{'topid':'" + topid + "','lastid':'" + lastid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                $(divsee).parent().before(data.d);
                $(divsee).html(tempvar);
            } else {
                $(divsee).html("");
            }
        }
    });

}

function LoadNewestComments() {
    //this function called from interval in DisplayTopic()
    var first = $(".tpc-comment-view-box:first");
    var firstid = $(first).attr("comid"); //first comment
    var topid = $(first).attr("tpcid");
    //alert("topid=" + topid + ",commid=" + firstid);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LoadNewestComments",
        data: "{'topid':'" + topid + "','firstid':'" + firstid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                $(first).html(data.d);
            }
        }
    });

}

/***********************************************/
//                   topics
/***********************************************/

var currentTopicsCount = 0; //the value that will appear in button refresh to load the newest topics
var firstid = 0; //the biggest topic id in page
var lastid = 0; //the smallest topic id in page

//looking for new topics
//اظهار عدد المشاركات الجديدة على زر التحديث في اول الصفحه
function getCountOfNewTopics() {
    //first topics id from hidden value
    var firsttpcid = $("#hdnfirsttpcid").val();

    $.ajax({
        type: "POST",
        url: "/Methods.aspx/GetCountOfNewTopics",
        data: "{'firstid':'" + firsttpcid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var p = new Array();
                p = data.d;
                if (currentTopicsCount != p[0]) {
                    currentTopicsCount = p[0];
                    //alert(p[0] + "");
                    $("#new_posts").html(p[1]);
                }
                else if (p[0] == "0") {
                    //clear counter
                    $("#new_posts").html("");
                }
            }
        }
    });
}

/*function getFirstTopicID() {
alert("");
var biggestid = 0;
var currid = 0;
$(".tpc").each(function (index) {
currid = $(this).attr("tpcid");
//alert("currntid = " + currid.toString() + "  biggestid = " + biggestid.toString());
alert(currid + "");
if (currid > biggestid) biggestid = currid;
});
return biggestid;
}*/

function getFirstTopicID() {
    var biggestid = 0;
    var currid = 0;
    $(".tpc").each(function (index) {
        currid = $(this).attr("tpcid");
        if (parseInt(currid) > parseInt(biggestid)) biggestid = parseInt(currid);
    });
    //alert("firstid = " + biggestid);
    firstid = biggestid;
    return biggestid; //  biggestid;
}
function getLastTopicID() {
    var smallestid = 1;
    var currid = 0;
    $(".tpc").each(function (index) {
        currid = $(this).attr("tpcid");
        if (parseInt(currid) < parseInt(smallestid)) smallestid = parseInt(currid);
    });
    alert("lastid = " + smallestid);
    return smallestid; //  biggestid;
}

//load newest topics
/*function LoadNewTopicsForUser() {
//.tpc
$.ajax({
type: "POST",
url: "/Methods.aspx/LoadNewestTopicsOfUser",
data: "{'userid':'" + topid + "','firstid':'" + firstid + "'}",
contentType: "application/json; charset=utf-8",
dataType: "json",
success: function (data) {
if (data.d != "") {
$(first).html(data.d);
}
}
});
}*/

////display newest topics 
function LoadNewTopics() {
    //اظهار المقالات الحديثه بعد الضغط على رز التحديث

    //alert("looking for new topics");
    var first = $(".tpc:first"); //$(".tpc:first");
    var hdnfirstid = $("#hdnfirsttpcid"); //first topic id
    var pguid = $(first).attr("pguid"); //user ID of page
    $(".new_posts").html(imgTagLoadAjax);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LoadNewestTopicsOfMyWall",
        data: "{'firstid':'" + hdnfirstid.val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                //$(first).before(data.d);
                //PrepareMasnory();
                //$('.right_panel').masonry('reload');

                var p = new Array();
                p = data.d;

                //set new first topic id
                hdnfirstid.val(p[0]);

                //$(".tpc:first").before(p[1]);
                var $boxes = $(p[1]);
                $('.right_panel').append($boxes).masonry('appended', $boxes);

                PrepareMasnory();
                //setTimeout(PrepareMasnory, 4000); //resort tpoics one time
                $(".mosharka").remove();

            }
        }
    });
}

//see more topics
function SeeMoreTopics(lastid) {
    var seemoreon = $(".load_more_bar").html();

    $(".load_more_bar").html(imgTagLoadAjax); //show loader
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LoadMoreTopics",
        data: "{'lastid':'" + lastid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                //$(first).before(data.d);
                //PrepareMasnory();
                //$('.right_panel').masonry('reload');

                var p = new Array();
                p = data.d;
                //alert(p[0]);

                //keep the last topic id to load more topics
                $("#hdnlasttpcid").val(p[0]);

                var lastid = "SeeMoreTopics(" + p[0] + ")";
                var l = $('.right_panel:last');
                //l.after(p[1]);
                //PrepareMasnory();
                var $boxes = $(p[1]);
                $('.right_panel').append($boxes).masonry('appended', $boxes);
                setTimeout(PrepareMasnory, 4000); //resort tpoics one time
                //remove seemore button if there is more old topics 
                if (p[2] != "10")
                    $(".load_more_bar").remove();

                //reset see more loader
                $(".load_more_bar").html("<a href='javascript:void(0);' class='' onclick='" + lastid + "'> للمزيد </a>");

            }
        },
        error: function () {
            //reset see more loader
            $(tagseemore).html(seemoreon);
        }
    });
}
//display topic in popup
function DisplayTopic(id) {
    //$(".popup").html("<div class='tpc-view'><div class='tpc-view-head'>Head</div></div>");

    /*    $.post("ashx/hdlSocial.ashx?action=loadtopic&topid=" + id,
    function (data) {
    if (data != null) {
    //alert("finished retrive topic");
    //$(".popup").html(data);
    }
    });*/
    //alert(id + "");

    //$("#pnlTopic").html(imgTagLoadAjax);
    $("#popup_left").remove();
    $("#content_1").remove();
    //اعادة بناء البوب اب  من السيرفر عند الضغط على المقالة
    $('.popup').reveal({ // The item which will be opened with reveal
        animation: 'fade',                   // fade, fadeAndPop, none
        animationspeed: 600,                       // how fast animtions are
        closeonbackgroundclick: true,              // if you click background will modal close?
        dismissmodalclass: 'close'    // the class of a button or element that will close an open modal
    });
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/GetTopic",
        data: "{'topid':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                $("#pnlTopic").after(data.d);

                clearInterval(LoadNewestComments); //first clear any other interval called LoadNewestComments
                setInterval(LoadNewestComments, 5000); //get newest comments

                PrepareScroll();
            }
        }
    });



}

function PrepareScroll() {
    $("#content_1").mCustomScrollbar({
        autoHideScrollbar: true,
        theme: "dark-thin"
    });
}

function PrepareMasnory() {
    //alert("PrepareMasnory");
    $('.right_panel').masonry({
        itemSelector: '.tpc',
        isOriginLeft: false
    });
    clrTimer += 1;
    //clear interval
    if (clrTimer == 3) clearTimeout(timeoutMasnory);
}
function PrepareFriendPage() {
    //moved this code from ready()
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
}

//تحميل صفحة المستخدم
/*function LoadPage(objcode, objid, pageno, tab) {
// alert(hash); //
// document.location.hash(hash);
//window.location.url = '&page=' + pageno;
$.ajax({
type: "POST",
url: "/Methods.aspx/LoadPage",
data: "{'objcode':'" + objcode + "','objid':'" + objid + "','page':'" + pageno + "','tab':'" + tab + "'}",
contentType: "application/json; charset=utf-8",
dataType: "json",
success: function (data) {
if (data.d != "") {
if (data.d != "error") {
$("#page_content").html(data.d);
//window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1]);
window.history.pushState("string", "Title", "person/" + p[0] + "/" + p[1]);
PrepareMasnory();
//if (pageno == 3) PrepareFriendPage();
}
}
}
});

}*/

//Hisham Change loadpage function

//تحميل صفحة المستخدم بالكامل
function LoadPage(userid, pageno, tab) {
    // alert(hash); //
    // document.location.hash(hash);
    //window.location.url = '&page=' + pageno;
    var pguser = $("#hdnpguser").val();
    if (userid == pguser)
    //عرض صفحتي الشخصية
        PreparePage(userid, pageno);
    else
    //عرض صحفة شخص آخر
        LoadWholePage(userid, pageno, tab);

    $("#hdnpguser").val(userid);
}

function LoadWholePage(userid, pgno, tab) {
    $(".content-insider").remove(); //حذف المحتوى تمهيدا لإضافة محتوى جديد
    $(".gridContainer:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LoadPage",
        data: "{'userid':'" + userid + "','page':'" + pgno + "','tab':'" + tab + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "" && data.d != null) {
                var p = new Array();
                p = data.d;
                $(".content-insider").remove(); //remove loader

                $("#page_content").html(p[2]);
                //                    $("#page_content").html(data.d);
                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1]);
                window.history.pushState("string", "Title", "/person/" + p[0] + "/" + p[1]);
                //var stateObj = { foo: "bar" };
                //window.history.replaceState("string", "Title", "person/" + p[0] + "/" + p[1]);
                PrepareMasnory();
                //if (pageno == 3) PrepareFriendPage();
            }
        }
    });
}




function LoadFriends(userid, area, actv) {
   
    var mnubarloader = $("#loader-mnubar"); //loader for menu bar (friend bar)
    mnubarloader.html(imgTagLoadAjax);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LoadFriendBlocks",
        data: "{'userid':'" + userid + "','friendarea':'" + area + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            
            if (data.d != "") {
                if (data.d != "error") {
                    $(".red-active").removeClass("red-active");
                    $(actv).addClass("red-active");
                    $("#friends_request_blocks").html(data.d);
                    mnubarloader.html("");
                    //PrepareFriendPage();
                }
            }
        }
    });
}

//load my shops, other shops and products of activity chosen 
function LoadContentActv(userid, actvid) {
    $(".content-shop").remove(); //remove my shops, other shops and products
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LoadShopsArea",
        data: "{'userid':'" + userid + "','actvid':'" + actvid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                if (data.d != "error") {
                    $("#shop_blocks").after(data.d);
                }
            }
        }
    });
}

//see more shops
function SeeMoreShops(divshop) {
    var lastLi = $("#" + divshop + ">li:last-child");
    var shopid = $(lastLi).attr("shpid");
    var actv = $(lastLi).attr("actvid");
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/GetShops",
        data: "{'activ':'" + actv + "','lastid':'" + shopid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                $(lastLi).after(data.d);
            }
            else {
                $("." + divshop).remove();
            }
        }
    });
}


/**********************************************/
/************ Like Unlike comments ************/
/**********************************************/

//Add New Comment
function AddComment(text, objid) {
    if (text.value == "") {
        return false;
    }
    $(text).before(imgTagLoadSmall); //show image loading
    $(text).attr("disabled", true);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/SaveComment",
        data: "{'objid':'" + objid + "','desc':'" + text.value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                $(text).attr("disabled", false);
                $(".ImgLoadSmall").remove();
                text.value = "";
            }
        }
    });
}

//add like
function AddLike(objid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LikeTopic",
        data: "{'objid':'" + objid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                //delete onclick event from <a> tag
                $("#AddLike_" + objid).attr("onclick", "");
                $("#AddLike_" + objid).removeClass("cursor");
                $("#AddUnLike_" + objid).attr("onclick", "AddUnLike(" + objid + ")");
            }
        }
    });
}

// close/open comment on topic
function CommentStatus(top) {
    //alert("" + $(top).attr("tpcid"));
    var tid = $(top).attr("tpcid");
    var opt = $("#nocomopt_" + tid);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/NoCommentTopic",
        data: "{'topid':'" + tid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                var p = new Array();
                p = data.d;
                if (p[0] == "0") {
                    $(top).after(p[1]);
                    opt.html("منع التعليقات");
                    PrepareMasnory();
                }
                else if (p[0] == "1") {
                    $(top).parent().children(".tpc-comm").remove();
                    opt.html("السماح بالتعليقات");
                }
            }
        }
    });
}

//un lilke
function AddUnLike(objid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/UnLikeTopic",
        data: "{'objid':'" + objid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                //delete onclick event from <a> tag
                $("#AddUnLike_" + objid).attr("onclick", "");
                $("#AddUnLike_" + objid).removeClass("cursor");
                //add new onclick to <a>
                $("#AddLike_" + objid).attr("onclick", "AddLike(" + objid + ")");
            }
        }
    });
}

// delete favorite
function UnFavotite(id) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/Unfavorite",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}

function AddFavotite(Tobjid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/FavoriteProduct",
        data: "{'objid':'" + Tobjid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}

//Likes Count of topic
function getLikes(objcode, objid) {

}

//Likes Count of Inlike
function getUnLikes(objcode, objid) {

}

//عدد الغير معجبين
function getUsersUnLikes(objcode, objid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/GetUserUnLikesCount",
        data: "{'objcode':'" + objcode + "','objid':'" + objid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}

//عدد المعجبين 
function getUsersLikes(objcode, objid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/NamesUsersOfLikes",
        data: "{'objcode':'" + objcode + "','objid':'" + objid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {

                alert(data.d + "");
            }
        }
    });
}

//عدد المستخدمين الذين علقوا 
function getUsersComments(objcode, objid) {

    $.ajax({
        type: "POST",
        url: "/Methods.aspx/getNumbersOfComments",
        data: "{'objcode':'" + objcode + "','objid':'" + objid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}

//عرض اسماء المعلقين
function getNamesUsersOfComments(objcode, objid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/NamesUsersOfComments",
        data: "{'objcode':'" + objcode + "','objid':'" + objid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}
// تتبع المنتج 
function FollowShop(Shopid, Userid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/FollowShop",
        data: "{'Shopid':'" + Shopid + "','Userid':'" + Userid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}

// عدم تتبع المنتج
function UnFollowShop(Shopid, Userid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/UnFollowShop",
        data: "{'Shopid':'" + Shopid + "','Userid':'" + Userid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}
// عدد  المتابعين للمنتج
function NumberFollowShop(Shopid, status) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/GetNumbersFollow",
        data: "{'Shopid':'" + Shopid + "','status':'" + status + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}

// ************************ Friendship functions ************************* //
// *********************************************************************** //
function FriendshipAction(divid, lnkid, nextaction, user) {
    //$("#frndboxopt_" + lnkid).html("");
    //alert(divid);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/FriendshipNextAction",
        data: "{'linkid':'" + lnkid + "','nextaction':'" + nextaction + "','user':'" + user + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                //if (nextaction == 5) //in case unfriend
                $(divid).fadeOut("slow");
                //alert("#frndboxopt_" + lnkid);
                //alert(data.d);
                //$("#frndboxopt_" + lnkid).html(data.d);
                //PrepareFriendPage();
            }
        }
    });
}

// اضافة صديق
function AddFriend(user, touser) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/LinkFriend",
        data: "{'userid':'" + user + "','touserid':'" + touser + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}

// قبول صديق
function AcceptFriendShip(link) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/AcceptFrindShip",
        data: "{'linkid':'" + link + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data.d > 0) {
                $('#liRequest_' + link).hide('slow');
                $('#liRequest_' + link).remove();
            }
        }

    });
}
// رفض صديق صديق
//unfriend
function NotAllowRequest(link) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/NotAllowRequest",
        data: "{'linkid':'" + link + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                $('#liRequest_' + link).hide('slow');
            }
        }
    });
}



// رفص طلب الصداقة-المرسل
function UnLinkFriend(link) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/UnLinkFriend",
        data: "{'linkid':'" + link + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}
// الغاء طلب الصداقة - المستقبل
function CancelRequest(link) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/CancelRequest",
        data: "{'linkid':'" + link + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}
//حالة الصديق
function StatusFriendShip(user, frind) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/GetFriendShip",
        data: "{'userid':'" + user + "','frindid':'" + frind + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                alert(data.d + "");
            }
        }
    });
}

function GetCountOfRequest(user) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/GetCountOfRequest",
        data: "{'userid':'" + user + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data.d > 0) {
                return data.d;
            }
        }
    });
}
//add new topic , fire on btnNewTopic clicked button
function AddTopic(text, videourl) {
    //validation
    var c = $(".files").find(".template-download").length; //count of files uploaded
    if (c == 0 && text.value == "" && videourl.value == "") {
        alert("اكتب مشاركة او ارفع صورة");
        return false;
    }
    var x = $("#hdnUsrNo").attr("value");
    // alert(x);

    var btnSend = $("#btnSendTopic");
    keepContent = btnSend.html();
    //alert(keepContent);
    btnSend.html(imgTagLoadAjax);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/SaveTopic",
        data: "{'description':'" + text.value + "','url':'" + videourl.value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                var p = new Array();
                p = data.d;

                btnSend.html(keepContent);
                RemoveFilesOfUploader();
                text.value = "";
                ClosePopup("#modal");

                //PreparePage(x, 1);
                var $boxes = $(p[1]);
                $('.right_panel').append($boxes).masonry('appended', $boxes);
                PrepareMasnory();
            }
            else {
                btnSend.html(keepContent);
            }
        }
    });
}

function DeleteTopic(tpc) {
    var tpid = $(tpc).attr("tpcid");
    if (confirm('هل انت متأكد من حذف المشاركة؟')) {
        $.ajax({
            type: "POST",
            url: "/Methods.aspx/DeleteTopic",
            data: "{'topid':'" + tpid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d > 0) {
                    $('.right_panel').masonry('remove', $(tpc));
                }
            }
        });
    }
}


function OnUploadStart() {
    var btnSend = $("#btnSendTopic");
    keepContent = btnSend.html();
    btnSend.html(imgTagLoadAjax);
}

function OnUploadStop() {
    // alert("stop from social.js");
}

function OnUploadFail() {
    //alert("fail from social.js");
}

function OnUploadDone() {
    $("#btnSendTopic").html(keepContent);
}

//close any popup
function ClosePopup(popupid) {
    $(popupid).trigger('reveal:close');
}
//حذف كل الملفات التي تم رفعها 
function RemoveFilesOfUploader() {
    $("#fileupload").find(".files").empty();
}

function RenewHeaderImages() {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/RandomizeHeaderImages",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                $(".galleryContainer").html(data.d);
            }
        }
    });
}


//=============================Notifications Hisham=================================
//تحميل محتويات اصدقائي في الصفحة
function LoadOldestNotification() {
    //if there is no topics tags then exit

    text = getMainNotificationID
    $(".alert_items_con li").remove(); //حذف المحتوى تمهيدا لإضافة محتوى جديد
    $(".alert_items_con  li:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/NewestNotification",
        data: "{'eventid':'" + text + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var p = new Array();
                p = data.d;
                // $(".content-insider").remove(); //remove loader
                $(".alert_items_con li:last").after(p);
                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1] + "&tab=" + p[2]);
                // window.history.pushState("string", "Title", p[1]);
            }
        }
    });
}


function LoadNewstNotification() {
    //if there is no topics tags then exit
    text1 = getMaxNotificationID();
    //   $(".alert_items_con li").remove(); //حذف المحتوى تمهيدا لإضافة محتوى جديد
    //$(".alert_items_con  li:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/NewestNotification",
        data: "{'eventid':'" + text1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data.d != "") {

                var p = new Array();

                p = data.d;
                // $(".content-insider").remove(); //remove loader
                $(".alert_items_con li:first").before(p);
                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1] + "&tab=" + p[2]);
                // window.history.pushState("string", "Title", p[1]);
            }
        }
    });

    //alert("maxval=" + getMaxNotificationID());
    // alert("minval=" + getMainNotificationID());
}


function NotReadingCount() {
    //if there is no topics tags then exit
    text1 = getMainNotificationID();
    //   $(".alert_items_con li").remove(); //حذف المحتوى تمهيدا لإضافة محتوى جديد
    //$(".alert_items_con  li:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/NotReadingEvents",
        data: "{'eventid':'" + text1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data.d != "") {

                var p = new Array();

                p = data.d;
                // $(".content-insider").remove(); //remove loader
                if (parseInt(p) > 0) {
                    $(".notiredEvent").first().addClass("notired");
                    $(".notiredEvent").html(p);
                }
                //html(p);
                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1] + "&tab=" + p[2]);
                // window.history.pushState("string", "Title", p[1]);
            }
        }
    });

    //alert("maxval=" + getMaxNotificationID());
    // alert("minval=" + getMainNotificationID());
}

function CallEventDetails(ids) {
    // alert(ids);

}

function getMaxNotificationID() {
    var biggestid = 0;
    var currid = 0;
    $(".alert_items_con ul li").each(function (index) {
        currid = $(this).attr("max");

        if (parseInt(currid) > parseInt(biggestid)) biggestid = parseInt(currid);
    });
    //alert("firstid = " + biggestid);
    // firstid = biggestid;
    return biggestid; //  biggestid;
}


function getMainNotificationID() {
    var minimum = 100000 * 45554;
    var currid = 0;
    $(".alert_items_con ul li").each(function (index) {
        currid = $(this).attr("max");

        if (parseInt(minimum) > parseInt(currid)) {
            minimum = parseInt(currid);

        }

    });
    //alert("firstid = " + biggestid);
    // firstid = biggestid;
    return minimum; //  biggestid;
}


//hisham friends status Dontknow,MyFriends,OtherRequests,MayKnow and MyRequests See More..

function seemorePepole() {
    var id = $(".FreiendsLinkStatus").attr("id");
    $(".FreiendsLinkStatus").remove();
    //alert(id.split('_')[0]);
    var iditem = $(".friend-li-block-hover:last").attr("id");
    // alert(iditem.split('_')[1]);
    var mnubarloader = $("#loader-mnubar"); //loader for menu bar (friend bar)
   mnubarloader.html(imgTagLoadAjax);
    
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/BuildFriendAreaSeemore",
        data: "{'frndArea':" + id.split('_')[0] + ",'lastid':" + iditem.split('_')[1] + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            mnubarloader.html("");
            if (data.d != "") {

                var p = new Array();

                p = data.d;
                // $(".content-insider").remove(); //remove loader
                if (p != "") {
                    // alert(data.d);
                    // alert("#" + iditem);
                   
                    $("#" + iditem).after(data.d);
                }
                //html(p);
                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1] + "&tab=" + p[2]);
                // window.history.pushState("string", "Title", p[1]);
            }
        }
        
    });
    
   
    
}


//اظهار التالي عبر البحث عن الاصدقاء بحسب الاسم او الايميل




function SearchseemorePepole() {
   
    var id = $(".SearchFreiendsLinkStatus").attr("id");
    $(".SearchFreiendsLinkStatus").remove();
    //alert(id.split('_')[0]);
    var iditem = $(".friend-li-block-hover:last").attr("id");

    var mnubarloader = $("#loader-mnubar"); //loader for menu bar (friend bar)
    mnubarloader.html(imgTagLoadAjax);


    $.ajax({
        type: "POST",
        url: "/Methods.aspx/SearchFriendsRelation",
        data: "{'mail':'" + $("#fsearch").val() + "','area':" + id.split('_')[0] + ",'lastid':" + iditem.split('_')[1] + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          //  mnubarloader.html("");
            if (data.d != "") {

                var p = new Array();

                p = data.d;
                // $(".content-insider").remove(); //remove loader
                if (p != "") {
                    // alert(data.d);
                    // alert("#" + iditem);

                    $("#" + iditem).after(data.d);
                }
                //html(p);
                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1] + "&tab=" + p[2]);
                // window.history.pushState("string", "Title", p[1]);
            }
        }
        
    });
    

}


/////////// Change Notification Count to Zero and Hide red Color from Notifications


function ChangeZero(type) {

    var id = ""; //    
    var cfunction = "";
    if (type == 1) {
        id = "msgcountLable";
        cfunction = "MessageNotiZero";
    }
    else
        if (type == 2) {
            id = "notiredEvent110";
            cfunction = "EventNotiZero";

        }
        else
            if (type == 3) {
                id = "lblCount";
                cfunction = "FriendNotiZero";

            }
            $("#" + id).html("");
            $("#" + id).removeClass("notired");
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/" + cfunction,
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //  mnubarloader.html("");
           // alert(data.d);

            if (data.d == "true") {

              
            }
        }

    });


}






function GetNotiCount() {
    //if there is no topics tags then exit

    var idmsg = "msgcountLable"; //    
    var idevnt = "notiredEvent110"; // 
    var idfrnd = "lblCount"; //
    // alert(idmsg + " " + idevnt + " " + idfrnd);
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/GetNotiCount",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var p = new Array();
                p = data.d;
                //                alert("#" + idmsg + "= " + p[0].toString());

                //                alert("#" + idevnt + "= " + p[1].toString());

                //                alert("#" + idfrnd + "= " + p[2].toString());

                $("#" + idmsg).html(p[0].toString());
                $("#" + idevnt).html(p[1].toString());
                $("#" + idfrnd).html(p[2].toString());
                if (p[0].toString() == "0" || p[0].toString() == "") {
                    $("#" + idmsg).html("");
                    $("#" + idmsg).removeClass("notired");
                }
                else {
                    $("#" + idmsg).removeClass("notired");
                    $("#" + idmsg).addClass("notired");
                }

                if (p[1].toString() == "0" || p[1].toString() == "") {
                    $("#" + idevnt).html("");
                    $("#" + idevnt).removeClass("notired");
                }
                else {
                    $("#" + idevnt).removeClass("notired");
                    $("#" + idevnt).addClass("notired");
                }

                if (p[2].toString() == "0" || p[2].toString() == "") {
                    $("#" + idfrnd).html("");
                    $("#" + idfrnd).removeClass("notired");
                }
                else {
                    $("#" + idfrnd).removeClass("notired");
                    $("#" + idfrnd).addClass("notired");
                }

            }
        }
    });
}


//Hisham
//function Acomplete() {
////    var id = $(".FreiendsLinkStatus").attr("id");
////    $(".FreiendsLinkStatus").remove();
////    //alert(id.split('_')[0]);
////    var iditem = $(".friend-li-block-hover:last").attr("id");
//    // alert(iditem.split('_')[1]);

//    alert("complete");

//    $.ajax({
//        type: "POST",
//        url: "/Methods.aspx/AutoComplete",
//        data: "{'userid':'1'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {

//            if (data.d != "") {



//                var p = new Array();

//                p = data.d;
//                alert(p[0]);
//                alert(p[1]);
//                //                // $(".content-insider").remove(); //remove loader
//                //                if (p != "") {
//                //                    // alert(data.d);
//                //                    // alert("#" + iditem);
//                //                    $("#" + iditem).after(data.d);
//                //}
//                //html(p);
//                //window.history.pushState("string", "Title", "usrmain.aspx?uid=" + p[0] + "&pg=" + p[1] + "&tab=" + p[2]);
//                // window.history.pushState("string", "Title", p[1]);
//            }
//        }
//    });

//}





//serach friends by Name
