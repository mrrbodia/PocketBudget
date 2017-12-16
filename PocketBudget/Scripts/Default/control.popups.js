var PersonalFinances = PersonalFinances || {};
PersonalFinances.Popups = (function () {

    var t = function(){};

    var fillPopupInputs = function (popup, inputs) {
        for (var prop in inputs) {
            var input = popup.find('input[name=' + prop + ']');
            if (input) {
                input.value = inputs[prop];
            }
        }
    };

    var open = function (selector, options) {
        var popup = $(selector);
        if (!popup)
            return;
        if (options && options.inputs) {
            fillPopupInputs(popup, options.inputs);
        }
        $(selector).openModal();
        //popup.show();
    };

    var close = function (selector) {
        var popup = $(selector);
        if (!popup)
            return;
        $(selector).closeModal();
        //popup.hide();
    };
    return {
        open: open,
        close: close
    };
}());