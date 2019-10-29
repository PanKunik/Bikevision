window.addEventListener("load", function () {
    $("body").toggleClass("hidden-x");
});

window.addEventListener("unload", function () {
    $("body").toggleClass("hidden-x");
});

function showFilters() {
    $(".overlay").toggleClass("show-overlay");
    $(".left-column").toggleClass("show-menu");
    $("body").css("overflow-y", $(".left-column").hasClass("show-menu") ? "hidden": "visible");
    $(".floating-button").css("transform", $(".left-column").hasClass("show-menu") ? "translateX(-100px)" : "translateX(0)");
}
