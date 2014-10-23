<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain2.Master" AutoEventWireup="true"
    CodeBehind="usrRegStep4.aspx.cs" Inherits="FamoCity.usrRegStep4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/step4.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function changeFollow(shid) {
            var strID = "#f" + shid;
            if ($(strID).hasClass("css_btn_class-add")) {
                

                $.ajax({
                    type: "POST",
                    url: '/usrRegStep4.aspx/follow',
                    data: "{'shopID':'" + shid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d > 0) {
                            //save follow success 
                            $(strID).removeClass('css_btn_class-add');
                            $(strID).addClass('css_btn_class-no');
                            $(strID).text('إلغاء');
                        }
                    },
                    error: AjaxFailed
                });

            }
            else {
                $.ajax({
                    type: "POST",
                    url: '/usrRegStep4.aspx/unFollow',
                    data: "{'shopID':'" + shid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d > 0) {
                            //save follow success 
                            $(strID).removeClass('css_btn_class-no');
                            $(strID).addClass('css_btn_class-add');
                            $(strID).text('متابعة');
                        }
                    },
                    error: AjaxFailed
                });
            }

            function AjaxFailed(result) {
                alert(result.status + ' ' + result.statusText);
            }

        }

    </script>
    <div id="content">
        <div class="ccontent size">
            <div class="meter">
                <ol>
                    <li class="active"><span class="step">1</span> <span class="stage">انشاء حساب</span>
                    </li>
                    <li class="active"><span class="step">2</span> <span class="stage">معلومات شخصية</span>
                    </li>
                    <li class="active"><span class="step">3</span> <span class="stage">صورة شخصية</span>
                    </li>
                    <li class="active"><span class="step">4</span> <span class="stage">الاهتمامات</span>
                    </li>
                    <li><span class="step">5</span> <span class="stage">دعوة الاصدقاء</span> </li>
                </ol>
            </div>
            <div class="bigbox boxsize-small">
                <form id="form1" runat="server">
                <div class="title-line">
                    <asp:Label ID="lblHeader" runat="server" Text="الاهتمامات"></asp:Label></div>
                <div class="form-register-r">
                    <div class=" form-label-x">
                        </div>
                    <div class="validation">
                        <asp:CustomValidator ID="CustomValidatorFollowCount" runat="server" 
                            ErrorMessage="تابع 3 شركات على الأقل" 
                            onservervalidate="CustomValidatorFollowCount_ServerValidate" ValidateEmptyText="true"> </asp:CustomValidator></div>
                    <div class="custom_scrollbar">
                        <asp:Literal ID="ltrShops" runat="server"></asp:Literal>
                        <%--<div class="interests-box">
                            <div class="title-line-small">
                                الملابس</div>
                            <div class="box-ul">
                                <div class="box-li box-hover">
                                    <img src="#" />
                                    <span>اسم المعرض</span>
                                    <div class="hide-btn">
                                        <a href="javascript:changeFollow(17);" class="css_btn_class-add" id="f" >متابعة</a></div>
                                </div>
                                <div class="box-li">
                                    <img src="#" />
                                    <span>اسم المعرض</span>
                                    <div class="hide-btn">
                                        <a href="#" class="css_btn_class-no">الغاء</a></div>
                                </div>
                                <div class="box-li box-hover">
                                    <img src="#" />
                                    <span>اسم المعرض</span>
                                    <div class="hide-btn">
                                        <a href="#" class="css_btn_class-add">متابعة</a></div>
                                </div>
                                <div class="box-li">
                                    <img src="#" />
                                    <span>اسم المعرض</span>
                                    <div class="hide-btn">
                                        <a href="#" class="css_btn_class-add">متابعة</a></div>
                                </div>
                                <div class="box-li box-hover">
                                    <img src="#" />
                                    <span>اسم المعرض</span>
                                    <div class="hide-btn">
                                        <a href="#" class="css_btn_class-no">الغاء</a></div>
                                </div>
                                <div class="box-li">
                                    <img src="#" />
                                    <span>اس م الم عرضالمعرض</span>
                                    <div class="hide-btn">
                                        <a href="#" class="css_btn_class-add">متابعة</a></div>
                                </div>
                                <div class="box-li">
                                    <img src="#" />
                                    <span>اسم المعرض</span>
                                    <div class="hide-btn">
                                        <a href="#" class="css_btn_class-add">متابعة</a></div>
                                </div>
                                <div class="box-li">
                                    <img src="#" />
                                    <span>اسم المعرض</span>
                                    <div class="hide-btn">
                                        <a href="#" class="css_btn_class-add">متابعة</a></div>
                                </div>
                                <div class="box-li">
                                    <img src="#" />
                                    <span>اسم المعرض</span>
                                    <div class="hide-btn">
                                        <a href="#" class="css_btn_class-add">متابعة</a></div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <div class=" form-label-cancel">
                        <a href="/user/profile" class="css_btn_class">تجاهل</a>
                    </div>
                    <div class=" form-label-r">
                        <a href="/step3" class="css_btn_class">السابق</a>
                    </div>
                    <div class=" form-label-r">
                        <asp:Button ID="Button1" runat="server" Text="التالي" class="css_btn_class" 
                            onclick="Button1_Click"/>
                   <%--     <a href="#" class="css_btn_class">التالي</a>--%>
                    </div>
                </div>
                </form>
            </div>
        </div>
    </div>
  
</asp:Content>
