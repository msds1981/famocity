
function ActiveSubMnu(mnu, submnu) {
    //sub menu
    $(".lisub").each(function () {
        $(this).removeClass("sub_show");
    });
    $('#' + submnu).addClass('sub_show');
    //main menu
    $(".mainmenu").each(function () {
        if ($(this).attr('id') == mnu)
            $(this).addClass("current");
        else
            $(this).addClass("select");
    });
}
