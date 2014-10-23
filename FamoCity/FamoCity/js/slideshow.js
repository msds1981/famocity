$(document).ready(function(){

$("#sliding").zAccordion({
	slideWidth: '60%',
	width: '100%',
	height: "250px",
	timeout: 2000
});
$("#start").click(function() {
	$("#sliding").zAccordion("start");
	$(this).css("display", "none");
	$("#stop").css("display", "block");
	return false;
});
$("#stop").click(function() {
	$("#sliding").zAccordion("stop");
	$(this).css("display", "none");
	$("#start").css("display", "block");
	return false;
});
$("#thumbs a").click(function() {
	$("#sliding").zAccordion("trigger", $(this).parent().index());
	return false;
});


});