var imgLoadSmallchat = "/newfiles/images/load-small.gif";
var imgTagLoadSmallchat = "<img class='ImgLoadSmall' src='" + imgLoadSmallchat + "'/>";
var imgTagLoadAjaxchat = "<img src='/images/ajax-loader2.gif'/>";
var imgTagLoadAjaxCenterchat = "<img src='/images/ajax-loader2.gif' class='content-insider imgloadercenter'/>";

//The Ready and timer functions
$(function () {
//    $("#msgtextchat").keydown(function (e) {
//        if (e.keyCode == 13) {

//            sendmessage();
//        }



  //  });

    $(".ps-bc-input-submit").click(function () {

        sendmessage();
        
    });

    $("#newMsg").click(function () {
        $(".ps-bc-m-self").remove();
        $(".ps-bc-m-he").remove();
        $(".newMessgae").css("display", "block");
        $("#toNewMsg").val("");
    });

  

//    $(document).on('click', '.mb-m-item', function () {
//        $(".page-messages .ps-bc-messages").mCustomScrollbar();

//    });

    $(document).on('click', '.ps-bc-m-old-messages', function () {
        BuildConversationDetails("old", $(".ps-bc-input").attr("fid"));

    });

    $(document).on('click', '.mb-messages-more', function () {
        BuildFriendsSide("old");

    });


    //    $(".ps-bc-m-old-messages").click(function () { alert(".ps-bc-m-old-messages clicked"); });
    window.setInterval(function () {
        BuildFriendsSide("new"); 
    BuildConversationDetails("new", $(".ps-bc-input").attr("fid")); }, 10000);

});

// Send Message Function

function sendmessage() {
    var sender = $("#myid").val();
    var reciver = $(".ps-bc-input").attr("fid");
    if ($(".newMessgae").css("display") == "block") {
        reciver = $("#toNewMsg").attr("toid");
            }
    $("#toNewMsg").attr("toid","0");
    
   
    var message = $("#msgtextchat").val();
    $("#msgtextchat").val("");
    if ($(".newMessgae").css("display") == "block")
        $(".newMessgae").css("display", "none");
    if (message.trim() == "") {
       
    return "";

}
if (!(reciver > 0)) return "";
var urlfunctionname = "SendMessage";
var parameter = "{'Message':'" + message + "','Sender':'" + sender + "','Reciver':'" + reciver + "'}";

messageSending(urlfunctionname, parameter);

   
}



function messageSending(urlfunctionname, parameter) {
   // closepopup("#newChatMsg");
    
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/" + urlfunctionname,
        data: parameter,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert("send");

        }

            ,
        error: function () {
           

        }
    });
    ClosePopup("#newChatMsg");

}

function BuildChatPage() {// this the main Function 
   

    BuildConversationList();
}

function BuildConversationList() {// this function builds the all page contnt
    BuildUserInfoSide();
    BuildSearchSide();
    BuildFriendsSide("0");
    
//    if (type == "0")
//        $(".page-messages .bm-messages").mCustomScrollbar("update");
//   // if (type == "new" && arr.length > 0)
//        $(".page-messages .bm-messages").height($(window).height() - 198);

  

}


