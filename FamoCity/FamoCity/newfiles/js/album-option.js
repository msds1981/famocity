var isssOppenedd;
var ccurrenttlistt;
$(document).ready(function () {
    $(document).click(function () {
        //alert(isssOppenedd + "");
        if (isssOppenedd == true) {
            isssOppenedd = false;
        }		
        else {
            isssOppenedd = false;
            $(".drpdwn_album").hide();
            ccurrenttlistt = "";
        }	
    });




    $(".album-option").live('click', function () {
    //$(".album-option").click(function () {
        var laastnameee = $(this).attr("evvntt");
		
        if (ccurrenttlistt == laastnameee) {
            ClearLists();
            return;
        }
        OpenListt(laastnameee);
    });



    function ClearLists() {
        $(".drpdwn_album").hide();
        ccurrenttlistt = "";
    }

    function OpenListt(laastnameee) {
        ccurrenttlistt = laastnameee;
        isssOppenedd = true;
        $(".drpdwn_album").hide();
        $("#"+laastnameee).toggle();
    }
	
});



