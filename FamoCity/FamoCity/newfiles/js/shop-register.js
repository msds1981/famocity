//في حالة ان الايميل صحيح يتم تغيير القيمة الى صحيح

var valdEmlShp = false;

// تشيط الايميل للمتجر 
//فحص تكرار الايميل  بعد الخروج من المربع النصي
$(function () {
    $("#txtEmailShop").blur(function () {
        checkEmailShop();
    });

    // تشيط كلمة مرور المتجر
    $("#TxtPassShop").blur(function () {
        CheckPasswordShop();
    });

    // تشيك اعادة كتابة كلمة المرور للمتجر
    $("#TxtRePassShop").blur(function () {
        CheckRePasswordShop();
        checkPasswordMatch();
    });

    // تشيك اسم المتجر 
    $("#TxtNameShop").blur(function () {
        CheckNameShop();
    });

});

// مقارنة اعادة كلمة المرور مع كتابة كلمة المرور الاصلية
function checkPasswordMatch() {
    var password = $("#TxtPassShop").val();
    var confirmPassword = $("#TxtRePassShop").val();
    var SpnViledMatchPss = $("#MsRepass");
    SpnViledMatchPss.css({ 'display': 'block' });
    if (password != confirmPassword) {
        SpnViledMatchPss.html("كلمة المرور ليست متطابقة ");
        valdEmlShp = false;
    }
    else
        SpnViledMatchPss.html(" ");
    if (valdEmlShp) SpnViledMatchPss.html("");
}

function checkEmailShop() {
    var email = $("#txtEmailShop"); // document.getElementsByName('txtEmailShop');
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var spanvalidEmail = $("#MsEmail");

    spanvalidEmail.css({ 'display': 'block' });
    if (email.val() == "") {
        spanvalidEmail.html("يرجى كتابة الايميل ");
        valdEmlShp = false;
    } else if (!filter.test(email.val())) {
        spanvalidEmail.html("يرجى كتابة الايميل بطريقة صحيحة ");
        email.focus;
        valdEmlShp = false;
    } else {
        $.ajax({
            type: "POST",
            url: "Methods.aspx/CheckEmailExist",
            data: "{'email':'" + email.val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (!data.d) {
                    spanvalidEmail.html("الايميل موجود سابقا ");
                    valdEmlShp = false;
                } else
                    valdEmlShp = true;
            }
        });
    }
    if (valdEmlShp) spanvalidEmail.html("");
}

function CheckPasswordShop() {
    var Pass = $("#TxtPassShop");
    var SpnViledPass = $("#MsgPass");
    SpnViledPass.css({ 'display': 'block' });
    if (Pass.val() == "") {
        SpnViledPass.html("يرجى كتابة كلمة المرور ");
        Pass.focus;
        valdEmlShp = false;
    }
    if (valdEmlShp) SpnViledPass.html("");
}

function CheckRePasswordShop() {
    var RePass = $("#TxtRePassShop");
    var SpnViledRePass = $("#MsgRePassUser");

    SpnViledRePass.css({ 'display': 'block' });
    if (RePass.val() == "") {
        SpnViledRePass.html("يرجى اعادة كلمة المرور ");
        RePass.focus;
        valdEmlShp = false;
    } else
        SpnViledRePass.html(" ");

    if (valdEmlShp) SpnViledRePass.html("");
}

function CheckNameShop() {
    var ShopName = $("#TxtNameShop");
    var SpnViledShopName = $("#MsShopName");
    SpnViledShopName.css({ 'display': 'block' });
    if (ShopName.val() == "") {
        $("#MsShopName").html("يرجى ادخال اسم الشركة  ");
        ShopName.focus;
        valdEmlShp = false;
    }
    if (valdEmlShp) SpnViledShopName.html("");
    return valdEmlShp;
}

function CheckAgreeShop() {
    var isChecked = $('#ChkBoxShop').is(':checked');
    var SpnViledvalidate = $("#MsShopName");
    SpnViledvalidate.css({ 'display': 'block' });

    if (isChecked)
        SpnViledvalidate.html(" ");

    else {
        SpnViledvalidate.html("يرجى  الموافقة  ");
        valdEmlShp = false;
    }

    if (valdEmlShp) SpnViledMatchPss.html("");
}

function checkShopRegister() {
    //shop register click
    checkEmailShop();
    CheckPasswordShop();
    CheckRePasswordShop();
    checkPasswordMatch();
    if (CheckNameShop())
        CheckAgreeShop();
    if (valdEmlShp) return true;
    return false;
}