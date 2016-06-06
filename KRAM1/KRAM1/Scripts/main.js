//// external js: masonry.pkgd.js, imagesloaded.pkgd.js
jQuery.noConflict();
var grid = document.querySelector('.grid');

var msnry = new Masonry(grid, {
    itemSelector: '.grid-item',
    columnWidth: '.grid-sizer',
    percentPosition: false
});

imagesLoaded(grid).on('progress', function () {
    // layout Masonry after each image loads
    msnry.layout();
});
