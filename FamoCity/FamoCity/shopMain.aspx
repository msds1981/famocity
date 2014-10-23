<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpMain.Master" AutoEventWireup="true"
    CodeBehind="shopMain.aspx.cs" Inherits="FamoCity.shopMain" %>

<%@ Register Src="userControls/ucShopLogo.ascx" TagName="ucShopLogo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/slidshow.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery.zaccordion.js"></script>
    <script src="/js/slideshow.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("pre.js").each(function (index) {
                eval($(this).text());
            });
        });

        function FlollowShop(shopid, userid) {
            
        }
   </script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDkz3C5Gd2H0cFFIEmuAQ-70MrtVljHPgI&sensor=false">
    </script>
    <script type="text/javascript">
        var map;
        var marker;
        var lat;
        var flat;
        var lng;
        var flng
        function initialize() {
            $.ajax({
                type: "POST",
                url: "/shopMain.aspx/getLocationLat",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    lat = data.d;
                    flat = parseFloat(lat);
                    $.ajax({
                        type: "POST",
                        url: "/shopMain.aspx/getLocationLng",
                        data: "{}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            lng = data.d;
                            flng = parseFloat(lng);
                            var position = new google.maps.LatLng(flat, flng);
                            var mapOptions = {
                                center: position,
                                zoom: 5,
                                mapTypeId: google.maps.MapTypeId.ROADMAP
                            };
                            map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
                            marker = new google.maps.Marker({
                                position: position,
                                map: map
                            });
                        }
                    });
                }
            });



        }
        google.maps.event.addDomListener(window, 'load', initialize);

    </script>
<style>
    /*
	@font-face {
	font-family: 'arbold';
	src: url('../newfont/bold/GE-SS-TWO-BOLD_0.eot') ;
	src: url('../newfont/bold/GE-SS-TWO-BOLD_0.eot?#iefix') format('embedded-opentype'), 
		 url('../newfont/bold/GE-SS-TWO-BOLD_0.woff') format('woff'), 
		 url('../newfont/bold/GE-SS-TWO-BOLD_0.ttf') format('truetype'), 
		 url('../newfont/bold/GE-SS-TWO-BOLD_0.svg#MHGOZ-Light') format('svg') ;
	font-weight: normal;
	font-style: normal;
	}
	
	@font-face {
	font-family: 'arlight';
	src: url('../newfont/light/GE-SS-TWO-LIGHT_0.eot') ;
	src: url('../newfont/light/GE-SS-TWO-LIGHT_0.eot?#iefix') format('embedded-opentype'), 
		 url('../newfont/light/GE-SS-TWO-LIGHT_0.woff') format('woff'), 
		 url('../newfont/light/GE-SS-TWO-LIGHT_0.ttf') format('truetype'), 
		 url('../newfont/light/GE-SS-TWO-LIGHT_0.svg#MHGOZ-Light') format('svg') ;
	font-weight: normal;
	font-style: normal;
	}
	
	@font-face {
	font-family: 'armedium';
	src: url('../newfont/medium/GE-SS-TWO-MEDIUM_0.eot') ;
	src: url('../newfont/medium/GE-SS-TWO-MEDIUM_0.eot?#iefix') format('embedded-opentype'), 
		 url('../newfont/medium/GE-SS-TWO-MEDIUM_0.woff') format('woff'), 
		 url('../newfont/medium/GE-SS-TWO-MEDIUM_0.ttf') format('truetype'), 
		 url('../newfont/medium/GE-SS-TWO-MEDIUM_0.svg#MHGOZ-Light') format('svg') ;
	font-weight: normal;
	font-style: normal;
	}
    */

