var PersonalFinances = PersonalFinances || {};
PersonalFinances.Path.AdditionalPath = (function () {
    var saveDepositSelection = function (fromAge) {
        PersonalFinances.Path.AdditionalPath.Deposits = PersonalFinances.Path.AdditionalPath.Deposits || [];
        var currencyId = $('input[name=deposit]:checked').val();
        var deposit = {
            CurrencyId: currencyId,
            Total: +$('input[name=' + currencyId + 'total]').val(),
            Percentage: $('input[name=' + currencyId + 'percentage]').val(),
            Years: +$('input[name=' + currencyId + 'years]').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath.Deposits.push(deposit);
    };

    var saveCreditSelection = function (fromAge) {
        PersonalFinances.Path.AdditionalPath.Credits = PersonalFinances.Path.AdditionalPath.Credits || [];
        var currencyId = $('input[name=credit]:checked').val();
        var credit = {
            CurrencyId: currencyId,
            Total: +$('input[name=' + currencyId + 'total-credit]').val(),
            Percentage: $('input[name=' + currencyId + 'percentage-credit]').val(),
            Years: +$('input[name=' + currencyId + 'years-credit]').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath.Credits.push(credit);
    };

    var saveAdditionalValuesSelection = function () {
        var incomeFrom = +$('input.income-from').val();
        var costFrom = +$('input.cost-from').val();
        if ($('input[type=checkbox].add-deposit:checked').length) {
            saveDepositSelection(incomeFrom);
        }
        if ($('input[type=checkbox].add-credit:checked').length) {
            saveCreditSelection(costFrom);
        }
    };

    $(document).on('click', 'input[name=deposit]', function (e) {
        $('.deposit-input.active').removeClass('active').addClass('hidden');
        $(e.target).parent().find('.deposit-input.hidden').removeClass('hidden').addClass('active');
    });

    $(document).on('click', 'input[name=credit]', function (e) {
        $('.credit-input.active').removeClass('active').addClass('hidden');
        $(e.target).parent().find('.credit-input.hidden').removeClass('hidden').addClass('active');
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