var isOpened;
var currentlist;
var listdown;
$(document).ready(function () {
    $(document).click(function () {
        //alert("document");
        if (isOpened == true) {
            isOpened = false;
            //alert("isOpened = fasle");
        }
        else {
            isOpened = false;
            listdown = false;
            $(".listdown").hide();
            currentlist = "";
        }
    });

    $(".friends_noti").on('click', function () {
        //alert("noti");
        if (listdown) return;//exit when clicked on list
        var lstname = ".drpdwn_friends";
        if (currentlist == lstname) {
            ClearLists();
            return;
        }
        OpenList(lstname);

    });

    $(".alerts_noti").on('click', function () {
        if (listdown) return; //exit when clicked on list
        var lstname = ".drpdwn_alerts";
        if (currentlist == lstname) {
            ClearLists();
            return;
        }
        OpenList(lstname);
    });
        

    $(".msgs_noti").on('click', function () {
        if (listdown) return; //exit when clicked on list
        var lstname = ".drpdwn_msgs";
        if (currentlist == lstname) {
            ClearLists();
            return;
        }
        OpenList(lstname);
    });


    $(".settings_noti").on('click', function () {
        if (listdown) return; //exit when clicked on list
        var lstname = ".drpdwn_settings";
        if (currentlist == lstname) {
            ClearLists();
            return;
        }
        OpenList(lstname);
    });

    function ClearLists() {
        $(".listdown").hide();
        currentlist = "";
    }

    function OpenList(lstname) {
        currentlist = lstname;
        isOpened = true;
        $(".listdown").hide();
        $(lstname).toggle();
    }

    $(".listdown").on('click', function () {
        // alert("isOpened = true");
        //alert("listdown");
        listdown = true;
        isOpened = true;
    });
});



