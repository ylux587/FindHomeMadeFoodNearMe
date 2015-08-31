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
    window.location.href = webBaseAddress + url;
}

function RedirectToLogin() {
    RedirectTo("/Home/Login");
}

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});
