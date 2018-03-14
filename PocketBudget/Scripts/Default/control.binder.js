﻿var PersonalFinances = PersonalFinances || {};
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
        return deposit;
    };

    return binder;
}());