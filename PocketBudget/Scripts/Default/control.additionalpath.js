//TODO: move all calculation login to backend (return properly values for Chart)
var PersonalFinances = PersonalFinances || {};
PersonalFinances.Path.AdditionalPath = (function () {
    var saveDepositSelection = function (fromAge, toAge) {
        PersonalFinances.Path.AdditionalPath.Deposits = [];//PersonalFinances.Path.AdditionalPath.Deposit || [];
        var currencyId = $('input[name=deposit]:checked').val();
        var deposit = {
            CurrencyId: currencyId,
            Total: +$('input[name=' + currencyId + 'total]').val(),
            Percentage: +$('input[name=' + currencyId + 'percentage]').val(),
            Years: +$('input[name=' + currencyId + 'years]').val(),
            FromAge: fromAge,
            ToAge: toAge
        };
        PersonalFinances.Path.AdditionalPath.Deposits.push(deposit);
    };

    var saveAdditionalValuesSelection = function () {
        var fromAge = +$('input[name=FromAge]').val();
        var toAge = +$('input[name=ToAge]').val();
        saveDepositSelection(fromAge, toAge);
    };
    
    $(document).on('click', 'input[name=deposit]', function (e) {
        $('.deposit-input.active').removeClass('active').addClass('hidden');
        $(e.target).parent().find('.deposit-input.hidden').removeClass('hidden').addClass('active');
    });

    return {
        saveAdditionalValuesSelection: function () {
            var editFinancesContent = $('.edit-finances-content');
            if (editFinancesContent && editFinancesContent.length) {
                saveAdditionalValuesSelection();
            }
        }
    }

    //var getCurrencyExchangeValue = function (currencyId) {
    //    switch (currencyId)
    //    {
    //        case 'dollar':
    //            return 27.95;
    //        case 'euro':
    //            return 31.15;
    //        case 'hrn':
    //            return 1.0;
    //        default:
    //            return 1.0;
    //    }
    //};

    //var getDepositIncomePerYear = function(deposit) {
    //    return PersonalFinances.Path.AdditionalPath.Deposit[0].Total
    //        * (PersonalFinances.Path.AdditionalPath.Deposit[0].Percentage / 100)
    //        * getCurrencyExchangeValue(PersonalFinances.Path.AdditionalPath.Deposit[0].CurrencyId);
    //};

    //var applyDepositSelection = function (values) {
    //    //TODO: if any additional income exist
    //    if (!PersonalFinances.Path.AdditionalPath.Deposit[0])
    //        return values;
    //    var position = PersonalFinances.Path.AdditionalPath.FromAge - PersonalFinances.Path.CurrentAge;
    //    for (var i = position; i < position + PersonalFinances.Path.AdditionalPath.Deposit[0].Years; i++) {
    //        values[i] = values[i] + getDepositIncomePerYear();
    //    }
    //    return values;
    //};

    //var applyAdditionalSavingsChanges = function (values) {
    //    var result = values;
    //    result = applyDepositSelection(result);
    //    return result;
    //};
}());