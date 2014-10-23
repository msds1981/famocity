var issOppennedd;
var ccurreennttlistt;
$(document).ready(function () {
    $(document).click(function () {
        //alert(issOppennedd + "");
        if (issOppennedd == true) {
            issOppennedd = false;
        }		
        else {
            issOppennedd = false;
            $(".drpdwn_videos").hide();
            ccurreennttlistt = "";
        }	
    });

	
	

    $(".videos-option").on('click', function () {
        var llaasttnameee = $(this).attr("evvnnttt");
		
        if (ccurreennttlistt == llaasttnameee) {
            ClearLists();
            return;
        }
        OOppenListt(llaasttnameee);
    });



    function ClearLists() {
        $(".drpdwn_videos").hide();
        ccurreennttlistt = "";
    }

    function OOppenListt(llaasttnameee) {
        ccurreennttlistt = llaasttnameee;
        issOppennedd = true;
        $(".drpdwn_videos").hide();
        $("#"+llaasttnameee).toggle();
    }
	
});



