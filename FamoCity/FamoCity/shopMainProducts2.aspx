<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopMainProducts2.aspx.cs" Inherits="FamoCity.shopMainProducts2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>:: Famo City [ Products ] ::</title>
    <script src="js/jquery-1.10.1.min.js"></script>
    <script src="js/libs/jquery.js"></script>
    <script src="js/respond.min.js"></script>
    <link href="css/boilerplate.css" rel="stylesheet" type="text/css">
    <link href="css/Untitled-9.css" rel="stylesheet" type="text/css">
    <link href="css/style.css" rel="stylesheet" type="text/css">
    <!-- The Box Content stylesheets -->
    <link rel="stylesheet" href="css/framewarp.css" />
    <!--[if lt IE 9]>
          <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->
    <!-- 
To learn more about the conditional comments around the html tags at the top of the file:
paulirish.com/2008/conditional-stylesheets-vs-css-hacks-answer-neither/

Do the following if you're using your customized build of modernizr (http://www.modernizr.com/):
* insert the link to your js here
* remove the link below to the html5shiv
* add the "no-js" class to the html tags at the top
* you can also remove the link to respond.min.js if you included the MQ Polyfill in your modernizr build 
-->
    <!--[if lt IE 9]>
<script src="//html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
    <script src="js/respond.min.js"></script>
    <!-- Jquery Sliding Panel -->
    <script type="text/javascript">
        $(document).ready(function () {
            $(".btn-slide").click(function () {
                $("#user_information").slideToggle("slow");
                $(this).toggleClass("active"); return false;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- USER INFORMATION START -->
    <div id="user_information">
        <div class="gridContainer clearfix">
            <ul id="user_info_list">
                <li><a href="#">تعديل المعلومات <span>
                    <img src="images/user_info_icon.png" alt="" title="" />
                </span></a></li>
                <li><a href="#">تعديل المعلومات <span>
                    <img src="images/user_info_icon.png" alt="" title="" />
                </span></a></li>
                <li><a href="#">تعديل المعلومات <span>
                    <img src="images/user_info_icon.png" alt="" title="" />
                </span></a></li>
                <li><a href="#">تعديل المعلومات <span>
                    <img src="images/user_info_icon.png" alt="" title="" />
                </span></a></li>
                <li><a href="#">تعديل المعلومات <span>
                    <img src="images/user_info_icon.png" alt="" title="" />
                </span></a></li>
                <li><a href="#">تعديل المعلومات <span>
                    <img src="images/user_info_icon.png" alt="" title="" />
                </span></a></li>
                <li><a href="#">تعديل المعلومات <span>
                    <img src="images/user_info_icon.png" alt="" title="" />
                </span></a></li>
            </ul>
        </div>
        <!-- USER INFORMATION END -->
    </div>
    <!-- BAR WRAPPER START -->
    <div id="bar_wrapper">
        <div class="gridContainer clearfix">
            <div class="slide">
                <a href="#" class="btn-slide">بياناتك الشخصية</a></div>
        </div>
        <!-- BAR WRAPPER END -->
    </div>
    <!-- HEADER START -->
    <div id="header">
        <div class="gridContainer clearfix">
            <!-- User Data Start -->
            <div id="user_data">
                <div class="user_nickname">
                    محمد مصطفي
                </div>
                <div class="user_picture">
                    <img class="user_picture2" src="images/user_pic.png" alt="" title="" />
                </div>
                <div class="change_picture">
                    <a href="#" class="orange-btn bg">غير صورتك</a>
                </div>
                <div class="user_status">
                    <div class="status">
                        نعلم مدى أهمية بياناتك و محتويات موقعك، لذلك نضمن لك عدم فقدانها بتوفير نسخ إحتياطي
                        آمن يومي ، أسبوعي ،شهرى .
                    </div>
                </div>
                <!-- User Data End -->
            </div>
            <!-- Logo Start-->
            <div id="logo">
                <img src="images/logo.png" />
                <!-- Logo End-->
            </div>
        </div>
        <!-- HEADER END -->
    </div>
    <!-- NAVBAR START -->
    <div id="navbar">
        <div class="gridContainer clearfix">
            <!-- Notification Start-->
            <div id="notification">
                <ul id="notify_list">
                    <li><a href="http://google.com">
                        <img src="images/shopping.png" alt="" title="" /></a></li>
                    <li><a href="#">
                        <img src="images/friend.png" alt="" title="" /></a></li>
                    <li><a href="#">
                        <img src="images/inbox.png" alt="" title="" /></a></li>
                </ul>
                <!-- Notification End-->
            </div>
            <!-- Search Start-->
            <div id="search">
                <ul class="search_list">
                    <li><a href="#" class="orange-btn bg">ابحث</a></li>
                    <li>
                        <input type="text" name="search_box" class="search_box" value="كلمة البحث" /></li>
                </ul>
                <!-- Search End-->
            </div>
            <div id="menu">
                <!-- Menu Start -->
                <ul id="navigator">
                    <li><a href="#">رابط تجربة</a></li>
                    <li><a href="#">رابط تجربة</a></li>
                    <li><a href="#">رابط تجربة</a></li>
                    <li><a href="#">رابط تجربة</a></li>
                    <li><a href="#">رابط تجربة</a></li>
                    <li><a href="#">رابط تجربة</a></li>
                </ul>
            </div>
            <!-- Menu End -->
        </div>
        <!-- NAVBAR END -->
    </div>
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
                    جسم الموقع
                </div>
                <!-- -->
                <div id="inside_left_content">
                    <!-- Products Container Start -->
                    <div id="products_container">
                        <div class="container">
                            <!-- Box START -->
                            <div class="box">
                                <div class="article">
                                    <div class="article_frame">
                                        <h3>
                                            سيارة هيونداي</h3>
                                        <img alt="Jaipure picture" class="thumbnail" src="img/Jaipur-2.JPG">
                                        <h4>
                                            <span>
                                                <img src="images/slashed_icons.png" />
                                            </span>تومي هيلفجر
                                        </h4>
                                        <p>
                                            تجربة اضافة وصف تتم هنا فى هذه الخانه حيث يقوم المستخدم بوصف حاله السلعة اضافة وصف
                                            تتم هنا هنا فى هذه الخانه حيث يقوم <a class="" href="#"></a>
                                        </p>
                                    </div>
                                    <ul>
                                        <li class="sale">4000 ريال سعودي</li>
                                        <li class="offer">خصم 200 ريال</li>
                                    </ul>
                                </div>
                                <!-- Box END -->
                            </div>
                            <!-- Box START -->
                            <div class="box">
                                <div class="article">
                                    <div class="article_frame">
                                        <h3>
                                            سيارة هيونداي</h3>
                                        <img alt="Jaipure picture" class="thumbnail" src="img/Jaipur-3.JPG">
                                        <h4>
                                            <span>
                                                <img src="images/slashed_icons.png" />
                                            </span>تومي هيلفجر
                                        </h4>
                                        <p>
                                            تجربة اضافة وصف تتم هنا فى هذه الخانه حيث يقوم المستخدم بوصف حاله السلعة اضافة وصف
                                            تتم هنا هنا فى هذه الخانه حيث يقوم <a class="" href="#"></a>
                                        </p>
                                    </div>
                                    <ul>
                                        <li class="sale">4000 ريال سعودي</li>
                                        <li class="offer">خصم 200 ريال</li>
                                    </ul>
                                </div>
                                <!-- Box END -->
                            </div>
                            <!-- Box START -->
                            <div class="box">
                                <div class="article">
                                    <div class="article_frame">
                                        <h3>
                                            مصطفى</h3>
                                        <a href="pop_up.html" class="button1" id="b1">
                                            <img alt="Jaipure picture" class="thumbnail" src="img/Jaipur-4.JPG">
                                        </a>
                                        <h4>
                                            <span>
                                                <img src="images/slashed_icons.png" />
                                            </span>تومي هيلفجر
                                        </h4>
                                        <p>
                                            تجربة اضافة وصف تتم هنا فى هذه الخانه حيث يقوم المستخدم بوصف حاله السلعة اضافة وصف
                                            تتم هنا هنا فى هذه الخانه حيث يقوم <a class="" href="#"></a>
                                        </p>
                                    </div>
                                    <ul>
                                        <li class="sale">4000 ريال سعودي</li>
                                        <li class="offer">خصم 200 ريال</li>
                                    </ul>
                                </div>
                                <!-- Box END -->
                            </div>
                            <!-- Box START -->
                            <div class="box">
                                <div class="article">
                                    <div class="article_frame">
                                        <h3>
                                            سيارة هيونداي</h3>
                                        <img alt="Jaipure picture" class="thumbnail" src="img/Jaipur-5.JPG">
                                        <h4>
                                            <span>
                                                <img src="images/slashed_icons.png" />
                                            </span>تومي هيلفجر
                                        </h4>
                                        <p>
                                            تجربة اضافة وصف تتم هنا فى هذه الخانه حيث يقوم المستخدم بوصف حاله السلعة اضافة وصف
                                            تتم هنا هنا فى هذه الخانه حيث يقوم <a class="" href="#"></a>
                                        </p>
                                    </div>
                                    <ul>
                                        <li class="sale">4000 ريال سعودي</li>
                                        <li class="offer">خصم 200 ريال</li>
                                    </ul>
                                </div>
                                <!-- Box END -->
                            </div>
                            <!-- Box START -->
                            <div class="box">
                                <div class="article">
                                    <div class="article_frame">
                                        <h3>
                                            سيارة هيونداي</h3>
                                        <img alt="Jaipure picture" class="thumbnail" src="img/Jaipur-1.JPG">
                                        <h4>
                                            <span>
                                                <img src="images/slashed_icons.png" />
                                            </span>تومي هيلفجر
                                        </h4>
                                        <p>
                                            تجربة اضافة وصف تتم هنا فى هذه الخانه حيث يقوم المستخدم بوصف حاله السلعة اضافة وصف
                                            تتم هنا هنا فى هذه الخانه حيث يقوم <a class="" href="#"></a>
                                        </p>
                                    </div>
                                    <ul>
                                        <li class="sale">4000 ريال سعودي</li>
                                        <li class="offer">خصم 200 ريال</li>
                                    </ul>
                                </div>
                                <!-- Box END -->
                            </div>
                            <!-- Box START -->
                            <div class="box">
                                <div class="article">
                                    <div class="article_frame">
                                        <h3>
                                            سيارة هيونداي</h3>
                                        <img alt="Jaipure picture" class="thumbnail" src="img/Jaipur-1.JPG">
                                        <h4>
                                            <span>
                                                <img src="images/slashed_icons.png" />
                                            </span>تومي هيلفجر
                                        </h4>
                                        <p>
                                            تجربة اضافة وصف تتم هنا فى هذه الخانه حيث يقوم المستخدم بوصف حاله السلعة اضافة وصف
                                            تتم هنا هنا فى هذه الخانه حيث يقوم <a class="" href="#"></a>
                                        </p>
                                    </div>
                                    <ul>
                                        <li class="sale">4000 ريال سعودي</li>
                                        <li class="offer">خصم 200 ريال</li>
                                    </ul>
                                </div>
                                <!-- Box END -->
                            </div>
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
                    البحث عن منتج
                </div>
                <!-- INSIDE RIGHT SIDE BAR START -->
                <div id="inside_right_sidebar">
                    <!-- COMPANY LOGO START -->
                    <div id="company_logo">
                        <div class="welcome_comp">
                            مرحــباً سيتـي كافــية</div>
                        <div class="logo_circle">
                            <img src="images/comp_circle.png" /></div>
                    </div>
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
                    <div id="full_column">
                        <div class="column_left">
                            <div class="product_title">
                                سيارة مرسيدس</div>
                            <div class="views_numbers">
                                <ul>
                                    <li>166 <span>
                                        <img src="images/views_icon.png" /></span></li>
                                    <li>245 <span>
                                        <img src="images/numbers_icon.png" /></span></li>
                                </ul>
                            </div>
                        </div>
                        <div class="column_right">
                            <img src="images/product_item.png" />
                        </div>
                    </div>
                    <!-- First Result End -->
                    <!-- First Result Start -->
                    <div id="full_column">
                        <div class="column_left">
                            <div class="product_title">
                                سيارة مرسيدس</div>
                            <div class="views_numbers">
                                <ul>
                                    <li>166 <span>
                                        <img src="images/views_icon.png" /></span></li>
                                    <li>245 <span>
                                        <img src="images/numbers_icon.png" /></span></li>
                                </ul>
                            </div>
                        </div>
                        <div class="column_right">
                            <img src="images/product_item.png" />
                        </div>
                    </div>
                    <!-- First Result End -->
                    <!-- First Result Start -->
                    <div id="full_column">
                        <div class="column_left">
                            <div class="product_title">
                                سيارة مرسيدس</div>
                            <div class="views_numbers">
                                <ul>
                                    <li>166 <span>
                                        <img src="images/views_icon.png" /></span></li>
                                    <li>245 <span>
                                        <img src="images/numbers_icon.png" /></span></li>
                                </ul>
                            </div>
                        </div>
                        <div class="column_right">
                            <img src="images/product_item.png" />
                        </div>
                    </div>
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
    <!-- GRID CONTAINER END -->
    <div id="footer">
    </div>
    <!-- Box Pop Up -->
    <script src="js/jquerypp.custom.js"></script>
    <script src="js/framewarp.js"></script>
    <script src="js/script.js"></script>
    <!-- Gallery Content  -->
    <script src="js/libs/jquery.masonry.min.js"></script>
    <script src="js/base.js" type="text/javascript"></script>
    </form>
</body>
</html>
