$(document).ready(function () {
    var sub = $("#shopping_list");
    var root = $("#shopping a");
    var curractv;

    /*$("body").click(function(){
    $(".notifBox").fadeOut("fast");
    });
    */

    $('.notif').click(function () {//visibility
        $(".notifBox").hide();
        var name = $(this).attr("ID");

        //var m=$(this).next("div.notifBox");
        var div = $("." + name);
        //div = div.attr("id");

        curractv = div;
        if (div.css("display") == "none")
            div.fadeIn("fast");
        else
            div.fadeOut("fast");
    });

    $(".all_noty").mouseleave(function () {
        curractv.fadeOut("fast");
    });

    /*$('#shopping a').on('click',function(){
    sub.fadeIn("fast");
    if (flag) 
    else if (!flag) 
		
    flage=true;
    })
		
    $(this).off('click',function(){
    sub.fadeOut("fast");
		
    })
    */



});









/*
	
$(document).ready(function(){
var sub = $("#inbox_list");
var root = $("#inbox a");
				
$(root).hover(
function() {
sub.fadeIn("fast");
$("#inbox a").addClass("hvr");
return false;
},
function() {
sub.fadeOut("fast");
$("#inbox a").removeClass("hvr");
return false;
});
});
	
	
	
	
	
	
$(document).ready(function(){
var sub = $("#friend_list");
var root = $("#friend a");
						
$(root).hover(
function() {
sub.fadeIn("fast");
$("#friend a").addClass("hvr");
return false;
},
function() {
sub.fadeOut("fast");
$("#friend a").removeClass("hvr");
return false;
});
});
					
					
					
*/