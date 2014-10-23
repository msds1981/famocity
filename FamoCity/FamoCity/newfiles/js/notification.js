var isOpened;
var currentlist;
$(document).ready(function () {
    $(document).click(function () {
        //alert(isOpened + "");
        if (isOpened == true) {
            isOpened = false;
        }
        else {
            isOpened = false;
            $(".listdown").fadeOut("normal");
            currentlist = "";
        }
		

		
    });

	
	

    $(".friends_noti").on('click', function () {
        var lstname = ".drpdwn_friends";
        if (currentlist == lstname) {
            ClearLists();
            return;
        }
        OpenList(lstname);

    });

    $(".alerts_noti").on('click', function () {
        var lstname = ".drpdwn_alerts";
        if (currentlist == lstname) {
            ClearLists();
            return;
        }
        OpenList(lstname);
    });


    $(".msgs_noti").on('click', function () {
        var lstname = ".drpdwn_msgs";
        if (currentlist == lstname) {
            ClearLists();
            return;
        }
        OpenList(lstname);
    });


    $(".settings_noti").on('click', function () {
        var lstname = ".drpdwn_settings";
        if (currentlist == lstname) {
            ClearLists();
            return;
        }
        OpenList(lstname);
    });
	
	
	

	

    function ClearLists() {
        $(".listdown").fadeOut("normal");
        currentlist = "";
    }

    function OpenList(lstname) {
        currentlist = lstname;
        isOpened = true;
        $(".listdown").fadeOut("normal");
        $(lstname).toggle("fast").fadeIn("normal");
    }
	
	
	
	
	
	

	


	
	

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	var toggle = false;
//$('.left_hidden_panel').css("display","none");
//$('.left_hidden_panel').css("width","0px");
$('.tab-menu').on('click',function () {

	
    if (toggle == false) {
		$('.left_hidden_panel').fadeIn("fast");//.css("display","block");
        $('.left_hidden_panel').stop().animate({
            'left': '0px',
			width :'260px'	
        },50);
		
		$('#page_content').stop().animate({
            'right': '-260px'	
        },50);
		//$('#Header_Center_Wrapper').stop().animate({
            //'right': '-260px'	
       // },50);
		
		$('body').css('max-height','1121px');
		
		
        toggle = true;
    } else {
		$('.left_hidden_panel').fadeOut("fast");//.css("display","none");

        $('#page_content').stop().animate({
            'right': '0px'
        },50);
       // $('#Header_Center_Wrapper').stop().animate({
        //    'right': '0px'
       // },50);
		
		$('.left_hidden_panel').stop().animate({
			'left':'0px',
            width: '0px'
        },50);
		$('body').css('max-height','100%');
			
		
        toggle = false;
    }
});
	
	
	
	
	
	
	

	
	

	//Tablet Friends Menu
	$('.grey-tab-menu ul').hide();	
	$('.grey-open-menu a').click(function(){
		$('.grey-tab-menu ul').fadeToggle("fast");
	});
	
	
	

	
	
	//Tablet Search Dispear then Apear Function
	$('.friend-tab-sear').hide();
	$('#friends-tab-search a').click(function(){
		//$('.friend-tab-sear').fadeToggle("fast");
		$('.friend-tab-sear').animate({
		opacity:1,
		height:"toggle",
		},100);
	});

	
	
	
	
	
	
	
});


//Tablet Friends Menu Close On Click Outside Menu Tablet
	$(document).click(function (e) {
    e.stopPropagation();
    var container = $(".grey-open-menu");

    //check if the clicked area is dropDown or not
    if (container.has(e.target).length === 0) {
        $('.grey-tab-menu ul').hide();
    }
});