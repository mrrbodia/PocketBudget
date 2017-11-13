﻿var Graph = (function () {
    var ctx = document.getElementById("chart");
    var goal = {
        label: 'Мета',
        data: [40000, 40000, 40000, 40000, 40000, 40000, 40000, 40000],
        backgroundColor: [
            'rgba(54, 162, 235, 0.2)'
        ],
        borderColor: [
            'rgba(255,99,132,1)'
        ],
        borderCapStyle: 'dash',
        borderDash: [10, 15],
        borderDashOffset: 0,
        fill: false
    };
    var income = {
        label: 'Доходи',
        data: [8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000],
        backgroundColor: [
            'rgba(21, 229, 171, 0.4)'
        ],
        borderColor: [
            'rgba(54, 162, 235, 1)'
        ],
        fill: 0
    };
    var deposit = {
        'sum': 108000, //4000$ dollars on deposit
        'hrn': 14,
        'dollar': 3.75,
        'euro': 2.35
    };
    var costs = {
        'economically': [2300, 3000, 2200, 3300, 2250, 2900, 3000, 2500],
        'non-economically': [5000, 5800, 4900, 4700, 5200, 4500, 4700, 4600]
    };
    var getMonthIncome = function (currency, month) {
        if (month == 0)
            return 0;
        return (deposit['sum'] * Math.pow(deposit[currency] / 100 / 12), month) / 12;
    };
    var getMonthCosts = function (position, strategy) {
        return costs[strategy][position];
    };
    var updateGraphWithData = function (savings) {
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ["Січень", "Лютий", "Березень", "Квітень", "Травень", "Червень", "Липень", "Серпень"],
                datasets: [
                    goal,
                    savings
                ]
            },
            options: {
                scales: {
                    yAxes: [{
                        time: {
                            type: 'time',
                            unit: 'month'
                        }
                    }]
                }
            }
        });
    };
    var getSavedMoney = function (savings, current) {
        if (current == 0)
            return 0;
        return savings[current - 1];
    };
    var changeStrategy = function (incomeStrategy, costsStrategy)
    {
        var savings = income;
        var salary = 10000;
        for (var i = 0; i < savings.data.length; i++) {
            savings.data[i] = salary + getMonthIncome(incomeStrategy, i) - getMonthCosts(i, costsStrategy) + getSavedMoney(savings.data, i);
        }
        updateGraphWithData(savings);
    };

    var init = function () {
        $('input[type=radio]').on('click', function (e) {
            var incomeStrategy = $('.input-strategy[name=income]:checked').val();
            var costsStrategy = $('.input-strategy[name=costs]:checked').val();
            changeStrategy(incomeStrategy, costsStrategy);
        });
        changeStrategy('hrn', 'economically');
    };

    return {
        init: function () {
            init();
        }
    };
})();


//var Graph = {
//    init: function ()
//    {
//        //TEST DATA
//        //var goal = {
//        //    label: 'Мета',
//        //    data: [50000, 50000, 50000, 50000, 50000, 50000, 50000, 50000],
//        //    backgroundColor: [
//        //        'rgba(54, 162, 235, 0.2)'
//        //    ],
//        //    borderColor: [
//        //        'rgba(255,99,132,1)'
//        //    ],
//        //    borderCapStyle: 'dash',
//        //    borderDash: [10, 15],
//        //    borderDashOffset: 0,
//        //    fill: false
//        //};
//        //var income = {
//        //    label: 'Доходи',
//        //    data: [10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000],
//        //    backgroundColor: [
//        //        'rgba(21, 229, 171, 0.4)'
//        //    ],
//        //    borderColor: [
//        //        'rgba(54, 162, 235, 1)'
//        //    ],
//        //    fill: 0
//        //};
//        //var salary = {
//        //    label: 'Заробітня плата',
//        //    data: [10000, 20000, 30000, 40000, 50000, null, null, null],
//        //    backgroundColor: [
//        //        'rgba(54, 162, 235, 0.2)'
//        //    ],
//        //    borderColor: [
//        //        'rgba(54, 162, 235, 1)'
//        //    ],
//        //    fill: false
//        //};
        
//        //Deposit 4000$
//        //Hrn = 108000 (14% per year), income = 1260 hrn/month
//        //Dollar = 4000 (3.75% per year), income = 337,5 hrn/month
//        //Euro = 3440 (2.35% per year), income = 212 hrn/month
//        //Add deposits by clicking on button
//        //Change strategies by deposits

//        //var depositHrn = {
//        //    label: 'Депозит в гривнях',
//        //    data: [1260, 2520, 3780, 5040, 6300, 7560, 8820, 10080],
//        //    backgroundColor: [
//        //        'rgba(255, 206, 86, 0.2)'
//        //    ],
//        //    borderColor: [
//        //        'rgba(255, 206, 86, 1)'
//        //    ]
//        //};
//        //var depositDollar = {
//        //    label: 'Депозит в доларах',
//        //    data: [337.5, 675, 1012.5, 1350, 1687.5, 2025, 2362.5, 2700],
//        //    backgroundColor: [
//        //        'rgba(153, 102, 255, 0.2)'
//        //    ],
//        //    borderColor: [
//        //        'rgba(153, 102, 255, 1)'
//        //    ]
//        //};
//        //var depositEuro = {
//        //    label: 'Депозит в євро',
//        //    data: [212, 424, 636, 838, 950, 1162, 1374, 1586],
//        //    backgroundColor: [
//        //        'rgba(75, 192, 192, 0.2)'
//        //    ],
//        //    borderColor: [
//        //        'rgba(75, 192, 192, 1)'
//        //    ]
//        //};

//        //Different costs per month (tighten belts or not)
//        //TEST DATA

//    }
//}

$(document).ready(function () {
    if (document.getElementById('chart-block'))
        Graph.init();
});