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
    var bankRating = {
        'Name': ['Райффайзен Банк Аваль', 'Креди Агриколь Банк', 'УкрСиббанк', 'Ощадбанк', 'Кредобанк', 'Укргазбанк', 'ОТП Банк', 'ПроКредит Банк', 'Укрэксимбанк', 'Укрсоцбанк', 'Банк Форвард'],
        'Rating': [4.59, 4.37, 4.29, 4.07, 4.04, 3.83, 3.79, 3.67, 3.65, 3.48, 2.36]
    };
    var initBankRating = function () {
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
    var createChart = function (graphLine) {
        var svg = d3.select("svg"),
        margin = { top: 20, right: 20, bottom: 30, left: 50 },
        width = +svg.attr("width") - margin.left - margin.right,
        height = +svg.attr("height") - margin.top - margin.bottom;
        //g = svg.append("g").attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        var minX = Math.min.apply(Math, graphLine.map(function (d) { return d.age; }));
        var maxX = Math.max.apply(Math, graphLine.map(function (d) { return d.age; }));
        var maxY = Math.max.apply(Math, graphLine.map(function (d) { return d.value; }));
        var tooltipElement = document.getElementById('tooltip-element');
        var tooltip = document.getElementById('tooltip-element');
        if (!tooltip) {
            tooltip = d3.select('body')
                       .append('div')
                       .attr('id', 'tooltip-element')
                       .attr('class', 'tooltip')
                       .style('opacity', 0);
        }

        var xScale = d3.scaleLinear()
            .domain([minX, maxX])
            .rangeRound([0, width]);

        var yScale = d3.scaleLinear()
            .domain([0, maxY])
            .rangeRound([height, 0]);

        var line = d3.line()
            .x(function (d, i) { return xScale(d.age); })
            .y(function (d) { return yScale(d.value); })

        var svg = d3.select("body").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        svg.append("g")
            .attr("class", "x axis")
            .attr("transform", "translate(0," + height + ")")
            .call(d3.axisBottom(xScale));

        svg.append("g")
            .attr("class", "y axis")
            .call(d3.axisLeft(yScale));

        svg.append("path")
            .datum(graphLine)
            .attr("class", "line")
            .attr("d", line);

        svg.selectAll(".dot")
            .data(graphLine)
            .enter().append("circle")
            .attr("class", "dot")
            .attr("cx", function (d, i) { return xScale(d.age) })
            .attr("cy", function (d) { return yScale(d.value) })
            .attr("r", 5)
            .on('mouseover', function (d) {
                var innerHtml = '<div class="tooltip-element-titles">';
                innerHtml += '<h6>' + d.age + '</h6>';
                innerHtml += '</div><div class="tooltip-element-lines">';
                innerHtml += '<span class="tooltip-element-span">' + d.value + '</span></div>';
                innerHtml += '<a href="#" class="btn btn-edit-finances" data-age="' + d.age + '">Керувати фінансами</a>';
                tooltip.transition()
                    //.duration(500)
                    //.style('opacity', 0)
                    .duration(200)
                    .style('opacity', 1);
                tooltip.html(innerHtml)
                .style("left", (d3.event.pageX) + "px")
				.style("top", (d3.event.pageY - 28) + "px");
            });
            //.on('mouseout', function (d) {
            //    tooltip.transition()
            //        .duration(800)
            //        .style('opacity', 0);
            //});
            //.on('hover', function (d) {
                
            //});
    };
    var createChartLine = function (label, data) {
        var result = [];
        for (var i = 0; i < data.length; i++) {
            result[i] = {
                age: +label[i],
                value: +data[i]
            };
        }
        return result;
    };
    var updateGraphData = function (data) {
        var label = [];
        for (var i = currentAge; i < Strategy.Model.salaryPattern.showTillAge; i++) {
            label[i - currentAge] = i;
        }
        if (!chart) {
            var line = createChartLine(label, data[0]);
            chart = createChart(line);
            return;
        }

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
    var getRetirementData = function (arr) {
        if (!Strategy.Model.salaryPattern || !arr)
            return [];
        var amountOfYearsToRetire = Strategy.Model.salaryPattern.showTillAge - Strategy.Model.salaryPattern.retirementAge;
        var amountOfYearsToWork = Strategy.Model.salaryPattern.retirementAge - currentAge;
        var result = [];
        arr.forEach(function (value, index) {
            result[index] = value;
        });
        result[amountOfYearsToWork - 1] = arr[amountOfYearsToWork - 1];
        for (var i = amountOfYearsToWork; i < amountOfYearsToWork + amountOfYearsToRetire; i++) {
            result[i] = result[i - 1] - randomInteger(20000, 60000);
        }
        return result;
    };
    var getSavedMoney = function (data, position) {
        if (position == 0)
            return 0;
        return data[position - 1];
    };
    var getSelectedBankRating = function (){
        var slider = document.getElementById('bank-rating-slider');
        return slider.noUiSlider.get();
    }
    var setBank = function (index) {
        $('#sliderValue').html('<div>Банк: ' + bankRating.Name[index] + '</div><div>Рейтинг: ' + bankRating.Rating[index] + '</div>');
    };
    var prepareIncomeGraphData = function (data, currency, strategy) {
        currency = currency || 'hrn';
        strategy = strategy || 'economically';
        for (var i = 0; i < data.length; i++) {
            data[i] = data[i] + getSavedMoney(data, i) +
                getAdditionalIncome(currency, i) - getAdditionalCosts(i, strategy);
        }
        return data;
    };
    var prepareRetirementGraphData = function (data) {
        return data;
    };
    var setStrategy = function (currency, strategy)
    {
        var incomeData = prepareIncomeGraphData(getIncomeData(), currency, strategy);
        var mainLineData = prepareRetirementGraphData(getRetirementData(incomeData));
        var graphData = [mainLineData];
        updateGraphData(graphData);
    };

    var onDataChanged = function () {
        var incomeStrategy = $('.input-strategy[name=income]:checked').val();
        var costsStrategy = $('.input-strategy[name=costs]:checked').val();
        currentAge = +$('#salary-age').val();
        increasePercentage = +$('#salary-percentage').val();
        setStrategy(incomeStrategy, costsStrategy);
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
        $(document).on('click', 'a.btn-edit-finances', function (e) {
            var btn = $(e.target);
            var age = +btn.attr('data-age');
            var options = {
                inputs: {
                    'age': age
                }
            };
            PersonalFinances.Popups.open('#edit-finances-popup', options);
        });
        $('.btn-edit-finances').on('mouseout', function (e) {
            var self = $(e.target);
            self.hide();
        });
        setStrategy();
        setBank(0);
    };

    return {
        init: function () {
            init();
        }
    };
})();

$(document).ready(function () {
    //if (document.getElementById('chart-block'))
        //PersonalFinances.Graph.init();
});