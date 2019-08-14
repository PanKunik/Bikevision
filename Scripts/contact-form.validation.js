bootstrapValidate('#name', 'max: 30: Your name must not be longer than 30 characters', function (isValid) {
    if (isValid) {
        alert('Element is valid');
    } else {
        alert('Element is invalid');
    }
});