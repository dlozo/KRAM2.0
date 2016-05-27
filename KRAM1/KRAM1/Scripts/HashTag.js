$(document).ready(function () {
    $("span").hover(function () {
        $(this).css("color", "#02baff");
    }, function () {
        $(this).css("color", "");
    });
});