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
                        console.log("123");
                        $('#searchResults').html("No hashtags by that name: " + data);
                    }
                        //"<br /> @Html.ActionLink(item.Name, 'HashtagSearch', 'Image', new { hashtag = " + data[i].Name + " }, null)" + data[i].Name;
                    else {
                        console.log("456");
                        //arr1 += '@URL.Action("HashImg", "Image", new { fileName = "-1"})';
                        //arr1 = arr1.replace("-1", data[i].Id)
                        arr1 += "<br />" + data[i].Name;
                        //  var url = '@Html.Raw(@Url.Action("ImageSearch",   "Image"))' + '?hashtag=' + data[i].Name;
                        $('#searchResults').attr("href", "/Image/ImageSearch?searchInput=" + data[i].Name);//Skriver ut länk
                        $('#searchResults').text('#' + data[i].Name);
                    };
                } {
                }
                $('#searchResults').html(arr1); //Skriver ut arr1 som har alla "hashtags" som data loopat igenom.
            }
        });
    });
});