function BuildConversationDetails(type, myFriend) {
    
    // this function change the clicked friend style 
    ActiveDeactive(myFriend);
    //Õ–› ﬂ· «·„Õ«œÀ«  «·”«»ﬁ… · ﬂÊÌ‰ „Õ«œÀ«  ÃœÌœ…
   
    if (type != "new")
    $(".ps-bc-m-old-messages").after(imgTagLoadSmallchat);
    //  ⁄Ì‰ —ﬁ„ «·’œÌﬁ «·„—«œ ⁄„· „Õ«œÀ… „⁄Â ›Ì «·»—»— Ì «› «Ì œÌ «·Œ«’… »«·ﬂ·«” ps-bc-input
     $(".ps-bc-input").attr("fid", myFriend);
    var p = new Array();
    var ara = new Array();
    var arr = new Array();
    var urlfunctionname = "GetMyFriendConversation";

    if (type == "0") {
        //$(".mCSB_container").html(createConversitionSeemore(0));
        $(".ps-bc-messages .mCustomScrollBox .mCSB_container").html(createConversitionSeemore(0));
        var parameter = "{'myFriend':'" + myFriend + "','lastid':'0','type':'0'}"; //int myFriend,int lastid,string type
        $(".ps-bc-m-he").remove();
        $(".ps-bc-m-self").remove();
    }
    if (type == "new") {

        var parameter = "{'myFriend':'" + myFriend + "','lastid':'"+getfiristlastmessagedetailid("last")+"','type':'new'}"; //int myFriend,int lastid,string type
      //  alert(parameter);
    }
    if (type == "old")
        var parameter = "{'myFriend':'" + myFriend + "','lastid':'" + getfiristlastmessagedetailid("first") + "','type':'old'}"; //int myFriend,int lastid,string type
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/" + urlfunctionname,
        data: parameter,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            p = data.d;
            ara = p;
            if (!(ara.length > 0) && type == "old") { $(".ps-bc-m-old-messages").remove(); }
            for (i = 0; i < ara.length; i++) {

                if (ara[i].Sender.toString() == $("#myid").val().toString())
                    arr[i] = MessagesDetailTemplate(ara[i].SenderPhotoPath, ara[i].Message, ara[i].Time, "me", ara[i].ID);
                else
                    arr[i] = MessagesDetailTemplate(ara[i].SenderPhotoPath, ara[i].Message, ara[i].Time, "he", ara[i].ID);
                //msgdetail = msgdetail + arrmsgdet[i];
                //  $(".ps-bc-m-old-messages").after(arr[i])

            }


            //            $(".ps-bc-m-he").each(function (i, obj) {

            //                $(obj).remove();

            //            });



           createDetailSidehtml(type, arr);
//            if (type == "0")
                $(".page-messages .ps-bc-messages").mCustomScrollbar("update");
//            if(type=="new" && arr.length>0)
                $(".page-messages .ps-bc-messages").height($(window).height() - 198);
           

         //   $(".mCSB_scrollTools").css("display", "block");
            if (type != "old") {
               // $(".mCSB_dragger").css("top",/* $(".ps-bc-input").css("top") - 10 +*/ "0px");
                //alert($(".ps-bc-input").css("top")+10+"px");

            }
            //alert(" $(ps-bc-m-old-messages).next("+ $(".ps-bc-m-old-messages").next().attr("class"));
            $(".ps-bc-m-old-messages").attr("detoldmsg", $(".ps-bc-m-old-messages").next().attr("msgid"));
            // alert($el.is('[class^="ps-bc-m-"]').attr("msgid"));
            //            alert(getfiristlastmessagedetailid("first"));
            //            alert(getfiristlastmessagedetailid("last"));

        }
,

        error: function () {
            $(".ImgLoadSmall").remove();
            // alert("error");

        }
    });
    
}





function BuildUserInfoSide_Status() {

    var statusmenueDiv = createDiv(-1, "status-menu", -1, -1, -1, -1);

    var smarrowDiv = createDiv(-1, "sm-arrow", -1, -1, -1, -1);

    statusmenueDiv.appendChild(smarrowDiv);

    var smoptionA = CreateA(-1, "sm-option", -1, -1, "data-status", "");

    var spanonline = createSpan(-1, "online", -1, -1, -1, -1);

    smoptionA.appendChild(spanonline);
    //"online"
    smoptionA.appendChild(document.createTextNode("online"));
    statusmenueDiv.appendChild(smoptionA);

    smoptionA = CreateA(-1, "sm-option", -1, -1, "data-status", " ");
    spanonline = createSpan(-1, "offline", -1, -1, -1, -1);
    smoptionA.appendChild(spanonline);
    smoptionA.appendChild(document.createTextNode("offline"));
    statusmenueDiv.appendChild(smoptionA);


    smoptionA = CreateA(-1, "sm-option", -1, -1, "data-status", " ");

    spanonline = createSpan(-1, "busy", -1, -1, -1, -1);
    smoptionA.appendChild(spanonline);
    smoptionA.appendChild(document.createTextNode("Busy"));
    statusmenueDiv.appendChild(smoptionA);

    smoptionA = CreateA(-1, "sm-option", -1, -1, "data-status", " ");
    spanonline = createSpan(-1, "away", -1, -1, -1, -1);
    smoptionA.appendChild(spanonline);
    smoptionA.appendChild(document.createTextNode("work now"));
    statusmenueDiv.appendChild(smoptionA);

    smoptionA = CreateA(-1, "", -1, -1, -1, "");
    spanonline = createSpan(-1, "edit", -1, -1, -1, -1);
    smoptionA.appendChild(spanonline);
    smoptionA.appendChild(document.createTextNode("Edit"));
    statusmenueDiv.appendChild(smoptionA);



    return statusmenueDiv;
}





