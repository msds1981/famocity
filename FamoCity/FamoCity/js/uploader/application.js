﻿/*
* jQuery File Upload Plugin JS Example 5.0.2
* https://github.com/blueimp/jQuery-File-Upload
*
* Copyright 2010, Sebastian Tschan
* https://blueimp.net
*
* Licensed under the MIT license:
* http://creativecommons.org/licenses/MIT/
*/

/*jslint nomen: true */
/*global $ */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload();
    $('#fileupload2').fileupload();
//    var jqXHR = $('#fileupload').fileupload('send', { files: filesList })
//    .success(function (result, textStatus, jqXHR) { alert("success"); })
//    .error(function (jqXHR, textStatus, errorThrown) { /* ... */ })
//    .complete(function (result, textStatus, jqXHR) { /* ... */ });
    

    // Load existing files:
    /*    $.getJSON($('#fileupload form').prop('action'), function(files) {
    var fu = $('#fileupload').data('fileupload');
    fu._adjustMaxNumberOfFiles(-files.length);
    fu._renderDownload(files)
    .appendTo($('#fileupload .files'))
    .fadeIn(function() {
    // Fix for IE7 and lower:
    $(this).show();
    });
    });
    */
    // Open download dialogs via iframes,
    // to prevent aborting current uploads:
   
   
   /* $('#fileupload .files a:not([target^=_blank])').live('click', function (e) {
        e.preventDefault();
        $('<iframe style="display:none;"></iframe>')
            .prop('src', this.href)
            .appendTo('body');
    });*/

});