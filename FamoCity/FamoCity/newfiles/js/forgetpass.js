var ResentEmail = false;


function ResentEmail() {

    if (valdResetnEmail) return true;
    return false;
}

function checkResentEmail() {
    var email = $("#txtReEmail");
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var spanvalidEmail = $("#msgResntEmail");

    spanvalidEmail.css({ 'display': 'block' });
    if (email.val() == "") {
        spanvalidEmail.html("يرجى كتابة الايميل ");

        valdUser = false;
     
    } else if (!filter.test(email.val())) {
        spanvalidEmail.html("يرجى كتابة الايميل بطريقة صحيحة ");
     
        email.focus;
        valdResetnEmail = false;
    } else {
        spanvalidEmail.html(" ");
        $("#Imgfpass").show();
        $.ajax({
            type: "POST",
            url: "Methods.aspx/SendEmailForUser",
            data: "{'email':'" + email.val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert("" + data.d);
                if (data.d) {
                    //$("#btnReEmail").addClass("flip");
                    email.val("");
                    flipsuccess();
                    valdUser = false;
                } else {
                    valdUser = true;
                    spanvalidEmail.html("الايميل غير مسجل");
                }
                $("#Imgfpass").hide();
            }
        });
    }
    if (valdResetnEmail) spanvalidEmail.html("");
}

function flipsuccess() {
    var aCls = $("#btnReEmail").attr("addcls");
    var currflip = "flipped4";
    // Flipping the forms
    $('.loginWrapper').toggleClass(aCls);
    if (currflip != aCls)
        $('.loginWrapper').removeClass(currflip);
    currflip = aCls;
}