function BuildSearchSide() {
    var form = document.createElement("form");
    form.setAttribute("action", "");
    form.setAttribute("method", "");
    var input = document.createElement("input");
    input.setAttribute("type", "text");
    input.setAttribute("placeholder", "enter data");
    form.appendChild(input);
    $(".bm-search").html(form);

}




function BuildFriendsSide(type) {
  
    var lastid = 0;
   
    if (type == "old")
        lastid = getSeeMoremsgid(".mb-m-item", "last");
    if (type == "new")
        lastid = getSeeMoremsgid(".mb-m-item", "first");

    var parameter = "{'lastid':'" + lastid + "','type':'" + type + "','reciver':'"+0+"'}"
    var urlfunctionname = "GetMyLastPersonalMessages";
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/" + urlfunctionname,
        data: parameter,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            p = data.d;
            ara = p;

            var arr = new Array();
            var activ = "active";

            for (i = 0; i < ara.length; i++) {
                //alert("hidden=" + $("#myid").val() + "  sender=" + ara[i].Sender + "   reciver= " + ara[i].Reciver);
                // Â‰« ÌﬁÊ„ » ›Õ’ «·«’œﬁ«¡ «·„—«œ «÷Â«—Â„ «–« ﬂ«‰ »«”»Ê—  ÂÊ «·”Ì‰œ— ›ÌﬁÊ„ »«ŸÂ«— ·ÊÃÊ «·—Ì”Ì›— Ê«”„Â Ê«·⁄ﬂ” Êﬂ–·ﬂ »‰«¡  „»·Ì  «·Œ«’ »Õ”» ‰Ê⁄ 
                if (i != 0) activ = "";
                if ($("#myid").val() == ara[i].Sender) {

                    RemoveDublicate(ara[i].Reciver);
                    arr[i] = CreateOneFriendTemplate(ara[i].Sender, ara[i].Reciver, ara[i].ReciverPhotoPath, ara[i].ReciverName, ara[i].Message, ara[i].Time, activ, ara[i].ID, ara[i].MsdID, "BuildConversationDetails(0," + ara[i].Reciver + ")");
                }
                else {
                    //alert("$('#myid').val() == ara[i].Sender   Else");
                    RemoveDublicate(ara[i].Sender);
                    arr[i] = CreateOneFriendTemplate(ara[i].Sender, ara[i].Reciver, ara[i].SenderPhotoPath, ara[i].SenderName, ara[i].Message, ara[i].Time, activ, ara[i].ID, ara[i].MsdID, "BuildConversationDetails(0," + ara[i].Sender + ")");
                }
            }
            //the first one {load the message page}
            createFriendSidehtml(type, arr);

            //            document.on("load",function () { alert("onnnnnn"); });
            if(type=="0")
                $(".ps-box-messages .bm-messages").mCustomScrollbar();
            else
                $(".ps-box-messages .bm-messages").mCustomScrollbar("update");
            $(".ps-box-messages .bm-messages").height($(window).height() - 340);
//            


            //⁄‰œ„« Ì „ Ã·» ⁄‰«’— «·«’œﬁ«¡ ›Ì «·Ì„Ì‰ Â–Â «·›‰ﬂ‘‰  ⁄ÿÌ  ›«’Ì· «·—”«∆· »Ì‰Â Ê»Ì‰Â »œÊ‰ «·‰ﬁ— ⁄·ÌÂ ﬂœÌ›Ê·  
            if (arr.length > 0 && type != "new" && type != "old") {

                if ($("#LinkuserId").val() != "0")
                    BuildConversationDetails("0", $("#LinkuserId").val());
                else
                    BuildConversationDetails("0", getfriendid(".mb-m-item"));

            }

            // if( type=="old" && !(arr.length>0))
            $(".mb-messages-more").remove();
            if (type != "new" && !arr.length > 0) {
                var msgidfirst = getSeeMoremsgid(".mb-m-item", "first");
                var msgidlast = getSeeMoremsgid(".mb-m-item", "last");
                var msgmordiv = createDiv(-1, "mb-messages-more", -1, -1, -1, -1);
                msgmordiv.setAttribute("id", msgidfirst);
                var imagmor = Createimage(-1, -1, -1, "Messages/Styles/Images/icon-msg-more.png", -1, -1)
                var spantext = createSpan(-1, -1, -1, "old message", -1, -1);
                var imagback = Createimage(-1, -1, -1, "Messages/Styles/Images/arrow-down-black.png", -1, -1)
                msgmordiv.appendChild(imagmor);
                msgmordiv.appendChild(spantext);
                msgmordiv.appendChild(imagback);
                msgmordiv.setAttribute("lastid", msgidlast);
                $(".bm-messages").after(msgmordiv);



            }

        }
        ,
        error: function () {

        }
    });



}



