$(document).ready(function () {
    //Bild och filnamn visas för användaren när dom lagt till och inte ännu tryckt in submit
    $('#file').change(function (event) {
        var tmppath = URL.createObjectURL(event.target.files[0]);
        $("#browseimg").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
        $("#filename").html($('input[type=file]')[0].files[0].name);
     

    });
    //$("#imgurl").bind('paste', function(event) {
    //    var _this = this;
    //    var x = document.getElementById("#imgurl").value();
    //    setTimeout(function () {
    //        var text = $(_this).val();
          
    //    }, 100);
    //    $("#linkimage").fadeIn("fast").attr('src', +"'" + $(_this).val() + "'");
    //    //$("#imgurl").on("paste", function () {
    //    //    var x = document.getElementById("#imgurl").value();
    //    //    $("#linkimage").fadeIn("fast").attr('src', +"'" +x  + "'");
    //    });

    });
