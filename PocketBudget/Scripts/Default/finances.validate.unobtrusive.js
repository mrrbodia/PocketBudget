(function () {
    $.validator.unobtrusive.adapters.addSingleVal('min', null, 'data-min');

    $('form').off(".validate")
        .removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(document);
}());