function createFriendSidehtml(type, arr) {
 //   alert("crete now" + "   type=" + type+"  arr length = "+arr.length);
    if (type == 0) {
        var bamssgdiv = createDiv(-1, "bm-messages", -1, -1, -1, -1);
        for (i = 0; i < arr.length; i++) {
         //   $(".bm-messages").append(arr[i]);
            $(".bm-messages .mCustomScrollBox .mCSB_container").append(arr[i]);
           // bamssgdiv.appendChild(arr[i]);


        }
       // $(".bm-search").after(bamssgdiv);


    }
    // the new friends 
    if (type == "new") {
        for (i = 0; i < arr.length; i++) {
            //  alert(arr[i].InnerHTML);
            if ($('.bm-messages').find('.mb-m-item').length == 0)
                $(".bm-messages").html(arr[i]);
            $(".mb-m-item" + ":first").before(arr[i]);
        }

    }
    // the Old friends 
    if (type == "old") {
        for (i = 0; i < arr.length; i++)
            if ($('.bm-messages').find('.mb-m-item').length == 0)
                $(".bm-messages").html(arr[i]);
            $(".mb-m-item" + ":last").after(arr[i]);

    }



}

function createDetailSidehtml(type, arr) {
    
$(".ImgLoadSmall").remove();
if (type == "0") {
    for (i = arr.length-1; i >=0; i--) {

        //$(".ps-bc-m-old-messages").after(arr[i]);
        $(".ps-bc-messages .mCustomScrollBox .mCSB_container").append(arr[i]);
    }
    
   
}
if (type == "new") {
   // for (i = 0; i < arr.length; i++) {
    for (i = arr.length-1 ; i >= 0; i--) {
            $('[class*="ps-bc-m-"]').not(".ps-bc-m-old-messages").last().after(arr[i]);
        }
        //$(".mCSB_dragger").remove();
      
    }
     if (type == "old") {
         for (i = arr.length-1 ; i >= 0; i--) {
             $('[class*="ps-bc-m-"]').not(".ps-bc-m-old-messages").first().before(arr[i]);
         }
     }

//     $(".t-box .b-content").mCustomScrollbar();
//     $(".fixed-box-chat .fbc-messages").mCustomScrollbar();
//     $(".fbm-box-content").mCustomScrollbar();
}

  function createDiv(id,clas,onclik,innertext,attribute,attrvalue) {
      var element = document.createElement("div"); // document.createElement('div');
                      if(id!=-1)
                     element.id = Id;
                      if(clas!=-1)
                       element.className = clas;
                      if(innertext!=-1)
                      element.appendChild(document.createTextNode(innertext));
                      if(attribute!=-1)
                          element.setAttribute(attribute, attrvalue);
                      
                    return element;
                    }

