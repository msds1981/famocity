<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FamoCity.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>FamoCity | Social Network | Commerce</title>
    <link href="/css/LoginResponsive.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="/js/jquery.watermark.js"></script>
    <script type="text/javascript" src="/js/LoginScript.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.watermark.options = {
                className: 'watermark',
                useNative: false,
                hideBeforeUnload: false
            };
            $('#txtRegFirstName').watermark('الإسم الأول');
            $('#txtRegLastName').watermark('الإسم الأخير');
            $('#txtRegEmail').watermark('البريد الإلكتروني');
            $('#txtRegPassword').watermark('كلمة المرور');

            $("button.close").click(function () {
                $(".box-body").fadeOut();
            });
        });
    
    </script>
    <style type="text/css">
        .loader{position:absolute;top:5px;left:6px;}
        .alert {padding: 15px;margin-bottom: 20px;border: 1px solid transparent;border-radius: 4px;}
        .box-body {width:1350px;margin:0 auto;position:relative;top:10px;}
        .alert {padding-left: 30px;position: relative;}
        .alert-dismissable{padding-right:35px;}
        .alert-danger{color:#a94442;background-color:#f2dede;border-color:#ebccd1;font-size: 16px;}
        .alert > .fa, .alert > .glyphicon {position: absolute;left: -34px;top: -5px;width: 35px;height: 35px;-webkit-border-radius: 50%;-moz-border-radius: 50%;border-radius: 50%;
            line-height: 35px;text-align: center;background: inherit;border: inherit;}   
        .fa {display: inline-block;font-family: FontAwesome;font-style: normal;font-weight: normal;line-height: 1;-webkit-font-smoothing: antialiased;-moz-osx-font-smoothing: grayscale;}  
        .alert-dismissable .close {position: relative;top: 0px;right: -21px;color: inherit;}
        button.close {padding: 0;cursor: pointer;background: transparent;border: 0;-webkit-appearance: none;}   
      
        .facebook-btn{ background: url("../Images/facebook.png") center no-repeat;
                    background-size: 32px;
                    width: 32px;
                    height: 32px;
                    display: inline-block;
                    vertical-align: top;}
.facebook-btn:hover{background-position:0px -24px;}
.facebook-btn:active{background-position:0px -48px;}                  
    </style>
     <%--facebook externalsocial script>--%>
       <%--facebook external script>--%>
    <script type="text/javascript">
        // This is called with the results from from FB.getLoginStatus().
        <%--function statusChangeCallback(response) {
            console.log('statusChangeCallback');
            console.log(response);
            // The response object is returned with a status field that lets the
            // app know the current login status of the person.
            // Full docs on the response object can be found in the documentation
            // for FB.getLoginStatus().
            if (response.status === 'connected') {
            alert("connectdefult");
            access_token = response.authResponse.accessToken; //get access token
            user_id = response.authResponse.userID; //get FB UID
            alert(user_id+"!"+access_token);
             testAPI(user_id,access_token);

                // Logged into your app and Facebook.
            } else if (response.status === 'not_authorized') {
            alert("not_authorized");
                // The person is logged into Facebook, but not your app.
                document.getElementById('status').innerHTML = 'Please log ' +
              'into this app.';
            } else {
               alert("not connect");
                // The person is not logged into Facebook, so we're not sure if
                // they are logged into this app or not.
                document.getElementById('status').innerHTML = 'Please log ' +
              'into Facebook.';
            }
        }
        --%>
        // This function is called when someone finishes with the Login
        // Button.  See the onlogin handler attached to it in the sample
        // code below.
        function fb_login() {
            FB.login(function(response) {
        if (response.authResponse) {
            console.log('Welcome!  Fetching your information.... ');
            //console.log(response); // dump complete info
            access_token = response.authResponse.accessToken; //get access token
            user_id = response.authResponse.userID; //get FB UID
            FB.api('/me', function(response) {
                user_email = response.email; //get user email
          // you can store this data into your database             
            });
             testAPI(user_id,access_token);

        } else {
            //user hit cancel button
            console.log('User cancelled login or did not fully authorize.');

        }
    }, {
        scope: 'publish_stream,email'
    });
        }

        window.fbAsyncInit = function () {
            FB.init({
                appId: '706453596118221',
                cookie: true,  // enable cookies to allow the server to access 
                // the session
                xfbml: true,  // parse social plugins on this page
                version: 'v2.1' // use version 2.1
            });

            // Now that we've initialized the JavaScript SDK, we call 
            // FB.getLoginStatus().  This function gets the state of the
            // person visiting this page and can return one of three states to
            // the callback you provide.  They can be:
            //
            // 1. Logged into your app ('connected')
            // 2. Logged into Facebook, but not your app ('not_authorized')
            // 3. Not logged into Facebook and can't tell if they are logged into
            //    your app or not.
            //
            // These three cases are handled in the callback function.

            FB.getLoginStatus(function (response) {
                statusChangeCallback(response);
            });

        };

        // Load the SDK asynchronously
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/ar_AR/sdk.js#xfbml=1&appId=755202757872944&version=v2.0";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));
        // Here we run a very simple test of the Graph API after login is
        // successful.  See statusChangeCallback() for when this call is made.
        function testAPI(userid,accesstoken) {
            console.log('Welcome!  Fetching your information.... ');
            var Birthday="0000000";
            var providerSystemName="ExternalAuth.Facebook";
            FB.api('/me', function (response) {
                $.ajax({
                    type: "POST",
                    url: '/Default.aspx/Authentication_Prammeter',
                    //data: 'email='+response.email+'&firstName='+response.first_name+'&lastName='+response.last_name+'&Gendor='+response.response.gender,
                    data: "{'email':'" + response.email + "','firstName':'" + response.first_name + "','lastName':'" + response.last_name + "','gendor':'" + response.gender + "','birthday':'" + Birthday + "','user_id':'" + userid + "','ccess_token':'" + accesstoken + "','provider_SystemName':'" + providerSystemName + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                   

                });

            });
        }
        function AjaxFailed(result) {
            alert(result.status + ' ' + result.statusText);
        }

</script>
  <%--googleplus external script>--%>
   <script src="https://plus.google.com/js/client:plusone.js"></script>
   <script type="text/javascript">
    //addotionaal
    var accesstoken;
    function initiateSignIn() {
        var myParams = {
            'clientid': '335579536735-ubuvh0nr6hbp2gllafip27e9jjktuju8.apps.googleusercontent.com',
            'cookiepolicy': 'single_host_origin',
            'callback': 'onSignInCallback',
            'scope': 'https://www.googleapis.com/auth/plus.login',
            'requestvisibleactions': 'http://schema.org/AddAction'
            // Additional parameters
        };
        gapi.auth.signIn(myParams);
    }
    /**
     * Handler for the signin callback triggered after the user selects an account.
     */
    function onSignInCallback(resp) {
        gapi.client.load('plus', 'v1', apiClientLoaded);
       
        accesstoken = resp["access_token"];
        
    }

    /**
     * Sets up an API call after the Google API client loads.
     */
    function apiClientLoaded() {
        gapi.client.plus.people.get({ userId: 'me' }).execute(handleEmailResponse);
    }

    /**
     * Response callback for when the API client receives a response.
     *
     * @param resp The API response object with the user email and profile information.
     */
    function handleEmailResponse(resp) {
        var primaryEmail;
        for (var i = 0; i < resp.emails.length; i++) {
            if (resp.emails[i].type === 'account') primaryEmail = resp.emails[i].value;
        }
        //alert(primaryEmail + resp.id + resp.displayName + resp.name.givenName + resp.name.familyName + accesstoken);
        alert("hi");
        var Gender="male";
         var Birthday="0000000";
          var providerSystemName="ExternalAuth.Google+";
         $.ajax({
                    type: "POST",
                    url: '/Default.aspx/Authentication_Prammeter',
                    //data: 'email='+response.email+'&firstName='+response.first_name+'&lastName='+response.last_name+'&Gendor='+response.response.gender,
                    data: "{'email':'" + primaryEmail  + "','firstName':'" + resp.name.givenName + "','lastName':'" + resp.name.familyName+ "','gendor':'" + Gender+ "','birthday':'" + Birthday + "','user_id':'" + resp.id+ "','ccess_token':'" + accesstoken + "','provider_SystemName':'" + providerSystemName + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                   

                });
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <header>
            <div class="wd">
                <div class="logo">
                    <img src="../Images/famo_logo.png" height="94px" />
                </div>
                <nav class="menu">
                   <%-- <a class="sign-in" href="">الدخول لحسابك</a>
                    <a class="sign-up" href="">التسجيل</a>
                    <div class="lang">
                        <select id="customerlanguage" name="customerlanguage" onchange="setLocation(this.value);">
                            <option value="/shop/changelanguage/1?returnurl=%2f">English</option>
                            <option selected="selected" value="/shop/changelanguage/2?returnurl=%2f">عربي</option>
                        </select>
                    </div>--%>
                </nav>
            </div>
        </header>
    <section>
            <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
            <div class="wd">
                <aside class="sliced">
                    <div class="text">
                        <h3>أول موقع تواصل اجتماعي ثلاثي الأبعاد في الممكلة</h3>
                        <p>
                            موقع فيمو سيتي يتيح لمستخدميه واعضائه المسجلين التواصل فيما بينهم وتكوين صداقات جديدة والتعارف والدردشة ايضا، ومشاركة الصور والملفات والفيديوهات والاغاني ايضا. ويمكنهم ايضا كتابة المدونات ومشاركة افكارهم مع الاخرين.
                        </p>
                    </div>
                    <div class="item item-1">
                        <div class="item-1-ani item-1-ani-1"></div>
                        <div class="item-1-ani item-1-ani-2"></div>
                        <div class="item-1-ani item-1-ani-3"></div>
                        <div class="item-1-ani item-1-ani-4"></div>
                        <div class="item-1-ani item-1-ani-5"></div>
                    </div>
                </aside>
                <aside class="circles">
                    <div class="text">
                        <h3>انضم معنا في عالمك الجديد مع كل من تحب</h3>
                        <p>
                            موقع فيمو سيتي يتيح لمستخدميه واعضائه المسجلين التواصل فيما بينهم وتكوين صداقات جديدة والتعاف والدردشة وايضا مشاركة الصور والملفات والفيدوهات .
                        </p>
                    </div>
                    <div class="circles-box">
                        <div class="circle">
                            <div class="c-photo">
                                <a href="/shop"><img class="goweb-img" src="../Images/shop.png" /></a>
                            </div>
                            <div class="c-content">
                                <a href=""><div class="c-title">شراء وبيع</div></a>
                                <div class="c-text">
                                    تسوق معنا بكل سهولة عن طريق الوسيط البيعي فيمو سيتي وهو الأفضل دائما
                                </div>
                            </div>
                        </div>
                        <div class="circle">
                            <div class="c-photo">
                                <img class="opsign-img opensign" src="../Images/social.png" />
                            </div>
                            <div class="c-content">
                                <div class="c-title opensign">تواصل إجتماعي</div>
                                <div class="c-text">
                                    فيمو سيتي سوف يساعدك لتجد اصدقائك والتواصل والتشارك مع كل الاشخاص في حياتك
                                </div>
                            </div>
                        </div>
                        <div class="circle" style="margin-left: 0;">
                            <div class="c-photo">
                                <a href=""><img class="dngame-img" src="../Images/game.png" /></a>
                            </div>
                            <div class="c-content">
                                <a href="#"><div class="c-title">تحميل اللعبة</div></a>
                                <div class="c-text">
                                    حمل اللعبة ، لتنزه في مدينة الرياض ، عبر لعبة ثري دي .
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- -------------------------------------------------------------------------------- -->

                    <div class="form">
                        <div class="form_top">
                            <div class="form_inside">
                                <div class="close">×</div>
                                <form action="" method="">
                                    <img class="avatar" src="../Images/user_img.png" />
                                    <div class="des-frgpass">
                                        إذا كنت ترغب في تغيير كلمة المرور ،قم بأدخال البريد الالكتروني الخاص بحسابك وتابع الخطوات
                                    </div>
                                    <div class="boxname errordiv">
                                       <%--< <input name="name" class="name" type="text" value="الاسم الأول" onfocus="if (this.value == 'الاسم الأول') {this.value = '';}" onblur="if (this.value == '') {this.value = 'الاسم الأول';}" />
                                        <input name="name" class="name" type="text" value="الاسم الأخير" onfocus="if (this.value == 'الاسم الأخير') {this.value = '';}" onblur="if (this.value == '') {this.value = 'الاسم الأخير';}" />--%>
                                        <asp:TextBox ID="txtRegFirstName" CssClass="name" runat="server" Text="" ValidationGroup="regGrp"></asp:TextBox>
                                        <asp:TextBox ID="txtRegLastName" CssClass="name"  runat="server" Text="" ValidationGroup="regGrp"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" ControlToValidate="txtRegFirstName" ValidationGroup="regGrp">
                                        <div class="error" style="display:block;right:10px;top:34px;">
                                            يجب كتابة الإسم الأول
                                            <div class="err-arrow"></div>
                                        </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="errordiv">
                                     <%-- <input name="email" class="email" type="email" value="البريد الإلكتروني" onfocus="if (this.value == 'البريد الإلكتروني') {this.value = '';}" onblur="if (this.value == '') {this.value = 'البريد الإلكتروني';}" />
                                      <asp:TextBox ID="txtRegEmail" CssClass="email" runat="server" Text="البريد الإلكتروني"  onfocus="if (this.value == 'البريد الإلكتروني') {this.value = '';}" onblur="if (this.value == '') {this.value = 'البريد الإلكتروني';}"></asp:TextBox>--%>
                                        <img id="loaderuser" class="loader" src="/images/loadinfo.net.gif" style="display:none;"/>
                                        <asp:TextBox ID="txtRegEmail" CssClass="email" TextMode="Email" runat="server" Text=""></asp:TextBox>
                                        <div id="emailAvail" class="error" style="display:none;right:31px;top:32px;">
                                                الايميل المدخل تم تسجيله مسبقاً
                                                <div class="err-arrow"></div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtRegEmail" ValidationGroup="emailGrp">
                                            <div class="error" style="display:block;right:31px;top:32px;">
                                                يجب ادخال البريد الإلكتروني
                                                <div class="err-arrow"></div>
                                            </div>
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRegEmail" ValidationGroup="emailGrp" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                            <div class="error" style="display:block;right:31px;top:32px;">
                                                البريد الالكتروني المدخل غير صحيح
                                                <div class="err-arrow"></div>
                                            </div>
                                        </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="errordiv">
                                    <%--<input name="password" class="password" type="text" value="كلمة المرور" onfocus="if (this.value == 'كلمة المرور') {this.value = ''; this.type = 'password';}" onblur="if (this.value == '') {this.value = 'كلمة المرور'; this.type = 'text';}" />--%>
                                   <asp:TextBox ID="txtRegPassword" CssClass="password" runat="server" Text="" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtRegPassword" ValidationGroup="passGrp">
                                            <div class="error" style="display:block;right:28px;top:42px;">
                                                ادخل كلمة المرور
                                                <div class="err-arrow"></div>
                                            </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="born errordiv">
                                        <%--<select class="born-input" name="day">
                                            <option value="0" selected="1">اليوم</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="6">6</option><option value="7">7</option><option value="8">8</option><option value="9">9</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option><option value="22">22</option><option value="23">23</option><option value="24">24</option><option value="25">25</option><option value="26">26</option><option value="27">27</option><option value="28">28</option><option value="29">29</option><option value="30">30</option><option value="31">31</option>
                                        </select>
                                        <select class="born-input" name="month">
                                            <option value="0" selected="1">الشهر</option><option value="1">يناير</option><option value="2">فبراير</option><option value="3">مارس</option><option value="4">أبريل</option><option value="5">مايو</option><option value="6">يونيو</option><option value="7">يوليو</option><option value="8">أغسطس</option><option value="9">سبتمبر</option><option value="10">أكتوبر</option><option value="11">نوفمبر</option><option value="12">ديسمبر</option>
                                        </select>
                                        <select class="born-input" name="year">
                                            <option value="0" selected="1">السنة</option><option value="2014">2014</option><option value="2013">2013</option><option value="2012">2012</option><option value="2011">2011</option><option value="2010">2010</option><option value="2009">2009</option><option value="2008">2008</option><option value="2007">2007</option><option value="2006">2006</option><option value="2005">2005</option><option value="2004">2004</option><option value="2003">2003</option><option value="2002">2002</option><option value="2001">2001</option><option value="2000">2000</option><option value="1999">1999</option><option value="1998">1998</option><option value="1997">1997</option><option value="1996">1996</option><option value="1995">1995</option><option value="1994">1994</option><option value="1993">1993</option><option value="1992">1992</option><option value="1991">1991</option><option value="1990">1990</option><option value="1989">1989</option><option value="1988">1988</option><option value="1987">1987</option><option value="1986">1986</option><option value="1985">1985</option><option value="1984">1984</option><option value="1983">1983</option><option value="1982">1982</option><option value="1981">1981</option><option value="1980">1980</option><option value="1979">1979</option><option value="1978">1978</option><option value="1977">1977</option><option value="1976">1976</option><option value="1975">1975</option><option value="1974">1974</option><option value="1973">1973</option><option value="1972">1972</option><option value="1971">1971</option><option value="1970">1970</option><option value="1969">1969</option><option value="1968">1968</option><option value="1967">1967</option><option value="1966">1966</option><option value="1965">1965</option><option value="1964">1964</option><option value="1963">1963</option><option value="1962">1962</option><option value="1961">1961</option><option value="1960">1960</option><option value="1959">1959</option><option value="1958">1958</option><option value="1957">1957</option><option value="1956">1956</option><option value="1955">1955</option><option value="1954">1954</option><option value="1953">1953</option><option value="1952">1952</option><option value="1951">1951</option><option value="1950">1950</option><option value="1949">1949</option><option value="1948">1948</option><option value="1947">1947</option><option value="1946">1946</option><option value="1945">1945</option><option value="1944">1944</option><option value="1943">1943</option><option value="1942">1942</option><option value="1941">1941</option><option value="1940">1940</option><option value="1939">1939</option><option value="1938">1938</option><option value="1937">1937</option><option value="1936">1936</option><option value="1935">1935</option><option value="1934">1934</option><option value="1933">1933</option><option value="1932">1932</option><option value="1931">1931</option><option value="1930">1930</option><option value="1929">1929</option><option value="1928">1928</option><option value="1927">1927</option><option value="1926">1926</option><option value="1925">1925</option><option value="1924">1924</option><option value="1923">1923</option><option value="1922">1922</option><option value="1921">1921</option><option value="1920">1920</option><option value="1919">1919</option><option value="1918">1918</option><option value="1917">1917</option><option value="1916">1916</option><option value="1915">1915</option><option value="1914">1914</option><option value="1913">1913</option><option value="1912">1912</option><option value="1911">1911</option><option value="1910">1910</option><option value="1909">1909</option><option value="1908">1908</option><option value="1907">1907</option><option value="1906">1906</option><option value="1905">1905</option>
                                        </select>--%>
                                       
                                <asp:DropDownList ID="ddlRegDay" runat="server" CssClass="born-input" ValidationGroup="regGrp">
                                    <asp:ListItem Value="0">اليوم</asp:ListItem>
                                    <asp:ListItem Value="01">01</asp:ListItem>
                                    <asp:ListItem Value="02">02</asp:ListItem>
                                    <asp:ListItem Value="03">03</asp:ListItem>
                                    <asp:ListItem Value="04">04</asp:ListItem>
                                    <asp:ListItem Value="05">05</asp:ListItem>
                                    <asp:ListItem Value="06">06</asp:ListItem>
                                    <asp:ListItem Value="07">07</asp:ListItem>
                                    <asp:ListItem Value="08">08</asp:ListItem>
                                    <asp:ListItem Value="09">09</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="21">21</asp:ListItem>
                                    <asp:ListItem Value="22">22</asp:ListItem>
                                    <asp:ListItem Value="23">23</asp:ListItem>
                                    <asp:ListItem Value="24">24</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="26">26</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="28">28</asp:ListItem>
                                    <asp:ListItem Value="29">29</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="31">31</asp:ListItem>
                                </asp:DropDownList>
                               
                                <asp:DropDownList ID="ddlRegMonth" runat="server" CssClass="born-input" ValidationGroup="regGrp">
                                    <asp:ListItem Value="0">الشهر</asp:ListItem>
                                    <asp:ListItem Value="01">01</asp:ListItem>
                                    <asp:ListItem Value="02">02</asp:ListItem>
                                    <asp:ListItem Value="03">03</asp:ListItem>
                                    <asp:ListItem Value="04">04</asp:ListItem>
                                    <asp:ListItem Value="05">05</asp:ListItem>
                                    <asp:ListItem Value="06">06</asp:ListItem>
                                    <asp:ListItem Value="07">07</asp:ListItem>
                                    <asp:ListItem Value="08">08</asp:ListItem>
                                    <asp:ListItem Value="09">09</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlRegYear" runat="server" CssClass="born-input" ValidationGroup="regGrp">
                                </asp:DropDownList>

                                       <%-- <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                            ErrorMessage="RangeValidator" ControlToValidate="ddlRegYear" MinimumValue="1" MaximumValue="2200">
                                            
                                        </asp:RangeValidator>--%>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator" ControlToValidate="ddlRegYear" Operator="NotEqual" ValidationGroup="regGrp" ValueToCompare="0">
                                            <div class="error" style="display:block;right:71px;top:30px;">
                                                يجب اختيار تاريخ الميلاد
                                                <div class="err-arrow" style="margin:-31px 99px 0 0;"></div>
                                            </div>
                                        </asp:CompareValidator>
                                        
                                    </div>
                                    <div class="sex errordiv">
                                        <span class="s-title">الجنس</span>
                                        <span class="male maleA"></span>
                                        <span class="female"></span>
                                        <%--<input name="sex" type="text" value="male" hidden="hidden">--%>
                                        <asp:HiddenField ID="sex" runat="server" value="male"></asp:HiddenField>
                                        <div class="error">
                                            <div class="err-arrow"></div>
                                        </div>
                                    </div>
                                   <div class="logfrmsocial errordiv">
                                   <%--<fb:login-button scope="public_profile,email"  onlogin="checkLoginState();"></fb:login-button>
                                        <div class="fb-login-button" data-max-rows="1" data-size="medium" data-show-faces="false" data-auto-logout-link="false"></div>
                                        
                                <a onclick="checkLoginState()" class="facebook-btn"></a>--%>
                                   <a href="#" onclick="fb_login();"class="lfs-fb"></a>
     <a href="#" class="lfs-gp" onclick="initiateSignIn();"></a>
                                     <%--  <a class="lfs-fb" href=""></a>
                                        <a class="lfs-tw" href=""></a>
                                        <a class="lfs-gp" href=""></a>--%>
                                        <div class="error">
                                            <div class="err-arrow"></div>
                                        </div>
                                    </div>
                                    <%--<div class="regfrmsocial errordiv">
                                        <a class="rfs-fb" href=""></a>
                                        <a class="rfs-tw" href=""></a>
                                        <a class="rfs-gp" href=""></a>
                                        <div class="error">
                                            <div class="err-arrow"></div>
                                        </div>
                                    </div>--%>
                                    <div class="right errordiv">
                                        <div class="oploguser"><a class="loguser">تسجيل الدخول</a></div>
                                        <div class="chkon"><asp:CheckBox ID="chkRemember" runat="server" Text="البقاء متصلا"></asp:CheckBox></div>
                                        <div class="opfrtps"><a class="forgetpass">نسيت كلمة السر ؟</a></div>
                                        <div class="opnwusr"><a class="newuser">إنشاء حساب جديد</a></div>
                                        <div class="error">
                                            <div class="err-arrow"></div>
                                        </div>
                                    </div>
                                    <div class="left errordiv">
                                        <%--<input type="submit" value="دخول">--%>
                                        <asp:Button ID="btnRigister" runat="server" Text="دخول" 
                                            onclick="btnRigister_Click" OnClientClick="return Validate()"/>
                                        <asp:HiddenField ID="HFTypeButton" ClientIDMode="Static" runat="server" value="login"></asp:HiddenField>
                                        <script type="text/javascript">
                                            function Validate() {
                                                var isValid = false;
                                                var grp = $("#HFTypeButton").val();
                                                if (grp == "login")
                                                    isValid = Page_ClientValidate('passGrp') && Page_ClientValidate('emailGrp');
                                                else if (grp == "register")
                                                    isValid = Page_ClientValidate('passGrp') && Page_ClientValidate('regGrp') && Page_ClientValidate('emailGrp');
                                                else if (grp == "fpass")
                                                    isValid = Page_ClientValidate('emailGrp');
                                               return isValid;
                                            }
                                        </script>

                                        <div class="error">
                                            <div class="err-arrow"></div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                        <div class="form_shadow"><img src="../Images/gbs.png"></div>
                    </div>
                </aside>

            </div>
        </section>
    <footer>
            <div class="wd">
                <nav class="menu">
                    <a href="">الخصوصية</a>
                    <a href="">وظائف</a>
                    <a href="">سياسة الخصوصية</a>
                    <a href="">أحكام الإستخدام</a>
                </nav>
                <div class="cr">جميع الحقوق الملكية الفكرية © فيمو سيتي</div>
            </div>
        </footer>
    </form>
</body>
</html>
