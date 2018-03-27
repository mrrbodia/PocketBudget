var PersonalFinances = PersonalFinances || {};
PersonalFinances.Path.AdditionalPath = (function () {
    var saveDepositSelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath['Deposit'] = PersonalFinances.Path.AdditionalPath['Deposit'] || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var deposit = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            Percentage: selected.find('.percentage').val(),
            Years: +selected.find('.years').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath['Deposit'].push(deposit);
    };

    var saveSaleSelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath['Sale'] = PersonalFinances.Path.AdditionalPath['Sale'] || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var sale = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath['Sale'].push(sale);
    };

    var saveCreditSelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath['Credit'] = PersonalFinances.Path.AdditionalPath['Credit'] || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var credit = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            Percentage: selected.find('.percentage').val(),
            Years: +selected.find('.years').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath['Credit'].push(credit);
    };

    var savePurchaseSelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath['Purchase'] = PersonalFinances.Path.AdditionalPath['Purchase'] || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var purchase = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath['Purchase'].push(purchase);
    };

    var saveAdditionalValuesSelection = function () {
        //TODO: validate
        var incomeFrom = +$('input.income-from').val();
        var costFrom = +$('input.cost-from').val();
        //TODO: REPLACE WITH DEFAULT MVC BINDING / SESSION SHOULD BE CREATED
        //Refactor
        if ($('input[type=checkbox].add-deposit:checked').length) {
            saveDepositSelection('.deposits', incomeFrom);
        }
        if ($('input[type=checkbox].add-sale:checked').length) {
            saveSaleSelection('.sales', incomeFrom);
        }
        if ($('input[type=checkbox].add-credit:checked').length) {
            saveCreditSelection('.credits', costFrom);
        }
        if ($('input[type=checkbox].add-purchase:checked').length) {
            savePurchaseSelection('.purchases', costFrom);
        }
    };

    $(document).on('click', '.option-input', function (e) {
        e.preventDefault();
        if ($(e.target).is('.active')) {
            return;
        }
        $(e.target).closest('.collapsible-body').find('.option-input[type=radio]:checked').removeAttr('checked');
        $(e.target).closest('.collapsible-body').find('.option-input.active').removeClass('active').addClass('hidden');
        $(e.target).parent().find('.option-input.hidden[type=radio]').prop('checked', true);
        $(e.target).parent().find('.option-input.hidden').removeClass('hidden').addClass('active');
    });

    return {
        saveAdditionalValuesSelection: function () {
            var editFinancesContent = $('.edit-finances-content');
            if (editFinancesContent && editFinancesContent.length) {
                saveAdditionalValuesSelection();
            }
        }
    }
}());