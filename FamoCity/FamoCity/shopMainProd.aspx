<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopMainProd.aspx.cs" Inherits="FamoCity.shopMainProd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Message Embed Page</title>
    <!-- The stylesheet -->
    <link rel="stylesheet" href="/css/pop_up.css" />
    <!--[if lt IE 9]>
          <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->
    <link rel="stylesheet" type="text/css" href="/css/global.css" />
    <link rel="stylesheet" type="text/css" href="/css/sliding-image-gallery-v2.css" />
    <!--jQuery-->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.0/jquery-ui.min.js"></script>
    <!--Image Gallery-->
    <script type="text/javascript" src="/js/jquery.sliding-image-gallery-v2.js"></script>
    <!--Preview - Remove it-->
    <script type="text/javascript" src="/js/preview.js"></script>
    <!--Initialize-->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.banner').slidingImageGalleryV2({
                buttonPosition: "right",
                pauseRollOver: true,
                transition: "random"
            });
        });
    </script>
    <!-- comment option icon drop menu-->
    <%--<script type="text/javascript" src="js/v1.js"></script>--%>
</head>
<body class="framePage">
    <form id="form1" runat="server">
    <!-- BANNER CONTAINER -->
    <div class="banner-container">
        <div class="inner">
            <!--
				#######################################
					- IMAGE GALLERY / START -
				#######################################
				-->
            <div class="banner">
                <div class="screen">
                    <noscript>
                        <!--Placeholder Image When Javascript is Off-->
                        <img src="/images/popup/1.jpg" alt="" />
                    </noscript>
                </div>
                <div class="items">
                    <ul>
                        <asp:Literal ID="ltrBaner" runat="server"></asp:Literal>
                       <%-- <li>
                            <div class="button">
                                <img src="images/thumb/1.jpg" /><p>
                                    <span class="title">Autumn Leaf Way</span></p>
                            </div>
                            <a href="images/popup/1.png"></a><a href="http://www.codegrape.com/" target="_blank">
                            </a></li>
                        <li>
                            <div class="button">
                                <img src="images/thumb/2.jpg" /><p>
                                    <span class="title">Autumn Leaf Way</span></p>
                            </div>
                            <a href="images/popup/2.png"></a><a href="http://www.codegrape.com/" target="_blank">
                            </a></li>
                        <li>
                            <div class="button">
                                <img src="images/thumb/3.jpg" /><p>
                                    <span class="title">Autumn Leaf Way</span></p>
                            </div>
                            <a href="images/popup/3.png"></a><a href="http://www.codegrape.com/" target="_blank">
                            </a></li>
                        <li>
                            <div class="button">
                                <img src="images/thumb/4.jpg" /><p>
                                    <span class="title">Autumn Leaf Way</span></p>
                            </div>
                            <a href="images/popup/4.png"></a><a href="http://www.codegrape.com/" target="_blank">
                            </a></li>
                        <li>
                            <div class="button">
                                <img src="images/thumb/5.jpg" /><p>
                                    <span class="title">Autumn Leaf Way</span></p>
                            </div>
                            <a href="images/popup/5.png"></a><a href="http://www.codegrape.com/" target="_blank">
                            </a></li>--%>
                    </ul>
                </div>
            </div>
            <!--
				#######################################
					- IMAGE GALLERY / END -
				#######################################
				-->
        </div>
    </div>
    <!--
				#######################################
					- COMMENTS & ICONS WRAPPER -
				#######################################
				-->
    <!-- WRAPPER START -->
    <div id="comments_icons_wrapper">
        <!-- COMMENTS START -->
        <div id="comments">
            <div class="comments_glow">
                <div class="add_comment_title">
                    اضافة تعليق</div>
                <div class="comment_area">
                    <textarea class="comment_textarea"> اكتب تعليق ... </textarea>
                </div>
                <div class="send_comment">
                    <div class="author_picture">
                        <img src="/images/amrdiab.jpg" /></div>
                    <input type="submit" value="ارسال" class="send_comment_button" />
                </div>
                <div class="clr">
                </div>
                <div class="show_comments">
                    <div class="comment_all">
                        <ul class="comment">
                            <!-- COMMENT START -->
                            <div class="seperated_comment">
                                <li><span>
                                    <div class="comment_content">
                                        <div class="author_name">
                                            عمرو دياب</div>
                                        <!-- -->
                                        <div class="comment_option">
                                            <nav id="colorNav">
			<ul>
				<li class="purple">
					<a href="#" class="icon-envelope"><img src="/images/settings.png" /></a>
					<ul>
						<li><a href="http://tutorialzine.com/contact/">Add</a></li>
                        <li><a href="http://tutorialzine.com/contact/">Delete</a></li>
                        <li><a href="http://tutorialzine.com/contact/">Invite Friend</a></li>
					</ul>
				</li>
			</ul>
		</nav>
                                        </div>
                                        <!-- -->
                                        <div class="author_comment">
                                            يابنى هتلعب بلاى ستيشن ولا مش هتلعب وكلمنى على الواتس أب دلوقتى لو هتيجي يابنى هتلعب
                                            بلاى ستيشن ولا مش هتلعب وكلمنى على الواتس أب دلوقتى لو هتيجي يابنى هتلعب بلاى ستيشن
                                            <div class="comment_time">
                                                منذ 10 ساعات وخمس دقائق<span> </span>
                                            </div>
                                        </div>
                                    </div>
                                </span>
                                    <div class="comment_photo">
                                        <img src="/images/amrdiab.jpg" /></div>
                                </li>
                                <!-- COMMENT END -->
                            </div>
                            <!-- COMMENT START -->
                            <div class="seperated_comment">
                                <li><span>
                                    <div class="comment_content">
                                        <div class="author_name">
                                            عمرو دياب</div>
                                        <!-- -->
                                        <div class="comment_option">
                                            <nav id="colorNav">
			<ul>
				<li class="purple">
					<a href="#" class="icon-envelope"><img src="/images/settings.png" /></a>
					<ul>
						<li><a href="http://tutorialzine.com/contact/">Add</a></li>
                        <li><a href="http://tutorialzine.com/contact/">Delete</a></li>
                        <li><a href="http://tutorialzine.com/contact/">Invite Friend</a></li>
					</ul>
				</li>
			</ul>
		</nav>
                                        </div>
                                        <!-- -->
                                        <div class="author_comment">
                                            يابنى هتلعب بلاى ستيشن ولا مش هتلعب وكلمنى على الواتس أب دلوقتى لو هتيجي يابنى هتلعب
                                            بلاى ستيشن ولا مش هتلعب وكلمنى على الواتس أب دلوقتى لو هتيجي يابنى هتلعب بلاى ستيشن
                                            <div class="comment_time">
                                                منذ 10 ساعات وخمس دقائق<span> </span>
                                            </div>
                                        </div>
                                    </div>
                                </span>
                                    <div class="comment_photo">
                                        <img src="/images/amrdiab.jpg" /></div>
                                </li>
                                <!-- COMMENT END -->
                            </div>
                            <!-- COMMENT START -->
                            <div class="seperated_comment">
                                <li><span>
                                    <div class="comment_content">
                                        <div class="author_name">
                                            عمرو دياب</div>
                                        <!-- -->
                                        <div class="comment_option">
                                            <nav id="colorNav">
			<ul>
				<li class="purple">
					<a href="#" class="icon-envelope"><img src="/images/settings.png" /></a>
					<ul>
						<li><a href="http://tutorialzine.com/contact/">Add</a></li>
                        <li><a href="http://tutorialzine.com/contact/">Delete</a></li>
                        <li><a href="http://tutorialzine.com/contact/">Invite Friend</a></li>
					</ul>
				</li>
			</ul>
		</nav>
                                        </div>
                                        <!-- -->
                                        <div class="author_comment">
                                            يابنى هتلعب بلاى ستيشن ولا مش هتلعب وكلمنى على الواتس
                                            <div class="comment_time">
                                                منذ 10 ساعات وخمس دقائق<span> </span>
                                            </div>
                                        </div>
                                    </div>
                                </span>
                                    <div class="comment_photo">
                                        <img src="/images/amrdiab.jpg" /></div>
                                </li>
                                <!-- COMMENT END -->
                            </div>
                            <!-- READ MORE START -->
                            <div class="seperated_comment">
                                <li class="more_comments"><a href="#">قراءة المزيد من التعليقات</a> </li>
                            </div>
                            <!-- READ MORE END -->
                        </ul>
                    </div>
                </div>
            </div>
            <!-- COMMENTS END -->
        </div>
        <!-- ICONS START -->
        <div id="icons_right">
            <div class="like_and_unlike">
                <ul>
                    <li><a href="#">Unlike <span>
                        <img src="/images/unlike.png" /></span></a></li>
                    <li><a href="#">Like <span>
                        <img src="/images/like.png" /></span></a></li>
                    <li><a href="#">Settings <span>
                        <img src="/images/setting.png" /></span></a></li>
                </ul>
            </div>
            <div class="clr">
            </div>
            <div class="product_info">
                <ul>
                    <li>اسم المنتج <span>
                        <asp:Label ID="lblPName" runat="server" Text="هيونداي اكسنت"></asp:Label></span></li>
                    <li>سعر المنتج <span><asp:Label ID="lblPPrice" runat="server" Text="هيونداي اكسنت"></asp:Label></span></li>
                    <li>الماركة <span><asp:Label ID="lblPBrand" runat="server" Text="هيونداي اكسنت"></asp:Label></span></li>
                    <li>وصف المنتج
                        <br />
                        <div class="desc">
                            <asp:Label ID="lblInfo" runat="server" Text="هيونداي اكسنت"></asp:Label></div>
                    </li>
                </ul>
            </div>
            <!-- ICONS END -->
        </div>
        <!-- WRAPPER END -->
    </div>
    </form>
</body>
</html>
