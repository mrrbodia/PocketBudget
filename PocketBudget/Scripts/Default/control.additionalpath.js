var PersonalFinances = PersonalFinances || {};
PersonalFinances.Path.AdditionalPath = (function () {
    var saveDepositSelection = function (fromAge) {
        PersonalFinances.Path.AdditionalPath.Deposits = PersonalFinances.Path.AdditionalPath.Deposits || [];
        var currencyId = $('input[name=deposit]:checked').val();
        var deposit = {
            CurrencyId: currencyId,
            Total: +$('input[name=' + currencyId + 'total]').val(),
            Percentage: +$('input[name=' + currencyId + 'percentage]').val(),
            Years: +$('input[name=' + currencyId + 'years]').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath.Deposits.push(deposit);
    };

    var saveAdditionalValuesSelection = function () {
        var fromAge = +$('input[name=From]').val();
        saveDepositSelection(fromAge);
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
}());