$(document).ready(function () {
    //Bild och filnamn visas för användaren när dom lagt till och inte ännu tryckt in submit
    $('#file').change(function (event) {
        var tmppath = URL.createObjectURL(event.target.files[0]);
        $("#browseimg").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
        $("#filename").html($('input[type=file]')[0].files[0].name);


    });
    //Hideshow images in gallery
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
    //Hide/SHow Flickr Search
    $("#flickrbutton").click(function () {
        if ($(".flickrs").is(":hidden")) {
            $(".flickrs").slideDown("slow");
        } else {
            $(".flickrs").hide();

        };
    });

    //HideSHowimageurl for upload/submit page
    $('#hideshowimage').click(function (e) {
        e.preventDefault();
        e.stopPropagation(); 
        $("#SubmitURLShow").css('visibility', 'visible');

    });

    $('#SubmitURLShow').click(function (e) {
        e.stopPropagation(); // when you click within the content area, it stops the page from seeing it as clicking the body too
    });
    $('body').click(function () {
        $("#SubmitURLShow").css('visibility', 'hidden');
    });
   
    $('#hideshowfile').click(function () {
      $("#SubmitURLShow").css('visibility', 'hidden');
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
    //HideSHowimageFILE for upload/submit page
    $('#hideshowfile').click(function (e) {
        e.preventDefault(); 
        e.stopPropagation(); 
        $("#SubmitFileShow").css('visibility', 'visible');

    });

    $('#SubmitFileShow').click(function (e) {
        e.stopPropagation(); 
    });
    $('body').click(function () {
        $("#SubmitFileShow").css('visibility', 'hidden');
    });

    $('#hideshowimage').click(function () {
        $("#SubmitFileShow").css('visibility', 'hidden');
    });
});