function createSpan(id,clas,onclik,innertext,attribute,attrvalue)
                    {
                        var element = document.createElement("span");
                    if(id!=-1)
                    element.id = Id;
                      if(clas!=-1)
                   element.className = clas;
                      if(onclik!=-1)
                   element.onclick = onclik;
                      if(innertext!=-1)
                      element.appendChild(document.createTextNode(innertext));
                      if(attribute!=-1)
                     element.setAttribute(attribute, attrvalue);
                    return element;
                    }

 function Createimage(id,clas,onclik,srctext,attribute,attrvalue)
                    {
                        var element = document.createElement("img");
                    if(id!=-1)
                    element.id = Id;
                      if(clas!=-1)
                     element.className = clas;
                      if(onclik!=-1)
                    element.onclick = onclik;
                      if(srctext!=-1)
                     element.src = srctext;
                      if(attribute!=-1)
                    element=SetAttribute(element,attribute,attrvalue);
                    return element;

                    }
        function CreateA(id,clas,onclik,href,attribute,attrvalue) {

            var element = document.createElement("a");
            
                    if(id!=-1)
                        element.id = Id;
                      if(clas!=-1)
                          element.className = clas;
                      if(href!=-1)
                          element.href = value;
                     if(onclik!=-1)
                    element.onclick = onclik;
                      if(attribute!=-1)
                          element.setAttribute(attribute, attrvalue);

                      
                    return element;

                    }

  
   function BuildUserInfoSide_logo( logo) {
       var path = logo;
       if(logo==null || logo=="")
       path="Messages/Styles/Images/avatar.jpeg";
                           var element = createDiv(-1, "avatar-status", -1, -1, -1, -1);
                           var x = CreateA(-1, -1, -1, -1, -1, -1);
                           element.appendChild(x);
                     // var element=createDiv(-1,"avatar-status",-1,-1,-1,-1);
                      var img=Createimage(-1,-1,-1,path,-1,-1);
                      var span=createSpan(-1,"online",-1,-1,-1,-1);
                      element.appendChild(img);
                      element.appendChild(span);
                      return element;
                
                  }
  function BuildUserInfoSide_userinfo(name) {
                      var element = createDiv(-1, "info", -1, -1, -1, -1);
                      var span = createSpan(-1, "info-name", -1, name, -1, -1);
                      element.appendChild(span);
                      span = createSpan(-1, "info-status", -1, -1, -1, -1);
                      var img = Createimage(-1, -1, -1, "/Messages/Styles/Images/gear-chat.png", -1, -1);
                      span.appendChild(img);
                      var spanmotah = createSpan(-1, -1, -1, -1, -1, -1);
                      span.appendChild(spanmotah);
                      span.appendChild(BuildUserInfoSide_Status());
                      element.appendChild(span);

                      return element;
                  }






function BuildUserInfoSide() {

    var logo ;
  
    var info ;
   // $(".gridContainer:last").after(imgTagLoadAjaxCenter); //loader
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/UserLoginData",
        data: "{'id':'" + 0 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          
            if (data.d != "") {
                logo = BuildUserInfoSide_logo(data.d.PhotoPath);
                info = BuildUserInfoSide_userinfo(data.d.Name);
                $(".bm-header").html(logo);
                $(".bm-header").append(info);
              
            }
        },
        error: function () {
            
            // $(".content-insider").remove();
        }
    });

   
  
    
    var ar = new Array();
//    ar[0] = info;
//    ar[1] = xx;
    return ar;
}



function CreateOneFriendTemplate(sender,receiver ,imgpath,name,msgtext,time, active,msgid,msgsdid,onclick) {
    var maindiv = createDiv(-1, "mb-m-item", onclick, -1, "sender", sender);
    if (active != "") {
        $(".mb-m-item").removeClass(active);
        maindiv.className += " " + active;
    }
                maindiv.setAttribute("reciver", receiver);
                maindiv.setAttribute("msgid", msgid);
                maindiv.setAttribute("msgsdid", msgsdid);
                maindiv.setAttribute("onclick", onclick);
                var avatardiv=createDiv(-1,"avatar-status",-1,-1,-1,-1);
                var img = Createimage(-1, -1, -1, imgpath, -1, -1);
                avatardiv.appendChild(img);
                var spanoffline=createSpan(-1,"offline",-1,-1,-1,-1);
                avatardiv.appendChild(spanoffline);
                var divinfo=createDiv(-1,"info",-1,-1,-1,-1);
                var spaninfoname = createSpan(-1, "info-name", -1, -1, -1, -1);
                spaninfoname.appendChild(document.createTextNode(name))
                var spaninfomsg = createSpan(-1, "info-msg", -1, -1, -1, -1);
                spaninfomsg.appendChild(document.createTextNode(msgtext));
                var spaninfodatetime = createSpan(-1, "info-datetime", -1, -1, -1, -1);
                spaninfodatetime.appendChild(document.createTextNode(time));
                divinfo.appendChild(spaninfoname);
                divinfo.appendChild(spaninfomsg);
                divinfo.appendChild(spaninfodatetime);
                maindiv.appendChild(avatardiv);
                maindiv.appendChild(divinfo);

return maindiv;

}





