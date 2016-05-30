$(document).ready(function () {
    $('#searchInput').bind('input', function () {
        $('#searchResults').text('Fetching search predictions...');

        $.ajax({
            method: 'GET',
            url: '/Image/HashtagSearch',
            data: {
                hashtag: $('#searchInput').val()
            },
            dataType: 'json',
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
            },
            success: function (data) {
                var arr1 = "";
                //$('#searchResults').each(function (i) For loop är bättre, fuck ".each"
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Name === undefined) {
                        console.log(data);
                        $('#searchResults').html("Undefined: " + data);
                    }
                    else {
                        arr1 += "<br /> " + data[i].Name;
                    };
                } {
                }
                $('#searchResults').html(arr1);
            }
        });
    });
});