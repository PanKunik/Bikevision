$(document).ready(function() {

    $(".lastViewed__button").click(function () {
        $(this).toggleClass("hidden");
        $(".lastViewed__container").toggleClass("hidden");
    });

    $(".lastViewed__container").mouseleave(function () {
        $(".lastViewed__button").toggleClass("hidden")
        $(this).toggleClass("hidden");
    });
//console.log("To ship: " + hours + ":" + minutes + ":" + seconds)

    //switch (shippingDate.getDay)
    //{
    //    case 0:
    //        break;
    //    case 5:
    //        shippingDate.setDate(shippingDate.Date + 2);
    //        break;
    //    case 6:
    //        shippingDate.setDate(shippingDate.Date + 1);
    //        break;
    //}

    setInterval(function() {
        var currentDate = new Date();
        var dayOfship = "";

        var timeToShipping = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), 14, 0, 0, 0);

        var timeToShip = new Date();

        if (currentDate.getDay() > 0 && currentDate.getDay() < 6) {
            if (currentDate.getHours() < 14) {
                dayOfship = "jeszcze dziś";
            }
            else {
                if (currentDate.getDay() == 5) {
                    timeToShipping.setDate(timeToShipping.getDate() + 3);
                    dayOfship = "w najbliższy poniedziałek";
                }
                else {
                    timeToShipping.setDate(timeToShipping.getDate() + 1);
                    dayOfship = "już jutro";
                }
            }
        }
        else {
            if (currentDate.getDay() == 6) {
                timeToShipping.setDate(timeToShipping.getDate() + 2);
                dayOfship = "pojutrze";
            }
            if (currentDate.getDay() == 0) {
                timeToShipping.setDate(timeToShipping.getDate() + 1);
                dayOfship = "już jutro";
            }
        }

        var timeToShip = timeToShipping.getTime() - currentDate.getTime();
        var hours = Math.floor(timeToShip / 1000 / 60 / 60);
        timeToShip -= hours * 1000 * 60 * 60;
        var minutes = Math.floor(timeToShip / 1000 / 60);
        timeToShip -= minutes * 1000 * 60;
        var seconds = Math.floor(timeToShip / 1000);

        hours = (hours < 10) ? '0' + hours : hours;
        minutes = (minutes < 10) ? '0' + minutes : minutes;
        seconds = (seconds < 10) ? '0' + seconds : seconds;

        $("#timeToShipping").text(hours + ":" + minutes + ":" + seconds);
        $("#dateOfShipping").text(dayOfship);
        //code goes here that will be run every 5 seconds.    
    }, 1000);

});