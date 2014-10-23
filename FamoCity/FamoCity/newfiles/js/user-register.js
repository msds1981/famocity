var valdUser = false;

// تشيط الايميل للمتجر 
//فحص تكرار الايميل  بعد الخروج من المربع النصي
$(function () {

    // تشيك ايميل تسجيل دخول المستخدم 
    $("#txtRegUser").focusout(function () {
        checkEmailRegistar();
    });

    //تشيك كلمة مرور  المستخدم 
    $("#txtPassUser").focusout(function () {
        CheckPassUser();
    });
    // تشيك اعادة كتابة كلمة المرور للمستخدم
    $("#txtRePassUser").focusout(function () {
        CheckRePass();
        // checkPasswordMatch();
    });
    //الموافقة على اتفافية المستخدم
});


function checkEmailRegistar() {
    var email = $("#txtRegUser");
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var spanvalidEmail = $("#MsgEmailRegistrar");
    var errtext = " ";

    //spanvalidEmail.css({ 'display': 'block' });

    //alert(email.val());
    if (email.val() == "") {
        showEmailValidUser("يرجى كتابة الايميل ");
        //errtext = "يرجى كتابة الايميل ";
        valdUser = false;
    } else if (!filter.test(email.val())) {
        showEmailValidUser("يرجى كتابة الايميل بطريقة صحيحة ");
        //errtext = "يرجى كتابة الايميل بطريقة صحيحة ";
        //email.focus;
        valdUser = false;
    } else {
        //فحص تكرار الايميل في قاعدة البيانات
        $("#loaderuser").show();
        $.ajax({
            type: "POST",
            url: "Methods.aspx/CheckEmailExist",
            data: "{'email':'" + email.val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (!data.d) {
                    showEmailValidUser("الايميل موجود سابقا ");
                    //spanvalidEmail.html("الايميل موجود سابقا ");
                    //email.addClass("v_error");
                    valdUser = false;
                } else {
                    showEmailValidUser("");
                    valdUser = true;
                    //email.removeClass("v_error");
                }
                $("#loaderuser").hide();
            }
        });
    }
}

function showEmailValidUser(text) {
    if (text == "")
        $("#txtRegUser").removeClass("v_error");
    else
        $("#txtRegUser").addClass("v_error");

    $("#MsgEmailRegistrar").html(text);
}

function CheckPassUser() {
    var Pass = $("#txtPassUser");
    var SpnViledPassUser = $("#MsgPassUser");
   // SpnViledPassUser.css({ 'display': 'block' });

    if (Pass.val() == "") {
        Pass.addClass("v_error");
        SpnViledPassUser.html("يرجى كتابة كلمة المرور ");
        //Pass.focus;
        valdUser = false;
    }

    if (valdUser) {
        SpnViledPassUser.html(" ");
        Pass.removeClass("v_error");
    }
}

function CheckRePass() {
    var RePass = $("#txtRePassUser");
    var SpnViledRePass = $("#MsgRePassUser");

    //SpnViledRePass.css({ 'display': 'block' });
    if (RePass.val() == "") {
        SpnViledRePass.html("يرجى اعادة كلمة المرور ");
        //RePass.focus;
        valdUser = false;
    } else
        SpnViledRePass.html("");

    if (valdUser) {
        SpnViledRePass.html("");

    }
    return valdUser;
}
function CheckAgreeUser() {
    var isChecked = $('#chkGreeting').is(':checked');
    var SpnViledvalidate = $("#MsgRePassUser");
    //SpnViledvalidate.css({ 'display': 'block' });

    if (isChecked)
        SpnViledvalidate.html("");

    else {
        SpnViledvalidate.html("يرجى  الموافقة  ");
        valdUser = false;
    }

    if (valdUser) SpnViledvalidate.html("");

}
// مقارنة اعادة كلمة المرور مع كتابة كلمة المرور الاصلية
function checkPasswordMatch() {
    var password = $("#txtPassUser").val();
    var confirmPassword = $("#txtRePassUser").val();
    var SpnViledMatchPss = $("#MsgRePassUser");
    SpnViledMatchPss.css({ 'display': 'block' });
    if (password != confirmPassword) {
        SpnViledMatchPss.html("كلمة المرور ليست متطابقة ");
        valdUser = false;
    }
    else
        SpnViledMatchPss.html("");
    if (valdUser) SpnViledMatchPss.html("");
}

function checkUserRegister() {
    checkEmailRegistar();
    CheckPassUser();
    if (CheckRePass());
    CheckAgreeUser();
    checkPasswordMatch();
    //        CheckRePass();
    CheckAgreeUser();
    //      
    if (valdUser) return true;
    return false;

}


