$(document).ready(function () {
    var m;
    var n = $(".banner").clone();
    $(".button_position").click(function () {
        configurator()
    });
    $(".timer_align").click(function () {
        configurator()
    });
    $(".shadow_type").click(function () {
        configurator()
    });
    $("#transition").change(function () {
        configurator()
    });
    $("#easing").change(function () {
        configurator()
    });
    $("#scroll_type").change(function () {
        configurator()
    });
    $(".show_thumb").click(function () {
        configurator()
    });
    $(".show_timer").click(function () {
        configurator()
    });
    $(".stop_timer").click(function () {
        configurator()
    });
    $(".select_item").click(function () {
        configurator()
    });

    function configurator() {
        var a = $("#frm_configurator input[name=button_position]:checked").val();
        var b = $("#frm_configurator input[name=timer_align]:checked").val();
        var c = parseInt($("#frm_configurator input[name=shadow_type]:checked").val());
        var d = parseInt($("#frm_configurator input[name=shadow_type]:checked").attr("data-height"));
        var e = $("#transition").val();
        var f = $("#easing").val();
        var g = $("#scroll_type").val();
        var h = convertToBoolean($("#frm_configurator input[name=show_thumb]:checked").val());
        var i = convertToBoolean($("#frm_configurator input[name=show_timer]:checked").val());
        var j = convertToBoolean($("#frm_configurator input[name=stop_timer]:checked").val());
        var k = convertToBoolean($("#frm_configurator input[name=select_item]:checked").val());
        try {
            m.resetTimer();
            m.resetText();
            if (m.vStripeEffect) {
                m.vStripes.clear()
            }
            if (m.hStripeEffect) {
                m.hStripes.clear()
            }
            if (m.blockEffect) {
                m.blocks.clear()
            }
        } catch (ex) {}
        $(".banner").remove();
        $(".sliding-image-gallery-v2-wrapper").remove();
        $(".banner-container .inner").append(n);
        n = $(".banner").clone();
        $(".banner").slidingImageGalleryV2({
            buttonPosition: a,
            timerAlign: b,
            shadowType: c,
            shadowHeight: d,
            transition: e,
            easing: f,
            scrollType: g,
            showThumb: h,
            showTimer: i,
            pauseRollOver: j,
            selectRollOver: k
        });
        var l = "$('.banner').slidingImageGalleryV2({\n	width:960,\n	height:328,\n	\n	selectRollOver:" + k + ",\n	\n	buttonPosition:\"" + a + "\",\n	buttonWidth:300,\n	buttonHeight:82,\n	showThumb:" + h + ",\n	thumbWidth:52,\n	thumbHeight:52,\n	\n	shadowType:" + c + ",\n	shadowHeight:" + d + ",\n	\n	transition:\"" + e + "\",\n	easing:\"" + f + "\",\n	\n	showTimer:" + i + ",\n	timerAlign:\"" + b + "\",\n	pauseRollOver:" + j + ",\n	\n	scrollType:\"" + g + "\"\n});";
        $(".show-options pre").html(l)
    }

    function convertToBoolean(a) {
        return (a == "true")
    }
});