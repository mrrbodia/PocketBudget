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
        //inputs.forEach(function (value, name) {
        //    var input = popup.find('input[@name="' + name + '"]');
        //    if (input) {
        //        input.value = value;
        //    }
        //});
    };
    var open = function (selector, options) {
        var popup = $(selector);
        if (!popup)
            return;
        if (options && options.inputs) {
            fillPopupInputs(popup, options.inputs);
        }
        popup.show();
    };
    var close = function (selector) {
        var popup = $(selector);
        if (!popup)
            return;
        popup.hide();
    };
    return {
        open: open,
        close: close
    }
}());