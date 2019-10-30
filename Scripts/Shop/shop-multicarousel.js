$(document).ready(function () {
    var itemsMainDiv = ('.MultiCarousel');
    var itemsDiv = ('.MultiCarousel-inner');
    var itemWidth = "";

    var cardMargins = 10;
 
    $('.leftLst, .rightLst').click(function () {
        var condition = $(this).hasClass("leftLst");
        if (condition)
            click(0, this);
        else
            click(1, this)
    });
 
    ResCarouselSize();
 
 
 
 
    $(window).resize(function () {
        ResCarouselSize();
    });
 
    //this function define the size of the items
    function ResCarouselSize() {
        var incno = 0;
        var dataItems = ("data-items");
        var itemClass = ('.shopping-card');
        var id = 0;
        var btnParentSb = '';
        var itemsSplit = '';
        var sampwidth = $(itemsMainDiv).width() - 20;
        var bodyWidth = $('body').width();
        $(itemsDiv).each(function () {
            id = id + 1;

            var shopping_cards = $(this).find(itemClass);
            var itemNumbers = shopping_cards.length;

            $(shopping_cards).each(function () {
                $(this).css({ 'padding-bottom': 'calc(calc(100% / ' + itemNumbers + ') + 60px)', 'margin': '0px', 'margin-right': (cardMargins / 2) + 'px', 'margin-left': (cardMargins / 2) + 'px' });
            });

            //$(".shopping-card:last-child").css({ 'margin-right': (cardMargins/2) + 'px' });
            //$(".shopping-card:first-child").css({ 'margin-left': (cardMargins/2) + 'px' });

            btnParentSb = $(this).parent().parent().attr(dataItems);
            itemsSplit = btnParentSb.split(',');
            $(this).parent().parent().attr("id", "MultiCarousel" + id);
 
 
            if (bodyWidth >= 1200) {
                incno = itemsSplit[3];
                itemWidth = (sampwidth / incno) - cardMargins;
            }
            else if (bodyWidth >= 992) {
                incno = itemsSplit[2];
                itemWidth = (sampwidth / incno) - cardMargins;
            }
            else if (bodyWidth >= 768) {
                incno = itemsSplit[1];
                itemWidth = (sampwidth / incno) - cardMargins;
            }
            else {
                incno = itemsSplit[0];
                itemWidth = (sampwidth / incno) - cardMargins;
            }
            $(this).css({ 'transform': 'translateX(0px)', 'width': ((itemWidth * itemNumbers) + (itemNumbers * cardMargins)) });
            $(this).find(itemClass).each(function () {
                $(this).outerWidth(itemWidth);
            });
 
            $(".leftLst").removeClass("over");
            $(".rightLst").removeClass("over");
 
        });
    }
 
 
    //this function used to move the items
    function ResCarousel(e, el, s) {
        var leftBtn = ('.leftLst');
        var rightBtn = ('.rightLst');
        var translateXval = '';
        var divStyle = $(el + ' ' + itemsDiv).css('transform');
        var values = divStyle.match(/-?[\d\.]+/g);
        var xds = Math.abs(values[4]);
        if (e == 0) {
            translateXval = xds - (itemWidth + cardMargins) * s;
            $(el + ' ' + rightBtn).removeClass("over");
 
            if (translateXval < 0) {
                var itemsCondition = $(el).find(itemsDiv).width() - $(el).width();
                translateXval = itemsCondition + (2 * cardMargins);
            }
        }
        else if (e == 1) {
            var itemsCondition = $(el).find(itemsDiv).width() - $(el).width();
            translateXval = xds + (itemWidth + cardMargins) * s;
            $(el + ' ' + leftBtn).removeClass("over");
 
            if (translateXval >= itemsCondition - itemWidth / 2) {
                translateXval = 0;
            }
        }
        $(el + ' ' + itemsDiv).css('transform', 'translateX(' + -translateXval + 'px)');
    }
 
    //It is used to get some elements from btn
    function click(ell, ee) {
        var Parent = "#" + $(ee).parent().attr("id");
        var slide = $(Parent).attr("data-slide");
        ResCarousel(ell, Parent, slide);
    }
 
});