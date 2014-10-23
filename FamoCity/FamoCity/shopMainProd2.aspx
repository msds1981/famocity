<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopMainProd2.aspx.cs" Inherits="FamoCity.shopMainProd2" %>


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

        function AddCommentProduct(text, obj) {
            //اضافة تعليق على منتج
            if (text.value == "") {
                return false;
            }

            $.ajax({
                type: "POST",
                url: "/Methods.aspx/SaveCommentProducts",
                data: "{'desc':'" + text.value + "','obj':'" + obj + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != null) {
                        return data.d;
                        alert(text.value);
                    }
                }
            });
        }

        //add like
        function AddLike(objid) {
            $.ajax({
                type: "POST",
                url: "/Methods.aspx/LikeTopic",
                data: "{'objid':'" + objid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != null) {
                        //delete onclick event from <a> tag
                        $("#AddLike_" + objid).attr("onclick", "");
                        $("#AddUnLike_" + objid).attr("onclick", "AddUnLike(" + objid + ")");
                    }
                }
            });
        }

        //un lilke
      /*  function AddUnLike(objid) {
            $.ajax({
                type: "POST",
                url: "/Methods.aspx/UnLikeTopic",
                data: "{'objid':'" + objid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != null) {
                        //delete onclick event from <a> tag
                        $("#AddUnLike_" + objid).attr("onclick", "");
                        //add new onclick to <a>
                        $("#AddLike_" + objid).attr("onclick", "AddLike(" + objid + ")");
                    }
                }
            });
        }*/
    </script>
    <style>
        .Desc{overflow:scroll;max-height:148px;float:right;overflow-x:hidden;}
    </style>
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
                    <textarea id="comment_textarea" class="comment_textarea"></textarea>
                </div>
                <div class="send_comment">
                    <div class="author_picture">
                        <asp:literal id="ltrimg" runat="server"> </asp:literal>
                    </div>
                    <input type="submit" value="ارسال" class="send_comment_button" onclick="AddCommentProduct(document.getElementById('comment_textarea'),0);"/> 
                </div>
                <div class="clr">
                </div>
                <div class="show_comments">
                    <div class="comment_all">
                        <ul class="comment">
                            <asp:Repeater ID="rpcomment" runat="server">
                            <ItemTemplate>
                            <div class="seperated_comment">
                                <li>
                                    <span>
                                        <div class="comment_content">
                                            <div class="author_name"><%#Eval("FullName")%></div>
                                            <div class="comment_option">

                                            </div>
                                            <div class="author_comment">
                                                <%#Eval("description") %>
                                                <div class="comment_time">
                                                    <%#Eval("cud")%><span> </span>
                                                </div>
                                            </div>
                                        </div>
                                    </span>
                                    <div class="comment_photo">
                                        <img src="<%#Eval("file_path")%>" />
                                    </div>
                                </li>
                            </div>
                            </ItemTemplate>
                            </asp:Repeater>                           
                            <%-- <div class="seperated_comment">
                                <li class="more_comments"><a href="#">قراءة المزيد من التعليقات</a> </li>
                            </div>--%>
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
                    <%--<li><a href="#">Settings <span>
                        <img src="/images/setting.png" /></span></a></li>--%>
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
