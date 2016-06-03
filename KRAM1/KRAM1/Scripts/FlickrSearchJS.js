$(document).ready(function () {
    $('#flickrSearchInput').bind('input', function () {
        $('#flickrSearchResults').text('Fetching search predictions...');

        $.ajax({
            method: 'GET',
            url: '/Api/FlickrApi/FlickrSearch',
            data: {
                searchInput: $('#flickrSearchInput').val()
            },
            dataType: 'json',
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
            },
            success: function (data) {
                var arr1 = "";
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Title === undefined) {
                        console.log("123");
                        $('#flickrSearchResults').html("No hashtags by that name: " + data);
                    }
                    else {
                        console.log("456");
                        arr1 += "<li>" + "<a href=/Api/FlickrApi/FlickrSearch?searchInput=" + data[i].Title + ">" + "#" + data[i].Tag + "</a>" + "</li>";
                        $('#flickrSearchResults').attr("href", "/Api/FlickrApi/FlickrSearch?searchInput=" + data[i].Title);//Skriver ut länk
                    };
                }
                $('#flickrSearchResults').html(arr1); //Skriver ut arr1 som har alla "hashtags" som data loopat igenom.
            }
        });
    });
});