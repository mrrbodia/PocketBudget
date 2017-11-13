var Graph = (function () {
    var ctx = document.getElementById("chart");
    var goal = {
        label: 'Мета',
        data: [500000, 500000, 40000, 40000, 40000, 40000, 40000, 40000],
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
    var bankRating = {
        'Name': ['Райффайзен Банк Аваль', 'Креди Агриколь Банк', 'УкрСиббанк', 'Ощадбанк', 'Кредобанк', 'Укргазбанк', 'ОТП Банк', 'ПроКредит Банк', 'Укрэксимбанк', 'Укрсоцбанк', 'Банк Форвард'],
        'Rating': [4.59, 4.37, 4.29, 4.07, 4.04, 3.83, 3.79, 3.67, 3.65, 3.48, 2.36]
    };
    //TODO: remove in new version
    var deposit = {
        'sum': 108000, //4000$ dollars on deposit
        'hrn': 14,
        'dollar': 3.75,
        'euro': 2.35
    };
    //TODO: remove in new version
    var costs = {
        'economically': [2300, 3000, 2200, 3300, 2250, 2900, 3000, 2500],
        'non-economically': [5000, 5800, 4900, 4700, 5200, 4500, 4700, 4600]
    };
    var getAdditionalIncome = function (currency, month) {
        if (month == 0)
            return 0;
        return (deposit['sum'] * Math.pow(deposit[currency] / 100 / 12), month) / 12;
    };
    var getAdditionalCosts = function (position, strategy) {
        return costs[strategy][position] || 0;
    };
    var updateGraphWithData = function (savings) {
        var labels = [];
        for (var i = Strategy.Model.salaryPattern.startWorkFrom; i < Strategy.Model.salaryPattern.showTillAge; i++)
        {
            labels[i - Strategy.Model.salaryPattern.startWorkFrom] = i;
        }
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [
                    //goal,
                    savings
                ]
            },
            options: {
                scales: {
                    xAxes: [{
                        time: {
                            type: 'time',
                            unit: 'year'
                        },
                        ticks: {
                            callback: function (value, index, array) {
                                return (value % 5) ? "" : "Вік" + value;
                            }
                        }
                    }]
                },
                tooltips: {
                    callbacks: {
                        label: function (item, data) {
                            return item.yLabel.toFixed(2);
                        }
                    }
                }
            }
        });
    };
    var getIncomeData = function () {
        if (!Strategy.Model.salaryPattern)
            return [];
        var arr = [];
        var amountOfYears = Strategy.Model.salaryPattern.showTillAge - Strategy.Model.salaryPattern.startWorkFrom;
        arr[0] = Strategy.Model.salaryPattern.incomePerYear;
        for (var i = 1; i < amountOfYears; i++)
        {
            arr[i] = arr[i - 1] + arr[i - 1] * (Strategy.Model.salaryPattern.increasePercentage / 100);
        }
        return arr;
    };
    var getDefaultIncome = function () {
        var result = {
            label: 'Доходи',
            backgroundColor: [
                'rgba(21, 229, 171, 0.4)'
            ],
            borderColor: [
                'rgba(54, 162, 235, 1)'
            ],
            fill: 0
        };
        result.data = getIncomeData();
        return result;
    };
    var getSavedMoney = function (savings, position) {
        if (position == 0)
            return 0;
        return savings.data[position - 1];
    };

    var setBank = function (index) {
        $('#sliderValue').html('<p>Bank: ' + bankRating.Name[index] + '</p><p>Rating: ' + bankRating.Rating[index] + '</p>');
    };

    var changeStrategy = function (incomeStrategy, costsStrategy)
    {
        var savings = getDefaultIncome();
        for (var i = 0; i < savings.data.length; i++) {
            savings.data[i] = savings.data[i] + getSavedMoney(savings, i) + getAdditionalIncome(incomeStrategy, i) - getAdditionalCosts(i, costsStrategy);
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
        setBank(0);

        var slider = document.getElementById('bank-rating-slider');
        noUiSlider.create(slider, {
            start: [4.59],
            snap: true,
            connect: true,
            range: {
                'min': 0,
                '91.8%': 4.59,
                '87.4%': 4.37,
                '85.8%': 4.29,
                '80.14%': 4.07,
                '80.08%': 4.04,
                '76.6%': 3.83,
                '75.8%': 3.79,
                '73.4%': 3.67,
                '73%': 3.65,
                '69.6%': 3.48,
                '47.2%': 2.36,
                'max': 5
            },
            pips: {
                mode: 'values',
                values: [0, 1, 2, 3, 4, 5],
                density: 5
            }
        });

        slider.noUiSlider.on('change', function (values, handle) {
            var min = Math.min.apply(Math, bankRating.Rating);
            var max = Math.max.apply(Math, bankRating.Rating);

            if (values[handle] < min) {
                slider.noUiSlider.set(min);
                values[handle] = min;
            } else if (values[handle] > max) {
                slider.noUiSlider.set(max);
                values[handle] = max;
            }

            var index = bankRating.Rating.indexOf(parseFloat(values[handle]));
            setBank(index);
        });
    };

    return {
        init: function () {
            init();
        }
    };
})();

$(document).ready(function () {
    if (document.getElementById('chart-block'))
        Graph.init();
});