var PersonalFinances = PersonalFinances || {};
PersonalFinances.Slider = (function (slider) {
    var self = this;

    self.getSelectedBankRating = function () {
        var slider = document.getElementById('bank-rating-slider');
        return slider.noUiSlider.get();
    }
    var bankRating = {
        'Name': ['Райффайзен Банк Аваль', 'Креди Агриколь Банк', 'УкрСиббанк', 'Ощадбанк', 'Кредобанк', 'Укргазбанк', 'ОТП Банк', 'ПроКредит Банк', 'Укрэксимбанк', 'Укрсоцбанк', 'Банк Форвард'],
        'Rating': [4.59, 4.37, 4.29, 4.07, 4.04, 3.83, 3.79, 3.67, 3.65, 3.48, 2.36]
    };
    var setBank = function (index) {
        $('#sliderValue').html('<div>Банк: ' + bankRating.Name[index] + '</div><div>Рейтинг: ' + bankRating.Rating[index] + '</div>');
    };

    return {
        init: function (slider) {
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
                if (PersonalFinances.Graph.onDataChanged) {
                    PersonalFinances.Graph.onDataChanged();
                }
            });

            setBank(0);
        }
    };
})();

$(document).ready(function () {
    var slider = document.getElementById('bank-rating-slider');
    if (slider)
        PersonalFinances.Slider.init(slider);
});