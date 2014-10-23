var issOpened;
var currentlistt;
$(document).ready(function () {

    
     $(document).click(function () {
        //alert(issOpened + "");
        if (issOpened == true) {
            issOpened = false;
        }		
        else {
            issOpened = false;
            $(".drpdwn_toolbar").hide();
            currentlistt = "";
        }	
    });

	
	

    $(".tool-option").on('click', function () {
        var laastname = $(this).attr("evnt");
		
        if (currentlistt == laastname) {
            ClearLists();
            return;
        }
        OpenListt(laastname);
    });



    function ClearLists() {
        $(".drpdwn_toolbar").hide();
        currentlistt = "";
    }

    function OpenListt(laastname) {
        currentlistt = laastname;
        issOpened = true;
        $(".drpdwn_toolbar").hide();
        $("#"+laastname).toggle();
    }
	
});



