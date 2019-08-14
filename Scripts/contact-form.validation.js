$(function () {
    $.validate({
        lang: 'pt'
    });
});

/*$(function () {
    $('#submit').on("click", function (e) {
        if ($('.invalid-feedback').length > 0) {
            alert('Niepoprawnie wypełniłeś pola');
            e.preventDefault(e);
        }
        if ($('.valid-feedback').length > 0) {
            alert('Poprawnie wypełniłeś pola');
            e.preventDefault(e);
        }
    });
});

$(function () {
    $('#name').on("focusin", function (e) {
        $('#message').val($('#name').val());
        
    });
    $('#name').on("focusout", function (e) {
        $('#message').val($('#name').val());

    });
    $('#name').on("change", function (e) {
        $('#message').val($('#name').val());

    });
});

bootstrapValidate('#name', 'max: 30: Imię musi być krótsze niż 30 znaków.|required: To pole jest wymagane.');
bootstrapValidate('#email', 'email: Wpisz poprawny adres email.|required: Podaj adres email.');
bootstrapValidate('#message', 'min: 10: Treść wiadomości musi zawierać minimum 10 znaków.');*/