* {font-family: 'arlight', Tahoma, Geneva, sans-serif;}


	/*body{ background-color:#CCC; padding:0; margin:0;}*/

	.shop_home_data{
		/*max-width:895px;*/
		min-width:483px;
		padding:10px;
		margin:0;
		/*border:solid 1px #eee;*/
	}
	.shop_big_box{
		width:68.9%;
		height:387px;
		background-color:#f2f2f2;
		float:left;
		
		-webkit-border-radius: 2px;
		-moz-border-radius: 2px;
		border-radius: 2px;
		-webkit-box-shadow: 0px 0px 5px rgba(124, 124, 124, 0.750);
		-moz-box-shadow: 0px 0px 5px rgba(124, 124, 124, 0.750);
		box-shadow: 0px 0px 5px rgba(124, 124, 124, 0.750);
	}
	.shop_big_box iframe{
		width:99%;
		height:350px;
		
		margin:0;
		padding:0.5%;
		border:none;
	}
	.shop_big_box span , .shop_small_box span{
		font-family:armedium;
		font-size:18px;
		color:#8f94a5;
		float:right;
		margin:4px;
		padding-right:10px;
		width:100%;
		text-align:right;
	}
	.shop_small_box div div{
		font-family:arlight;
		font-size:26px;
		color:#8f94a5;
		float:right;
		margin:4px;
		padding-right:10px;
		padding-top:14px;
		text-align:right;
	}
	.shop_small_box p{
		font-family:arlight;
		font-size:12px;
		color:#8f94a5;
		float:right;
		margin:4px;
		padding-right:10px;
		padding-top:14px;
		text-align:right;
	}
	.shop_all_small_box{
		width:30%;
		height:387px;
		float:left;
	}
	.shop_small_box{
		width:100%;
		height:123px;
		
		-webkit-border-radius: 2px;
		-moz-border-radius: 2px;
		border-radius: 2px;
		-webkit-box-shadow: 0px 0px 5px rgba(124, 124, 124, 0.550);
		-moz-box-shadow: 0px 0px 5px rgba(124, 124, 124, 0.550);
		box-shadow: 0px 0px 5px rgba(124, 124, 124, 0.550);
		
		background: #fefefe; /* Old browsers */
		background: -moz-linear-gradient(top,  #fefefe 0%, #ebebeb 100%); /* FF3.6+ */
		background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#fefefe), color-stop(100%,#ebebeb)); /* Chrome,Safari4+ */
		background: -webkit-linear-gradient(top,  #fefefe 0%,#ebebeb 100%); /* Chrome10+,Safari5.1+ */
		background: -o-linear-gradient(top,  #fefefe 0%,#ebebeb 100%); /* Opera 11.10+ */
		background: -ms-linear-gradient(top,  #fefefe 0%,#ebebeb 100%); /* IE10+ */
		background: linear-gradient(to bottom,  #fefefe 0%,#ebebeb 100%); /* W3C */
		filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#fefefe', endColorstr='#ebebeb',GradientType=0 ); /* IE6-9 */
	}
	.No_Content{ background-image:url(/images/shopmain/No_Content.png); background-repeat:no-repeat; background-position:center;}
	.shop_box_margin{ margin-right:1.1%;}
	.shop_box_margin_bottom{ margin-bottom:10px;}
	.shop_small_box_margin_bottom{ margin-bottom:9px;}
	.shop_clear_both{ clear:both;}
	.bg_zwar{ background-image:url(/images/shopmain/bg_zwar.png); background-repeat:no-repeat; width:100%; height:100%; background-size: 39%;
background-position: bottom left;}
	.bg_montg{ background-image:url(/images/shopmain/bg_montg.png); background-repeat:no-repeat; width:100%; height:100%; background-size: 39%;
background-position: bottom left;}
	.bg_fulo{ background-image:url(/images/shopmain/bg_fulo.png); background-repeat:no-repeat; width:100%; height:100%; background-size: 39%;
background-position: bottom left;}
	.bg_news{ background-image:url(/images/shopmain/bg_news.png); background-repeat:no-repeat; width:100%; height:100%; background-size: 39%;
background-position: bottom left;}
	.bg_3rod{ background-image:url(/images/shopmain/bg_3rod.png); background-repeat:no-repeat; width:100%; height:100%; background-size: 39%;
background-position: bottom left;}

	a:hover .shop_small_box span , a:hover .shop_small_box div , a:hover .shop_small_box p{ color:#93ae50;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- START HEADER SEPERATOR -->
    <div id="header_seperator">
        <!-- SLIDESHOW START -->
    </div>
    <div id="slide_show">
        <div class="gridContainer clearfix">
            <ul id="sliding">
                <asp:Literal ID="ltrBaner" runat="server"></asp:Literal>
            </ul>
        </div>
        <!-- SLIDESHOW END -->
    </div>
    <!-- END HEADER SEPERATOR -->
    <!-- GRID CONTAINER START -->
    <div class="gridContainer clearfix">
        <!-- MAIN CONTENT START -->
        <div id="main_content">
            <!-- Left Sidebar Start -->
            <div id="left_sidebar">
                <div id="left_content_master_title">
                   
                </div>
                <!-- -->
                <div id="inside_left_content">


                    <div class="shop_home_data">
	                    <div class="shop_big_box shop_box_margin shop_box_margin_bottom No_Content">
    	                    <span>فيديو اعلاني</span>
    	                    <%--<iframe  src="http://www.youtube.com/embed/ne8ZnJOMu9U?feature=player_detailpage" frameborder="0" allowfullscreen=""></iframe>--%>
                            <asp:Literal ID="ltrVideo" runat="server"></asp:Literal>
                        </div>
                        <div class="shop_all_small_box shop_box_margin_bottom">
                            <a href="#">
                                <div class="shop_small_box shop_small_box_margin_bottom">
                                    <div class="bg_zwar">
                                        <span>عدد الزوار</span>
                                        <div>
                        <asp:Literal ID="ltNumVist" runat="server"></asp:Literal></div>
                                    </div>
                                </div>
                            </a>
                            <a href="#">
                                <div class="shop_small_box shop_small_box_margin_bottom">
                                    <div class="bg_montg">
                                        <span>عدد المنتجات</span>
                                        <div>
                            <asp:Literal ID="ltCountProduct" runat="server"></asp:Literal></div>
                                    </div>
                                </div>
                            </a>
                            <a href="#">
                                <div class="shop_small_box">
                                    <div class="bg_fulo">
                                        <span>عدد المتابعين</span>
                                        <div>
                                <asp:Literal ID="ltFollow" runat="server"></asp:Literal></div>
                                    </div>
                                </div>
                            </a>
                        </div>
    
                        <div class="shop_all_small_box shop_box_margin">
    	                    <a href="#">
                                <div class="shop_small_box shop_small_box_margin_bottom">
                                    <div class="bg_news">
                                        <span>احدث الاخبار</span>
                                        <p>والمطر الاسود في عيني .. يتساقط زخات زخات ... يحملني معه يحملني .. لمساء وردي الشرفات </p>
                                    </div>
                                </div>
                            </a>
                            <a href="#">
                                <div class="shop_small_box shop_small_box_margin_bottom">
                                    <div class="bg_montg">
                                        <span>احدث المنتجات</span>
                                        <p>كم هي جميله هذه الكلمات و كم هو عميق هاذا اللحن احن اليه لكي ياخذني بعيدا عن متاعب هاذا الزمان الذي اشعرني بغربة المكان فلا ارى الا هلاكا ودمارا في بلادي</p>
                                    </div>
                                </div>
                            </a>
                            <a href="#">
    	                    <div class="shop_small_box">
        	                    <div class="bg_3rod">
                                    <span>احدث العروض</span>
                                    <p>كانت الاغنية التي رقصت عليها يوم عرسي ..قبل عشرين سنة..ولا تزال الاغنية التي احب ان ترفض عليها اليوم...</p>
                                </div>
                            </div>
                            </a>
                        </div>
                        <div class="shop_big_box No_Content">
                        <span>خارطة المعرض</span>
		                    <iframe  src="https://maps.google.com/?ie=UTF8&amp;ll=24.70941,46.756096&amp;spn=0.268223,0.528374&amp;t=m&amp;z=12&amp;output=embed"></iframe>
                        </div>
    
                        <div class="shop_clear_both"></div>
 
                    </div>



                </div>
                <!-- -->
                <!-- Left Sidebar End -->
            </div>
            <!-- Right Sidebar Start -->
            <div id="right_sidebar">
                <div id="master_title">
                    <!-- COMPANY LOGO START -->
                    <uc1:ucShopLogo ID="ucShopLogo1" runat="server" />
                    <!-- COMPANY LOGO END -->
                    <!-- First Result Start -->
                    <asp:Literal ID="ltrSideBar" runat="server"></asp:Literal>
                    <!-- First Result End -->
                    <!-- First Result Start -->
                    <!-- First Result End -->
                    <!-- First Result Start -->
                    <!-- First Result End -->
                </div>
                <!-- INSIDE RIGHT SIDE BAR START -->
                <div id="inside_right_sidebar">
                    <!-- -->
                    <section class="ac-container">
				<div>
					<input id="ac-1" name="accordion-1" type="radio" checked />
					<label for="ac-1">العنوان</label>
					<article class="ac-small">
						<p> <asp:Label ID="lblAddress" runat="server" Text="العليا حراج الكمبيوتر برج الخالدية مدخل 22 الدور الثاني"></asp:Label> </p>
					</article>
				</div>
				<div>
					<input id="ac-2" name="accordion-1" type="radio" />
					<label for="ac-2">رقم الجوال</label>
					<article class="ac-medium">
						<p> <asp:Label ID="lblPhone" runat="server" Text="+966 505376518"></asp:Label></p>
					</article>
				</div>
				<div>
					<input id="ac-3" name="accordion-1" type="radio" />
					<label for="ac-3">رقم الفاكس</label>
					<article class="ac-large">
						<p><asp:Label ID="lblFax" runat="server" Text="+966 505376518"></asp:Label></p>
					</article>
				</div>
				<div>
					<input id="ac-4" name="accordion-1" type="radio" />
					<label for="ac-4">عن الشركة</label>
					<article class="ac-large">
						<p> <asp:Label ID="lblAbout" runat="server" Text="اضافة نبذة مختصرة عن الشركة"></asp:Label> </p>
					</article>
				</div>
			</section>
                    <!-- -->
                </div>
                <!-- INSIDE RIGHT SIDE BAR END -->
                <div class="master_title">
                    عداد الزوار
                </div>
                <!-- INSIDE RIGHT SIDE BAR START -->
                <div id="inside_right_sidebar">
                    <!-- -->
                    <div id="counter_title">
                        Followers
                    </div>
                    <div id="counter_java">
                        <asp:Literal ID="ltrFollow" runat="server"></asp:Literal>
                        <%--<a href="javascript:void(0);" onclick="FollowShop();">
                            <asp:Label ID="lblFollowers" ClientIDMode="Static" runat="server" Text="4525.203"></asp:Label>
                        </a>--%>
                    </div>
                    <div>
                        <form id="form" runat="server">
                        <asp:Button ID="btnFollow" runat="server" Text="متابعة" class="follow_java" OnClick="btnFollow_Click" />
                        </form>
                    </div>
                    <!-- -->
                </div>
                <!-- INSIDE RIGHT SIDE BAR END -->
                <div class="master_title">
                    
                </div>
                <!-- INSIDE RIGHT SIDE BAR START -->
                <div id="inside_right_sidebar">
                    <!-- -->
                    <div class="video_show">
                        <%--<iframe id="vid" class="video_show_frame" width="286" height="200" src="http://www.youtube.com/embed/BK03wHkcaQA"
                            frameborder="0" runat="server"></iframe>--%>
                        <%--<asp:Literal ID="ltrVideo" runat="server"></asp:Literal>--%>
                    </div>
                    <!-- -->
                </div>
                <!-- INSIDE RIGHT SIDE BAR END -->
                <div class="master_title">
                   
                </div>
                <!-- INSIDE RIGHT SIDE BAR START -->
                <div id="inside_right_sidebar">
                    <!-- -->
                    <div id="google_map">
                        <div id="map-canvas" class="google_map_frame" style="height: 200px; width: 285px">
                        </div>
                    </div>
                    <!-- -->
                </div>
                <!-- INSIDE RIGHT SIDE BAR END -->
                <!-- Right Sidebar End -->
            </div>
            <!-- MAIN CONTENT END -->
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
