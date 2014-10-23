/* Created by Binadion on 7/17/14. */
/*
* Coding & Programming By : Binadion
* Twitter : http://twitter.com/Binadion_
* Website : http://Binadion.com/
*/

var or1, or2, or3; //open right
var cr1, cr2, cr3; //close right

function ReSetValues() { 
    //for responsive while cercles moving
    if ($(window).height() < 1200) {
        //tablet
        or1 = "30px"; or2 = "40px"; or3 = "40px";//open
        cr1 = "21px"; cr2 = "223px"; cr3 = "431px";//close
    } else {
        //normal
        or1 = "699px"; or2 = "710px"; or3 = "710px";//open
        cr1 = "710px"; cr2 = "918px"; cr3 = "1129px"; //close
    }

}
$(document).ready(function () {
    ReSetValues();


    $("section").css("min-height", $(window).height() - 145);


    // check availability of email
    $("#txtRegEmail").focusout(function () {
        if ($("#HFTypeButton").val() == "register")
            checkAvlilbilityEmail($(this));
    });



    $(window).resize(function () {
        $("section").css("min-height", $(window).height() - 109);
        ReSetValues();
    });

    $(".lang select").change(function () {
        window.location.assign($(this).val());
    });

    $(".opensign").click(function () {
        $(".c-content").fadeOut("slow", function () {
            $(".c-photo").animate({ paddingTop: 0, backgroundPosition: "31px 180px" });
            $(".opsign-img").animate({ width: "110px", height: "110px" }).animate({ top: "160px", right: or1, borderColor: "#91AD4A", borderWidth: "2px" });
            $(".goweb-img").animate({ width: "90px", height: "90px" }).animate({ top: "300px", right: or2 });
            $(".dngame-img").animate({ width: "90px", height: "90px" }).animate({ top: "420px", right: or3 }, function () {
                $(".form").fadeIn("fast", function () {
                    $(".close").fadeIn();
                });
            });
        });
    });

    $(".newuser").click(function () {
        $(".des-frgpass").slideUp();
        $(".logfrmsocial").slideUp();
        $(".avatar").animate({ right: "-125px" });
        $(".boxname").animate({ right: "30px" }, function () {
            $(".password").slideDown();
            $(".email").animate({ marginTop: "70px" }, function () {
                $(".born").slideDown();
                $(".left input[type=submit]").val("تسجيل");
                /* hessah
                modefi hiden filed of buttom
                */
                $("#HFTypeButton").val("register");
                //changeValidationGroup();


                $(".sex").slideDown();
                $(".regfrmsocial").slideDown();
                $(".chkon").slideUp();
                $(".right div").slideUp(500, function () {
                    $(".right .oploguser").fadeIn();
                    $(".right .oploguser a").animate({ marginTop: "7px" });
                });
            });
        });
    });

    $(".loguser").click(function () {
        $(".des-frgpass").slideUp();
        $(".born").slideUp();
        $(".sex").slideUp();
        $(".regfrmsocial").slideUp();
        $(".email").animate({ marginTop: "144px" });
        $(".password").slideDown();
        $(".left input[type=submit]").val("دخول");
        /* hessah
        modefi hiden filed of buttom*/
        $("#HFTypeButton").val("login");
        //changeValidationGroup();

        $(".boxname").animate({ right: "330px" }, function () {
            $(".avatar").animate({ right: "70px" }, function () {
                $(".right .oploguser").fadeOut(500, function () {
                    $(".chkon").slideDown();
                    $(".opfrtps").slideDown();
                    $(".opnwusr").slideDown();
                    $(".logfrmsocial").slideDown();
                });
            });
        });
    });

    $(".opfrtps").click(function () {
        $(".email").animate({ marginTop: "160px" });
        $(".avatar").animate({ right: "-125px" }, function () {
            $(".des-frgpass").slideDown(500, function () {
                $(".password").slideUp();
                $(".left input[type=submit]").val("إستعادة");
                /*saeed*/
                $("#HFTypeButton").val("fpass");
                //changeValidationGroup();


                $(".right .oploguser").fadeOut(500, function () {
                    $(".chkon").slideUp();
                    $(".opfrtps").slideUp();
                    $(".regfrmsocial").slideUp();
                    $(".right .oploguser").slideDown();
                    $(".right .oploguser a").animate({ marginTop: "0px" });
                    $(".opnwusr").slideDown();
                });
            });
        });
    });

    $(".male").click(function () {
        $(this).addClass("maleA");
        $(".female").removeClass("femaleA");
        $("input[name='sex']").attr("value", "male");
    });
    $(".female").click(function () {
        $(this).addClass("femaleA");
        $(".male").removeClass("maleA");
        $("input[name='sex']").attr("value", "female");
    });

    $(".close").click(function () {
        $(".form").fadeOut("slow", function () {
            $(".form").hide();
            $(".c-photo").animate({ paddingTop: "175px", backgroundPosition: "31px 180px" });
            $(".opsign-img").animate({ top: "180px", right: cr2, borderColor: "#91AD4A", borderWidth: "0px" }).animate({ width: "176px", height: "176px" });
            $(".goweb-img").animate({ top: "180px", right: cr1 }).animate({ width: "176px", height: "176px" });
            $(".dngame-img").animate({ top: "180px", right: cr3 }).animate({ width: "176px", height: "176px" }, function () {
                $(".c-content").fadeIn();
            });
        });
    });
});

function checkAvlilbilityEmail(email) {
    var errtext = " ";
    $("#emailAvail").hide();
    //فحص تكرار الايميل في قاعدة البيانات
    $("#loaderuser").show();
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/CheckEmailExist",
        data: "{'email':'" + email.val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (!data.d) {
                $("#emailAvail").show();
            } 
            $("#loaderuser").hide();
        },
        error: function () {
            $("#loaderuser").hide();
        }
    });
}


/*
created by saeed
this function for change validation group of button register,login and forgit password
*/
function changeValidationGroup() {
    var _ddlStatus = document.getElementById("HFTypeButton");
    var _selectedIndex = _ddlStatus.value;
    var btn = document.getElementById("btnRigister");
    var newValGroup;
    if (_selectedIndex == "login")
        newValGroup = "loginGrp";
    else if (_selectedIndex == "newuser")
        newValGroup = "regGrp";
    else if (_selectedIndex == "fpass")
        newValGroup = "fpasGrp";
    btn.onclick = function () {
        WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("btnRigister", "", true, "ssss", "", false, false));
    };
    //alert(newValGroup);
}
