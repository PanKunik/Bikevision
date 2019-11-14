$(document).ready(function () {
    $("#listButton").click(function () {
        //window.alert("List view");
        $(".shopping-card").each(function () {
            $(this).addClass("shopping-list").removeClass("shopping-card");
            $(this).find(".wrapRow").wrapAll("<div class='d-flex flex-row wrappedRow'></div>");
            $(this).find(".wrapColumn").wrapAll("<div class='d-flex flex-column justify-content-between flex-fill mx-1 px-1 wrappedColumn'></div>");
            $(this).find(".shopping-card__footer").addClass("d-flex flex-column");
            $(this).find(".shopping-card__hidden-content").removeClass("d-none d-lg-block");
            $(this).find(".shopping-card__hidden-content").appendTo($(this).find(".wrappedColumn"));
        });
    });

    $("#gridButton").click(function () {
        $(".shopping-list").each(function () {
            $(this).addClass("shopping-card").removeClass("shopping-list");
            $(this).find(".wrapColumn").unwrap();
            $(this).find(".wrapRow").unwrap();
            $(this).find(".shopping-card__footer").removeClass("d-flex flex-column");
            $(this).find(".shopping-card__hidden-content").addClass("d-none d-lg-block");
            $(this).find(".shopping-card__hidden-content").appendTo($(this).find(".shopping-card__main-column"));
        });
    });

    $.ajax({
        type: "POST",
        url: "/Shop/SortProducts",
        data: JSON.stringify(customers),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            alert(r + " record(s) inserted.");
        }
    });
});