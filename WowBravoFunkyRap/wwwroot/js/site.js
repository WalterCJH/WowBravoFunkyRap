
function _scroll() {
    var scrollTop = $(window).scrollTop();
    if (scrollTop < 10) {
        $(".navbar-opacity").css("opacity", 1);
    }
    else {
        $(".navbar-opacity").css("opacity", 0.95);
    }
}
$(window).on("scroll", function () {
    _scroll()
});;