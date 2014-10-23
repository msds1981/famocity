var issOppenedd;
var ccurrentlisttt;
$(document).ready(function () {
    $(document).click(function () {
        //alert(issOppenedd + "");
        if (issOppenedd == true) {
            issOppenedd = false;
        }		
        else {
            issOppenedd = false;
            $(".drpdwn_topic").hide();
            ccurrentlisttt = "";
        }	
    });

	
	

    $(".topic-option").live('click', function () {
        var llaastnamee = $(this).attr("eevntt");
		
        if (ccurrentlisttt == llaastnamee) {
            ClearLists();
            return;
        }
        OOpenListtt(llaastnamee);
    });



    function ClearLists() {
        $(".drpdwn_topic").hide();
        ccurrentlisttt = "";
    }

    function OOpenListtt(llaastnamee) {
        ccurrentlisttt = llaastnamee;
        issOppenedd = true;
        $(".drpdwn_topic").hide();
        $("#"+llaastnamee).toggle();
    }
	
});



