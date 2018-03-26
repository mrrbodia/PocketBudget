var PersonalFinances = PersonalFinances || {};
PersonalFinances.Binder = (function () {
    var binder = {};

    $("html").removeClass("no-js");

    //TODO: bind additional path model via standard MVC
    binder.bindDeposit = function (index) {
        var deposit = '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].Percentage=' + PersonalFinances.Path.AdditionalPath.Deposits[index].Percentage;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath.Deposits[index].Total;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath.Deposits[index].CurrencyId;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].Years=' + PersonalFinances.Path.AdditionalPath.Deposits[index].Years;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath.Deposits[index].FromAge;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].IsHidden=' + PersonalFinances.Path.AdditionalPath.Deposits[index].IsHidden;
        return deposit;
    };

    binder.bindSale = function (index) {
        var sale = '&AdditionalPath.AdditionalIncome.Sales[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath.Sales[index].Total;
        sale += '&AdditionalPath.AdditionalIncome.Sales[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath.Sales[index].CurrencyId;
        sale += '&AdditionalPath.AdditionalIncome.Sales[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath.Sales[index].FromAge;
        return sale;
    };

    binder.bindSalary = function (index) {
        var salary = '&AdditionalPath.AdditionalIncome.ChangedSalary[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath.Salaries[index].Total;
        salary += '&AdditionalPath.AdditionalIncome.ChangedSalary[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath.Salaries[index].CurrencyId;
        salary += '&AdditionalPath.AdditionalIncome.ChangedSalary[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath.Salaries[index].FromAge;
        return salary;
    };

    binder.bindCredit = function (index) {
        var credit = '&AdditionalPath.AdditionalCost.Credits[' + index + '].Percentage=' + PersonalFinances.Path.AdditionalPath.Credits[index].Percentage;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath.Credits[index].Total;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath.Credits[index].CurrencyId;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].Years=' + PersonalFinances.Path.AdditionalPath.Credits[index].Years;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath.Credits[index].FromAge;
        return credit;
    };

    binder.bindPurchase = function (index) {
        var purchase = '&AdditionalPath.AdditionalCost.Purchases[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath.Purchases[index].Total;
        purchase += '&AdditionalPath.AdditionalCost.Purchases[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath.Purchases[index].CurrencyId;
        purchase += '&AdditionalPath.AdditionalCost.Purchases[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath.Purchases[index].FromAge;
        return purchase;
    };

    return binder;
}());