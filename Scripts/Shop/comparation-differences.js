$(document).ready(function () {

    var tableTds = $("td[name^='comparation_']");

    var maxHeight = -1;

    $("th").each(function () {
        maxHeight = maxHeight > $(this).height() ? maxHeight : $(this).height();
    });

    $("th").each(function () {
        $(this).height(maxHeight);
    });

    tableTds.each(function () {
        var elements = $("td[name='" + $(this).attr('name') + "']");
        var namedTds = elements.toArray();

        maxHeight = -1;

        $("td[name='" + $(this).attr('name') + "']").each(function () {
            maxHeight = maxHeight > $(this).height() ? maxHeight : $(this).height();
        });

        $("td[name='" + $(this).attr('name') + "']").each(function () {
            $(this).height(maxHeight);
        });

        var lengths = namedTds.length;
        
        var same = true;
        if (lengths > 1) {
            for (var i = 1; i < lengths; i++) {
                if (namedTds[0].innerText != namedTds[i].innerText)
                    same = false;
            }

            if (!same)
                elements.parent().addClass("table-info");
        }
    });

});