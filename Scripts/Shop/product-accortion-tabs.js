var lastWindowState = "";

function TabsToAccordion() {
    if (($(".product__menu-card").css("display") == "block") && (lastWindowState != "smallScreen")) {
        lastWindowState = "smallScreen";
        $(".product__menu").find(".nav-link").removeClass("active");
        $(".tab-pane").removeClass("show");
        $(".tab-pane").removeClass("active");
    }

    if (($(".product__menu-card").css("display") == "none") && (lastWindowState != "bigScreen")) {
        lastWindowState = "bigScreen";
        $(".tab-pane").removeClass("show");
        $(".tab-pane").removeClass("active");

        $(".tab-pane").first().addClass("show");
        $(".tab-pane").first().addClass("active");

        $(".rotate").removeClass("down");

        $(".product__menu").find(".nav-link").first().addClass("active");
    }
}

$(document).ready(function () {
    if ($(".product__menu-card").css("display") == "block") {
        lastWindowState = "smallScreen";
    }

    if ($(".product__menu-card").css("display") == "none") {
        lastWindowState = "bigScreen";

        $(".tab-pane").first().addClass("show");
        $(".tab-pane").first().addClass("active");

        $(".product__menu").find(".nav-link").first().addClass("active");
    }

    TabsToAccordion();

    $(".product__menu-card").click(function () {
        $(this).next().toggleClass("show").toggleClass("active");
        $(this).children("i.rotate").toggleClass("down");
    });

    $(window).resize(function () {
        TabsToAccordion();
    });

    $('select').on('change', function () {
        $(".product__final-price").text(($('option:selected', this).attr('value') * $(".product__actual-price").attr('value')).toString() + " zł");

        $("#HiddenQuantity").attr('value', $('option:selected', this).attr('value'));
    });
    //$("#itemQuantity").change(function () {
    //    $(window).alert("Ok");
    //    $(".product__final-price").text($("#itemQuantity").val * $(".product__actual-price").text());
    //});
});