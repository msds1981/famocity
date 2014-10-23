function GoToPage(page) {
    //
    $.ajax({
        type: "POST",
        url: '/Methods.aspx/PassportIsValid',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (!data.d){
                window.location = '/Login';
            }
            else{
                $("#left_sidebar_cms").load(page);
            }
        },
    });
}
