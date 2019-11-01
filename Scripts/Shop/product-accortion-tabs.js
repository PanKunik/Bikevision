$(document).ready(function () {
    $("input[type='radio'").click(function (event) {
        $(".product__single-tab.d-block").removeClass("d-block");
        $(".product__single-tab[data-id='" + event.target.id + "']").addClass("d-block");// + "    " + event.target.id);
    });
});