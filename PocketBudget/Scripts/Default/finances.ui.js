var PersonalFinances = PersonalFinances || {};
PersonalFinances.UI = (function () {
    var ui = {};

    ui.resetValidationFor = function (formSelector) {
        var form = $(formSelector);
        form = form.removeData("validator")
                   .removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
    };

    return ui;
}());