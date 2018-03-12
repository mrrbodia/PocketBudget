var PersonalFinances = PersonalFinances || {};
PersonalFinances.Binder = (function () {
    var binder = {};

    $("html").removeClass("no-js");

    //TODO: bind additional path model via standard MVC
    binder.bindDeposit = function (index) {
        var deposit = '&AdditionalPath.AdditionalIncomes[' + index + '].Deposit.Percentage=' + PersonalFinances.Path.AdditionalPath.Deposits[index].Percentage;
        deposit += '&AdditionalPath.AdditionalIncomes[' + index + '].Deposit.Total=' + PersonalFinances.Path.AdditionalPath.Deposits[index].Total;
        deposit += '&AdditionalPath.AdditionalIncomes[' + index + '].Deposit.CurrencyId=' + PersonalFinances.Path.AdditionalPath.Deposits[index].CurrencyId;
        deposit += '&AdditionalPath.AdditionalIncomes[' + index + '].Deposit.Years=' + PersonalFinances.Path.AdditionalPath.Deposits[index].Years;
        deposit += '&AdditionalPath.AdditionalIncomes[' + index + '].Deposit.FromAge=' + PersonalFinances.Path.AdditionalPath.Deposits[index].FromAge;
        return deposit;
    };

    return binder;
}());