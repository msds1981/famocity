<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopMap.aspx.cs" Inherits="FamoCity.shopMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <script src="\js/jquery.zaccordion.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuPages', 'submnuMap');
            $("pre.js").each(function (index) {
                eval($(this).text());
            });
        });
    </script>

    <style type="text/css">
        html
        {
            height: 100%;
        }
        body
        {
            height: 100%;
            margin: 0;
            padding: 0;
        }
        #map-canvas
        {
            height: 100%;
        }
    </style>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDkz3C5Gd2H0cFFIEmuAQ-70MrtVljHPgI&sensor=false">
    </script>
    <script type="text/javascript">
        var map;
        var lat = 10;
        var flat;
        var lng = 45;
        var flng;
        var marker;
        var hasPosition = false;
        function initialize() {
            $.ajax({ //is there any stored position?
                type: "POST",
                url: "shopMap.aspx/hasPosition",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    hasPosition = data.d;
                    if (hasPosition) {
                        // save new lat
                        $.ajax({
                            type: "POST",
                            url: "shopMap.aspx/getLocationLat",
                            data: "{}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                flat = data.d;
                                lat = parseFloat(flat);
                                $.ajax({
                                    type: "POST",
                                    url: "shopMap.aspx/getLocationLng",
                                    data: "{}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (data) {
                                        flng = data.d;
                                        lng = parseFloat(flng);

                                        ///
                                        var position = new google.maps.LatLng(lat, lng);
                                        var mapOptions = {
                                            center: position,
                                            zoom: 3,
                                            mapTypeId: google.maps.MapTypeId.ROADMAP
                                        };
                                        map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
                                        //if there is a saved location
                                        marker = new google.maps.Marker({
                                            position: position,
                                            map: map
                                        }); //marker
                                        google.maps.event.addListener(map, 'click', function (event) {
                                            placeMarker(event.latLng);
                                        });
                                        ///

                                    } //success function
                                }); //ajax get lng

                            } //success function

                        }); //ajax get lat
                    }
                }
            });    //position?


            var position = new google.maps.LatLng(lat, lng);
            var mapOptions = {
                center: position,
                zoom: 3,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
            google.maps.event.addListener(map, 'click', function (event) {
                placeMarker(event.latLng);
            });


        }

        google.maps.event.addDomListener(window, 'load', initialize);

        function placeMarker(location) {
            if (marker) {
                marker.setPosition(location);

            } else {
                marker = new google.maps.Marker({
                    position: location,
                    map: map
                });
            } //else
            var lat = location.lat();
            var lng = location.lng();
            $.ajax({
                type: "POST",
                url: 'shopMap.aspx/saveLocation',
                data: "{'lat':'" + lat + "','lng':'" + lng + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d > 0) {
                    }
                }, //function
                error: AjaxFailed
            }); //ajax savelocation
            function AjaxFailed(result) {
                alert(result.status + ' ' + result.statusText);
            } //ajaxfailed
        } //placeMarker
          
           
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-outer">
        <!-- start content -->
        <div id="content">
            <div id="page-heading">
                <h1>
                    معلومات المتجر</h1>
            </div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0" id="content-table">
                <tr>
                    <th rowspan="3" class="sized">
                        <img src="\images/shared/side_shadowright.jpg" width="20" height="300" alt="" />
                    </th>
                    <th class="topleft">
                    </th>
                    <td id="tbl-border-top">
                        &nbsp;
                    </td>
                    <th class="topright">
                    </th>
                    <th rowspan="3" class="sized">
                        <img src="\images/shared/side_shadowleft.jpg" width="20" height="300" alt="" />
                    </th>
                </tr>
                <tr>
                    <td id="tbl-border-left">
                    </td>
                    <td>
                        <!--  start content-table-inner -->
                        <div id="content-table-inner">
                            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                <tr valign="top">
                                    <td>
                                        <!-- start id-form -->
                                        <table border="0" cellpadding="0" cellspacing="0" id="id-form">
                                            <tr>
                                                <th valign="top">
                                                    <asp:Label ID="lblLogo" runat="server" Text="موقع المتجر على الخارطة"></asp:Label>
                                                </th>
                                                <td>
                                                    <div id="map-canvas" style="width: 500px; height: 500px" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th valign="top">
                                                    &nbsp;
                                                </th>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    &nbsp;
                                                </th>
                                                <td valign="top">
                                                    <asp:Button ID="btnAddImage" runat="server" OnClick="btnAddImage_Click" CssClass="form-submit"
                                                        Text="حفظ" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- end id-form  -->
                                    </td>
                                    <td>
                                        <!--  start related-activities -->
                                        <div id="related-activities">
                                            <!--  start related-act-top -->
                                            <div id="related-act-top">
                                                <img src="\images/forms/header_related_act.gif" width="271" height="43" alt="" />
                                            </div>
                                            <!-- end related-act-top -->
                                            <!--  start related-act-bottom -->
                                            <div id="related-act-bottom">
                                                <!--  start related-act-inner -->
                                                <div id="related-act-inner">
                                                    <div class="left">
                                                        <a href="">
                                                            <img src="\images/forms/icon_plus.gif" width="21" height="21" alt="" /></a></div>
                                                    <div class="right">
                                                        <h5>
                                                            شرح الاستخدام</h5>
                                                        شرح الاستخدا شرح الاستخدا شرح الاستخدا شرح الاستخدا شرح الاستخدا شرح الاستخدا شرح
                                                        الاستخدا
                                                        <ul class="greyarrow">
                                                            <li></li>
                                                            <li></li>
                                                        </ul>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                                <!-- end related-act-inner -->
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- end related-act-bottom -->
                                        </div>
                                        <!-- end related-activities -->
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="\images/shared/blank.gif" width="695" height="1" alt="blank" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <div class="clear">
                            </div>
                        </div>
                        <!--  end content-table-inner  -->
                    </td>
                    <td id="tbl-border-right">
                    </td>
                </tr>
                <tr>
                    <th class="sized bottomleft">
                    </th>
                    <td id="tbl-border-bottom">
                        &nbsp;
                    </td>
                    <th class="sized bottomright">
                    </th>
                </tr>
            </table>
            <div class="clear">
                &nbsp;</div>
        </div>
        <!--  end content -->
        <div class="clear">
            &nbsp;</div>
    </div>
</asp:Content>
