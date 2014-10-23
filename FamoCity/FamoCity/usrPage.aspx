<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usrPage.aspx.cs" Inherits="FamoCity.usrPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
        <!-- Center Header Start -->
        <div class="gridContainer clearfix">
            <!-- Center Header Wrapper Start -->
            <div id="Header_Center_Wrapper">
                <div class="status">
                    <asp:Literal ID="ltrUserInfo" runat="server"></asp:Literal>
                    <!--
                    <div class="status_info">
                        <ul class="status_ino_ul">
                            <li class="user_info_name">داليا محمد مصطفي</li>
                            <li class="user_info_country">المملكة العربية السعودية الرياض</li>
                            <li class="user_info_descriptin">نادي النصر السعودي تأسس عام 1985 ميلادي</li>
                        </ul>
                    </div>
                    <div class="status_profile_pic">
                        <img src="newfiles/images/prof_pic.jpg" /></div>
                        -->
                </div>
                <div class="galleryWrapper">
                    <div class="galleryInnerWrapper">
                        <div class="galleryContainer">
                            <!-- Header Gallery Start-->
                            <asp:Literal ID="ltrImages" runat="server"></asp:Literal>
                            <!--<div class="galleryPhoto galleryPhoto1">
                                <a href="#">
                                    <div class="Image iLoaded " style="background-image: url(newfiles/images/1_04.gif); width: 100%;
                                        height: 100%;" src="newfiles/images/1_04.gif">
                                    </div>
                                </a>
                            </div>
                            <div class="galleryPhoto galleryPhoto2">
                                <a href="#">
                                    <div class="Image iLoaded " style="background-image: url(newfiles/images/1_03.jpg); width: 100%;
                                        height: 100%;" src="newfiles/images/1_03.jpg">
                                    </div>
                                </a>
                            </div>
                            <div class="galleryPhoto galleryPhoto3">
                                <a href="#">
                                    <div class="Image iLoaded " style="background-image: url(newfiles/images/1_05.gif); width: 100%;
                                        height: 100%;" src="newfiles/images/1_05.gif">
                                    </div>
                                </a>
                            </div>
                            <div class="galleryPhoto galleryPhoto4">
                                <a href="#">
                                    <div class="Image iLoaded " style="background-image: url(newfiles/images/1_02.gif); width: 100%;
                                        height: 100%;" src="newfiles/images/1_02.gif">
                                    </div>
                                </a>
                            </div>
                            <div class="galleryPhoto galleryPhoto5">
                                <a href="#">
                                    <div class="Image iLoaded " style="background-image: url(newfiles/images/1_01.gif); width: 100%;
                                        height: 100%;" src="newfiles/images/1_01.gif">
                                    </div>
                                </a>
                            </div>-->
                            <!-- Header Gallery End-->
                        </div>
                    </div>
                </div>
                <!-- Center Header Wrapper End -->
            </div>
            <!-- Center Header End -->
        </div>
        <!-- Navbar Start -->
        <div class="gridContainer clearfix">
            <!-- Navbar Wrapper Start -->
            <div id="Navbar">
                <!-- Menu1 Start -->
                <div id="navbar_first_menu">
                    <ul>
                        <li id="homepage"><a href="#">صفحتي</a></li>
                        <li><a href="#">الصور والفيديو</a></li>
                        <li><a href="#">اصدقائي</a></li>
                        <li><a href="#">متاجر ومنتجات</a></li>
                        <li><a href="#">فيمو سيتي</a></li>
                    </ul>
                    <!-- Menu1 End -->
                </div>
                <!-- Menu2 Start -->
                <div id="navbar_second_menu">
                    <ul>
                        <li class="nationality">
                            <div class="nation">
                                سعودي</div>
                            <div class="nation_country">
                                الجنسية</div>
                        </li>
                        <li class="nationality">
                            <div class="nation">
                                الرياض</div>
                            <div class="nation_country">
                                مدينتي</div>
                        </li>
                        <li>
                            <div class="menucounter">
                                35</div>
                            <img src="newfiles/images/down_arrow.png" />
                            <div class="menulink">
                                <a href="#">الاصدقاء</a></div>
                        </li>
                        <li>
                            <div class="menucounter">
                                125</div>
                            <img src="newfiles/images/down_arrow.png" />
                            <div class="menulink">
                                <a href="#">الصور</a></div>
                        </li>
                        <li>
                            <div class="menucounter">
                                72</div>
                            <img src="newfiles/images/down_arrow.png" />
                            <div class="menulink">
                                <a href="#">الفيديو</a></div>
                        </li>
                        <li>
                            <div class="menucounter">
                                12</div>
                            <img src="newfiles/images/down_arrow.png" />
                            <div class="menulink">
                                <a href="#">متاجر أعجبتني</a></div>
                        </li>
                        <li>
                            <div class="menucounter">
                                12</div>
                            <img src="newfiles/images/down_arrow.png" />
                            <div class="menulink">
                                <a href="#">منتجات أعجبتني</a></div>
                        </li>
                        <li>
                            <div class="menucounter">
                                3220</div>
                            <img src="newfiles/images/down_arrow.png" />
                            <div class="menulink">
                                <a href="#">نقاطي</a></div>
                        </li>
                        <li>
                            <div class="menucounter">
                                4</div>
                            <img src="newfiles/images/down_arrow.png" />
                            <div class="menulink">
                                <a href="#">اهتماماتي</a></div>
                        </li>
                    </ul>
                    <!-- Menu2 End -->
                </div>
                <!-- Navbar Wrapper End -->
            </div>
            <!-- Navbar End -->
        </div>
        <!-- Div For All Content End -->
        <div class="topics">
        
        </div>
</body>
</html>
