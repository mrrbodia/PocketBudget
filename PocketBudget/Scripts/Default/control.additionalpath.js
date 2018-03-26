var PersonalFinances = PersonalFinances || {};
PersonalFinances.Path.AdditionalPath = (function () {
    var saveDepositSelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath.Deposits = PersonalFinances.Path.AdditionalPath.Deposits || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var deposit = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            Percentage: selected.find('.percentage').val(),
            Years: +selected.find('.years').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath.Deposits.push(deposit);
    };

    var saveSaleSelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath.Sales = PersonalFinances.Path.AdditionalPath.Sales || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var sale = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath.Sales.push(sale);
    };

    var saveSalarySelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath.Salaries = PersonalFinances.Path.AdditionalPath.Salaries || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var salary = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath.Salaries.push(salary);
    };

    var saveCreditSelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath.Credits = PersonalFinances.Path.AdditionalPath.Credits || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var credit = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            Percentage: selected.find('.percentage').val(),
            Years: +selected.find('.years').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath.Credits.push(credit);
    };

    var savePurchaseSelection = function (parent, fromAge) {
        PersonalFinances.Path.AdditionalPath.Purchases = PersonalFinances.Path.AdditionalPath.Purchases || [];
        var selected = $(parent).find('.currency[type=radio]:checked').parent();
        var purchase = {
            CurrencyId: selected.find('.currency').val(),
            Total: +selected.find('.total').val(),
            FromAge: fromAge
        };
        PersonalFinances.Path.AdditionalPath.Purchases.push(purchase);
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
        if ($('input[type=checkbox].add-salary:checked').length) {
            saveSalarySelection('.salaries', incomeFrom);
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