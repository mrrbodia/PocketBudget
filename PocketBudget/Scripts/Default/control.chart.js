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
                        if (item !== undefined) {
                            item.IsHidden = item.IsHidden === undefined ? true : !item.IsHidden;
                            updateGraph();
                        }
                        else
                            if (legendItem.datasetIndex !== 0) {
                                //education line.
                                currentValue = $("#EducationDegrees_IsHidden").val().toLowerCase() === "true";
                                $("#EducationDegrees_IsHidden").val(!currentValue);
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
        return '₴' + formatAsPrice(value);
    }

    var formatAsPrice = function (value) {
        return value.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
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
            if (line.Type === 'Education') {
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
                var chartLines = getChartLines(data.lines);
                updateGraphWithData(chartLines);
                if (data.model) {
                    PersonalFinances.Path.AdditionalPath.bindModel(data.model);
                }
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
            updateMaterialize();
        }
    };

    var updateMaterialize = function () {
        $.each($('input[type=checkbox]'), function (index, elem, value) {
            var formId = $(elem).closest('form').attr('id');
            var label = $(elem).next('label');

            var inputValue = $(elem).val();
            var inputId = $(elem).attr('id');
            var newName = '#' + formId + '_#' + inputId + '_#' + inputValue;
            $(elem).attr('id', newName);
            $(label).attr('for', newName);
        });
    };

    var getAdditionalPathDataForLegendItem = function (chart, legendItem) {
        var index = legendItem.datasetIndex;
        var dataset = chart.data.datasets[index];

        var typeLines = chart.data.datasets.filter(function (obj) { return obj.lineType === dataset.lineType; });
        var lineIndexInLineTypes = typeLines.indexOf(dataset);
        var items = PersonalFinances.Path.AdditionalPath[dataset.lineType];
        if (items !== undefined)
            return items[lineIndexInLineTypes];
        return items;
    };

    var updateMinimalInformation = function (input) {
        var id = $(input).attr("id");
        $('.' + id).html(formatAsPrice($(input).val()));
    };

    var changeSavingsSymbol = function () {
        $(".percentageSymbol").toggle();
        $(".currencyofSavings").toggle();
    };

    var savePopupMinimalPresentation = function ($popup) {
        var infoTarget = $popup.attr('data-info');
        var $targetInput = $popup.find('.' + infoTarget);
        if ($targetInput && $targetInput.length) {
            var $minimalInfo = $('.info .' + infoTarget);
            var value = getPopupMinimalValue($popup);
            $minimalInfo.text(value);
        }
    };

    var getPopupMinimalValue = function ($popup) {
        if ($popup.hasClass('edit-salary-popup')) {
            return formatAsPrice($popup.find('.' + $popup.attr('data-info')).last().val());
        }
        else if ($popup.hasClass('edit-education-popup')) {
            var $target = $popup.find('.chb-is-reached:checked');
            if (!$target || !$target.length)
                return "";
            var $info = $target.last().parents('.form-row').find('.' + $popup.attr('data-info'));
            if (!$info || !$info.length)
                return "";
            return $info.text();
        }
    };

    var addPopupInfoToPath = function ($popup) {
        $popup.find('input').each(function (index, input) {
            if (input.type === "checkbox" && input.checked) {
                $(input).attr('value', true);
                $(input).attr('checked', 'checked');
            }
            else if (input.type === "checkbox" && !input.checked) {
                $(input).attr('value', false);
                $(input).removeAttr('checked');
            }
            else {
                $(input).attr('value', $(input).val());
            }
        });
        var info = $popup.find('.popup-content').html().trim();
        $('.' + $popup.attr('data-target')).html(info);
    };

    var loadPopupContent = function (to, source, popup) {
        var form = $('#path-form');
        if (form.valid()) {
            $.ajax({
                type: 'POST',
                url: to,
                data: form.serialize(),
                success: function (response) {
                    onLoadPopupSuccess(response, source, popup);
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    };

    var onLoadPopupSuccess = function (response, source, popup) {
        modifyTargetWithData(response, source, popup + ' .popup-content');
        addPopupInfoToPath($(popup));
        PersonalFinances.UI.resetValidationFor(popup + ' form');
    };

    var modifyTargetWithData = function (data, source, target)
    {
        var $target = $(target);
        var $newHtml = $(data.trim());
        var $container = $newHtml.find(source);
        if ($container.length) {
            $newHtml = $container.children();
        }
        $target.html($newHtml);
    };

    var initEvents = function () {
        $('.graph-updater[type=number]').on('input', function (e) {
            onDataChanged(this);
        });
        $(document).on('click', '.save-edit-finances', function (e) {
            var $modal = $(this).parents('.modal');
            var $form = $modal.find('form');
            if (!$form.valid()) {
                return;
            }
            PersonalFinances.Popups.close('#' + $modal.attr('id'));
            //TODO: add popup info to path
            PersonalFinances.Path.AdditionalPath.saveAdditionalValuesSelection();
            updateGraph();
        });
        $(document).on('click', '.save-edit-popup', function (e) {
            var $modal = $(this).parents('.modal');
            var $form = $modal.find('form');
            if (!$form.valid()) {
                return;
            }
            PersonalFinances.Popups.close('#' + $modal.attr('id'));
            addPopupInfoToPath($modal);
            updateGraph();
            savePopupMinimalPresentation($('#' + $modal.attr('id')));
        });
        $(document).on('change', '.profession-selection', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var url = $(this).attr('data-url');
            var option = $(this).find('option:selected');
            $('.selected-profession').val(option.attr('value'));
            $('.ProfessionSelection_Professions').html(option[0].label);
            var form = $('#path-form');
            $.ajax({
                url: url,
                type: 'POST',
                data: form.serialize(),
                success: function (data) {
                    modifyTargetWithData(data, '.education-degrees', '.education-degrees');
                    PersonalFinances.UI.resetValidationFor('#path-form');
                }
            });
        });
        //TODO: refactor methods below
        $(document).on('click', '.edit-education', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var newHtml = $('.education-degrees').html().trim();
            $('#edit-education-popup').find('.edit-education-content').html(newHtml);
            PersonalFinances.Popups.open('#edit-education-popup');
            PersonalFinances.UI.resetValidationFor('.form-edit-education-content');
            updateMaterialize();
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
            loadPopupContent('getsalaryperiod', '.salary-periods', '#edit-salary-popup');
        });
        $(document).on('click', '.delete-salary-period', function (e) {
            loadPopupContent('deletesalaryperiod', '.salary-periods', '#edit-salary-popup');
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
                    PersonalFinances.UI.resetValidationFor('#edit-finances-popup form');
                    $('ul.tabs').tabs();
                    $('.collapsible').collapsible({
                        accordion: false
                    });
                },
                complete: function () {
                    PersonalFinances.UI.resetValidationFor("#additional-path-form");
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
            PersonalFinances.UI.resetValidationFor('#additional-path-form');
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
        $(document).on('change', '.savings-strategy', function (e) {
            changeSavingsSymbol();
        });
        $(document).on('change', '#additional-path-form input', function (e) {
            var $input = $(e.target);
            var $error = $('.field-validation-error[data-valmsg-for="' + $input.attr('name') + '"');
            if ($error && $error.length) {
                $error.text('');
            }
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
    $('select').material_select();
    if (document.getElementById('chart-block-new'))
        PersonalFinances.Graph.init();
});