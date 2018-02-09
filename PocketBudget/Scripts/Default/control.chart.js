var PersonalFinances = PersonalFinances || {};
PersonalFinances.Graph = (function () {
    var ctx = document.getElementById("chart");
    var chart = null;

    var initTimeout = function (el, timeout) {
        return setTimeout(function () {
            el.style.display = 'none';
        }, timeout || 0);
    };
    var tmpTimer = null;
    var createChart = function (labels, datasets) {
        var date = new Date();
        var currentYear = date.getFullYear();
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
                                var year = currentYear + index;
                                return index % 5 ? "" : ["Вік " + value + " / " + year + " рік"];
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
                        //TODO:
                        //if (neededLine)
                        //    return;

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
                            var currentIndex = tooltipModel.dataPoints[0].index;
                            var age = currentIndex + 20;
                            var year = currentYear + parseInt(currentIndex)
                            var mapObj = {
                                "AGE": age,
                                "TOOLTIP_TITLE": "Вік " + age + " / " + year + " рік",
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

                        if (tooltipModel.opacity === 1) {
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

    var replaceTooltipTags = function (str, mapObj) {
        var result = str;
        for (var key in mapObj) {
            result = result.replace(new RegExp('\\[' + key + '\\]', 'g'), mapObj[key]);
        }
        return result;
    };

    var updateGraphWithData = function (datasets) {
        var labels = [];
        var date = new Date();
        var currentYear = date.getFullYear();
        for (var i = PersonalFinances.Path.CurrentAge; i < PersonalFinances.Path.LifeExpectancy; i++)
        {
            var index = i - PersonalFinances.Path.CurrentAge;
            labels[index] = i;
            //if (i % 5) {
            //    labels[index] = "";
            //}
            //else {
            //    var year = currentYear + index;
            //    labels[index] = "Вік " + i + " / " + year + " рік";
            //}
        }
        if (!chart) {
            chart = createChart(labels, datasets);
            return;
        }
        removeChartData(chart);
        addChartData(chart, labels, datasets);
    };

    var removeChartData = function (chart) {
        chart.data.labels = [];
        chart.data.datasets.forEach(function (dataset, index) {
            dataset.data = [];
        });
        chart.update();
    };

    var addChartData = function (chart, labels, datasets) {
        chart.data.labels = labels;
        datasets.forEach(function (dataset, index) {
            chart.data.datasets[index] = dataset;
        });
        chart.update();
    };

    var onDataChanged = function () {
        updateGraph();
    };

    var getChartLines = function (data) {
        var result = [];
        for (var i = 0; i < data.length; i++) {
            result[i] = getChartLine(data[i]);
        }
        return result;
    };

    var getChartLine = function (line) {
        if (line.Type === 'savings') {
            return {
                label: 'Збереження',
                backgroundColor: [
                    'rgba(21, 229, 171, 0.4)'
                ],
                borderColor: [
                    'rgba(54, 162, 235, 1)'
                ],
                fill: true,
                data: line.Points
            }
        }
        else {
            return {
                label: 'Витрати на пенсії',
                backgroundColor: [
                    'rgba(255, 179, 179, 0.4)'
                ],
                borderColor: [
                    'rgba(255, 102, 102, 1)'
                ],
                fill: true,
                data: line.Points
            }
        }
    };

    var initInputValues = function () {
        PersonalFinances.Path.CurrentAge = +$('#age-current').val();
        PersonalFinances.Path.RetirementAge = +$('#age-retirement').val();
        PersonalFinances.Path.LifeExpectancy = +$('#age-life-expectancy').val();
        PersonalFinances.Path.Savings.Amount = +$('#target-savings').val();
        PersonalFinances.Path.Spendings.Amount = +$('#target-spendings').val();
    };

    var updateGraphLines = function ()
    {
        console.log($.toJSON(PersonalFinances.Path))
        var model = bindPathModel();
        $.ajax({
            url: 'getchartlines',
            type: 'POST',
            dataType: "json",
            data: model,
            success: function (data) {
                var chartLines = getChartLines(data);
                updateGraphWithData(chartLines);
            },
            error: function (err) {
                console.log(err);
            }
        });
    };

    var bindPathModel = function () {
        var data = {
            'CurrentAge': PersonalFinances.Path.CurrentAge,
            'RetirementAge': PersonalFinances.Path.RetirementAge,
            'LifeExpectancy': PersonalFinances.Path.LifeExpectancy,
            'Savings.Amount': PersonalFinances.Path.Savings.Amount,
            'Spendings.Amount': PersonalFinances.Path.Spendings.Amount
        }
        if (PersonalFinances.Path.AdditionalPath.Deposits) {
            //each additional income, not deposit
            $.each(PersonalFinances.Path.AdditionalPath.Deposits, function (index) {
                PersonalFinances.Binder.bindDeposit(data, index);
            });
        }
        return data;
    };

    var updateGraph = function ()
    {
        initInputValues();
        updateGraphLines();
    };

    var initEvents = function () {
        $('.graph-updater[type=number]').on('input', function (e) {
            onDataChanged();
        });
        $(document).on('click', '.save-edit-finances', function (e) {
            PersonalFinances.Path.AdditionalPath.saveAdditionalValuesSelection();
            updateGraph();
        });
        $(document).on('click', '.tooltip-moreoptions', function (e) {
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
                    $('#edit-finances-popup').find('.modal-content').html(newHtml);
                    PersonalFinances.Popups.open('#edit-finances-popup');
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
    };

    var init = function () {
        initEvents();
        updateGraph();
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
    if (document.getElementById('chart-block-new'))
        PersonalFinances.Graph.init();
});