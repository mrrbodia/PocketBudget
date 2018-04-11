var PersonalFinances = PersonalFinances || {};
PersonalFinances.Path.AdditionalPath = (function () {

    var clearAdditionalData = function () {
        PersonalFinances.Path.AdditionalPath['Deposit'] = [];
        PersonalFinances.Path.AdditionalPath['Sale'] = [];
        PersonalFinances.Path.AdditionalPath['Credit'] = [];
        PersonalFinances.Path.AdditionalPath['Purchase'] = [];
    }

    var bindDeposit = function (currencyId, total, percentage, years, fromAge) {
        PersonalFinances.Path.AdditionalPath['Deposit'] = PersonalFinances.Path.AdditionalPath['Deposit'] || [];

        var deposit = {
            CurrencyId: currencyId,
            Total: total,
            Percentage: percentage,
            Years: years,
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath['Deposit'].push(deposit);
    }

    var bindSale = function (currencyId, total, fromAge) {
        PersonalFinances.Path.AdditionalPath['Sale'] = PersonalFinances.Path.AdditionalPath['Sale'] || [];

        var sale = {
            CurrencyId: currencyId,
            Total: total,
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath['Sale'].push(sale);
    }

    var bindCredit = function (currencyId, total, percentage, years, fromAge) {
        PersonalFinances.Path.AdditionalPath['Credit'] = PersonalFinances.Path.AdditionalPath['Credit'] || [];

        var credit = {
            CurrencyId: currencyId,
            Total: total,
            Percentage: percentage,
            Years: years,
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath['Credit'].push(credit);
    }

    bindPurchase = function (currencyId, total, fromAge) {
        PersonalFinances.Path.AdditionalPath['Purchase'] = PersonalFinances.Path.AdditionalPath['Purchase'] || [];

        var purchase = {
            CurrencyId: currencyId,
            Total: total,
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath['Purchase'].push(purchase);
    }

    var saveDepositSelection = function (parent, fromAge) {
        var selected = $(parent).find('.currency[type=radio]:checked').parent();

        bindDeposit(selected.find('.currency').val(),
            +selected.find('.total').val(),
            selected.find('.percentage').val(),
            +selected.find('.years').val(),
            fromAge
        );
    };

    var saveSaleSelection = function (parent, fromAge) {
        var selected = $(parent).find('.currency[type=radio]:checked').parent();

        bindSale(selected.find('.currency').val(),
            +selected.find('.total').val(),
            fromAge
        );
    };

    var saveCreditSelection = function (parent, fromAge) {
        var selected = $(parent).find('.currency[type=radio]:checked').parent();

        bindCredit(selected.find('.currency').val(),
            +selected.find('.total').val(),
            selected.find('.percentage').val(),
            +selected.find('.years').val(),
            fromAge
        );
    };

    var savePurchaseSelection = function (parent, fromAge) {
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        bindPurchase(selected.find('.currency').val(),
            +selected.find('.total').val(),
            fromAge
        );
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

    //should be refactored
    var bindModel = function (model) {
        if (model.AdditionalIncomes != undefined)
        {
            $.each(model.AdditionalIncomes, function (id, data) {
                var type = data.LineType;
                if (type == 'Deposit') {
                    bindDeposit(data.CurrencyId,
                        data.Total,
                        data.Percentage,
                        data.Years,
                        data.From
                    );
                }
                if (type == 'Sale') {
                    bindSale(data.CurrencyId,
                        data.Total,
                        data.From);
                }
            });

            $.each(model.AdditionalCosts, function (id, data) {
                var type = data.LineType;
                if (type == 'Credit') {
                    bindCredit(data.CurrencyId,
                        data.Total,
                        data.Percentage,
                        data.Years,
                        data.From
                    );
                }
                if (type == 'Purchase') {
                    bindPurchase(data.CurrencyId,
                        data.Total,
                        data.From);
                }
            });
        }
    }

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
        },
        bindModel: function (pathModel) {
            if (pathModel.AdditionalPath != undefined) {
                clearAdditionalData();
                bindModel(pathModel.AdditionalPath);
            }
        }
    }
}());