var PersonalFinances = PersonalFinances || {};
PersonalFinances.Binder = (function () {
    var binder = {};

    $("html").removeClass("no-js");

    binder.bindDeposit = function (data, index) {
        data['AdditionalPath.AdditionalIncomes[' + index + '].Deposit.Percentage'] = PersonalFinances.Path.AdditionalPath.Deposits[index].Percentage;
        data['AdditionalPath.AdditionalIncomes[' + index + '].Deposit.Total'] = PersonalFinances.Path.AdditionalPath.Deposits[index].Total;
        data['AdditionalPath.AdditionalIncomes[' + index + '].Deposit.CurrencyId'] = PersonalFinances.Path.AdditionalPath.Deposits[index].CurrencyId;
        data['AdditionalPath.AdditionalIncomes[' + index + '].Deposit.Years'] = PersonalFinances.Path.AdditionalPath.Deposits[index].Years;
        data['AdditionalPath.AdditionalIncomes[' + index + '].Deposit.FromAge'] = PersonalFinances.Path.AdditionalPath.Deposits[index].FromAge;
    };

    return binder;
}());