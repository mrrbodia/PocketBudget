var PersonalFinances = PersonalFinances || {};
PersonalFinances.Binder = (function () {
    var binder = {};

    $("html").removeClass("no-js");

    //TODO: bind additional path model via standard MVC
    binder.bindDeposit = function (index) {
        var deposit = '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].Percentage=' + PersonalFinances.Path.AdditionalPath['Deposit'][index].Percentage;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath['Deposit'][index].Total;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath['Deposit'][index].CurrencyId;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].Years=' + PersonalFinances.Path.AdditionalPath['Deposit'][index].Years;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath['Deposit'][index].FromAge;
        deposit += '&AdditionalPath.AdditionalIncome.Deposits[' + index + '].IsHidden=' + PersonalFinances.Path.AdditionalPath['Deposit'][index].IsHidden;
        return deposit;
    };

    binder.bindSale = function (index) {
        var sale = '&AdditionalPath.AdditionalIncome.Sales[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath['Sale'][index].Total;
        sale += '&AdditionalPath.AdditionalIncome.Sales[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath['Sale'][index].CurrencyId;
        sale += '&AdditionalPath.AdditionalIncome.Sales[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath['Sale'][index].FromAge;
        sale += '&AdditionalPath.AdditionalIncome.Sales[' + index + '].IsHidden=' + PersonalFinances.Path.AdditionalPath['Sale'][index].IsHidden;
        return sale;
    };

    binder.bindCredit = function (index) {
        var credit = '&AdditionalPath.AdditionalCost.Credits[' + index + '].Percentage=' + PersonalFinances.Path.AdditionalPath['Credit'][index].Percentage;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath['Credit'][index].Total;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath['Credit'][index].CurrencyId;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].Years=' + PersonalFinances.Path.AdditionalPath['Credit'][index].Years;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath['Credit'][index].FromAge;
        credit += '&AdditionalPath.AdditionalCost.Credits[' + index + '].IsHidden=' + PersonalFinances.Path.AdditionalPath['Credit'][index].IsHidden;
        return credit;
    };

    binder.bindPurchase = function (index) {
        var purchase = '&AdditionalPath.AdditionalCost.Purchases[' + index + '].Total=' + PersonalFinances.Path.AdditionalPath['Purchase'][index].Total;
        purchase += '&AdditionalPath.AdditionalCost.Purchases[' + index + '].CurrencyId=' + PersonalFinances.Path.AdditionalPath['Purchase'][index].CurrencyId;
        purchase += '&AdditionalPath.AdditionalCost.Purchases[' + index + '].FromAge=' + PersonalFinances.Path.AdditionalPath['Purchase'][index].FromAge;
        purchase += '&AdditionalPath.AdditionalCost.Purchases[' + index + '].IsHidden=' + PersonalFinances.Path.AdditionalPath['Purchase'][index].IsHidden;
        return purchase;
    };

    return binder;
}());