$(function () {
    var path = $(location).attr('href');
    var id = path.substr(path.lastIndexOf('=') + 1);
    var url = '/Pictures/GetPic/' + id;
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (pic) {
            var dataArr = "data:" + pic.MIME + ";base64," + pic.Image;
            $("#note-pic").prop('src', dataArr);
        },
        error: function (error) {
            $(that).remove();
            DisplayError(error.statusText);
        }
    });
});