// JavaScript Document
$(document).ready(function(){
	
	//default settings
	
	$("#tabs ul li:eq(0)").siblings().hide();
	
	//end default
	
	$("#sidebarright ul li").click(function(){
		
		var val = $(this).index();
	
		showthis(val+1);
		
		});

function showthis(val){
	
		var btn_index = val-1;
		
		//sidebar buttons
		$("#sidebarright ul li:eq("+btn_index+")").siblings().removeClass("active");
		$("#sidebarright ul li:eq("+btn_index+")").addClass("active");
		//end sidebar buttons
		
		//tab contents 
		$("#tabs ul li:eq("+btn_index+")").show();
		$("#tabs ul li:eq("+btn_index+")").siblings().hide();
		//$("#tab"+val).show();
	
		}// end if
	
	});