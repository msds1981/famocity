<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpMain.Master" AutoEventWireup="true"
    CodeBehind="shopMainProducts.aspx.cs" Inherits="FamoCity.shopMainProducts" %>

<%@ Register Src="userControls/ucShopLogo.ascx" TagName="ucShopLogo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="/js/jquery-1.10.1.min.js"></script>--%>
    <%--<script src="/js/libs/jquery.js"></script>--%>
    <%--<script src="/js/respond.min.js"></script>--%>
    <link rel="stylesheet" href="/css/framewarp.css" />
    <%--<script src="http://code.jquery.com/jquery-latest.min.js"></script>--%>
    <script>
        $(document).ready(function () {
            $(window).scroll(function () {
                if ($('body').height() <= ($(window).height() + $(window).scrollTop())) {
                    $.ajax({
                        type: "POST",
                        data: "",
                        url: '/shopMainProducts.aspx/updatePage',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            $(".box:last").after(data.d);
                        },
                        error: function (msg) {
                            //alert("error: " + msg);
                        }
                    });
                }
            });


            //display product
            $("").click(function () { 
                
            });
        });
    </script>

<!-- Pop Up -->
<script type="text/javascript" src="/js/popup.js"></script>
<link rel="stylesheet" href="/css/pop_up.css" />

        <link rel="stylesheet" type="text/css" href="/css/sliding-image-gallery-v2.css" />
		<!--Image Gallery-->
		<script type="text/javascript" src="/js/jquery.sliding-image-gallery-v2.js"></script>
		<!--Preview - Remove it-->
		<script type="text/javascript" src="/js/preview.js"></script>
		<!--Initialize-->
		<script type="text/javascript">
		    $(window).load(function () {
		        $('.banner').slidingImageGalleryV2({
		            buttonPosition: "right",
		            pauseRollOver: true,
		            transition: "random"
		        });
		    }); 
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- START HEADER SEPERATOR -->
    <div id="header_seperator">
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
                    <!-- Products Container Start -->
                    <div id="products_container">
                        <div class="container">
                            <!-- Box START -->
                            <asp:Literal ID="ltrBoxes" runat="server"></asp:Literal>
                            <!--Box END -->
                        </div>
                        <!-- Products container End-->
                    </div>
                </div>
                <!-- -->
                <!-- Left Sidebar End -->
            </div>
            <!-- Right Sidebar Start -->
            <div id="right_sidebar">
                <div class="master_title">
                </div>
                <!-- INSIDE RIGHT SIDE BAR START -->
                <div id="inside_right_sidebar">
                    <!-- COMPANY LOGO START -->
                    <uc1:ucShopLogo ID="ucShopLogo1" runat="server" />
                    <!-- COMPANY LOGO END -->
                    <!-- SEARCH SECTION START -->
                    <div id="search_section">
                        <input type="text" value="اكتب كلمة البحث" name="search2" class="search2">
                        <span>
                            <input type="submit" name="submit" class="submit_search2" value="ابحث">
                        </span>
                    </div>
                    <!-- SEARCH SECTION END -->
                    <!-- First Result Start -->
                    <asp:Literal ID="ltrSideBar" runat="server"></asp:Literal>
                    <!-- First Result End -->
                    <!-- First Result Start -->
                    <!-- First Result End -->
                    <!-- First Result Start -->
                    <!-- First Result End -->
                </div>
                <!-- INSIDE RIGHT SIDE BAR END -->
                <!-- Right Sidebar End -->
            </div>
            <!-- MAIN CONTENT END -->
        </div>
        <div class="clear">
        </div>
    </div>
    <!-- Pop up hidden start -->
    <div id="toPopup">
        <div class="close">
            <img src="/images/close.png" /></div>
        <div id="popup_content">
            
        </div>
        <!--your content end-->
    </div>
    <!-- pop up hidden end-->
    <!-- Box Pop Up -->
    <script src="/js/jquerypp.custom.js"></script>
    <script src="/js/framewarp.js"></script>
    <script src="/js/script.js"></script>
    <!-- Gallery Content  -->
    <script src="/js/libs/jquery.masonry.min.js"></script>
    <script src="/js/base.js" type="text/javascript"></script>
</asp:Content>
