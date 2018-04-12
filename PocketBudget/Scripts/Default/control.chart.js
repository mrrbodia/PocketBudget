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
                legend: {
                    labels: {
                        lineWidth: 1
                    },
                    onHover: function (e, legendItem) {
                        var item = getAdditionalPathDataForLegendItem(this.chart, legendItem);
                    },
                    onClick: function (e, legendItem) {
                        var item = getAdditionalPathDataForLegendItem(this.chart, legendItem);
                        if (item != undefined) {
                            item.IsHidden = item.IsHidden == undefined ? true : !item.IsHidden;
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

    //TODO: refactor
    var getChartLine = function (line) {
        if (line.Type === 'Base') {
            return {
                label: line.Title,
                borderColor: [
                    'rgba(20, 40, 110, 0.5)'
                ],
                fill: false,
                borderWidth: 4,
                data: toPointsObject(line.Points)
            }
        }
        else
            if (line.Type === 'Deposit') {
            return {
                label: line.Title,
                lineType: line.Type,
                hidden: line.IsHidden,
                borderColor: [
                    'rgba(0, ' + getShade() + ', 0, 0.5)'
                ],
                fill: false,
                data: toPointsObject(line.Points)
            }
        }
        else
            if (line.Type === 'Sale') {
            return {
                label: line.Title,
                lineType: line.Type,
                hidden: line.IsHidden,
                borderColor: [
                    'rgba(0, ' + getShade() + ', 0, 0.5)'
                ],
                fill: false,
                data: toPointsObject(line.Points)
            }
        }
        else
            if (line.Type === 'Purchase') {
            return {
                label: line.Title,
                lineType: line.Type,
                hidden: line.IsHidden,
                borderColor: [
                    'rgba(' + getShade() + ', 0, 0, 0.5)'
                ],
                fill: false,
                data: toPointsObject(line.Points)
            }
        }
        else {
            return {
                label: line.Title,
                lineType: line.Type,
                hidden: line.IsHidden,
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

        if (PersonalFinances.Path.AdditionalPath['Deposit']) {
            $.each(PersonalFinances.Path.AdditionalPath['Deposit'], function (index) {
                data += PersonalFinances.Binder.bindDeposit(index);
            });
        }
        if (PersonalFinances.Path.AdditionalPath['Sale']) {
            $.each(PersonalFinances.Path.AdditionalPath['Sale'], function (index) {
                data += PersonalFinances.Binder.bindSale(index);
            });
        }
        if (PersonalFinances.Path.AdditionalPath['Credit']) {
            $.each(PersonalFinances.Path.AdditionalPath['Credit'], function (index) {
                data += PersonalFinances.Binder.bindCredit(index);
            });
        }
        if (PersonalFinances.Path.AdditionalPath['Purchase']) {
            $.each(PersonalFinances.Path.AdditionalPath['Purchase'], function (index) {
                data += PersonalFinances.Binder.bindPurchase(index);
            });
        }
        return data;
    };

    var updateGraph = function () {
        if ($('#path-form').valid()) {
            updateGraphLines();
        }
    };

    var getAdditionalPathDataForLegendItem = function (chart, legendItem) {
        var index = legendItem.datasetIndex;
        var dataset = chart.data.datasets[index];

        var typeLines = chart.data.datasets.filter(function (obj) { return obj.lineType === dataset.lineType; });
        var lineIndexInLineTypes = typeLines.indexOf(dataset);
        var items = PersonalFinances.Path.AdditionalPath[dataset.lineType];
        if (items != undefined)
            return items[lineIndexInLineTypes];
        return items;
    };

    var getAdditionalPathItems = function (lineType) {
        return PersonalFinances.Path.AdditionalPath['Deposit'];
    };

    var updateMinimalInformation = function (input) {
        var id = $(input).attr("id");
        $('.' + id).html(formatAsCurrency($(input).val()));
    };

    var addSalaryPeriodToPath = function () {
        $('#edit-salary-popup').find('input').each(function (index, input) {
            $(input).attr('value', $(input).val());
        });
        var salaryPeriods = $('#edit-salary-popup').find('.edit-salary-content').html().trim();
        $('.salary-periods').html(salaryPeriods);
    };

    var sendSalaryPeriodRequestTo = function (to) {
        var form = $('#path-form');
        if (form.valid()) {
            $.ajax({
                type: 'POST',
                url: to,
                data: form.serialize(),
                success: function (response) {
                    onSalaryPeriodSuccess(response);
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    };

    var onSalaryPeriodSuccess = function (response) {
        var _source = '.salary-periods';
        var $target = $('.edit-salary-content');
        var $newHtml = $(response.trim());
        var $container = $newHtml.find(_source);
        if ($container.length) {
            $newHtml = $container.children();
        }
        $target.html($newHtml);
        PersonalFinances.UI.resetValidationFor('.form-edit-salary-content');
        addSalaryPeriodToPath();
    };

    $.validator.unobtrusive.parseDynamicContent = function (selector) {
        //use the normal unobstrusive.parse method
        $.validator.unobtrusive.parse(selector);

        //get the relevant form
        var form = $(selector).first().closest('form');

        //get the collections of unobstrusive validators, and jquery validators
        //and compare the two
        var unobtrusiveValidation = form.data('unobtrusiveValidation');
        var validator = form.validate();

        $.each(unobtrusiveValidation.options.rules, function (elname, elrules) {
            if (validator.settings.rules[elname] == undefined) {
                var args = {};
                $.extend(args, elrules);
                args.messages = unobtrusiveValidation.options.messages[elname];
                //edit:use quoted strings for the name selector
                $("[name='" + elname + "']").rules("add", args);
            } else {
                $.each(elrules, function (rulename, data) {
                    if (validator.settings.rules[elname][rulename] == undefined) {
                        var args = {};
                        args[rulename] = data;
                        args.messages = unobtrusiveValidation.options.messages[elname][rulename];
                        //edit:use quoted strings for the name selector
                        $("[name='" + elname + "']").rules("add", args);
                    }
                });
            }
        });
    }

    var initEvents = function () {
        //TODO: graph-updater select
        //education selection will be losed (till session will be implemented)
        $('.graph-updater[type=number]').on('input', function (e) {
            onDataChanged(this);
        });
        $(document).on('click', '.save-edit-finances', function (e) {
            PersonalFinances.Path.AdditionalPath.saveAdditionalValuesSelection();
            updateGraph();
        });
        //TODO: refactor the same event behavior
        $(document).on('click', '.save-edit-salary', function (e) {
            var $form = $('.form-edit-salary-content');
            if (!$form.valid()) {
                return;
            }
            PersonalFinances.Popups.close('#edit-salary-popup');
            addSalaryPeriodToPath();
            updateGraph();
        });
        $(document).on('click', '.save-edit-profession', function (e) {
            var $form = $('.form-edit-profession-content');
            if (!$form.valid()) {
                return;
            }
            PersonalFinances.Popups.close('#edit-profession-popup');
            //addEducationToPath();
            updateGraph();
        });
        $(document).on('click', '.edit-profession', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var option = $('.profession-selection option:selected');
            var newHtml = $('.' + option.attr('value')).html().trim();
            $('#edit-profession-popup').find('.edit-profession-content').html(newHtml);
            PersonalFinances.Popups.open('#edit-profession-popup');
            PersonalFinances.UI.resetValidationFor('.form-edit-profession-content');
        });
        $(document).on('click', '.edit-salary', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var newHtml = $('.salary-periods').html().trim();
            $('#edit-salary-popup').find('.edit-salary-content').html(newHtml);
            PersonalFinances.Popups.open('#edit-salary-popup');
            PersonalFinances.UI.resetValidationFor('.form-edit-salary-content');
        });
        $(document).on('click', '.add-salary-period', function (e) {
            sendSalaryPeriodRequestTo('getsalaryperiod');
        });
        $(document).on('click', '.delete-salary-period', function (e) {
            sendSalaryPeriodRequestTo('deletesalaryperiod');
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
                complete: function () {
                    $('#additional-path-form').off(".validate")
                        .removeData("validator")
                        .removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse('#additional-path-form');
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
        $(document).on('change', '#additional-path-form', function () {
            var formId = '#additional-path-form';
            $(formId).removeData('validator');
            $(formId).removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse(formId);
        });
        $(document).on('change', '.usersExamplesData', function (e) {
            var url = $(this).parent().attr('data-url');

            $.ajax({
                url: url,
                type: 'POST',
                data: { modelId: $(this).val() },
                success: function (data) {
                    PersonalFinances.Path.updatePathForm(data.model);
                    PersonalFinances.Path.AdditionalPath.bindModel(data.model);

                    var chartLines = getChartLines(data.lines);
                    updateGraphWithData(chartLines);
                }
            });
        });
        $('select').material_select();
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