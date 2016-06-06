$('flickrSearchBtn').click(function () {
    $.ajax({
        method: 'GET',
        url: '/FlickrApi/FlickrSearch',
        data: {
            flickrSearchInput: $('#flickrSearchInput').val()
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //   alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
        },
        success: function (data) {
            //$('#notifcationCount').html(data);
        }
    })
});