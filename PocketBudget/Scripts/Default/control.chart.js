var Graph = (function () {
    var ctx = document.getElementById("chart");
    var currentAge = +$('#salary-age').val();
    var increasePercentage = +$('#salary-percentage').val();
    var chart = null;
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
    //START
    var deposit = {
        'sum': 108000, //4000$ dollars on deposit
        'hrn': 14,
        'dollar': 3.75,
        'euro': 2.35
    };
    var getAdditionalIncome = function (currency, month) {
        if (month == 0)
            return 0;
        return (deposit['sum'] * (deposit[currency] / 100)) / 12;
    };
    //END
    var randomInteger = function (min, max) {
        var rand = min + Math.random() * (max - min);
        rand = Math.round(rand);
        return rand;
    };
    var getAdditionalCosts = function (position, strategy) {
        return strategy == 'economically' ? randomInteger(3000, 5000) : randomInteger(6000, 10000);
    };
    var removeChartData = function (chart) {
        chart.data.labels = [];
        chart.data.datasets.forEach(function(dataset, index) {
            dataset.data = [];
        });
        chart.update();
    };
    var addChartData = function (chart, labels, datasets) {
        chart.data.labels = labels;
        chart.data.datasets.forEach(function(dataset, index) {
            dataset.data = datasets[index].data;
        });
        chart.update();
    };
    var createChart = function(labels, datasets){
        return new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: datasets
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
                                return (index % 5) ? "" : "Вік " + value;
                            }
                        }
                    }]
                },
                elements: {
                    point: {
                        radius: 0,
                        hitRadius: 10,
                        hoverRadius: 5
                    }
                },
                tooltips: {
                    callbacks: {
                        label: function (item, data) {
                            //return (item.index % 5) ? "" : item.yLabel.toFixed(2);
                            return item.yLabel.toFixed(2);
                        }
                    }
                }
            }
        });
    };
    var updateGraphWithData = function (datasets) {
        var label = [];
        for (var i = currentAge; i < Strategy.Model.salaryPattern.showTillAge; i++)
        {
            label[i - currentAge] = i;
        }
        if (!chart) {
            chart = createChart(label, datasets);
            return;
        }
        removeChartData(chart);
        addChartData(chart, label, datasets);
    };
    var getIncomeData = function () {
        if (!Strategy.Model.salaryPattern)
            return [];
        var arr = [];
        var amountOfYearsToWork = Strategy.Model.salaryPattern.retirementAge - currentAge;
        arr[0] = +$('#salary-income').val();
        for (var i = 1; i < amountOfYearsToWork; i++)
        {
            arr[i] = arr[i - 1] + arr[i - 1] * (increasePercentage / 100);
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
            fill: true
        };
        result.data = getIncomeData();
        return result;
    };
    var getRetirementData = function (arr) {
        if (!Strategy.Model.salaryPattern || !arr)
            return [];
        var amountOfYearsToRetire = Strategy.Model.salaryPattern.showTillAge - Strategy.Model.salaryPattern.retirementAge;
        var amountOfYearsToWork = Strategy.Model.salaryPattern.retirementAge - currentAge;
        var result = [];
        arr.forEach(function (value, index) {
            result[index] = null;
        });
        result[amountOfYearsToWork - 1] = arr[amountOfYearsToWork - 1];
        for (var i = amountOfYearsToWork; i < amountOfYearsToWork + amountOfYearsToRetire; i++) {
            result[i] = result[i - 1] - randomInteger(20000, 60000);
        }
        return result;
    };
    var getRetirements = function (savings) {
        var result = {
            label: 'Витрати на пенсії',
            backgroundColor: [
                'rgba(255, 179, 179, 0.4)'
            ],
            borderColor: [
                'rgba(255, 102, 102, 1)'
            ],
            fill: true
        };
        result.data = getRetirementData(savings);
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
            savings.data[i] = savings.data[i] + getSavedMoney(savings, i) +
                getAdditionalIncome(incomeStrategy, i) - getAdditionalCosts(i, costsStrategy);
        }
        var retirements = getRetirements(savings.data);
        var datasets = [savings, retirements];
        updateGraphWithData(datasets);
    };

    var onDataChanged = function () {
        var incomeStrategy = $('.input-strategy[name=income]:checked').val();
        var costsStrategy = $('.input-strategy[name=costs]:checked').val();
        currentAge = +$('#salary-age').val();
        increasePercentage = +$('#salary-percentage').val();
        changeStrategy(incomeStrategy, costsStrategy);
    };

    var init = function () {
        $('.graph-updater[type=radio]').on('change', function(e) {
            onDataChanged();
            $('.deposit-input.active').removeClass('active').addClass('hidden');
            $(e.target).parent().find('.deposit-input.hidden').removeClass('hidden').addClass('active');
        });
        $('.graph-updater[type=number]').on('input', function (e) {
            onDataChanged();
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