function MessagesDetailTemplate(imgpath,msgtext,timetext,sendertype,msgid) 
                {
                //sender type me or he
                    var back;
                    
               if (sendertype == "me")
                {
                var maindiv = createDiv(-1, "ps-bc-m-self", -1, -1, -1, -1);
                maindiv.setAttribute("msgid", msgid);
                var avatardiv=createDiv(-1,"avatar",-1,-1,-1,-1);
                var image=Createimage(-1,-1,-1,imgpath,-1,-1);
                avatardiv.appendChild(image);
                maindiv.appendChild(avatardiv);
                var divtext=createDiv(-1,"text",-1,-1,-1,-1);
                var spantext=createSpan(-1,-1,-1,-1,-1,-1);
                divtext.appendChild(spantext);
                divtext.appendChild(document.createTextNode(msgtext));

                var datetimediv = createDiv(-1, "datetime", -1, timetext, -1, -1);
                divtext.appendChild(datetimediv);
                maindiv.appendChild(divtext);
               
                back=maindiv;
                }

                if (sendertype=="he")
                {
                var maindiv=createDiv(-1,"ps-bc-m-he",-1,-1,-1,-1);

                maindiv.setAttribute("msgid", msgid);
                var divtext=createDiv(-1,"text",-1,-1,-1,-1);
                var spantext=createSpan(-1,-1,-1,-1,-1,-1);
                divtext.appendChild(spantext);
                divtext.appendChild(document.createTextNode(msgtext));

                var datetimediv = createDiv(-1, "datetime", -1, timetext, -1, -1);
                divtext.appendChild(datetimediv);

                maindiv.appendChild(divtext);
                var avatardiv=createDiv(-1,"avatar",-1,-1,-1,-1);
                var image=Createimage(-1,-1,-1,imgpath,-1,-1);
                avatardiv.appendChild(image);
                maindiv.appendChild(avatardiv);


               
                back=maindiv;
                }
                return back;
                

                }

 




 // Â–Â «·›‰ﬂ‘‰”  ƒœÌ ÊŸ«∆› „⁄Ì‰…

function getfriendid(classname) {
 var sender=$(classname+":first").attr("sender");
 var reciver = $(classname + ":first").attr("reciver");
 //alert("sender=" + sender + "   reciver=" + reciver + "    " + classname + ":first");
    var friend;
    if ($("#myid").val() == sender)
        return reciver;
        else
        return sender;

    }

    function getSeeMoremsgid(classname, firstorlast) {
       // alert(classname + ":" + firstorlast);
        var msgid = $(classname + ":" + firstorlast).attr("msgid");
       // alert("getSeeMoremsgid=" + msgid);
        return msgid;
    }
    








    function RemoveDublicate(id) {


    $(".mb-m-item").each(function (i, obj) {
        if (id == $(obj).attr("sender") || id == $(obj).attr("reciver"))
            $(obj).remove();
       // alert("i=" + i + "  obj" + $(obj).attr("sender"));
        //test
    });
}
function ActiveDeactive(friend) {
   
    $(".mb-m-item").removeClass("active");
    $(".mb-m-item").each(function (i, obj) {

        if (friend == $(obj).attr("sender") || friend == $(obj).attr("reciver")) {
            $(obj).addClass("active");
          
        }
     
    });

}

function getfiristlastmessagedetailid(type) {
    var back = 0;
if(type=="first")
  back=   $('[class*="ps-bc-m-"]').not(".ps-bc-m-old-messages").first().attr("msgid");
    else
        back = $('[class*="ps-bc-m-"]').not(".ps-bc-m-old-messages").last().attr("msgid");
   // alert("back" + back);
    return back;
}


