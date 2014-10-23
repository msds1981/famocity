var issOOpenedd;
var ccuurrentlistt;
$(document).ready(function () {

    $(document).click(function () {



        /************ options ************/

        //alert(issOOpenedd + "");
        if (issOOpenedd == true) {
            issOOpenedd = false;
        }
        else {
            issOOpenedd = false;
            $(".drpdwn_photos").hide();
            ccuurrentlistt = "";
        }
    });




    $(".photos-option").live('click', function () {
        var llaastnameee = $(this).attr("eevvntt");

        if (ccuurrentlistt == llaastnameee) {
            ClearLists();
            return;
        }
        OOpenListt(llaastnameee);
    });



    function ClearLists() {
        $(".drpdwn_photos").hide();
        ccuurrentlistt = "";
    }

    function OOpenListt(llaastnameee) {
        ccuurrentlistt = llaastnameee;
        issOOpenedd = true;
        $(".drpdwn_photos").hide();
        $("#" + llaastnameee).toggle();
    }

});



