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
    $(".floating-button").css("left", $(".left-column").hasClass("show-menu") ? "5px" : "calc(50% - 100px)");
    $(".floating-button").html($(".left-column").hasClass("show-menu") ? "Zamknij filtry" : "<i class=\"fas fa-filter\"></i> Filtruj");
    $(".left-column").css("padding-bottom", $(".left-column").hasClass("show-menu") ? "75px" : "0");
}
