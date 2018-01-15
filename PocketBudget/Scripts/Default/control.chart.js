var PersonalFinances = PersonalFinances || {};
PersonalFinances.GraphNew = (function () {
    var ctx = document.getElementById("chart");
    var chart = null;

    var initTimeout = function (el, timeout) {
        return setTimeout(function () {
            el.style.display = 'none';
        }, timeout || 0);
    };
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
                                return index % 5 ? "" : "Вік " + value;
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
        for (var i = PersonalFinances.Path.CurrentAge; i < PersonalFinances.Path.LifeExpectancy; i++)
        {
            labels[i - PersonalFinances.Path.CurrentAge] = i;
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
        chart.data.datasets.forEach(function (dataset, index) {
            dataset.data = datasets[index].data;
        });
        chart.update();
    };

    var onDataChanged = function () {
        updateGraph();
    };

    var getSavingsChartLine = function (data) {
        var result = {
            label: 'Збереження',
            backgroundColor: [
                'rgba(21, 229, 171, 0.4)'
            ],
            borderColor: [
                'rgba(54, 162, 235, 1)'
            ],
            fill: true
        };
        result.data = data;
        return result;
    };

    var getSpendingsChartLine = function (data) {
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
        result.data = data;
        return result;
    };

    var getNeededChartLine = function () {

    };

    var initInputValues = function () {
        PersonalFinances.Path.CurrentAge = +$('#age-current').val();
        PersonalFinances.Path.RetirementAge = +$('#age-retirement').val();
        PersonalFinances.Path.LifeExpectancy = +$('#age-life-expectancy').val();
        PersonalFinances.Path.Savings = +$('#target-savings').val();
        PersonalFinances.Path.Spendings = +$('#target-spendings').val();
    };

    var updateGraphLines = function ()
    {
        return $.ajax({
            url: 'getchartlines',
            type: 'POST',
            data: PersonalFinances.Path,
            success: function (data) {
                var savings = getSavingsChartLine(data[0]);
                var spendings = getSpendingsChartLine(data[1]);
                var lines = [savings, spendings];
                updateGraphWithData(lines);
            },
            error: function (err) {
                console.log(err);
            }
        });
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
            PersonalFinances.AdditionalPath.saveAdditionalValuesSelection();
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
        PersonalFinances.GraphNew.init();
});