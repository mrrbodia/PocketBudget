//TODO: move all calculation login to backend (return properly values for Chart)
var PersonalFinances = PersonalFinances || {};
PersonalFinances.AdditionalPath = (function () {
    var saveDepositSelection = function () {
        PersonalFinances.AdditionalPath.Deposit = PersonalFinances.AdditionalPath.Deposit || {};
        var currencyId = $('input[name=deposit]:checked').val();
        PersonalFinances.AdditionalPath.Deposit.CurrencyId = currencyId;
        PersonalFinances.AdditionalPath.Deposit.Total = +$('input[name='+currencyId+'total]').val();
        PersonalFinances.AdditionalPath.Deposit.Percentage = +$('input[name='+currencyId+'percentage]').val();
        PersonalFinances.AdditionalPath.Deposit.Years = +$('input[name=' + currencyId + 'years]').val();
    };

    var getCurrencyExchangeValue = function (currencyId) {
        switch (currencyId)
        {
            case 'dollar':
                return 27.95;
            case 'euro':
                return 31.15;
            case 'hrn':
                return 1.0;
            default:
                return 1.0;
        }
    };

    var getDepositIncomePerYear = function(deposit) {
        return PersonalFinances.AdditionalPath.Deposit.Total * (PersonalFinances.AdditionalPath.Deposit.Percentage / 100) * getCurrencyExchangeValue(PersonalFinances.AdditionalPath.Deposit.CurrencyId);
    };

    var applyDepositSelection = function (values) {
        //TODO: if any additional income exist
        if (!PersonalFinances.AdditionalPath.Deposit)
            return values;
        var position = PersonalFinances.AdditionalPath.FromAge - PersonalFinances.Path.CurrentAge;
        for (var i = position; i < position + PersonalFinances.AdditionalPath.Deposit.Years; i++) {
            values[i] = values[i] + getDepositIncomePerYear();
        }
        return values;
    };

    var applyAdditionalSavingsChanges = function (values) {
        var result = values;
        result = applyDepositSelection(result);
        return result;
    };

    var saveAdditionalValuesSelection = function () {
        PersonalFinances.AdditionalPath.FromAge = +$('input[name=FromAge]').val();
        saveDepositSelection();
    };

    $(document).on('click', 'input[name=deposit]', function (e) {
        $('.deposit-input.active').removeClass('active').addClass('hidden');
        $(e.target).parent().find('.deposit-input.hidden').removeClass('hidden').addClass('active');
    });

    $(document).on('click', '.save-edit-finances', function (e) {
        PersonalFinances.AdditionalPath.saveAdditionalValuesSelection();
        updateGraph();
    });

    return {
        saveAdditionalValuesSelection: saveAdditionalValuesSelection,
        applyAdditionalSavingsChanges: applyAdditionalSavingsChanges
    };
})();

