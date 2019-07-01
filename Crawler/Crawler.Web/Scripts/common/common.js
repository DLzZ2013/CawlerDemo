var successCallback;
function invokeDefault(url, data) {
    $.ajax({
        url: url,
        contentType: "application/json",
        dataType: "json",
        data: data,
        type: "Post",
        success: successCallback,
        error: function (error) {
            alert("数据错误！");
        }
    });
}