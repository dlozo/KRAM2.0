$(document).ready(function () {
    $('#searchInput').on('input', function () {
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
                if (data.Name === undefined)
                    $('#searchResults').html("Undefined: " + data);
                else
                    $('#searchResults').html
                        (data.Name);
            }
        });
    });
});