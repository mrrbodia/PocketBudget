(function () {
    $.validator.addMethod('data-min', function (value, element, param) {
        return this.optional(element) || value >= Number(param);
    }, "Невірне значення");
    $.validator.addMethod('data-max', function (value, element, param) {
        return this.optional(element) || value < Number(param);
    }, "Невірне значення");
    $.validator.addMethod('data-less-than', function (value, element, param) {
        if (this.optional(element)) return true;
        var i = parseInt(value);
        var j = parseInt($(param).val());
        return i < j;
    }, "Невірне значення");
    $.validator.addMethod('data-greater-than', function (value, element, param) {
        if (this.optional(element)) return true;
        var i = parseInt(value);
        var j = parseInt($(param).val());
        return i > j;
    }, "Невірне значення");
}());