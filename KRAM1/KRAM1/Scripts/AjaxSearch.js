//$(document).ready(function () {
//    $('#searchInput').bind('input', function () {
//        $('#searchResults').text('Fetching search predictions...');
//        var searchInput = $('#searchInput');
//        $.ajax({
//            method: 'GET',
//            url: '/Image/HashtagSearch',
//            data: {
//                hashtag: $('#searchInput').val()
//            },
//            dataType: 'json',
//            error: function (jqXHR, textStatus, errorThrown) {
//                // alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
//            },
//            success: function (data) {
//                var arr1 = "";
//                var test = [];
//                for (var i = 0; i < data.length; i++) {
//                    if (data[i].Name === undefined) {
//                        console.log("123");
//                        $('#searchResults').html("No hashtags by that name: " + data);
//                    }
//                    else {
//                        console.log("456");
//                        test.push(data[i].Name)
//                        arr1 += "<li>" + "<a href=/Image/ImageSearch?searchInput=" + data[i].Name + ">" + "#" + data[i].Name + "</a>" + "</li>";
//                        //$('#searchResults').attr("href", "/Image/ImageSearch?searchInput=" + data[i].Name);//Skriver ut länk
//                    };
//                }
//                $('#searchInput').autocomplete({
//                    source: test
//                });
//                $('#searchResults').html(arr1); //Skriver ut arr1 som har alla "hashtags" som data loopat igenom.
//                if (searchInput.val().length === 0) {
//                    arr1 = "";
//                    $('#searchResults').html(arr1)
//                }
//            }
//        });
//    });
//});

$(document).ready(function () {
    $("#searchInput").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Image/HashtagSearch",
                type: "POST",
                dataType: "json",
                data: { hashtag: $('#searchInput').val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
});