$(document).ready(function () {

    $(".lastViewed__button").click(function () {
        $(this).toggleClass("hidden");
        $(".lastViewed__container").toggleClass("hidden");
    });

    $(".lastViewed__container").mouseleave(function () {
        $(".lastViewed__button").toggleClass("hidden")
        $(this).toggleClass("hidden");
    });

});