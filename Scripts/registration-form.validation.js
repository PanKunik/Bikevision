var myLanguage = {
    errorTitle: 'Przesyłanie formularza nie powiodło się!',
    requiredField: 'To pole jest wymagane.',
    requiredFields: 'Nie odpowiedziano na wszystkie wymagane pola.',
    badTime: 'Nie podano prawidłowego czasu.',
    badEmail: 'Niepoprawny adres e-mail.',
    badTelephone: 'Niepoprawny numer telefonu.',
    badSecurityAnswer: 'Nie udzielono prawidłowej odpowiedzi na pytanie bezpieczeństwa.',
    badDate: 'Nie podano prawidłowej daty.',
    lengthBadStart: 'Pole musi zawierać pomiędzy ',
    lengthBadEnd: ' znaków.',
    lengthTooLongStart: 'The input value is longer than ',
    lengthTooShortStart: 'The input value is shorter than ',
    notConfirmed: 'Nie można potwierdzić wartości wejściowych.',
    badDomain: 'Niepoprawna wartość domeny.',
    badUrl: 'Niepoprawny adres URL.',
    badCustomVal: 'Niepoprawna wartość wejściowa.',
    andSpaces: ' i spacje.',
    badInt: 'Wartośc wejściowa nie była poprawną liczbą.',
    badSecurityNumber: 'Numer ubezpieczenia społecznego jest niepoprawny.',
    badUKVatAnswer: 'Nieprawidłowy numer UK VAT.',
    badStrength: 'Hasło nie jest wystarczająco silne.',
    badNumberOfSelectedOptionsStart: 'Musi zostać wybrana co najmniej ',
    badNumberOfSelectedOptionsEnd: ' odpowiedź.',
    badAlphaNumeric: 'Wartość wejściowa może zawierać tylko znaki alfanumeryczne ',
    badAlphaNumericExtra: ' i ',
    wrongFileSize: 'Plik, który próbujesz przesłać jest za duży (maks. %s).',
    wrongFileType: 'Dozwolone są tylko pliki typu %s.',
    groupCheckedRangeStart: 'Proszę wybrać pomiędzy ',
    groupCheckedTooFewStart: 'Proszę wybrać co najmniej ',
    groupCheckedTooManyStart: 'Proszę wybrać maksymalnie ',
    groupCheckedEnd: ' pozycję.',
    badCreditCard: 'Numer karty kredytowej jest nieprawidłowy.',
    badCVV: 'Numer CVV jest niepoprawny.',
    wrongFileDim: 'Niepoprawne wymiary obrazu,',
    imageTooTall: 'obraz nie może być wyższy niż',
    imageTooWide: 'obraz nie może być szerszy niż',
    imageTooSmall: 'obraz jest zbyt mały.',
    min: 'min',
    max: 'max',
    imageRatioNotAccepted: 'Współczynnik obrazu nie jest akceptowany.'
};

$(function () {
    $.validate({
        language: myLanguage,

        inlineErrorMessageCallback: function ($input, errorMessage, config) {

            if (errorMessage != "")
                $("#validation-errors-container").removeClass("d-none")

            var errorElement = $("<li></li>").text(errorMessage).attr("id", $input.attr("id") + "ErrorListElement")

            if (errorMessage) {
                if ($("#errorList").children("li[id^='" + $input.attr("id") + "']").length == 0) {
                    $("#errorList").append(errorElement);
                }
            }
            else {
                $("li").remove("#" + $input.attr("id") + "ErrorListElement");

                if ($("li[id*='ErrorListElement']").length == 0)
                    $("#validation-errors-container").addClass("d-none")
            }

            return false;
        },

        submitErrorMessageCallback: function ($form, errorMessages, config) {
            //foreach
        }
});

    $('#message').restrictLength($('#max-length-element'));
});
