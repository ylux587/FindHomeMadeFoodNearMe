//var baseAddress = "http://findhomemadefoodnearmeservices.azurewebsites.net/api";
var baseAddress = "http://localhost:53772/api";

function PostData(url, data, onSuccess, onError) {
    $.ajax({
        url: url,
        data: data,
        type: 'POST',
        success: onSuccess,
        error: onError
    });
}

function GetData(url, onSuccess, onError) {
    $.ajax({
        url: url,
        type: 'GET',
        success: onSuccess,
        error: onError
    });
}

function ValidateField(value, errorLabelId) {
    if (!value || value == "") {
        $(errorLabelId).show();
        return false;
    }
    return true;
}

function GetUserIdFromCookie() {
    return $.cookie("userId");
}

function RedirectTo(url) {
    window.location.href = base + url;
}

function RedirectToLogin() {
    RedirectTo("/Home/Login");
}
