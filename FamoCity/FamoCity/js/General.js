
//Encrypt Parameters
function encrypt(parameter) {
    var u = "";
    //alert(parameter);
    $.ajax({
        type: "POST",
        url: '/Methods.aspx/getEncryptedParameter',
        data: "{'parameter':'" + parameter + "'}",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            u = data.d;
        },
        error: AjaxFailed

    });
    return u;
}

function fireAjax(url, data) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(data.d+"");
            if (data.d > 0)
                return true;
            else
                return false;
        }
    });
}

function AjaxFailed(result) {
    alert(result.status + ' ' + result.statusText);
}
