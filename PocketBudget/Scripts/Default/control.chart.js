var PersonalFinances = PersonalFinances || {};
PersonalFinances.Graph = (function () {
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
    var editFinances = function (e) {
        var self = $(e.target);
        var age = +self.attr('data-age');
        var options = {
            inputs: {
                'age': age
            }
        };
        PersonalFinances.Popups.open('#edit-finances-popup', options);
    };
    var bankRating = {
        'Name': ['Райффайзен Банк Аваль', 'Креди Агриколь Банк', 'УкрСиббанк', 'Ощадбанк', 'Кредобанк', 'Укргазбанк', 'ОТП Банк', 'ПроКредит Банк', 'Укрэксимбанк', 'Укрсоцбанк', 'Банк Форвард'],
        'Rating': [4.59, 4.37, 4.29, 4.07, 4.04, 3.83, 3.79, 3.67, 3.65, 3.48, 2.36]
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
            if (deposit['sum'] + profit > 200000 && randomInteger(1, 100) > getSelectedBankRating() * 20)
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
            if (deposit['sum'] + profit > 200000 && randomInteger(1, 100) > getSelectedBankRating() * 20)
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
                    enabled: false,
                    intersect: false,
                    custom: function (tooltipModel) {
                        var tooltipElement = document.getElementById('tooltip-element');
                        if (!tooltipElement){
                            tooltipElement = document.createElement('div');
                            tooltipElement.id = "tooltip-element";
                            tooltipElement.innerHTML = "<div></div>";
                            document.body.appendChild(tooltipElement);
                        }
                        if (tooltipModel.opacity == 0) {
                            tooltipElement.style.opacity = 0;
                            return;
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
                            var innerHtml = "<div class='tooltip-element-titles'>";
                            var age = tooltipModel.title || 18;
                            innerHtml += "<h6>" + age + "</h6>";
                            innerHtml += "</div><div class='tooltip-element-lines'>";
                            bodyLines.forEach(function (body, i) {
                                var colors = tooltipModel.labelColors[i];
                                var style = "background:" + colors.backgroundColor;
                                style += "; border-color:" + colors.borderColor;
                                style += "; border-width: 2px";
                                var span = "<span class='tooltip-element-span' style='" + style + "'>" + body + "</span>";
                                innerHtml += "<div>" + span + "</div>";
                            });
                            innerHtml += "</div><div class='tooltip-element-option'>";
                            innerHtml += '<a href="#" class="btn btn-edit-finances" data-age="' + age + '">Керувати фінансами</a>';
                            innerHtml += "</div>";
                            var divRoot = tooltipElement.querySelector('div');
                            divRoot.innerHTML = innerHtml;
                        }
                        var position = this._chart.canvas.getBoundingClientRect();
                        tooltipElement.style.opacity = 1;
                        tooltipElement.style.left = position.left + tooltipModel.caretX + 'px';
                        tooltipElement.style.top = position.top + tooltipModel.caretY + 'px';
                        tooltipElement.style.fontFamily = tooltipModel._fontFamily;
                        tooltipElement.style.fontSize = tooltipModel.fontSize;
                        tooltipElement.style.fontStyle = tooltipModel._fontStyle;
                        tooltipElement.style.padding = tooltipModel.yPadding = 'px ' + tooltipModel.xPadding + 'px';
                        tooltipElement.onclick = function (e) {
                            var self = $(e.target);
                            var age = +self.attr('data-age');
                            var options = {
                                inputs: {
                                    'age': age
                                }
                            };
                            PersonalFinances.Popups.open('#edit-finances-popup', options);
                        };
                        tooltipElement
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

    var getSelectedBankRating = function (){
        var slider = document.getElementById('bank-rating-slider');
        return slider.noUiSlider.get();
    }
    var setBank = function (index) {
        $('#sliderValue').html('<div>Банк: ' + bankRating.Name[index] + '</div><div>Рейтинг: ' + bankRating.Rating[index] + '</div>');
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

        $('.graph-updater[type=checkbox]').on('change', function (e) {
            onDataChanged();
        });
        //$('#chart').on('click', function (e) {
        //});
        $(document).on('click', '.btn-edit-finances', function (e) {
            var self = $(this);
            var age = +self.attr('data-age');
            var options = {
                inputs: {
                    'age': age
                }
            };
            PersonalFinances.Popups.open('#edit-finances-popup', options);
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
            onDataChanged();
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
        PersonalFinances.Graph.init();
    setTimeout(function () {
        var el = document.createElement('div');
        el.id = "btn-edit";
        el.innerHTML = "<button class='btn-edit-finances'>TEST</button>";
        document.body.appendChild(el);
    }, 2000);
});