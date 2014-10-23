/**************************************************************************
 * jQuery Sliding Image Gallery v2
 * @info: http://www.codegrape.com/item/jquery-sliding-image-gallery-v2/1383
 * @version: 1.0 (03.07.2012)
 * @requires: jQuery v1.7 or later (tested on 1.9.1)
 * @author: flashblue - http://www.codegrape.com/user/flashblue
 **************************************************************************/
;
(function ($) {
    $.fn.slidingImageGalleryV2 = function (o) {
        var p = 250;
        var q = 82;
        var r = 47;
        var s = 5000;
        var t = 800;
        var u = 500;
        var v = 300;
        var x = 0.1;
        var y = 600;
        var z = 0;
        var A = {
            "block.top": z++,
            "block.right": z++,
            "block.bottom": z++,
            "block.left": z++,
            "block.drop": z++,
            "diag.fade": z++,
            "diag.exp": z++,
            "rev.diag.fade": z++,
            "rev.diag.exp": z++,
            "block.fade": z++,
            "block.exp": z++,
            "block.top.zz": z++,
            "block.bottom.zz": z++,
            "block.left.zz": z++,
            "block.right.zz": z++,
            "spiral.in": z++,
            "spiral.out": z++,
            "vert.tl": z++,
            "vert.tr": z++,
            "vert.bl": z++,
            "vert.br": z++,
            "fade.left": z++,
            "fade.right": z++,
            "alt.left": z++,
            "alt.right": z++,
            "blinds.left": z++,
            "blinds.right": z++,
            "vert.random.fade": z++,
            "horz.tl": z++,
            "horz.tr": z++,
            "horz.bl": z++,
            "horz.br": z++,
            "fade.top": z++,
            "fade.bottom": z++,
            "alt.top": z++,
            "alt.bottom": z++,
            "blinds.top": z++,
            "blinds.bottom": z++,
            "horz.random.fade": z++,
            "none": z++,
            "fade": z++,
            "h.slide": z++,
            "v.slide": z++,
            "random": z++
        };
        var B = "auto_adjust";
        var C = "update_text";
        var D = "update_buttons";
        var E = "show_scrollbar";
        var F = "hide_scrollbar";
        var G = "update_knob";
        var H = {
            width: 960,
            height: 328,
            currentItem: 0,
            randomize: false,
            autoPlay: true,
            playOnce: false,
            selectRollOver: false,
            bgColor: "#000000",
            buttonPosition: "right",
            buttonWidth: 300,
            buttonHeight: 82,
            buttonDisplayLength: 4,
            showThumb: true,
            thumbWidth: 52,
            thumbHeight: 52,
            autoAdjust: true,
            showBorder: true,
            borderWidth: 5,
            borderRadius: 4,
            borderColor: "#FFFFFF",
            showShadow: true,
            shadowType: 3,
            shadowCornerWidth: 100,
            shadowHeight: 30,
            delay: s,
            transition: "random",
            transitionSpeed: t,
            easing: "",
            showTimer: true,
            timerAlign: "bottom",
            pauseRollOver: false,
            autoCenter: true,
            textEasing: "easeOutQuint",
            textSync: true,
            blockSize: q,
            verticalSize: r,
            horizontalSize: r,
            blockDelay: 25,
            verticalStripeDelay: 90,
            horizontalStripeDelay: 90,
            scrollType: "mouse_move",
            showScrollbar: true,
            moveBy1: true,
            scrollMouseWheel: true,
            oldItem: -1,
            button: "next"
        };
        var o = $.extend({}, H, o);
        var I = (o.width - o.buttonWidth);
        var J = o.buttonDisplayLength;

        function randomizeArray(a) {
            var b = a.length;
            for (var i = 0; i < b; i++) {
                var c = Math.floor(Math.random() * b);
                var d = a[i];
                a[i] = a[c];
                a[c] = d
            }
        }

        function getPosNumber(a, b) {
            if (!isNaN(a) && a > 0) {
                return a
            }
            return b
        }

        function preventDefault() {
            return false
        }

        function SlidingImageGalleryV2(a, b) {
            this.$obj = a;
            this.settings = b;
            this.$wrapper = $('<div class="sliding-image-gallery-v2-wrapper" />');
            this.$banner;
            this.$screen;
            this.$items;
            this.$list;
            this.$listItems;
            this.$button;
            this.itemLength = 0;
            this.$selectedItem;
            this.$preloader;
            this.$timer;
            this.$wrapperBg = $('<div class="wrapper-bg" />');
            this.$border = $('<div class="border" />');
            this.$shadow = $('<div class="shadow" />');
            this.$button;
            this.$strip;
            this.$mainLink;
            this.blockEffect = false;
            this.blocks;
            this.hStripeEffect = false;
            this.hStripes;
            this.vStripeEffect = false;
            this.vStripes;
            this.timerId = null;
            this.delay;
            this.paused = false;
            this.slideCoord;
            this.widths = 0;
            this.heights = 0;
            this.range;
            this.dest;
            this.scrollId = null;
            this.prevSlots;
            this.nextSlots;
            this.maxSlots;
            this.pos = 0;
            this.init()
        }
        SlidingImageGalleryV2.prototype = {
            init: function () {
                this.$banner = this.$obj;
                this.$screen = this.$banner.find(".screen");
                this.$items = this.$banner.find(".items");
                this.$list = this.$items.find(">ul:first");
                this.$listItems = this.$list.find(">li");
                this.$button = this.$listItems.find(".button");
                this.itemLength = this.$listItems.size();
                this.maxSlots = this.itemLength - this.settings.buttonDisplayLength;
                this.prevSlots = 0;
                this.nextSlots = this.maxSlots;
                if (window.Touch) {
                    this.settings.pauseRollOver = this.settings.selectRollOver = false
                }
                if (this.settings.randomize) {
                    this.randomizeItems()
                }
                this.$banner.css({
                    width: this.settings.width,
                    height: this.settings.height,
                    "background-color": o.bgColor
                });
                this.blockEffect = this.hStripeEffect = this.vStripeEffect = false;
                this.checkEffect(A[this.settings.transition]);
                this.createMainScreen();
                this.createButtons();
                this.createTimerBar();
                this.createBorder();
                this.createShadow();
                if (this.settings.showBorder || this.settings.showShadow) {
                    this.$wrapper.css({
                        width: this.$banner.outerWidth(true) + this.widths,
                        height: this.$banner.outerHeight(true) + this.heights
                    });
                    this.$banner.wrap(this.$wrapper)
                }
                this.$banner.bind(C, {
                    elem: this
                }, this.updateText);
                if (this.vStripeEffect) {
                    this.vStripes = new VertStripes(this)
                }
                if (this.hStripeEffect) {
                    this.hStripes = new HorzStripes(this)
                }
                if (this.blockEffect) {
                    this.blocks = new Blocks(this)
                }
                if (window.Touch) {
                    this.slideCoord = {
                        start: -1,
                        end: -1
                    };
                    if (this.settings.transition == "v.slide") {
                        this.$banner.bind("touchstart", {
                            elem: this
                        }, this.touchVStart).bind("touchmove", {
                            elem: this
                        }, this.touchVMove)
                    } else {
                        this.$banner.bind("touchstart", {
                            elem: this
                        }, this.touchStart).bind("touchmove", {
                            elem: this
                        }, this.touchMove)
                    }
                    this.$banner.bind("touchend", {
                        elem: this
                    }, this.touchEnd)
                } else if (this.settings.scrollMouseWheel) {
                    try {
                        this.$banner.bind("mousewheel", {
                            elem: this
                        }, this.mouseScrollContent).bind("DOMMouseScroll", {
                            elem: this
                        }, this.mouseScrollContent)
                    } catch (ex) {}
                }
                this.loadImage(0);
                this.loadContent()
            },
            createMainScreen: function () {
                var a = "	<div class='preloader'></div>								<div class='timer'></div>";
                this.$screen.append(a).css({
                    width: I,
                    height: this.settings.height
                });
                this.$preloader = this.$screen.find(".preloader");
                this.$timer = this.$screen.find(".timer").data("pct", 1);
                this.$strip = $("<div class='strip'></div>");
                if (this.settings.transition == "h.slide") {
                    this.$screen.append(this.$strip);
                    this.$strip.css({
                        width: (I * 2),
                        height: this.settings.height
                    });
                    this.$listItems.removeAttr("effect")
                } else if (this.settings.transition == "v.slide") {
                    this.$screen.append(this.$strip);
                    this.$strip.css({
                        width: I,
                        height: (this.settings.height * 2)
                    });
                    this.$listItems.removeAttr("effect")
                } else {
                    this.$mainLink = $("<a href='#'></a>");
                    this.$screen.append(this.$mainLink)
                }
            },
            createButtons: function () {
                var a = this;
                this.$items.addClass("items-" + this.settings.buttonPosition);
                var b = parseInt(this.$items.css("border-" + (this.settings.buttonPosition == "left" ? "right" : "left") + "-width"));
                var w = this.settings.buttonWidth;
                if (this.settings.buttonPosition == "left") {
                    w -= b
                }
                this.$button.css({
                    width: w,
                    height: (this.settings.buttonHeight - 1)
                });
                this.$list.height(this.itemLength * this.settings.buttonHeight);
                this.$listItems.css({
                    width: w,
                    height: this.settings.buttonHeight
                });
                this.$items.css({
                    width: w,
                    height: this.settings.buttonDisplayLength * this.settings.buttonHeight
                });
                this.range = this.$list.height() - this.$items.height();
                this.$listItems.mousedown(preventDefault);
                if (this.settings.selectRollOver) {
                    this.$listItems.bind("mouseover", {
                        elem: this
                    }, this.selectItem)
                } else {
                    this.$listItems.bind("click", {
                        elem: this
                    }, this.selectItem)
                }
                this.pauseRollOverBind();
                if (o.buttonPosition == "left") {
                    this.$screen.css("left", this.settings.buttonWidth)
                } else {
                    this.$items.css("left", this.settings.width - this.$items.width());
                    this.$screen.css("left", 0)
                }
                for (var i = 0; i < this.itemLength; i++) {
                    var c = this.$listItems.eq(i);
                    var d = c.find(".button");
                    var e = b;
                    if (d.find("img")) {
                        var f = d.find("img");
                        if (this.settings.showThumb) {
                            var g = parseInt(f.css("border-top-width")) + parseInt(f.css("border-bottom-width"));
                            f.css({
                                width: this.settings.thumbWidth,
                                height: this.settings.thumbHeight,
                                marginTop: (this.settings.buttonHeight - (this.settings.thumbHeight + g)) / 2
                            });
                            e += this.settings.thumbWidth + parseInt(f.css("border-left-width")) + parseInt(f.css("border-right-width")) + parseInt(f.css("margin-left")) + parseInt(f.css("margin-right"))
                        } else {
                            f.hide()
                        }
                    }
                    var h = d.find("p");
                    e += parseInt(h.css("margin-left")) + parseInt(h.css("margin-right")) + 1;
                    h.width(this.settings.buttonWidth - e);
                    h.css({
                        marginTop: (this.settings.buttonHeight - h.outerHeight()) / 2
                    });
                    var j = c.find(">a:first");
                    var k = A[c.data("effect")];
                    if (k == undefined || k == A["h.slide"] || k == A["v.slide"]) {
                        k = A[this.settings.transition]
                    } else {
                        this.checkEffect(k)
                    }
                    var l = getPosNumber(c.data("delay"), this.settings.delay);
                    c.data({
                        imgurl: j.attr("href"),
                        effect: k,
                        delay: l
                    })
                }
                this.$listItems.hover(function () {
                    if (o.currentItem != $(this).index()) {
                        $(this).find(".button").addClass("over")
                    }
                }, function () {
                    $(this).find(".button").removeClass("over")
                });
                switch (o.scrollType) {
                case "mouse_click":
                    this.createDropDownButtons();
                    this.$upPane.find("#up-btn").css("cursor", "pointer");
                    this.$downPane.find("#down-btn").css("cursor", "pointer");
                    this.$upPane.click({
                        elem: this
                    }, this.prevThumbs);
                    this.$downPane.click({
                        elem: this
                    }, this.nextThumbs);
                    break;
                case "mouse_over":
                    this.createDropDownButtons();
                    this.$upPane.mouseenter({
                        elem: this
                    }, this.scrollUp).mouseleave({
                        elem: this
                    }, this.stopThumbList);
                    this.$downPane.mouseenter({
                        elem: this
                    }, this.scrollDown).mouseleave({
                        elem: this
                    }, this.stopThumbList);
                    break;
                case "mouse_move":
                    this.$items.mousemove({
                        elem: this
                    }, this.mousemoveScroll);
                    break;
                default:
                    this.settings.moveBy1 = true;
                    this.$listItems.bind("click", {
                        elem: this
                    }, this.itemClick)
                }
                if (this.settings.showScrollbar && this.range > 0) {
                    this.createScrollbar()
                }
                if (this.settings.autoAdjust) {
                    this.$banner.bind(B, {
                        elem: this
                    }, this.adjustThumbs);
                    this.$items.hover(function () {
                        a.$banner.unbind(B)
                    }, function () {
                        a.$banner.bind(B, {
                            elem: a
                        }, a.adjustThumbs)
                    })
                }
            },
            pauseRollOverBind: function () {
                if (this.settings.pauseRollOver && this.settings.autoPlay) {
                    this.$banner.bind("mouseenter", {
                        elem: this
                    }, this.pause).bind("mouseleave", {
                        elem: this
                    }, this.play)
                }
            },
            pauseRollOverUnBind: function () {
                if (this.settings.pauseRollOver && this.settings.autoPlay) {
                    this.$banner.unbind("mouseenter");
                    this.$banner.unbind("mouseleave")
                }
            },
            createTimerBar: function () {
                if (this.settings.showTimer) {
                    this.$timer.css("top", this.settings.timerAlign.toLowerCase() == "top" ? 0 : (this.settings.height - this.$timer.height())).css("visibility", "visible")
                } else {
                    this.$timer.hide()
                }
            },
            createBorder: function () {
                if (this.settings.showBorder) {
                    this.$border.css({
                        width: this.$banner.outerWidth(true),
                        height: this.$banner.outerHeight(true),
                        "border": this.settings.borderWidth + "px solid " + this.settings.borderColor,
                        borderRadius: this.settings.borderRadius
                    });
                    this.$wrapper.append(this.$border);
                    this.widths = this.heights = this.settings.borderWidth * 2
                } else {
                    this.$wrapperBg.css({
                        width: this.$banner.outerWidth(true),
                        height: this.$banner.outerHeight(true)
                    });
                    this.$wrapper.append(this.$wrapperBg)
                }
            },
            createShadow: function () {
                if (this.settings.showShadow) {
                    var w = this.$banner.outerWidth(true) + this.widths;
                    this.$shadow.addClass("shadow" + this.settings.shadowType);
                    switch (this.settings.shadowType) {
                    case 1:
                        var a = $('<div class="left" />').width(w / 2);
                        var b = $('<div class="right" />').width(w / 2);
                        this.$shadow.append(a);
                        this.$shadow.append(b);
                        this.$wrapper.append(this.$shadow);
                        break;
                    case 2:
                        var a = $('<div class="left" />');
                        var c = $('<div class="middle" />');
                        var b = $('<div class="right" />');
                        this.$shadow.append(a);
                        this.$shadow.append(c);
                        this.$shadow.append(b);
                        this.$wrapper.append(this.$shadow);
                        c.width(w - (this.settings.shadowCornerWidth * 2));
                        break;
                    case 3:
                        this.$wrapper.append(this.$shadow);
                        break
                    }
                    this.$shadow.css({
                        width: w,
                        height: this.settings.shadowHeight,
                        top: this.$banner.outerHeight(true) + this.heights
                    });
                    this.heights += this.settings.shadowHeight
                }
            },
            loadImage: function (a) {
                try {
                    var b = this;
                    var c = this.$listItems.eq(a);
                    var d = $("<img class='main-img'/>");
                    c.append(d);
                    d.load(function () {
                        if (!c.data("img")) {
                            b.storeImage(c, $(this))
                        }
                        a++;
                        if (a < b.itemLength) {
                            b.loadImage(a)
                        }
                    }).error(function () {
                        a++;
                        if (a < b.itemLength) {
                            b.loadImage(a)
                        }
                    });
                    d.attr("src", c.data("imgurl"))
                } catch (ex) {}
            },
            storeImage: function (a, b) {
                if (this.settings.transition == "h.slide" || this.settings.transition == "v.slide") {
                    this.$strip.append(b);
                    this.centerImage(b);
                    var c = $("<div class='content-box'></div>").css({
                        width: I,
                        height: this.settings.height
                    });
                    b.wrap(c);
                    b.css("display", "block");
                    var d = a.find(">a:nth-child(3)");
                    if (d) {
                        b.wrap(d)
                    }
                } else {
                    this.$mainLink.append(b);
                    this.centerImage(b)
                }
                a.data("img", b)
            },
            centerImage: function (a) {
                if (this.settings.autoCenter && a.width() > 0 && a.height() > 0) {
                    a.css({
                        top: (this.settings.height - a.height()) / 2,
                        left: (I - a.width()) / 2
                    })
                }
            },
            loadContent: function () {
                this.$banner.trigger(B);
                if (this.settings.playOnce) {
                    this.pauseLast()
                }
                if (this.settings.oldItem >= 0) {
                    var a = $(this.$listItems.get(this.settings.oldItem));
                    var b = a.find(".selected");
                    b.removeClass("selected")
                }
                this.$selectedItem = $(this.$listItems.get(this.settings.currentItem));
                var c = this.$selectedItem.find(".button");
                c.removeClass("over").addClass("selected");
                this.delay = this.$selectedItem.data("delay");
                this.resetText();
                if (!this.settings.textSync) {
                    this.$banner.trigger(C)
                }
                if (this.$mainLink) {
                    var d = this.$selectedItem.find(">a:nth-child(3)");
                    var e = d.attr("href");
                    if (e) {
                        this.$mainLink.unbind("click", preventDefault).css("cursor", "pointer").attr({
                            href: e,
                            target: d.attr("target")
                        })
                    } else {
                        this.$mainLink.click(preventDefault).css("cursor", "default")
                    }
                }
                if (this.$selectedItem.data("img")) {
                    this.$preloader.hide();
                    this.displayContent(this.$selectedItem.data("img"))
                } else {
                    var f = this;
                    var g = $("<img class='main-img'/>");
                    g.load(function () {
                        f.$preloader.hide();
                        f.storeImage(f.$selectedItem, $(this));
                        f.displayContent($(this))
                    }).error(function () {
                        alert("Error loading image!")
                    });
                    this.$preloader.show();
                    g.attr("src", this.$selectedItem.data("imgurl"))
                }
            },
            displayContent: function (a) {
                if (this.vStripeEffect) {
                    this.vStripes.clear()
                }
                if (this.hStripeEffect) {
                    this.hStripes.clear()
                }
                if (this.blockEffect) {
                    this.blocks.clear()
                }
                if (this.vStripeEffect || this.hStripeEffect || this.blockEffect) {
                    this.setPrevious()
                }
                var b = $(this.$listItems.get(this.settings.currentItem)).data("effect");
                if (b == A["none"] || (typeof b == "undefined")) {
                    this.showContent(a);
                    return
                } else if (b == A["fade"]) {
                    this.fadeInContent(a);
                    return
                } else if (b == A["h.slide"]) {
                    this.slideContent(a, "left", I);
                    return
                } else if (b == A["v.slide"]) {
                    this.slideContent(a, "top", this.settings.height);
                    return
                }
                if (b == A["random"]) {
                    b = Math.floor(Math.random() * (z - 5))
                }
                if (b <= A["spiral.out"]) {
                    this.blocks.displayContent(a, b)
                } else if (b <= A["vert.random.fade"]) {
                    this.vStripes.displayContent(a, b)
                } else {
                    this.hStripes.displayContent(a, b)
                }
            },
            setPrevious: function () {
                if (this.settings.oldItem >= 0) {
                    var a = this.$mainLink.find("img#curr-img").attr("src");
                    var b = $(this.$listItems.get(o.oldItem)).data("imgurl");
                    if (a != b) {
                        this.$mainLink.find("img.main-img").attr("id", "").hide();
                        var c = this.$mainLink.find("img.main-img").filter(function () {
                            return $(this).attr("src") == b
                        });
                        c.eq(0).show()
                    }
                }
            },
            showContent: function (a) {
                if (this.settings.textSync) {
                    this.$banner.trigger(C)
                }
                this.$mainLink.find("img.main-img").attr("id", "").hide();
                a.attr("id", "curr-img").show();
                this.startTimer()
            },
            fadeInContent: function (a) {
                var b = this;
                this.$mainLink.find("img#curr-img").stop(true, true);
                this.$mainLink.find("img.main-img").attr("id", "").css("z-index", 0);
                a.attr("id", "curr-img").stop(true, true).css({
                    opacity: 0,
                    "z-index": 1
                }).show().animate({
                    opacity: 1
                }, this.settings.transitionSpeed, this.settings.easing, function () {
                    b.$mainLink.find("img.main-img:not('#curr-img')").hide();
                    if (b.settings.textSync) {
                        b.$banner.trigger(C)
                    }
                    b.startTimer()
                })
            },
            slideContent: function (a, b, c) {
                this.$strip.stop(true, true);
                var d = $("#curr-img", this.$strip);
                if (d.size() > 0) {
                    this.$strip.find(".main-img").attr("id", "").parents(".content-box").css({
                        top: 0,
                        left: 0
                    });
                    a.attr("id", "curr-img").parents(".content-box").show();
                    var e, dest;
                    if (this.settings.button == "previous") {
                        this.$strip.css(b, -c);
                        e = d;
                        dest = 0
                    } else {
                        e = a;
                        dest = -c
                    }
                    e.parents(".content-box").css(b, c);
                    var f = (b == "top") ? {
                        top: dest
                    } : {
                        left: dest
                    };
                    var g = this;
                    this.$strip.stop(true, true).animate(f, this.settings.transitionSpeed, this.settings.easing, function () {
                        g.$strip.find(".main-img:not('#curr-img')").parents(".content-box").hide();
                        g.$strip.find("#curr-img").parents(".content-box").show();
                        e.parents(".content-box").css({
                            top: 0,
                            left: 0
                        });
                        g.$strip.css({
                            top: 0,
                            left: 0
                        });
                        if (g.settings.textSync) {
                            g.$banner.trigger(C)
                        }
                        g.startTimer()
                    })
                } else {
                    this.$strip.css({
                        top: 0,
                        left: 0
                    });
                    this.$strip.find(".main-img").parents(".content-box").hide().css({
                        top: 0,
                        left: 0
                    });
                    a.attr("id", "curr-img").parents(".content-box").show();
                    if (this.settings.textSync) {
                        this.$banner.trigger(C)
                    }
                    this.startTimer()
                }
            },
            selectItem: function (e) {
                var a = e.data.elem;
                var b = $(e.target);
                if (b[0].nodeName != "LI") {
                    b = b.parents("li").eq(0)
                }
                var i = b.index();
                if (i >= 0 && i != a.settings.currentItem) {
                    a.settings.button = i < a.settings.currentItem ? "previous" : "next";
                    a.resetTimer();
                    a.settings.oldItem = a.settings.currentItem;
                    a.settings.currentItem = i;
                    a.loadContent()
                }
                return false
            },
            previousItem: function (e) {
                var a = (typeof e != "undefined") ? e.data.elem : this;
                a.settings.button = "previous";
                a.resetTimer();
                a.settings.oldItem = a.settings.currentItem;
                a.settings.currentItem = a.settings.currentItem > 0 ? (a.settings.currentItem - 1) : (a.itemLength - 1);
                a.loadContent();
                return false
            },
            nextItem: function (e) {
                var a = (typeof e != "undefined") ? e.data.elem : this;
                a.settings.button = "next";
                a.resetTimer();
                a.settings.oldItem = a.settings.currentItem;
                a.settings.currentItem = a.settings.currentItem < (a.itemLength - 1) ? (a.settings.currentItem + 1) : 0;
                a.loadContent();
                return false
            },
            play: function (e) {
                var a = e.data.elem;
                a.paused = false;
                a.settings.autoPlay = true;
                a.startTimer()
            },
            pause: function (e) {
                var a = e.data.elem;
                a.paused = true;
                a.settings.autoPlay = false;
                a.pauseTimer()
            },
            pauseLast: function () {
                if (this.settings.currentItem == (this.itemLength - 1)) {
                    this.settings.autoPlay = false
                }
            },
            startTimer: function () {
                if (this.settings.autoPlay && this.timerId == null) {
                    var a = this;
                    var b = Math.round(this.$timer.data("pct") * this.delay);
                    if (this.settings.showTimer) {
                        this.$timer.animate({
                            width: (I + 1)
                        }, b, "linear")
                    }
                    this.timerId = setTimeout(function (e) {
                        a.resetTimer();
                        a.settings.button = "next";
                        a.settings.oldItem = a.settings.currentItem;
                        a.settings.currentItem = a.settings.currentItem < a.itemLength - 1 ? a.settings.currentItem + 1 : 0;
                        a.loadContent()
                    }, b)
                }
            },
            resetTimer: function () {
                clearTimeout(this.timerId);
                this.timerId = null;
                this.$timer.stop(true).data("pct", 1);
                if (this.settings.showTimer) {
                    this.$timer.width(0)
                }
            },
            pauseTimer: function () {
                clearTimeout(this.timerId);
                this.timerId = null;
                this.$timer.stop(true);
                if (this.settings.showTimer) {
                    this.$timer.data("pct", 1 - (this.$timer.width() / (I + 1)))
                }
            },
            updateText: function (e) {
                var f = e.data.elem;
                if (f.$selectedItem.find(".content").length > 0) {
                    var g = $('<div class="textbox" />');
                    g.width(I).height(f.settings.height);
                    f.$screen.append(g);
                    if (f.$selectedItem.find(">a:nth-child(3)").length) {
                        var j = f.$selectedItem.find(">a:nth-child(3)").clone();
                        j.width(f.settings.width).height(f.settings.height).addClass("bg-url");
                        g.append(j)
                    }
                    var k = f.$selectedItem.find(".content"),
                        content;
                    k.each(function (i) {
                        content = $(this).clone();
                        var b = 0,
                            yPos = 0,
                            animation, animationDirection, animationOffset, animationTime, animationDelay;
                        animation = {
                            "opacity": 1
                        };
                        animationDirection = content.data("direction") ? content.data("direction") : "";
                        animationOffset = content.data("offset") ? parseInt(content.data("offset")) : 0;
                        animationTime = content.data("time") ? parseInt(content.data("time")) : 0;
                        animationDelay = content.data("delay") ? parseInt(content.data("delay")) : 0;
                        animationEasing = content.data("easing") ? content.data("easing") : f.settings.textEasing;
                        animationAlpha = content.data("alpha") ? Number(content.data("alpha")) : 0;
                        animationVideo = content.data("video") ? content.data("video") : "";
                        if (animationAlpha != 0) {
                            content.css({
                                opacity: animationAlpha
                            })
                        }
                        if (animationVideo != "") {
                            var w = f.settings.width - f.$items.width();
                            var h = f.settings.height;
                            content.width(w);
                            content.height(h);
                            var c = $('<div class="video-frame"></div>');
                            content.append(c);
                            var d = $('<div class="video-play"></div>');
                            content.append(d);
                            d.bind("click", function () {
                                if (f.paused) {
                                    f.settings.autoPlay = true
                                }
                                f.pauseRollOverUnBind();
                                f.pauseTimer();
                                if (f.settings.showTimer) {
                                    f.$timer.hide()
                                }
                                c.html('<iframe frameborder="0" width="' + w + '" height="' + h + '" src="' + animationVideo + '" /><div class="video-close"></div>');
                                var a = c.find(".video-close");
                                a.bind("click", function () {
                                    c.html("");
                                    f.pauseRollOverBind();
                                    f.startTimer();
                                    if (f.settings.showTimer) {
                                        f.$timer.show()
                                    }
                                })
                            })
                        }
                        switch (animationDirection) {
                        case "top":
                            if (!isNaN(parseInt(content.css("top")))) {
                                yPos = parseInt(content.css("top"));
                                content.css({
                                    "top": (yPos - animationOffset)
                                });
                                animation["top"] = yPos
                            } else {
                                yPos = parseInt(content.css("bottom"));
                                content.css({
                                    "bottom": (yPos + animationOffset)
                                });
                                animation["bottom"] = yPos
                            }
                            break;
                        case "left":
                            if (!isNaN(parseInt(content.css("left")))) {
                                b = parseInt(content.css("left"));
                                content.css({
                                    "left": (b - animationOffset)
                                });
                                animation["left"] = b
                            } else {
                                b = parseInt(content.css("right"));
                                content.css({
                                    "right": (b + animationOffset)
                                });
                                animation["right"] = b
                            }
                            break;
                        case "bottom":
                            if (!isNaN(parseInt(content.css("top")))) {
                                yPos = parseInt(content.css("top"));
                                content.css({
                                    "top": (yPos + animationOffset)
                                });
                                animation["top"] = yPos
                            } else {
                                yPos = parseInt(content.css("bottom"));
                                content.css({
                                    "bottom": (yPos - animationOffset)
                                });
                                animation["bottom"] = yPos
                            }
                            break;
                        case "right":
                            if (!isNaN(parseInt(content.css("left")))) {
                                b = parseInt(content.css("left"));
                                content.css({
                                    "left": (b + animationOffset)
                                });
                                animation["left"] = b
                            } else {
                                b = parseInt(content.css("right"));
                                content.css({
                                    "right": (b - animationOffset)
                                });
                                animation["right"] = b
                            }
                            break
                        }
                        g.append(content);
                        var w = content.width();
                        var h = content.height();
                        if (navigator.userAgent.indexOf("MSIE") != -1) {
                            content.find("span.border").width(w);
                            content.find("span.bg").width(w).height(h)
                        }
                        content.stop().delay(animationDelay).animate(animation, animationTime, animationEasing)
                    })
                }
            },
            resetText: function () {
                this.$screen.find("div.textbox").fadeOut(u, function () {
                    $(this).remove()
                })
            },
            touchStart: function (e) {
                e.data.elem.slideCoord.start = e.originalEvent.touches[0].pageX
            },
            touchMove: function (e) {
                e.preventDefault();
                e.data.elem.slideCoord.end = e.originalEvent.touches[0].pageX
            },
            touchVStart: function (e) {
                e.data.elem.slideCoord.start = e.originalEvent.touches[0].pageY
            },
            touchVMove: function (e) {
                e.preventDefault();
                e.data.elem.slideCoord.end = e.originalEvent.touches[0].pageY
            },
            touchEnd: function (e) {
                var a = e.data.elem;
                if (a.slideCoord.end >= 0) {
                    if (Math.abs(a.slideCoord.start - a.slideCoord.end) > SWIPE_MIN) {
                        if (a.slideCoord.end < a.slideCoord.start) {
                            a.nextItem()
                        } else {
                            a.previousItem()
                        }
                    }
                }
                a.slideCoord.start = a.slideCoord.end = -1
            },
            mouseScrollContent: function (e) {
                var a = e.data.elem;
                if (!a.$strip.is(":animated")) {
                    var b = (typeof e.originalEvent.wheelDelta == "undefined") ? -e.originalEvent.detail : e.originalEvent.wheelDelta;
                    b > 0 ? a.previousItem() : a.nextItem()
                }
                return false
            },
            createScrollbar: function () {
                var a = this;
                this.$items.append("<div id='scrollbar'><div id='knob'></div></div>");
                this.$scrollbar = this.$items.find("#scrollbar");
                this.$knob = this.$scrollbar.find("#knob");
                this.$scrollbar.css("left", this.settings.buttonPosition == "left" ? 0 : this.$items.width() - this.$scrollbar.width());
                this.$knob.height(Math.floor((this.settings.buttonDisplayLength / this.itemLength) * this.$scrollbar.height()));
                var b = this.$scrollbar.height() - this.$knob.height();
                var c = b / this.range;
                this.$scrollbar.data({
                    range: b,
                    ratio: c
                });
                this.$banner.bind(E, function () {
                    a.$scrollbar.stop(true, true).fadeIn(v)
                }).bind(F, function () {
                    a.$scrollbar.stop(true, true).fadeOut(v)
                }).bind(G, function () {
                    a.$knob.stop(true).animate({
                        top: Math.round(-a.pos * c)
                    }, a.scrollSpeed)
                });
                this.$scrollbar.hide().css("visibility", "visible")
            },
            createDropDownButtons: function () {
                this.$items.append("<div class='btn-pane'><div id='up-btn'></div></div>									<div class='btn-pane'><div id='down-btn'></div></div>");
                var a = this.$items.find(".btn-pane");
                a.css({
                    opacity: 0,
                    width: this.settings.buttonWidth
                });
                a.hover(this.showDPane, this.hideDPane);
                this.$upPane = a.has("#up-btn");
                this.$downPane = a.has("#down-btn");
                this.$downPane.css("top", this.$items.height() - this.$downPane.height());
                a.css("visibility", "visible");
                this.$banner.bind(D, {
                    elem: this
                }, this.updateButtons).trigger(D)
            },
            showDPane: function () {
                $(this).stop(true, true).animate({
                    opacity: 1
                }, v)
            },
            hideDPane: function () {
                $(this).stop(true, true).animate({
                    opacity: 0
                }, v)
            },
            updateButtons: function (e) {
                var a = e.data.elem;
                a.pos < 0 ? a.$upPane.stop(true, true).fadeIn(v) : a.$upPane.stop(true, true).fadeOut(v);
                a.pos > -a.range ? a.$downPane.stop(true, true).fadeIn(v) : a.$downPane.stop(true, true).fadeOut(v)
            },
            prevThumbs: function (e) {
                var a = e.data.elem;
                if (a.nextSlots < a.maxSlots) {
                    var b = a.settings.moveBy1 ? 1 : Math.min(a.maxSlots - a.nextSlots, a.settings.buttonDisplayLength);
                    a.nextSlots += b;
                    a.prevSlots -= b;
                    a.moveList()
                }
                return false
            },
            nextThumbs: function (e) {
                var a = e.data.elem;
                if (a.prevSlots < a.maxSlots) {
                    var b = a.settings.moveBy1 ? 1 : Math.min(a.maxSlots - a.prevSlots, a.settings.buttonDisplayLength);
                    a.prevSlots += b;
                    a.nextSlots -= b;
                    a.moveList()
                }
                return false
            },
            itemClick: function (e) {
                var a = e.data.elem;
                var b = ($(this).index() - a.prevSlots) % a.settings.buttonDisplayLength;
                if (b + 1 == a.settings.buttonDisplayLength) {
                    a.nextThumbs(e)
                } else if (b == 0) {
                    a.prevThumbs(e)
                }
            },
            scrollUp: function (e) {
                var a = e.data.elem;
                a.$downPane.stop(true, true).fadeIn(v);
                a.$banner.trigger(E);
                a.scrollSpeed = -a.$list.stop(true).position().top * J;
                a.$list.animate({
                    top: 0
                }, a.scrollSpeed, function () {
                    a.$upPane.stop(true, true).fadeOut(v);
                    a.$banner.trigger(F)
                });
                a.$knob.stop(true).animate({
                    top: 0
                }, a.scrollSpeed)
            },
            scrollDown: function (e) {
                var a = e.data.elem;
                a.$upPane.stop(true, true).fadeIn(v);
                a.$banner.trigger(E);
                a.scrollSpeed = (a.range + a.$list.stop(true).position().top) * J;
                a.$list.animate({
                    top: -a.range
                }, a.scrollSpeed, function () {
                    a.$downPane.stop(true, true).fadeOut(v);
                    a.$banner.trigger(F)
                });
                a.$knob.stop(true).animate({
                    top: a.$scrollbar.data("range")
                }, a.scrollSpeed)
            },
            stopThumbList: function (e) {
                var a = e.data.elem;
                a.$list.stop(true);
                try {
                    a.$knob.stop(true)
                } catch (ex) {};
                a.$banner.trigger(F)
            },
            mousemoveScroll: function (e) {
                var a = e.data.elem;
                var b = Math.round(((e.pageY - a.$items.offset().top) / a.$items.height()) * 100) / 100;
                a.dest = -Math.round(a.range * b);
                if (a.scrollId == null && a.dest != a.$list.position().top) {
                    a.stopThumbList(e);
                    a.$banner.trigger(E);
                    a.scrollId = setInterval(function () {
                        a.scrollList(a)
                    }, 30)
                }
            },
            scrollList: function (a) {
                var b = a.$list.stop(true).position().top;
                if (b == a.dest) {
                    a.stopScrollTimer();
                    a.$banner.trigger(F)
                } else {
                    var c = (a.dest - b) * x;
                    a.pos += c < 0 ? Math.min(-1, Math.round(c)) : Math.max(1, Math.round(c));
                    a.$list.css("top", a.pos);
                    try {
                        a.$knob.css("top", Math.round(-a.pos * a.$scrollbar.data("ratio")))
                    } catch (ex) {}
                }
            },
            stopScrollTimer: function () {
                clearInterval(this.scrollId);
                this.scrollId = null
            },
            adjustThumbs: function (e) {
                var a = e.data.elem;
                if (a.scrollId == null) {
                    var b = Math.min(a.settings.currentItem, a.maxSlots);
                    a.prevSlots = b;
                    a.nextSlots = a.maxSlots - a.prevSlots;
                    a.moveList()
                }
            },
            moveList: function () {
                var a = this;
                this.pos = -this.prevSlots * this.$listItems.outerHeight();
                this.scrollSpeed = Math.min(y, Math.abs(this.$list.position().top - this.pos) * J);
                if (this.scrollSpeed > 0) {
                    this.$banner.trigger(E)
                }
                this.$list.stop(true).animate({
                    top: this.pos
                }, this.scrollSpeed, function () {
                    a.$banner.trigger(D);
                    a.$banner.trigger(F)
                });
                this.$banner.trigger(G)
            },
            randomizeItems: function () {
                var a = new Array(this.itemLength);
                var i = 0;
                for (i = 0; i < this.itemLength; i++) {
                    a[i] = $(this.$listItems.get(i)).clone(true)
                }
                for (i = 0; i < this.itemLength; i++) {
                    var b = Math.floor(Math.random() * this.itemLength);
                    var c = a[i];
                    a[i] = a[b];
                    a[b] = c
                }
                for (i = 0; i < this.itemLength; i++) {
                    $(this.$listItems.get(i)).replaceWith(a[i])
                }
                this.$listItems = this.$list.find(">li")
            },
            checkEffect: function (a) {
                if (a == A["random"]) {
                    this.blockEffect = this.hStripeEffect = this.vStripeEffect = true
                } else if (a <= A["spiral.out"]) {
                    this.blockEffect = true
                } else if (a <= A["blinds.right"]) {
                    this.vStripeEffect = true
                } else if (a <= A["blinds.bottom"]) {
                    this.hStripeEffect = true
                }
            },
            stopList: function (e) {
                e.data.elem.$list.stop(true)
            },
            addToScreen: function (a) {
                this.$mainLink.append(a)
            }
        };

        function VertStripes(a) {
            this._$stripes;
            this._arr;
            this._total;
            this._intervalId = null;
            this._rotator = a;
            this._areaWidth = I;
            this._areaHeight = a.settings.height;
            this._size = a.settings.verticalSize;
            this._delay = a.settings.verticalStripeDelay;
            this.init()
        }
        VertStripes.prototype = {
            init: function () {
                this._total = Math.ceil(this._areaWidth / this._size);
                if (this._total > p) {
                    this._size = Math.ceil(this._areaWidth / p);
                    this._total = Math.ceil(this._areaWidth / this._size)
                }
                var a = "";
                for (var i = 0; i < this._total; i++) {
                    a += "<div class='vpiece' id='" + i + "' style='left:" + (i * this._size) + "px; height:" + this._areaHeight + "px'></div>"
                }
                this._rotator.addToScreen(a);
                this._$stripes = this._rotator.$obj.find("div.vpiece");
                this._arr = this._$stripes.toArray()
            },
            clear: function () {
                clearInterval(this._intervalId);
                this._intervalId = null;
                this._$stripes.stop(true).css({
                    "z-index": 2,
                    opacity: 0
                })
            },
            displayContent: function (a, b) {
                this.setPieces(a, b);
                if (b == A["vert.random.fade"]) {
                    this.animateRandom(a)
                } else {
                    this.animate(a, b)
                }
            },
            setPieces: function (a, b) {
                switch (b) {
                case A["vert.tl"]:
                case A["vert.tr"]:
                    this.setVertPieces(a, -this._areaHeight, 1, this._size, false);
                    break;
                case A["vert.bl"]:
                case A["vert.br"]:
                    this.setVertPieces(a, this._areaHeight, 1, this._size, false);
                    break;
                case A["alt.left"]:
                case A["alt.right"]:
                    this.setVertPieces(a, 0, 1, this._size, true);
                    break;
                case A["blinds.left"]:
                case A["blinds.right"]:
                    this.setVertPieces(a, 0, 1, 0, false);
                    break;
                default:
                    this.setVertPieces(a, 0, 0, this._size, false)
                }
            },
            setVertPieces: function (a, b, c, d, e) {
                var f = a.attr("src");
                var g = 0;
                var h = 0;
                if (this._rotator.settings.autoCenter) {
                    g = (this._areaHeight - a.height()) / 2;
                    h = (this._areaWidth - a.width()) / 2
                }
                for (var i = 0; i < this._total; i++) {
                    var j = this._$stripes.eq(i);
                    var k = ((-i * this._size) + h);
                    if (e) {
                        b = (i % 2) == 0 ? -this._areaHeight : this._areaHeight
                    }
                    j.css({
                        background: "url('" + f + "') no-repeat",
                        backgroundPosition: k + "px " + g + "px",
                        opacity: c,
                        top: b,
                        width: d,
                        "z-index": 3
                    })
                }
            },
            animate: function (a, b) {
                var c = this;
                var d, end, incr, limit;
                switch (b) {
                case A["vert.tl"]:
                case A["vert.bl"]:
                case A["fade.left"]:
                case A["blinds.left"]:
                case A["alt.left"]:
                    d = 0;
                    end = this._total - 1;
                    incr = 1;
                    break;
                default:

                    d = this._total - 1;
                    end = 0;
                    incr = -1
                }
                this._intervalId = setInterval(function () {
                    c._$stripes.eq(d).animate({
                        top: 0,
                        opacity: 1,
                        width: c._size
                    }, c._rotator.settings.transitionSpeed, c._rotator.settings.easing, function () {
                        if ($(this).attr("id") == end) {
                            c._rotator.showContent(a)
                        }
                    });
                    if (d == end) {
                        clearInterval(c._intervalId);
                        c._intervalId = null
                    }
                    d += incr
                }, this._delay)
            },
            animateRandom: function (a) {
                var b = this;
                randomizeArray(this._arr);
                var i = 0;
                var c = 0;
                this._intervalId = setInterval(function () {
                    $(b._arr[i++]).animate({
                        opacity: 1
                    }, b._rotator.settings.transitionSpeed, b._rotator.settings.easing, function () {
                        if (++c == b._total) {
                            b._rotator.showContent(a)
                        }
                    });
                    if (i == b._total) {
                        clearInterval(b._intervalId);
                        b._intervalId = null
                    }
                }, this._delay)
            }
        };

        function HorzStripes(a) {
            this._$stripes;
            this._arr;
            this._total;
            this._intervalId = null;
            this._rotator = a;
            this._areaWidth = I;
            this._areaHeight = a.settings.height;
            this._size = a.settings.horizontalSize;
            this._delay = a.settings.horizontalStripeDelay;
            this.init()
        }
        HorzStripes.prototype = {
            init: function () {
                this._total = Math.ceil(this._areaHeight / this._size);
                if (this._total > p) {
                    this._size = Math.ceil(this._areaHeight / p);
                    this._total = Math.ceil(this._areaHeight / this._size)
                }
                var a = "";
                for (var i = 0; i < this._total; i++) {
                    a += "<div class='hpiece' id='" + i + "' style='top:" + (i * this._size) + "px; width:" + this._areaWidth + "px'><!-- --></div>"
                }
                this._rotator.addToScreen(a);
                this._$stripes = this._rotator.$obj.find("div.hpiece");
                this._arr = this._$stripes.toArray()
            },
            clear: function () {
                clearInterval(this._intervalId);
                this._intervalId = null;
                this._$stripes.stop(true).css({
                    "z-index": 2,
                    opacity: 0
                })
            },
            displayContent: function (a, b) {
                this.setPieces(a, b);
                if (b == A["horz.random.fade"]) {
                    this.animateRandom(a)
                } else {
                    this.animate(a, b)
                }
            },
            setPieces: function (a, b) {
                switch (b) {
                case A["horz.tr"]:
                case A["horz.br"]:
                    this.setHorzPieces(a, this._areaWidth, 1, this._size, false);
                    break;
                case A["horz.tl"]:
                case A["horz.bl"]:
                    this.setHorzPieces(a, -this._areaWidth, 1, this._size, false);
                    break;
                case A["alt.top"]:
                case A["alt.bottom"]:
                    this.setHorzPieces(a, 0, 1, this._size, true);
                    break;
                case A["blinds.top"]:
                case A["blinds.bottom"]:
                    this.setHorzPieces(a, 0, 1, 0, false);
                    break;
                default:
                    this.setHorzPieces(a, 0, 0, this._size, false)
                }
            },
            setHorzPieces: function (a, b, c, d, e) {
                var f = a.attr("src");
                var g = 0;
                var h = 0;
                if (this._rotator.settings.autoCenter) {
                    g = (this._areaHeight - a.height()) / 2;
                    h = (this._areaWidth - a.width()) / 2
                }
                for (var i = 0; i < this._total; i++) {
                    var j = this._$stripes.eq(i);
                    var k = ((-i * this._size) + g);
                    if (e) {
                        b = (i % 2) == 0 ? -this._areaWidth : this._areaWidth
                    }
                    j.css({
                        background: "url('" + f + "') no-repeat",
                        backgroundPosition: h + "px " + k + "px",
                        opacity: c,
                        left: b,
                        height: d,
                        "z-index": 3
                    })
                }
            },
            animate: function (a, b) {
                var c = this;
                var d, end, incr;
                switch (b) {
                case A["horz.tl"]:
                case A["horz.tr"]:
                case A["fade.top"]:
                case A["blinds.top"]:
                case A["alt.top"]:
                    d = 0;
                    end = this._total - 1;
                    incr = 1;
                    break;
                default:
                    d = this._total - 1;
                    end = 0;
                    incr = -1
                }
                this._intervalId = setInterval(function () {
                    c._$stripes.eq(d).animate({
                        left: 0,
                        opacity: 1,
                        height: c._size
                    }, c._rotator.settings.transitionSpeed, c._rotator.settings.easing, function () {
                        if ($(this).attr("id") == end) {
                            c._rotator.showContent(a)
                        }
                    });
                    if (d == end) {
                        clearInterval(c._intervalId);
                        c._intervalId = null
                    }
                    d += incr
                }, this._delay)
            },
            animateRandom: function (a) {
                var b = this;
                randomizeArray(this._arr);
                var i = 0;
                var c = 0;
                this._intervalId = setInterval(function () {
                    $(b._arr[i++]).animate({
                        opacity: 1
                    }, b._rotator.settings.transitionSpeed, b._rotator.settings.easing, function () {
                        if (++c == b._total) {
                            b._rotator.showContent(a)
                        }
                    });
                    if (i == b._total) {
                        clearInterval(b._intervalId);
                        b._intervalId = null
                    }
                }, this._delay)
            }
        };

        function Blocks(a) {
            this._$blockArr;
            this._$blocks;
            this._arr;
            this._numRows;
            this._numCols;
            this._total;
            this._intervalId;
            this._rotator = a;
            this._areaWidth = I;
            this._areaHeight = a.settings.height;
            this._size = a.settings.blockSize;
            this._delay = a.settings.blockDelay;
            this.init()
        }
        Blocks.prototype = {
            init: function () {
                this._numRows = Math.ceil(this._areaHeight / this._size);
                this._numCols = Math.ceil(this._areaWidth / this._size);
                this._total = this._numRows * this._numCols;
                if (this._total > p) {
                    this._size = Math.ceil(Math.sqrt((this._areaHeight * this._areaWidth) / p));
                    this._numRows = Math.ceil(this._areaHeight / this._size);
                    this._numCols = Math.ceil(this._areaWidth / this._size);
                    this._total = this._numRows * this._numCols
                }
                var a = "";
                for (var i = 0; i < this._numRows; i++) {
                    for (var j = 0; j < this._numCols; j++) {
                        a += "<div class='block' id='" + i + "-" + j + "'></div>"
                    }
                }
                this._rotator.addToScreen(a);
                this._$blocks = this._rotator.$obj.find("div.block");
                this._$blocks.data({
                    tlId: "0-0",
                    trId: "0-" + (this._numCols - 1),
                    blId: (this._numRows - 1) + "-0",
                    brId: (this._numRows - 1) + "-" + (this._numCols - 1)
                });
                var k = 0;
                this._arr = this._$blocks.toArray();
                this._$blockArr = new Array(this._numRows);
                for (var i = 0; i < this._numRows; i++) {
                    this._$blockArr[i] = new Array(this._numCols);
                    for (var j = 0; j < this._numCols; j++) {
                        this._$blockArr[i][j] = this._$blocks.filter("#" + (i + "-" + j)).data("top", i * this._size)
                    }
                }
            },
            clear: function () {
                clearInterval(this._intervalId);
                this._intervalId = null;
                this._$blocks.stop(true).css({
                    "z-index": 2,
                    opacity: 0
                })
            },
            displayContent: function (a, b) {
                switch (b) {
                case A["diag.fade"]:
                    this.setBlocks(a, 0, this._size, 0);
                    this.diagAnimate(a, {
                        opacity: 1
                    }, false);
                    break;
                case A["diag.exp"]:
                    this.setBlocks(a, 0, 0, 0);
                    this.diagAnimate(a, {
                        opacity: 1,
                        width: this._size,
                        height: this._size
                    }, false);
                    break;
                case A["rev.diag.fade"]:
                    this.setBlocks(a, 0, this._size, 0);
                    this.diagAnimate(a, {
                        opacity: 1
                    }, true);
                    break;
                case A["rev.diag.exp"]:
                    this.setBlocks(a, 0, 0, 0);
                    this.diagAnimate(a, {
                        opacity: 1,
                        width: this._size,
                        height: this._size
                    }, true);
                    break;
                case A["block.fade"]:
                    this.setBlocks(a, 0, this._size, 0);
                    this.randomAnimate(a);
                    break;
                case A["block.exp"]:
                    this.setBlocks(a, 1, 0, 0);
                    this.randomAnimate(a);
                    break;
                case A["block.drop"]:
                    this.setBlocks(a, 1, this._size, -(this._numRows * this._size));
                    this.randomAnimate(a);
                    break;
                case A["block.top.zz"]:
                case A["block.bottom.zz"]:
                case A["block.left.zz"]:
                case A["block.right.zz"]:
                    this.setBlocks(a, 0, this._size, 0);
                    this.zigZag(a, b);
                    break;
                case A["spiral.in"]:
                    this.setBlocks(a, 0, this._size, 0);
                    this.spiral(a, false);
                    break;
                case A["spiral.out"]:
                    this.setBlocks(a, 0, this._size, 0);
                    this.spiral(a, true);
                    break;
                default:
                    this.setBlocks(a, 1, 0, 0);
                    this.dirAnimate(a, b)
                }
            },
            setBlocks: function (a, b, c, d) {
                var e = 0;
                var f = 0;
                if (this._rotator.settings.autoCenter) {
                    e = (this._areaHeight - a.height()) / 2;
                    f = (this._areaWidth - a.width()) / 2
                }
                var g = a.attr("src");
                for (var i = 0; i < this._numRows; i++) {
                    for (var j = 0; j < this._numCols; j++) {
                        var h = ((-i * this._size) + e);
                        var k = ((-j * this._size) + f);
                        this._$blockArr[i][j].css({
                            background: "url('" + g + "') no-repeat",
                            backgroundPosition: k + "px " + h + "px",
                            opacity: b,
                            top: (i * this._size) + d,
                            left: (j * this._size),
                            width: c,
                            height: c,
                            "z-index": 3
                        })
                    }
                }
            },
            diagAnimate: function (a, b, c) {
                var d = new Array(this._total);
                var e, end, incr, lastId;
                var f = (this._numRows - 1) + (this._numCols - 1);
                if (c) {
                    e = f;
                    end = -1;
                    incr = -1;
                    lastId = this._$blocks.data("tlId")
                } else {
                    e = 0;
                    end = f + 1;
                    incr = 1;
                    lastId = this._$blocks.data("brId")
                }
                var g = 0;
                while (e != end) {
                    i = Math.min(this._numRows - 1, e);
                    while (i >= 0) {
                        j = Math.abs(i - e);
                        if (j >= this._numCols) {
                            break
                        }
                        d[g++] = this._$blockArr[i][j];
                        i--
                    }
                    e += incr
                }
                g = 0;
                var h = this;
                this._intervalId = setInterval(function () {
                    d[g++].animate(b, h._rotator.settings.transitionSpeed, h._rotator.settings.easing, function () {
                        if ($(this).attr("id") == lastId) {
                            h._rotator.showContent(a)
                        }
                    });
                    if (g == h._total) {
                        clearInterval(h._intervalId);
                        h._intervalId = null
                    }
                }, this._delay)
            },
            zigZag: function (a, b) {
                var c = this;
                var d = true;
                var i = 0,
                    j = 0,
                    incr, lastId, horz;
                if (b == A["block.left.zz"]) {
                    lastId = (this._numCols % 2 == 0) ? this._$blocks.data("trId") : this._$blocks.data("brId");
                    j = 0;
                    incr = 1;
                    horz = false
                } else if (b == A["block.right.zz"]) {
                    lastId = (this._numCols % 2 == 0) ? this._$blocks.data("tlId") : this._$blocks.data("blId");
                    j = this._numCols - 1;
                    incr = -1;
                    horz = false
                } else if (b == A["block.top.zz"]) {
                    lastId = (this._numRows % 2 == 0) ? this._$blocks.data("blId") : this._$blocks.data("brId");
                    i = 0;
                    incr = 1;
                    horz = true
                } else {
                    lastId = (this._numRows % 2 == 0) ? this._$blocks.data("tlId") : this._$blocks.data("trId");
                    i = this._numRows - 1;
                    incr = -1;
                    horz = true
                }
                this._intervalId = setInterval(function () {
                    c._$blockArr[i][j].animate({
                        opacity: 1
                    }, c._duration, c._rotator.settings.easing, function () {
                        if ($(this).attr("id") == lastId) {
                            c._rotator.showContent(a)
                        }
                    });
                    if (c._$blockArr[i][j].attr("id") == lastId) {
                        clearInterval(c._intervalId);
                        c._intervalId = null
                    }
                    if (horz) {
                        (d ? j++ : j--);
                        if (j == c._numCols || j < 0) {
                            d = !d;
                            j = (d ? 0 : c._numCols - 1);
                            i += incr
                        }
                    } else {
                        (d ? i++ : i--); if (i == c._numRows || i < 0) {
                            d = !d;
                            i = (d ? 0 : c._numRows - 1);
                            j += incr
                        }
                    }
                }, this._delay)
            },
            dirAnimate: function (a, b) {
                var c = new Array(this._total);
                var d;
                var e = 0;
                switch (b) {
                case A["block.left"]:
                    d = this._$blocks.data("brId");
                    for (var j = 0; j < this._numCols; j++) {
                        for (var i = 0; i < this._numRows; i++) {
                            c[e++] = this._$blockArr[i][j]
                        }
                    }
                    break;
                case A["block.right"]:
                    d = this._$blocks.data("blId");
                    for (var j = this._numCols - 1; j >= 0; j--) {
                        for (var i = 0; i < this._numRows; i++) {
                            c[e++] = this._$blockArr[i][j]
                        }
                    }
                    break;
                case A["block.top"]:
                    d = this._$blocks.data("brId");
                    for (var i = 0; i < this._numRows; i++) {
                        for (var j = 0; j < this._numCols; j++) {
                            c[e++] = this._$blockArr[i][j]
                        }
                    }
                    break;
                default:
                    d = this._$blocks.data("trId");
                    for (var i = this._numRows - 1; i >= 0; i--) {
                        for (var j = 0; j < this._numCols; j++) {
                            c[e++] = this._$blockArr[i][j]
                        }
                    }
                }
                e = 0;
                var f = this;
                this._intervalId = setInterval(function () {
                    c[e++].animate({
                        width: f._size,
                        height: f._size
                    }, f._rotator.settings.transitionSpeed, f._rotator.settings.easing, function () {
                        if ($(this).attr("id") == d) {
                            f._rotator.showContent(a)
                        }
                    });
                    if (e == f._total) {
                        clearInterval(f._intervalId);
                        f._intervalId = null
                    }
                }, this._delay)
            },
            randomAnimate: function (a) {
                randomizeArray(this._arr);
                var i = 0;
                count = 0;
                var b = this;
                this._intervalId = setInterval(function () {
                    $(b._arr[i]).animate({
                        top: $(b._arr[i]).data("top"),
                        width: b._size,
                        height: b._size,
                        opacity: 1
                    }, b._rotator.settings.transitionSpeed, b._rotator.settings.easing, function () {
                        if (++count == b._total) {
                            b._rotator.showContent(a)
                        }
                    });
                    i++;
                    if (i == b._total) {
                        clearInterval(b._intervalId);
                        b._intervalId = null
                    }
                }, this._delay)
            },
            spiral: function (a, b) {
                var i = 0,
                    j = 0;
                var c = this._numRows - 1;
                var d = this._numCols - 1;
                var e = 0;
                var f = d;
                var g = new Array();
                while (c >= 0 && d >= 0) {
                    var h = 0;
                    while (true) {
                        g[g.length] = this._$blockArr[i][j];
                        if ((++h) > f) {
                            break
                        }
                        switch (e) {
                        case 0:
                            j++;
                            break;
                        case 1:
                            i++;
                            break;
                        case 2:
                            j--;
                            break;
                        case 3:
                            i--
                        }
                    }
                    switch (e) {
                    case 0:
                        e = 1;
                        f = (--c);
                        i++;
                        break;
                    case 1:
                        e = 2;
                        f = (--d);
                        j--;
                        break;
                    case 2:
                        e = 3;
                        f = (--c);
                        i--;
                        break;
                    case 3:
                        e = 0;
                        f = (--d);
                        j++
                    }
                }
                if (g.length > 0) {
                    if (b) {
                        g.reverse()
                    }
                    var l = g.length - 1;
                    var m = g[l].attr("id");
                    var k = 0;
                    var n = this;
                    this._intervalId = setInterval(function () {
                        g[k].animate({
                            opacity: 1
                        }, n._rotator.settings.transitionSpeed, n._rotator.settings.easing, function () {
                            if ($(this).attr("id") == m) {
                                n._rotator.showContent(a)
                            }
                        });
                        if (k == l) {
                            clearInterval(n._intervalId);
                            n._intervalId = null
                        }
                        k++
                    }, this._delay)
                }
            }
        };
        var K = $(this);
        K.addClass("sliding-image-gallery-v2");
        K.css({
            visibility: "visible"
        });
        return this.each(function () {
            objImageGallery = new SlidingImageGalleryV2($(this), o)
        })
    }
})(jQuery);