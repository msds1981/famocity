/*$(".firstpane div.menu_head").click(function () {
$(this).next("span.menu_body").slideDown(300);
$(this).next("span.menu_body").css("display", "inline-block").siblings("span.menu_body").slideUp("slow");
});

$(".setting_box_form_title").click(function () {
$(this).children('.form_title_rowicon').css('background-position', '0px ' + v + 'px');

/*var v = $(this).attr("v");
	
if (v=="0") $(this).attr("v","-10");
else $(this).attr("v","0");
     
});
*/
var valdUser = false;
var valdUserAddress = false;
var valdPass = false;
var valdEmail = false;
var valdBirth = false;

function checkUser() {
    if (valdUser) return true;
    return false;
}

function checkAddress() {

    if (valdUserAddress) return true;
    return false;
}

function checkUserPass() {

    if (valdPass) return true;
    return false;
}

function checkEmail() {

    if (valdEmail) return true;
    return false;
}
function checkEmail() {

    if (valdBirth) return true;
    return false;
}

$(function () {
    $(".firstpane div.menu_head").live("click", function () {
        $(this).next("span.menu_body").slideDown(300);
        $(this).next("span.menu_body").css("display", "inline-block").siblings("span.menu_body").slideUp("slow");
    });

    $(".setting_box_form_title").live("click", function () {
        var $container = $(this).parent();
        $container.find('.menu_head .form_title_rowicon').removeClass('arrowup');
        $(this).find('.form_title_rowicon').addClass('arrowup');
    });
   
   
   
   
   
    $("#lnksavepersonal").live("click",function () {


        var fname = $("#txtfirstname").val();
        var lname = $("#txtlastname").val();

        if (fname == "") {
            checkValidMsg("sett_name_valid", "يرجى كتابة الاسم الاول", "");
            valdUser = false;
        } else if (lname == "") {
            checkValidMsg("sett_name_valid", "يرجى كتابة الاسم الاخير ", "");
            valdUser = false;
        } else {
            $("#lnksavepersonal").hide();
            $.ajax({
                type: "POST",
                url: "/Methods.aspx/SaveUserName",
                data: "{'firstname':'" + fname + "','lastname':'" + lname + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d > 0) {
                        $("#lnksavepersonal").show();
                        //   $(".form_lable_v").html("");
                        checkValidMsg("sett_name_valid", "تم الحفظ بنجاح ", "form_lable_green");
                        setTimeout('HideMsg()', 3000);
                    }

                    //  fullName();
                }

            });
        }
    });
    // ادخال العنوان
    $("#lnksavesec").live("click", function () {

        var city = $("#txtcity").val();
        var address = $("#txtaddreess").val();
        var cuntry = $("#selc_country").val();
        var nationality = $("#selc_nation").val();


        if (cuntry == "0") {
            checkValidMsg("sett_addre_valid", "يرجى اختيار الدولة ", "");
            valdUserAddress = false;

        } else {
            $("#lnksavesec").hide();
            $.ajax({
                type: "POST",
                url: "/Methods.aspx/SaveAddressUser",
                data: "{'address':'" + address + "','country':'" + cuntry + "','city':'" + city + "','nationality':'" + nationality + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d > 0) {
                        $("#lnksavesec").show();
                        checkValidMsg("sett_addre_valid", "تم الحفظ بنجاح ", "form_lable_green");
                        setTimeout('HideMsg()', 3000);
                    }
                }
            });
        }
    });

    //  حفظ الايميل و فحصه  

    $("#lnksaveemail").live("click", function () {
        var TEmail = $("#txtemail").val();

        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (TEmail == "") {
            checkValidMsg("sett_email_valid", "يرجى ادخال الايميل ", "");
            valdEmail = false;
        }
        else if (!filter.test(TEmail)) {
            checkValidMsg("sett_email_valid", "يرجى ادخال الايميل بطريقة صحيحة", "");
            TEmail.focus;
            valdEmail = false;
        } else {
            $("#lnksaveemail").hide();
            $.ajax({
                type: "POST",
                url: "/Methods.aspx/SaveEmail",
                data: "{'email':'" + TEmail + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        checkValidMsg("sett_email_valid", "لم يتم الحفظ لوجود خطأ ، قد يكون الايميل موجود", "");
                        valdEmail = false;
                        $("#lnksaveemail").show();
                    } else {
                        valdEmail = true;
                        $("#lnksaveemail").show();
                        checkValidMsg("sett_email_valid", "تم الحفظ بنجاح ", "form_lable_green");
                        setTimeout('HideMsg()', 3000);
                    }
                }
            });
        }
    });

    // حفظ معلومات الوظيفة 
    // الوظيفة
    $("#lnksavejobs").live("click", function () {

        var JobName = $("#txtjob").val();
        var ComanyName = $("#txtcompany").val();
        var College = $("#txtcollage").val();

        $("#lnksavejobs").hide();
        $.ajax({
            type: "POST",
            url: "/Methods.aspx/SaveJopInfo",
            data: "{'jop':'" + JobName + "','companyName':'" + ComanyName + "','college':'" + College + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d > 0) {
                    //  valdUser = true;
                    checkValidMsg("sett_jobs_valid", "تم الحفظ بنجاح ", "form_lable_green");
                    setTimeout('HideMsg()', 3000);
                    $("#lnksavejobs").show();

                }
                else {
                    checkValidMsg("sett_jobs_valid", "لم يتم حفظ البيانات ", "");
                    $("#lnksavejobs").show();
                }
            }
        });
    });

    // عني
    $("#lnksaveabout").live("click", function () {

        var about = $("#txtaboutme").val();

        $("#lnksaveabout").hide();
        $.ajax({
            type: "POST",
            url: "/Methods.aspx/SaveJopAbout",
            data: "{'about':'" + about + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d > 0) {

                    checkValidMsg("sett_aboutme_valid", "تم الحفظ بنجاح ", "form_lable_green");
                    $("#lnksaveabout").show();
                    setTimeout('HideMsg()', 3000);
                }
            }
        });
    });


    //  حفظ كلمة المرور للمستخدم 
    $("#lnksavepass").live("click", function () {
        var TCurrPss = $("#txtoldpassword").val();
        var TNewPss = $("#txtnewpassword").val();
        var TReapPss = $("#txtrenewpassword").val();

        if (TCurrPss == "") {

            checkValidMsg("sett_pass_valid", "يرجى ادخال كلمة المرور القديمة ", "");
            valdPass = false;
        }
        else if (TNewPss == "") {

            checkValidMsg("sett_pass_valid", "يرجى ادخال كلمة المرور الجديدة ", "");
            valdPass = false;
        }
        else if (TReapPss != TNewPss) {

            checkValidMsg("sett_pass_valid", "يرجى اعادة كلمة المرور ", "");
            valdPass = false;
        }
        else if (TReapPss != TNewPss) {

            checkValidMsg("sett_pass_valid", "كلمة المرور ليست متطابقة ", "");
            $("#lnksavepass").show();
            valdPass = false;

        }
        else {
            $("#lnksavepass").hide();

            $.ajax({
                type: "POST",
                url: "/Methods.aspx/SavePass",
                data: "{'currpass':'" + TCurrPss + "','newpass':'" + TNewPss + "','reapetpass':'" + TReapPss + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d > 0) {

                        $("#lnksavepass").show();
                        checkValidMsg("sett_pass_valid", " تم تعديل كلمة المرور بنجاح ", "form_lable_green");
                        $("#txtoldpassword").val("");
                        $("#txtnewpassword").val("");
                        $("#txtrenewpassword").val("");
                        setTimeout('HideMsg()', 3000);

                    } else {

                        $("#lnksavepass").show();
                        checkValidMsg("sett_pass_valid", " .هناك خطا في ادخال البيانات بطريقة صحيحة.لم يتم التعديل  ", "");
                    }
                }
            });
        }
    });

    // حفظ تاريخ الميلاد 
    $("#lnksavebirth").live("click", function () {

        var Day = $("#bdate_select_days").val();
        var Month = $("#bdate_select_months").val();
        var Year = $("#bdate_select_years").val();


        if (Day == "0") {
            checkValidMsg("sett_birth_valid", "يرجى اختياراليوم ", "");
            valdBirth = false;

        } else if (Month == "0") {
            checkValidMsg("sett_birth_valid", "يرجى اختيارالشهر ", "");
            valdBirth = false;

        } else if (Year == "0") {
            checkValidMsg("sett_birth_valid", "يرجى اختيارالسنة ", "");
            valdBirth = false;
        } else {
            $("#lnksavebirth").hide();
            $.ajax({
                type: "POST",
                url: "/Methods.aspx/SaveBithDate",
                data: "{'day':'" + Day + "','month':'" + Month + "','year':'" + Year + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d > 0) {
                        $("#lnksavebirth").show();
                        checkValidMsg("sett_birth_valid", "تم الحفظ بنجاح ", "form_lable_green");
                        setTimeout('HideMsg()', 3000);
                    }
                }
            });
        }
    });


    // اسم المستخدم
    $("#lnksaveuname").live("click", function () {


        var Username = $("#txtUserName").val();
        var username_regex = /^[\w]+$/; // allowed characters: any word .-, ( \w ) represents any word character (letters, digits, and the underscore _ ), equivalent to [a-zA-Z0-9_]

        if (Username == "") {
            checkValidMsg("sett_uname_valid", "يرجى كتابة اسم المستخدم", "");
            valdUserName = false;
        } else if (!username_regex.test(Username)) {
            checkValidMsg("sett_uname_valid", "يرجى ادخال اسم المستخدم بتنسيق صحيح .. مرفوض الكتابة بالعربي او عمل (.،-،:) ", "");
            Username.focus;
            valdUserName = false;

        } else {
            $("#lnksaveuname").hide();
            $.ajax({
                type: "POST",
                url: "/Methods.aspx/SaveUser",
                data: "{'User':'" + Username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d > 0) {
                        checkValidMsg("sett_uname_valid", "لم يتم الحفظ لوجود خطأ ، قد يكون اسم المستخدم مسجل سابقا", "");
                        valdUserName = false;
                        $("#lnksaveuname").show();
                    } else {
                        valdUserName = true;
                        $("#lnksaveuname").show();
                        checkValidMsg("sett_uname_valid", "تم الحفظ بنجاح ", "form_lable_green");
                        setTimeout('HideMsg()', 3000);
                    }

                }

            });
        }
    });


    // حفظ رقم التلفون 
    $("#lnksaveMobile").live("click", function () {

        var Phone = $("#txtMobile").val();
        var num_regex = /^\d+$/;

        if (Phone == "") {
            checkValidMsg("sett_Phone_valid", "يرجى ادخال رقم التلفون ", "");
            valdPhone = false;
        }
        else if (!num_regex.test(Phone)) {
            checkValidMsg("sett_Phone_valid", "يرجى ادخال ارقام فقط", "");
            Phone.focus;
            valdPhone = false;

        } else if (Phone.length < 8) {
            checkValidMsg("sett_Phone_valid", "رقم التلفون يجب ان يكون 8 او اكثر ", "");
            Phone.focus;
            valdPhone = false;
        }

        else {
            $("#lnksaveMobile").hide();
            $.ajax({
                type: "POST",
                url: "/Methods.aspx/SavePhone",
                data: "{'Phone':'" + Phone + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d > 0) {
                        checkValidMsg("sett_Phone_valid", "لم يتم الحفظ لوجود خطأ ، قد يكون رقم التلفون مسجل سابقا", "");
                        valdPhone = false;
                        $("#lnksaveMobile").show();
                    } else {
                        valdPhone = true;
                        $("#lnksaveMobile").show();
                        checkValidMsg("sett_Phone_valid", "تم الحفظ بنجاح ", "form_lable_green");
                        setTimeout('HideMsg()', 3000);
                    }
                }
            });
        }
    });

    // validation :password_regex = /^[A-Za-z\d]{6,8}$/;   لاجبار اسم المستخدم ادخال كلمة المرور محصورة بين 6 الى 7 ارقام او حروف كييرة او صغيرة 

    /* Check that the input contains only numeric characters
    if ( input.match(num_regex) ) {
    ok = true;	
    }
    }*/
});
function checkValidMsg(idvalid, msg, greenclass) {

    $("#" + idvalid).html("<div class='form_lable_v " + greenclass + "'>" + msg + "</div>");
}

function HideMsg() {
    $(".form_lable_green").fadeOut('slow');
}



