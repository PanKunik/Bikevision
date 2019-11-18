$(document).ready(function () {

    var tableTds = $("td[name^='comparation_']");
        
    tableTds.each(function () {
        var elements = $("td[name*='" + $(this).attr('name') + "']");
        var namedTds = elements.toArray();
        
        var lengths = namedTds.length;
        
        var same = true;

        for (var i = 1; i < lengths; i++) {
            if (namedTds[0].innerText != namedTds[i].innerText)
                same = false;
        }

        if (!same)
            elements.parent().addClass("table-info");
    });

});