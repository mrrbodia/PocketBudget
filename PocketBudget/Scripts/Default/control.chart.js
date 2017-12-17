var PersonalFinances = PersonalFinances || {};
PersonalFinances.Graph = (function () {
    var ctx = document.getElementById("chart");
    var currentAge = +$('#salary-age').val();
    var increasePercentage = +$('#salary-percentage').val();
    var chart = null;
    var onDataChanged = function () {
        var currency = $('.input-strategy[name=income]:checked').val();
        var strategy = $('.input-strategy[name=costs]:checked').val();
        currentAge = +$('#salary-age').val();
        increasePercentage = +$('#salary-percentage').val();
        setStrategy(currency, strategy);
    };
    //TODO: remove in new version
    //START
    var deposit = {
        'sum': 188000, //4000$ dollars on deposit
        'hrn': 14,
        'dollar': 3.75,
        'euro': 2.35,
        'dangerousYear': null
    };
    var getAdditionalIncome = function (currency, year) {
        if (year == 0)
            return 0;
            
        var profit = deposit['sum'] * Math.pow((1 + deposit[currency] / 100 / 12), year) - deposit['sum'] * Math.pow((1 + deposit[currency] / 100 / 12), year - 1);

        if ($('#ready-to-risk').is(':checked'))
        {
            if (deposit['sum'] + profit > 200000 && randomInteger(1, 100) > PersonalFinances.Slider.getSelectedBankRating() * 20)
            {
                deposit['sum'] = 200000;
                return 200000 - deposit['sum'];
            }

            return profit;
        }
        else
        {
            if (deposit['dangerousYear'] != null)
                return deposit['sum'] * (deposit[currency] / 100);
            if (deposit['sum'] + profit > 200000 && randomInteger(1, 100) > PersonalFinances.Slider.getSelectedBankRating() * 20)
            {
                deposit['dangerousYear'] = year;
                deposit['sum'] = 200000;
                return 0;
            }
            return profit;
        }
            
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
    var replaceTooltipTags = function (str, mapObj) {
        var result = str;
        for (var key in mapObj) {
            result = result.replace(new RegExp('\\[' + key + '\\]', 'g'), mapObj[key]);
        }
        return result;
    };
    var initTimeout = function(el, timeout) {
        return setTimeout(function () {
            el.style.display = 'none';
        }, timeout || 0);
    }
    var tmpTimer = null;
    var createChart = function(labels, datasets) {
        return new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: datasets
            },
            options: {
                //onClick: graphClickEvent,
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
                    enabled: false,
                    //intersect: false,
                    custom: function (tooltipModel) {
                        var tooltipElement = document.getElementById('tooltip-element');

                        if (!tooltipElement) {
                            tooltipElement = document.createElement('div');
                            tooltipElement.id = "tooltip-element";
                            document.body.appendChild(tooltipElement);
                        }
                        if (tooltipModel.yAlign) {
                            tooltipElement.classList.add(tooltipModel.yAlign);
                        }
                        else {
                            tooltipElement.classList.add('no-transform');
                        }
                        function getBody(bodyItem) {
                            return bodyItem.lines;
                        }
                        if (tooltipModel.body) {
                            var bodyLines = tooltipModel.body.map(getBody);
                            var age = tooltipModel.title || 18;
                            var mapObj = {
                                "AGE": age,
                                "ACUMULATED_AMOUNT": bodyLines[0]
                            };
                            var innerHtml = replaceTooltipTags($('#tooltip-element-content').html(), mapObj);
                            tooltipElement.innerHTML = innerHtml;
                        }
                        var position = this._chart.canvas.getBoundingClientRect();
                        tooltipElement.style.display = 'block';
                        tooltipElement.style.left = position.left + tooltipModel.caretX + 'px';
                        tooltipElement.style.top = position.top + tooltipModel.caretY + 'px';
                        tooltipElement.style.fontFamily = tooltipModel._fontFamily;
                        tooltipElement.style.fontSize = tooltipModel.fontSize;
                        tooltipElement.style.fontStyle = tooltipModel._fontStyle;
                        tooltipElement.style.padding = tooltipModel.yPadding = 'px ' + tooltipModel.xPadding + 'px';


                        if (tooltipModel.opacity == 1) {
                            clearTimeout(tmpTimer);
                            return;
                        }

                        clearTimeout(tmpTimer);
                        tmpTimer = initTimeout(tooltipElement, 750);

                        tooltipElement.onmouseenter = function () {
                            clearTimeout(tmpTimer);
                        };
                        tooltipElement.onmouseleave = function () {
                            tmpTimer = initTimeout(tooltipElement, 250);
                        };
                    },
                    callbacks: {
                        label: function (item, chart) {
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

    var getIncomeData = function (currency, strategy) {
        if (!Strategy.Model.salaryPattern)
            return [];
        var arr = [];
        var amountOfYearsToWork = Strategy.Model.salaryPattern.retirementAge - currentAge;
        arr[0] = +$('#salary-income').val();
        for (var i = 1; i < amountOfYearsToWork; i++)
        {
            arr[i] = arr[i - 1] + arr[i - 1] * (increasePercentage / 100);
        }
        for (var i = 0; i < arr.length; i++) {
            arr[i] = arr[i] + getSavedMoney(arr, i) +
                getAdditionalIncome(currency, i) - getAdditionalCosts(i, strategy);
        }
        return arr;
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

    var applyRetirements = function (income) {
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
        result.data = getRetirementData(income);
        return result;
    };

    var getSavedMoney = function (data, position) {
        if (position == 0)
            return 0;
        return data[position - 1];
    };

    var prepareIncomeGraphData = function (currency, strategy) {
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
        result.data = getIncomeData(currency, strategy);
        return result;
    };
    var setStrategy = function (currency, strategy)
    {
        var incomeLine = prepareIncomeGraphData(currency, strategy);
        var retirementLine = applyRetirements(incomeLine.data);
        var datasets = [incomeLine, retirementLine];
        updateGraphWithData(datasets);
    };

    var initEvents = function () {
        $('.graph-updater[type=radio]').on('change', function (e) {
            onDataChanged();
            $('.deposit-input.active').removeClass('active').addClass('hidden');
            $(e.target).parent().find('.deposit-input.hidden').removeClass('hidden').addClass('active');
        });
        $('.graph-updater[type=number]').on('input', function (e) {
            onDataChanged();
        });
        $('.graph-updater[type=checkbox]').on('change', function (e) {
            onDataChanged();
        });
        $(document).on('click', '.save-edit-finances', function () {

        });
        $(document).on('click', '.tooltip-moreoptions', function (e) {
            //$(this).next('.tooltip-body').toggle("slow");
                var btn = $(e.target);
                var age = +btn.attr('data-age');
                var data = {
                    fromAge: age
                };
                $.ajax({
                    url: 'editfinances',
                    type: 'POST',
                    data: data,
                    success: function (data) {
                        var newHtml = data.trim();
                        $('#edit-finances-popup').find('.popup-content').html(newHtml);
                        PersonalFinances.Popups.open('#edit-finances-popup');
                        $('.collapsible').collapsible();
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
        });
    };

    var init = function () {
        initEvents();
        setStrategy('hrn', 'economically');
    };

    return {
        init: function () {
            init();
        },
        onDataChanged: function() {
            onDataChanged();
        }
    };
})();

$(document).ready(function () {
    if (document.getElementById('chart-block'))
        PersonalFinances.Graph.init();
});