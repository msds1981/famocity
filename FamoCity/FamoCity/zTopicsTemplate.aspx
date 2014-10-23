<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zTopicsTemplate.aspx.cs" Inherits="FamoCity.zTopicsTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.10.1.min.js" type="text/javascript"></script>
    <script src="js/social.js" type="text/javascript"></script>
    <script type="text/javascript">
        function on() {
            alert(document.getElementById('txtNewComm_2').value);
        }


    </script>
</head>
<body>
    <!-- Start Topic -->
    <!-- user's topic contain photo -->
    <div class="tpc" tpcid="2"> 
        <div class="tpc-head">
        </div>
        <!-- Start Content -->
        <div class="tpc-cont">
            <div class="tpc-cont-photo">
                <a href="javascript:void(0);" onclick="LoadPage(11,99);" objcode="11" objid="99">
                    <!-- Load User/shop Page -->
                    <img src="/img.jpg" />
                </a>
            </div>
            <div class="tpc-cont-detail">
                <div class="tpc-cont-name">
                    <label>
                        اضاف</label>
                    <a href="javascript:void(0);" onclick="LoadPage(11,99);" objcode="11" objid="99">محمد
                        سعيد</a><!-- Load User/shop Page -->
                    <label>
                        مشاركة جديدة</label>
                </div>
                <div class="tpc-cont-desc">
                    <p>
                        تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل <a href="javascript:void(0);" onclick="Display(2);"
                            tpcid="2">اقرأ المزيد</a><!-- Display Topic Details -->
                    </p>
                </div>
            </div>
            <!-- Start Media -->
            <div class="tpc-cont-media">
                <a href="javascript:void(0);" onclick="Display(2);" tpcid="2">
                    <!-- Display Topic Details -->
                    <img src="/aa11.jpg" class="tpc-media-img1" />
                </a>
            </div>
            <!-- End Media -->
        </div>
        <!-- End Content -->
        <!-- Start Info -->
        <div class="tpc-info">
            <div class="tpc-info-opt">
                <!-- Display Topic Details -->
                <a href="javascript:void(0);" onclick="Display(2);" tpcid="2">
                    <img src="comment.jpg" />
                    <label class="comments_2" title="عدد التعليقات">
                        223</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <!-- Add Like to topic -->
                <a href="javascript:void(0);" onclick="AddLike(2);" tpcid="2">
                    <img src="Like.jpg" />
                    <label class="likes_2" title="عدد الاعجاب">
                        44</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <!-- Display Topic Details -->
                <a href="javascript:void(0);" onclick="UnLike(2);" tpcid="2">
                    <img src="UnLike.jpg" />
                    <label class="unlikes_2" title="عدد الغير معجبين">
                        44</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <a href="javascript:void(0);" onclick="RePublish(2);" tpcid="2">
                    <img src="shares.jpg" />
                    <label class="publishes_2" title="عدد المشاركات">
                        2</label>
                </a>
            </div>
        </div>
        <!-- End Info -->
        <!-- Start Comment -->
        <div class="tpc-comm">
            <div class="tpc-comm-photo">
                <img src="userphoto.jpg" />
            </div>
            <div class="tpc-comm-input">
                <input type="text" id="txtNewComm_2" class="txtNewComm" onkeypress="return onKeyPressNewComment(event,document.getElementById('txtNewComm_2'),2);" tpcid="2"/>
            </div>
        </div>
        <!-- End Comment -->
    </div>
    <!-- End Topic -->
    <!-- Start Topic -->
    <!-- user's topic contain video -->
    <div class="tpc" tpcid="7">
        <div class="tpc-head">
            <!-- options menu -->
        </div>
        <!-- Start Content -->
        <div class="tpc-cont">
            <div class="tpc-cont-photo">
                <a href="javascript:void(0);" onclick="LoadPage(5,600);" objcode="5" objid="600">
                    <!-- Load User/shop Page -->
                    <img src="/img.jpg" />
                </a>
            </div>
            <div class="tpc-cont-detail">
                <div class="tpc-cont-name">
                    <label>
                        اضاف</label>
                    <a href="javascript:void(0);" onclick="LoadPage(5,600);" objcode="5" objid="600">محمد
                        سعيد</a><!-- Load User/shop Page -->
                    <label>
                        مشاركة جديدة</label>
                </div>
                <div class="tpc-cont-desc">
                    <p>
                        تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل <a href="javascript:void(0);" onclick="Display(7);"
                            tpcid="7">اقرأ المزيد</a><!-- Display Topic Details -->
                    </p>
                </div>
            </div>
            <!-- Start Media -->
            <div class="tpc-cont-media">
                <a href="javascript:void(0);" onclick="Display(7);" tpcid="7">
                    <!-- Display Topic Details -->
                    <div class="tpc-media-video"><img src="playicon.png"/></div>
                    <img src="/videofromyoutube.jpg" class="tpc-media-lnkvideo" />
                </a>
            </div>
            <!-- End Media -->
        </div>
        <!-- End Content -->
        <!-- Start Info -->
        <div class="tpc-info">
            <div class="tpc-info-opt">
                <!-- Display Topic Details -->
                <a href="javascript:void(0);" onclick="Display(7);" tpcid="7">
                    <img src="comment.jpg" />
                    <label class="comments_7" title="عدد التعليقات">
                        223</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <!-- Add Like to topic -->
                <a href="javascript:void(0);" onclick="AddLike(7);" tpcid="7">
                    <img src="Like.jpg" />
                    <label class="likes_7" title="عدد الاعجاب">
                        22</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <!-- Display Topic Details -->
                <a href="javascript:void(0);" onclick="UnLike(7);" tpcid="7">
                    <img src="UnLike.jpg" />
                    <label class="unlikes_7" title="عدد الغير معجبين">
                        44</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <a href="javascript:void(0);" onclick="RePublish(7);" tpcid="7">
                    <img src="shares.jpg" />
                    <label class="publishes_7" title="عدد المشاركات">
                        2</label>
                </a>
            </div>
        </div>
        <!-- End Info -->
        <!-- Start Comment -->
        <div class="tpc-comm">
            <div class="tpc-comm-photo">
                <img src="userphoto.jpg" />
            </div>
            <div class="tpc-comm-input">
                <input type="text" id="txtNewComm_7" class="txtNewComm" onkeypress="return onKeyPressNewComment(event,document.getElementById('txtNewComm_7'),7);" tpcid="7"/>
            </div>
        </div>
        <!-- End Comment -->
    </div>
    <!-- End Topic -->
    <!-- Start Topic -->
    <!-- user's topic contain two photo -->
    <div class="tpc" tpcid="12">
        <div class="tpc-head">
        </div>
        <!-- Start Content -->
        <div class="tpc-cont">
            <div class="tpc-cont-photo">
                <a href="javascript:void(0);" onclick="LoadPage(5,600);" objcode="5" objid="600">
                    <!-- Load User/shop Page -->
                    <img src="/img.jpg" />
                </a>
            </div>
            <div class="tpc-cont-detail">
                <div class="tpc-cont-name">
                    <label>
                        اضاف</label>
                    <a href="javascript:void(0);" onclick="LoadPage(5,600);" objcode="5" objid="600">محمد
                        سعيد</a><!-- Load User/shop Page -->
                    <label>
                        مشاركة جديدة</label>
                </div>
                <div class="tpc-cont-desc">
                    <p>
                        تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل تفاصيل <a href="javascript:void(0);" onclick="Display(12);"
                            tpcid="12">اقرأ المزيد</a><!-- Display Topic Details -->
                    </p>
                </div>
            </div>
            <!-- Start Media -->
            <div class="tpc-cont-media">
                <a href="javascript:void(0);" onclick="Display(12);" tpcid="12">
                    <!-- Display Topic Details -->
                    <img src="/aa11.jpg" class="tpc-media-img2" />
                    <img src="/aa22.jpg" class="tpc-media-img2" />
                </a>
            </div>
            <!-- End Media -->
        </div>
        <!-- End Content -->
        <!-- Start Info -->
        <div class="tpc-info">
            <div class="tpc-info-time">
                <label title="real time">منذ 8 ساعات</label>
            </div>
            <div class="tpc-info-opt">
                <!-- Display Topic Details -->
                <a href="javascript:void(0);" onclick="Display(12);" tpcid="12">
                    <img src="comment.jpg" />
                    <label class="comments_12" title="عدد التعليقات">
                        223</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <!-- Add Like to topic -->
                <a href="javascript:void(0);" onclick="AddLike(12);" tpcid="12">
                    <img src="Like.jpg" />
                    <label class="likes_12" title="عدد الاعجاب">
                        22</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <!-- Display Topic Details -->
                <a href="javascript:void(0);" onclick="UnLike(12);" tpcid="12">
                    <img src="UnLike.jpg" />
                    <label class="unlikes_12" title="عدد الغير معجبين">
                        44</label>
                </a>
            </div>
            <div class="tpc-info-opt">
                <a href="javascript:void(0);" onclick="RePublish(12);" tpcid="12">
                    <img src="shares.jpg" />
                    <label class="publishes_12" title="عدد المشاركات">
                        2</label>
                </a>
            </div>
        </div>
        <!-- End Info -->
        <!-- Start Comment -->
        <div class="tpc-comm">
            <div class="tpc-comm-photo">
                <img src="userphoto.jpg" />
            </div>
            <div class="tpc-comm-input">
                <input type="text" id="Text1" class="txtNewComm" onkeypress="return onKeyPressNewComment(event,document.getElementById('txtNewComm_12'),12);" tpcid="12"/>
            </div>
        </div>
        <!-- End Comment -->
    </div>
    <!-- End Topic -->
</body>
</html>
