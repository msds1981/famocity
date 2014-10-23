
$(document).ready(function () {
    $(".album-bg-3").live('click', function () {ShowPhoto($(this));});      //show photos when clicked on album's photo
    $(".album-title").live('click', function () { ShowPhoto($(this)); });   //show photos when clicked on album's title

    /*********** animation *************/
    //$('.photo-top-caption').hide();   //hidden from css
    //$('.photo-bottom-caption').hide();//hidden from css

    $('#photos-section ul li').live('mouseenter', function () {
        $(this).children('.photo-top-caption').animate({
            height: "50"
        }, 100).show();

        //تم اخفاء الجزء السفلي من الصورة لعدم اكتمال البوب اب الخاص بعرض صورة
        //see ClassMain.cs function BuildButtonCaption() line #2845 

        /*    $(this).children('.photo-bottom-caption').animate({
        height: "50"

        }, 100).show();*/
    });

    $('#photos-section ul li').live('mouseleave', function () {
        $(this).children('.photo-top-caption').animate({
            height: "-=50"
        }, 100).hide();
        /*
        $(this).children('.photo-bottom-caption').animate({
        height: "-=50"
        }, 100).hide();*/
    });
});

function ShowPhoto(div) {

    var albid = div.parent().attr("albmid"); //album id getting from .album_blockli
    var uid = div.parent().attr("uid");
   // alert(albid + " " + uid);
    ShowAlbumContent(albid,uid);
}

//create new album
function NewAlbum() {
    //alert($("li.album_blockli :last").attr("id") + "");
    //return;
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/CreateNewAlbum",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                $("#seemorealbum").before(data.d);
            }
        }
    });
}

function showInputRenameAlbum(albm, div) {
    var d = $(div);
    var inpid = "inputtextalbum_" + d.attr("id");
    var alb = d.attr("albmid"); /// <reference path="../images/cross-small-icon.png" />

    d.html("<img src='/images/cross-small-icon.png' class='cancel-editing-img' title='الغاء التعديل' onclick='CancelAlbumEditing(" + alb + ",\"" + d.html() + "\");'/>" +
           "<input id='" + inpid + "' type='text' value='" + d.html() + "' class='album-rename'  onkeypress='return onKeyPressRenameAlbum(event,\"" + inpid + "\"," + alb + ");' " +
           " />");
    $("#" + inpid).focus();
    var p = document.getElementById(inpid);
    p.setSelectionRange(0, p.value.length);
}

//press enter to save new album name
function onKeyPressRenameAlbum(e, inputid, albmid) {
    var key = window.event ? e.keyCode : e.which;
    if (key == 13) {
        //alert($(div).find("input").val());
        var inp = $("#" + inputid);
        RenameAlbum(inp.val(), albmid);
        return false;
    }
    else
        return true;
}

function RenameAlbum(text, albid) {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/RenameAlbum",
        data: "{'albmid':'" + albid + "','name':'" + text + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d > 0) {
                //$("#albmname_" + albid).html(text);
                CancelAlbumEditing(albid,text);
            }
        }
    });
    
}

function CancelAlbumEditing(albid,text) {
    $("#albmname_" + albid).html(text);
}

function DeleteAlbum(albm) {
    var albmid = $(albm).attr("albmid");
    if (confirm('هل انت متأكد من حذف الألبوم؟')) {
        $.ajax({
            type: "POST",
            url: "/Methods.aspx/DeleteAlbum",
            data: "{'albmid':'" + albmid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d > 0) {
                    $(albm).fadeOut('slow');
                }
            }
        });
    }
}


// اظهار الصور الي داخل الالبوم 
//show album content
function ShowAlbumContent(albumid, uid) {

    $.ajax({
        type: "POST",
        url: "/Methods.aspx/ShowPhotos",
        data: "{'alboum':'" + albumid + "','userid':'" + uid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                $("#photos-section").html(data.d);
                FillAlbumList();
                $(".album-title").css("background", "#593753"); //blue not selected
                $("#albmname_" + albumid).css("background", "#92af4b"); //green selected
            }
        }
    });
}

function FillAlbumList() {
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/FillAlbum",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //  mnubarloader.html("");
            if (data.d != "") {
                $("select#albumlist").html(data.d);
            }
        }

    });
}

//move photo from album to another
function MoveToAlbum() {
    var el = document.getElementById('albumlist');
    var alb = el.options[el.selectedIndex].value;
    var fid = $('#hdnfileId');
    $("#movetheimage").attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/MoveToAlum",
        data: "{'file':'" + fid.val() + "','album':'" + alb + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(data.d);
            if (data.d == true) {
                $("#photo_file_" + fid.val()).remove();
                $("#movetheimage").removeAttr('disabled');
                $('#moveimage').trigger('reveal:close');
            }
        }
    });
}

//close the popup of Moving photos
function CloseMovingAlbum() {
    $('#moveimage').trigger('reveal:close');
}

function DeleteFile(filetag) {
    
    var fid = $(filetag).attr("file");
    if (confirm('هل انت متأكد من حذف الملف؟')) {
        $.ajax({
            type: "POST",
            url: "/Methods.aspx/DeleteFile",
            data: "{'fileid':'" + fid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d > 0) {
                    $(filetag).hide('slow', function () { $(filetag).remove(); });
                }
            }
        });
    }
}

function ChangeAlbumCover(fileid) {
    
    $.ajax({
        type: "POST",
        url: "/Methods.aspx/ChangeAlbumCover",
        data: "{'fileid':'" + fileid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var p = new Array();
                p = data.d;
                
                $("#albumimagecover_" + p[0]).attr("src",p[1]);
            }
        }
    });
}