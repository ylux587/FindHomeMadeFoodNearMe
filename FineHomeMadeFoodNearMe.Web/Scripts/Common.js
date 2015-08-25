function PostData(url, data, onSuccess, onError) {
    $.ajax({
        url: url,
        data: data,
        type: 'POST',
        success: onSuccess,
        error: onError
    });
}