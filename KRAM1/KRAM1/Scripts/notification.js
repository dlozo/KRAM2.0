$(document).ready(function(){

    $.ajax({
        type: "GET",
        url: "Image/notificationCount",
        contentType: "application/json;charset=utf-8",
        data ,
        dataType: "json",
        success: function () {
            alert("hejsan")
            //$("#notification").html(Data)
        },
        error: function () {

            alert('error');
        }
    });

});