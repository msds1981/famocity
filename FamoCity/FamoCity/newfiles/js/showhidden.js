$(document).ready(function(){


	$('.photo-top-caption').hide();
	$('.photo-bottom-caption').hide();



	$('#photos-section ul li').mouseenter(function(){
		
		$(this).children('.photo-top-caption').animate({
		height:"+=50"
		}, 100).show();
		
		
		$(this).children('.photo-bottom-caption').animate({
		height:"+=50"
		
		}, 100).show();
	});

	$('#photos-section ul li').mouseleave(function(){
		$(this).children('.photo-top-caption').animate({
		height:"-=50"
		}, 100).hide();
		
		$(this).children('.photo-bottom-caption').animate({
		height:"-=50"
		}, 100).hide();		
	});
});