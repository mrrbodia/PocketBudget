var PersonalFinances = PersonalFinances || {};
PersonalFinances.Graph = (function () {
    var ctx = document.getElementById("chart");
    var chart = null;

    var usersExamplesData = [
        {
            Salary_Amount: 7000,
            Savings_Amount: 3000,
            Pension_Amount: 3000,
            Spendings_Amount: 2500,
            CurrentAge: 20,
            RetirementAge: 60,
            LifeExpectancy: 80
        },
        {
            Salary_Amount: 12000,
            Savings_Amount: 7000,
            Pension_Amount: 2000,
            Spendings_Amount: 3500,
            CurrentAge: 26,
            RetirementAge: 60,
            LifeExpectancy: 80
        },
        {
            Salary_Amount: 16000,
            Savings_Amount: 10000,
            Pension_Amount: 1600,
            Spendings_Amount: 5000,
            CurrentAge: 28,
            RetirementAge: 65,
            LifeExpectancy: 80
        }];

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
                legend: {
                    labels: {
                        lineWidth: 1
                    },
                    onClick: function (e, legendItem) {
                        var deposits = PersonalFinances.Path.AdditionalPath.Deposits;
                        if (deposits != undefined)
                        {
                            deposits[0].IsHidden = deposits[0].IsHidden == undefined ? true : !deposits[0].IsHidden;
                            updateGraph();
                        }
                    }
                },
                annotation: {
                    drawTime: 'afterDatasetsDraw',
                    annotations: [{
                        borderColor: 'red',
                        borderDash: [2, 2],
                        borderWidth: 2,
                        mode: 'vertical',
                        type: 'line',
                        value: parseInt($('#RetirementAge').val()) - parseInt($('#CurrentAge').val()) - 1,
                        scaleID: 'x-axis-0',
                        label: {
                            content: "Пенсія",
                            enabled: true,
                            position: "top",
                            backgroundColor: "rgba(255, 255, 255, 0.7)",
                            fontColor: "black",
                        }
                    }]
                },
                //onClick: graphClickEvent,
                scales: {
                    xAxes: [{
                        id: 'x-axis-0',
                        type: 'linear',
                        display: false
                    }, {
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
                    }],
                    yAxes: [{
                        ticks: {
                            callback: function (value, index, array) {
                                return formatAsCurrency(value);
                            }
                        }
                    }]
                },
                elements: {
                    point: {
                        radius: 2,
                        hitRadius: 10,
                        hoverRadius: 5
                    }
                },
                pan: {
                    // Boolean to enable panning
                    enabled: true,

                    // Panning directions. Remove the appropriate direction to disable 
                    // Eg. 'y' would only allow panning in the y direction
                    mode: 'xy'
                },

                // Container for zoom options
                zoom: {
                    // Boolean to enable zooming
                    enabled: true,

                    // Zooming directions. Remove the appropriate direction to disable 
                    // Eg. 'y' would only allow zooming in the y direction
                    mode: 'xy'
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
                            var currentIndex = tooltipModel.dataPoints[0].index;
                            var age = currentIndex + parseInt($('#CurrentAge').val());
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
                            return formatAsCurrency(item.yLabel);
                        }
                    }
                }
            }
        });
    };

    var formatAsCurrency = function (value) {
        return '₴' + value.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
    }

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
        var currentAge = +$('.current-age').val();
        var lifeExpectancy = +$('.life-expectancy-age').val();
        for (var i = currentAge; i < lifeExpectancy; i++)
        {
            labels[i - currentAge] = i;
        }
        if (chart)
        {
            chart.destroy();
        }

        chart = createChart(labels, datasets);
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

    var onDataChanged = function (input) {
        updateGraph();
        updateMinimalInformation(input);
    };

    var getChartLines = function (data) {
        var result = [];
        for (var i = 0; i < data.length; i++) {
            result[i] = getChartLine(data[i]);
        }
        return result;
    };

    var toPointsObject = function (points)
    {
        var obj = [];
        for (var i = 0; i < points.length; ++i)
        {
            var item = {};
            item['x'] = i;
            item['y'] = points[i];
            obj[i] = item;
        }
        return obj;
    }

    var getChartLine = function (line) {
        if (line.Type === 'base') {
            return {
                label: 'Базова',
                borderColor: [
                    'rgba(20, 40, 110, 0.5)'
                ],
                fill: false,
                borderWidth: 4,
                data: toPointsObject(line.Points)
            }
        }
        if (line.Type === 'deposit') {
            return {
                label: 'Депозит',
                id: 'test',
                hidden: line.IsHidden,
                borderColor: [
                    'rgba(0, ' + getShade() + ', 0, 0.5)'
                ],
                fill: false,
                data: toPointsObject(line.Points)
            }
        }
        else {
            return {
                label: 'Кредит',
                borderColor: [
                    'rgba(' + getShade() + ', 0, 0, 0.5)'
                ],
                fill: false,
                data: toPointsObject(line.Points)
            }
        }
    };

    var getShade = function () {
        var max = 255;
        var min = 150;
        var r = Math.floor(Math.random() * (max - min + 1)) + min;
        return r;
    };

    var updateGraphLines = function ()
    {
        var $form = $('#path-form');
        if (!$form.valid()) {
            var notValidInput = $('.input-validation-error');
            notValidInput.focus();
            return;
        }
        var model = bindPathModel($form);
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

    var bindPathModel = function ($form) {
        var data = $form.serialize();

        //TODO:
        //var additionalForm = $('#additional-path-form');
        //if (additionalForm.length) {
        //    data += '&' + additionalForm.serialize();
        //}

        if (PersonalFinances.Path.AdditionalPath.Deposits) {
            $.each(PersonalFinances.Path.AdditionalPath.Deposits, function (index) {
                data += PersonalFinances.Binder.bindDeposit(index);
            });
        }
        if (PersonalFinances.Path.AdditionalPath.Sales) {
            $.each(PersonalFinances.Path.AdditionalPath.Sales, function (index) {
                data += PersonalFinances.Binder.bindSale(index);
            });
        }
        if (PersonalFinances.Path.AdditionalPath.Credits) {
            $.each(PersonalFinances.Path.AdditionalPath.Credits, function (index) {
                data += PersonalFinances.Binder.bindCredit(index);
            });
        }
        if (PersonalFinances.Path.AdditionalPath.Purchases) {
            $.each(PersonalFinances.Path.AdditionalPath.Purchases, function (index) {
                data += PersonalFinances.Binder.bindPurchase(index);
            });
        }
        return data;
    };

    var updateGraph = function () {
        updateGraphLines();
    };

    var updateMinimalInformation = function (input) {
        var id = $(input).attr("id");
        $('.' + id).html(formatAsCurrency($(input).val()));
    };

    var initEvents = function () {
        $('.graph-updater[type=number]').on('input', function (e) {
            onDataChanged(this);
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
                type: 'GET',
                data: data,
                success: function (data) {
                    var newHtml = data.trim();
                    $('#edit-finances-popup').find('.modal-content').html(newHtml);
                    PersonalFinances.Popups.open('#edit-finances-popup');
                    $('ul.tabs').tabs();
                    $('.collapsible').collapsible({
                        accordion: false
                    });
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
        $(document).on('change', '.pattern', function (e) {
            var value = $(this).attr('data-value');
            var selector = '#' + $(this).attr('data-for');
            $(selector).val(value);

            onDataChanged(selector);
        });
        $(".button-collapse").sideNav('show');
        $(document).ready(function () {
            $('select').material_select();
            $('.button-collapse').sideNav({
                menuWidth: 400,
                edge: 'right',
                closeOnClick: false,
                draggable: true
            });
        });
        $(document).on('change', '.usersExamplesData', function (e) {
            var value = $(this).val();
            var userData = usersExamplesData[value - 1];
            for (var key in userData) {
                $('#' + key).val(userData[key]);
                $('.' + key).text(userData[key]);
            }
            updateGraph();
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