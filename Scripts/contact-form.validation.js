$(function () {
    $('#submit').on("click", function () {
        if ($('.invalid-feedback').length > 0) {
            alert('the form is valid');
        }
    });
});

bootstrapValidate('#name', 'max: 30: Imię musi być krótsze niż 30 znaków.|required: To pole jest wymagane.');
bootstrapValidate('#email', 'email: Wpisz poprawny adres email.|required: Podaj adres email.');
bootstrapValidate('#message', 'min: 10: Treść wiadomości musi zawierać minimum 10 znaków.');