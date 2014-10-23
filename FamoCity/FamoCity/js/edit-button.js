$(document).ready(function () {
    $(".detailDiv").hide();

    $('.edit_button').click(function () {
        //$(".detailDiv").hide();
        $(".detailDiv").slideUp();
        var p = $(this).parent(".context");
        p.find(".detailDiv").slideToggle();

        //$(".detailDiv").slideToggle();
    });

    $('.btn_cancel').click(function () {
        $(".detailDiv").slideToggle();
    });

});