function createConversitionSeemore(detoldmsg) {
    var textseemor = "load old message";
var oldmsgdiv=createDiv(-1,"ps-bc-m-old-messages",-1,-1,"detoldmsg",detoldmsg);
var msgimg=Createimage(-1,-1,-1,"/Messages/Styles/Images/icon-msg-more.png",-1,-1);
//var span = createSpan(-1, -1, -1, "load old messages", -1, -1);
var span = createSpan(-1, -1, -1, textseemor, -1, -1);
oldmsgdiv.appendChild(msgimg);
oldmsgdiv.appendChild(span);
return oldmsgdiv;
//<div class="ps-bc-m-old-messages" detoldmsg="0">
//                        <img src="Messages/Styles/Images/icon-msg-more.png" />
//                        <span> Õ„Ì· —”«∆· √ﬁœ„</span>
//                    </div>
}




//----------------------------------------------------------------- New Message---------------------------------------------------

$(function () {

    $("#toNewMsg").keyup(function () {
        $(".ui-helper-hidden-accessible").html("");
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

                          //  alert(item.Name);
                            return {
                                value: item.Name,
                                label: item.Name,
                                imgsrc: item.PhotoPath,
                                countr: item.Country,
                                Link: item.Link,
                                id: item.ID
                            }
                        })) //ajax
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(textStatus);
                    }
                }); //source
            },
            select: function (event, ui) {
                //   alert(ui.item.value + " , " + ui.item.label);
                //$("#reciver").val(ui.item.label);
                // $("#reciverusrid").val(ui.item.id);
                $("#toNewMsg").attr("toid", ui.item.id);
            },
            minLength: 1
        })
    .data("autocomplete")._renderItem = function (ul, item) {
        //  alert(item.label);
        //alert(item.imgsrc+"  img");
        return $("<li class='ssss'></li>")
    .data("item.autocomplete", item)
    .append("<img src='" + item.imgsrc + "' />")
    .append("<span id='" + item.value + "' class ='m3rad_info' style='direction:rtl' >" + item.label + "</span>")
    .appendTo(ul);
    };


        ; //autocomplete
    }); //live

  
});      //function



$(function () {

    $("#msgReciver").keyup(function () {
       
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

                            //  alert(item.Name);
                            return {
                                value: item.Name,
                                label: item.Name,
                                imgsrc: item.PhotoPath,
                                countr: item.Country,
                                Link: item.Link,
                                id: item.ID
                            }
                        })) //ajax
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                       // alert(textStatus);
                    }
                }); //source
            },
            select: function (event, ui) {
               
               // alert(ui.item.value + " , " + ui.item.label);
                //$("#reciver").val(ui.item.label);
                // $("#reciverusrid").val(ui.item.id);
              //  $("#msgReciverid").val(ui.item.id);
            },
            minLength: 1
        })
    .data("autocomplete")._renderItem = function (ul, item) {
        //  alert(item.label);
        //alert(item.imgsrc+"  img");
        return $("<li class='ssss'></li>")
    .data("item.autocomplete", item)
  //  .append("<img src='" + item.imgsrc + "' />")
    .append("<span id='" + item.value + "  class ='m3rad_info' 'style='direction:rtl' >" + item.label + "</span>")// class ='m3rad_info' 
    .appendTo(ul);
    };


        ; //autocomplete
    }); //live


});       //function

//function SendMessagePopUp() {

//    function sendmessage() {
//        var sender = $("#myid").val();
//        var reciver = $("#msgReciverid").val();
//        


//        var message = $("#msgtextchat").val();
//        $("#msgtextchat").val("");
//        if (message.trim() == "") {

//            return "";

//        }

//        if (!(reciver > 0)) return "";
//        var urlfunctionname = "SendMessage";
//        var parameter = "{'Message':'" + message + "','Sender':'" + sender + "','Reciver':'" + reciver + "'}";
//        $("#msgReciverid").val("0")
//        messageSending(urlfunctionname, parameter);


//    }


//}


function MessagePopup(sender, receiverid, receivername) //
{
   
   $("#myid").val(sender);
   $("#msgReciverid").val(receiverid);
       $("#msgReciver").val(receivername);
}








function sendmessagepopup() {
    var sender = $("#myid").val();
    var reciver = $("#msgReciverid").val();



    var message = $("#msgtextchat").val();
    $("#msgtextchat").val("");
    if (message.trim() == "") {

        return "";

    }
   
    if (!(reciver > 0)) return "";
    var urlfunctionname = "SendMessage";
    var parameter = "{'Message':'" + message + "','Sender':'" + sender + "','Reciver':'" + reciver + "'}";
   // $("#msgReciverid").val("0")
    messageSending(urlfunctionname, parameter);





}