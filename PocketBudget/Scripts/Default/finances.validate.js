(function () {
    $.validator.addMethod('data-min', function (value, element, param) {
        return this.optional(element) || value >= Number(param);
    });
}());