$('#PostCommentButton').click(function () {
    var url = "/Home/PostComment";
    var id = $("#id").val();
    var comment = $("#comment").val();

    $.ajax({
        method: 'POST',
        url: '/Home/PostComment',
        data: {
            pictureId: id, comment: comment
        },
        dataType: 'json',
       
        success: function (data) {
            var date = new Date(parseInt(data.TimeStamp.substr(6)));
            $("#commentList").append("<li>" + "<p>" + data.Text + "</p><p>" + date.toLocaleString("sv-SE") + ", " + data.UserName + "</p></li>");


        }
    });
})
