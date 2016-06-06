$(document).ready(function () {
    //Bild och filnamn visas för användaren när dom lagt till och inte ännu tryckt in submit
    $('#file').change(function (event) {
        var tmppath = URL.createObjectURL(event.target.files[0]);
        $("#browseimg").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
        $("#filename").html($('input[type=file]')[0].files[0].name);


    });
    $("#click").click(function () {
        $(".middiv").css({
            "opacity": "0",
            "display": "block",
        }).show().animate({ opacity: 1 }, 1000)
        $("#click").css({
            "opacity": "0",
            "display": "block",
        }).show().animate({ opacity: 0 })
    });
    $("#flickrbutton").click(function () {
        if ($("li:nth-child(2)").is(":hidden")) {
            $("li").slideDown("slow");
        } else {
            $("li:nth-child(2)").hide();

        };
    });
    function nospaces(t) {
        if (t.value.match(/\s|\./g)) {
            $(this).closest('p').text("Please refrain from using space or unique symbols");
            $('#error').show();
            //$('#error').html("Please refrain from using space or unique symbols")
            setTimeout(function () {
                $('#error').fadeOut();
            }, 5000);

            t.value = t.value.replace(/\s/g, '');
        }
    }
});