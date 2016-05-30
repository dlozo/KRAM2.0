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
                $('#searchResults').each(function (i) {
                    if (data.Name === undefined) {
                        console.log(data);
                        $('#searchResults').html("Undefined: " + data);
                    }
                    else {
                        $('#searchResults').html
                            (data[i].Name);
                    };
                }
        );
            }
        });
    });
});