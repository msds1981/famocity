$(document).ready(function () {
    //Window Resize
    $(window).resize(function () {
        if ($(window).width() < 1200) {
            $(".header .search input[type='text']").width($(window).width() - 602);
            $("section.page").css("padding-right", "0px");
        }
        else {
            $(".header .search input[type='text']").css("width", "590px");
            var fbxr = $(".fixed-box-chat").css("right");
            if (fbxr == "0px") {
                $("section.page").css("padding-right", "280px");
            }
        }

        //Fixed Chats
        //Content - Messages
        $(".fbc-messages").height($(window).height() - 260);
    });

    //Search Adv
    $(".header .search input[type='text']").click(function () {
        $(".search-advance").fadeIn();
    });
    //Close Search Adv
    $("html, body").click(function (e) {
        if ($(e.target).hasClass('header-search')) {
            return false;
        }
        $(".search-advance").fadeOut();
    });

    //Header Tools
    $(".t-b-o").hide();
    $(".t-box .b-content").mCustomScrollbar();
    $('.t-link').click(function () {
        var ClassTool = $(this).attr('class');
        var Icon = ClassTool.replace(/t-link /gi, "");
        Icon = Icon.replace(/ t-active/gi, "");
        if (ClassTool == "t-link " + Icon) {
            $(".t-link").next(".t-b-o").fadeOut();
            $(this).next(".t-b-o").fadeIn();
            $(".t-link").removeClass("t-active");
            $(this).addClass("t-active");
        }
        else {
            $(this).next(".t-b-o").fadeOut();
            $(this).removeClass("t-active");
        }
        var customScrollbar = $(this).next(".t-box").find(".mCSB_scrollTools");
        customScrollbar.css({ "opacity": 0 });
        var HeaderBoxContent = $(this).next(".t-box").find(".b-content");
        HeaderBoxContent.mCustomScrollbar("update");
        customScrollbar.animate({ opacity: 1 }, "slow");
    });
    //Close Header Tools If Click On Anywhere in Body
    $("html, body").click(function (e) {
        if ($(e.target).hasClass('t-link')) {
            return false;
        }
        $(".t-link").next(".t-b-o").fadeOut();
        $(".t-link").removeClass("t-active");
    });

    //Fixed Chats
    //Open Fixed Box Chat
    $(".fixed-open-chat").click(function () {
        if ($(window).width() > 1200) {
            $(".fixed-open-chat").animate({ right: "-95px" });
            $(".fixed-box-chat").animate({ right: "0" });
            $("section.page").animate({ paddingRight: "280px" });
            $(".fixed-box-messages").animate({ paddingRight: "300px" });
        }
        else {
            $(".fixed-open-chat").animate({ right: "-95px" });
            $(".fixed-box-chat").animate({ right: "0" });
            $(".fixed-box-messages").animate({ paddingRight: "300px" });
        }
    });
    //Close Fixed Box Chat
    $(".fixed-box-chat .fbc-close").click(function () {
        $(".fixed-open-chat").animate({ right: "-47px" });
        $(".fixed-box-chat").animate({ right: "-280px" });
        $("section.page").animate({ paddingRight: "0" });
        $(".fixed-box-messages").animate({ paddingRight: "20px" });
    });
    //Menu Status
    $(".fixed-box-chat .info-status").click(function () {
        $(".status-menu").fadeToggle();
    });
    //Content - Messages
    $(".fixed-box-chat .fbc-messages").height($(window).height() - 260);
    $(".fixed-box-chat .fbc-messages").mCustomScrollbar();
    //Open Message
    /*$(".fbc-m-item").click(function () {
    $('.fixed-box-messages').prepend("<div class='fbm-box active'></div>");
    $('.fixed-box-messages .fbm-box:first').prepend("<div class='fbm-box-header'></div>");
    $('.fixed-box-messages .fbm-box:first .fbm-box-header').prepend("<div class='title'>" + $(this).find(".info-name").text() + "</div>");
    $('.fixed-box-messages .fbm-box:first .fbm-box-header').append("<div class='tools'></div>");
    $('.fixed-box-messages .fbm-box:first .fbm-box-header .tools').append('<span class="fbm-minimize"> <img src="Styles/Images/minimize-white.png" /> </span> <span class="fbm-options"> <img src="Styles/Images/gear-white.png" width="14px" /> </span> <div class="fbm-options-menu"> <div class="fbm-om-arrow"></div> <a href="">المحادثة كاملة</a> <a class="up-file-msg">إضافة ملف</a> <input name="up-file-msg" type="file" hidden="hidden" multiple="multiple" /> <a class="minimize-other-chatbox">تصغير باقي نوافذ</a> </div> <span class="fbm-close"> <img src="Styles/Images/close-white.png" /> </span>');
    });*/
    //Messages Content
    $(".fbm-box-content").mCustomScrollbar();
    //Messages Minimize
    $(".fbm-box .fbm-minimize").click(function () {
        var parentClass = $(this).parent().closest('.fbm-box').attr("class");
        if (parentClass == "fbm-box active") {
            $(this).parent().closest('.fbm-box').css("margin-bottom", "0");
            $(this).parent().closest('.fbm-box').removeClass("active");
            $(this).parent().closest('.fbm-box').animate({ marginBottom: "-" + ($(this).parent().closest('.fbm-box').height() - 30) + "px" });
            $(".emoji-box").fadeOut();
        }
        else {
            $(this).parent().closest('.fbm-box').css("margin-bottom", "-284px");
            $(this).parent().closest('.fbm-box').addClass("active");
            $(this).parent().closest('.fbm-box').animate({ marginBottom: "0px" });
        }
    });
    //Messages Minimize All
    $(".fbm-box .minimize-other-chatbox").click(function () {
        $('.fbm-box').removeClass("active");
        $('.fbm-box').animate({ marginBottom: "-" + ($(this).parent().closest('.fbm-box').height() - 30) + "px" });
        $(".fbm-options-menu").fadeOut();
        $(".emoji-box").fadeOut();
    });
    //Messages Close
    $(".fbm-box .fbm-close").click(function () {
        $(this).parent().closest('.fbm-box').remove();
    });
    //Messages Content Options
    $(".fbm-options").click(function () {
        $(this).next(".fbm-options-menu").fadeToggle();
    });
    //Messages Textarea
    $(".fbm-box-footer .input textarea").click(function () {
        $(".emoji-box").fadeOut();
        $(this).parent().closest('.fbm-box-footer').animate({ height: "79px" });
        $(this).animate({ height: "70px" });
    }).focusout(function () {
        $(this).parent().closest('.fbm-box-footer').animate({ height: "43px" });
        $(this).animate({ height: "34px" });
    });
    //Messages Content File Upload
    $(".fbm-box .up-file-msg").click(function () {
        $(this).next('input').click();
        $(".fbm-options-menu").fadeOut();
    });
    //Messages Content Photo Upload
    $(".fbm-box .photo").click(function () {
        $(this).next('input').click();
    });
    //Open Emoji
    $(".fbm-box .emoji").click(function () {
        $(this).next(".emoji-box").fadeToggle();
    });
    //Emoji Cat
    $(".fbm-box .eb-h-option").click(function () {
        var nc = $(this).attr("data-box");
        $(".eb-h-option").removeClass("active");
        $(this).addClass("active")
        $(".eb-c-box").hide();
        $("." + nc).fadeIn();
    });
    //Emoji Face
    $(".fbm-box .emoji-face").click(function () {
        var code = $(this).attr("data-code");
        var text = $(".fbm-box-footer .input textarea").val();
        $(".fbm-box-footer .input textarea").val(text + code);
    });


    //Messages Page
    //Menu Status
    //$(".ps-box-messages .info-status").click(function () {
    $(document).on('click', '.ps-box-messages .info-status', function () {
        $(".status-menu").fadeToggle();
    });
    //Content - Messages
    $(".ps-box-messages .bm-messages").height($(window).height() - 340);
    $(".ps-box-messages .bm-messages").mCustomScrollbar();
    //Message Content
    $(".page-messages .ps-bc-messages").height($(window).height() - 198);
    $(".page-messages .ps-bc-messages").mCustomScrollbar();
    //Messages Textarea
    $(".ps-bc-input .ps-bc-input-text textarea").click(function () {
        $(".emoji-box").fadeOut();
        $('.ps-bc-messages').animate({ height: ($(window).height() - 248) + "px" });
        $('.ps-bc-input').animate({ height: "90px" });
        $('.ps-bc-input-text').animate({ height: "88px" });
        $('.ps-bc-input-submit').animate({ height: "90px" });
        $('.ps-bc-input-submit-arrow').animate({ marginTop: "35px" });
        $('.ps-bc-input-tools').animate({ height: "74px" });
        $(this).animate({ height: "78px" });
    }).focusout(function () {
        $('.ps-bc-messages').animate({ height: ($(window).height() - 198) + "px" });
        $('.ps-bc-input').animate({ height: "40px" });
        $('.ps-bc-input-text').animate({ height: "38px" });
        $('.ps-bc-input-submit').animate({ height: "40px" });
        $('.ps-bc-input-submit-arrow').animate({ marginTop: "9px" });
        $('.ps-bc-input-tools').animate({ height: "24px" });
        $(this).animate({ height: "28px" });
    });
    //Messages Content Photo Upload
    $(".ps-bc-input-tools .up-photo").click(function () {
        $(this).next('input').click();
    });
    //Open Emoji
    $(".ps-bc-input-tools .emoji").click(function () {
        $(this).next(".emoji-box").fadeToggle();
    });
    //Emoji Cat
    $(".ps-bc-input-tools .ps-h-option").click(function () {
        var nc = $(this).attr("data-box");
        $(".ps-h-option").removeClass("active");
        $(this).addClass("active")
        $(".ps-c-box").hide();
        $("." + nc).fadeIn();
    });
    //Emoji Face
    $(".ps-bc-input-tools .emoji-face").click(function () {
        var code = $(this).attr("data-code");
        var text = $(".ps-bc-input .ps-bc-input-text textarea").val();
        $(".ps-bc-input .ps-bc-input-text textarea").val(text + code);
    });
});