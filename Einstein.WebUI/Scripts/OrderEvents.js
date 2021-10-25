//Валидация данных
(function ($, kendo) {
    $.extend(true, kendo.ui.validator, {
        rules: { // custom rules
            validmask: function (input) {
                if (input.is("[data-validmask-msg]") && input.val() != "") {
                    var maskedtextbox = input.data("kendoMaskedTextBox");
                    return maskedtextbox.value().indexOf(maskedtextbox.options.promptChar) === -1;
                }

                return true;
            },
            maximumvalidation: function (input, params) {

                if (input.is("[name='persons']") && input.val() != "") {

                    var numerictextbox = input.data("kendoNumericTextBox");
                    var val = numerictextbox.value();
                    var max = numerictextbox.max();
                    input.attr("data-maximumvalidation-msg", "Количество посетителей должно быть в диапазоне от 1 до " + max);

                    return (val <= max);
                }

                return true
            }
        }
    });
})(jQuery, kendo);


function getParameters(data)
{
    var grid = $("#gridOrders").data("kendoGrid");
    var uid = $('#eventid').closest(".k-popup-edit-form").data("uid");
    if (grid && uid) {
        var item = grid.dataSource.getByUid(uid);
        return { eventid: item.eventid };
    }
    return { eventid: 0 }
}
function SetFreePlaces(num) {
    
    var numerictextbox = $("#persons").data("kendoNumericTextBox");
    if (num > 0) {

        numerictextbox.value(1);
        numerictextbox.enable(true);
        numerictextbox.max(num);


    }
    else {

        numerictextbox.value(1);
        numerictextbox.enable(false);
    }
    //for view
    $("#freeplaces-view").text(num);

    $("#freeplaces").val(num);
    $("#freeplaces").change();

   
}

function SetEventId(id)
{
    $("#eventid").val(id);
    $("#eventid").change();
}

function SetTimes(times) {
    var dropdowntimes = $("#timeevent").data("kendoDropDownList");
    dropdowntimes.select(-1);
    dropdowntimes.trigger("select");

    if (times.length > 0) {
        var dataSource = new kendo.data.DataSource({
            data: times.sort()
        });
        dropdowntimes.setDataSource(dataSource);
        //times.forEach(function (obj, index) {
        //    dropdowntimes.da
        //});
        dropdowntimes.enable(true);
    }
    else {

        dropdowntimes.enable(false);
    }
}

function addDays(date, days) {
    const copy = new Date(Number(date))
    copy.setDate(date.getDate() + days)
    return copy
}

function inArray(dates, date) {

    for (var i = 0; i < dates.length; i++) {

        if (date.valueOf() === dates[i].valueOf()) {

            return true;
        }
    }

    return false;
}
function DisableDates(dates, start, end) {
    var currentDay = new Date(start);
    var result = []

    while (currentDay <= end) {
        if (!inArray(dates, currentDay)) {
            result.push(currentDay);
        }

        currentDay = addDays(currentDay, 1);
    }

    return result;
}

function SetDates(dates) {
    var datepicker = $("#dateevent").data("kendoDatePicker");
    var value = datepicker.value();
    datepicker.value("");
    datepicker.trigger("change");

    if (dates.length > 0) {
        var min = GetMinDate(dates);
        var max = GetMaxDate(dates);
        //Устанавливаем минимальную дату
        datepicker.min(min);
        //Устанавливаем максимальную дату
        datepicker.max(max);
        //Исключаем недоступные
        var disableDates = DisableDates(dates, min, max);

        datepicker.setOptions({
            disableDates: disableDates
        });

        datepicker.enable();
    }
    else {
        
        datepicker.disable();
    }

}

function GetMinDate(dates)
{
    var min = dates.reduce(function (a, b) { return a < b ? a : b; });
    return min;
   
}

function GetMaxDate(dates) {
    
    var max = dates.reduce(function (a, b) { return a > b ? a : b; });
    return max;
}

function AvailableEvents(dates) {
    var avdates = [];
    dates.forEach(function (obj, index) {
        avdates.push(kendo.parseDate(obj.Date));
    });

    SetDates(avdates.sort(function (a, b) {
        var c = new Date(a.date);
        var d = new Date(b.date);
        return c - d;
    }));
}


function onDropDownListEventsNamesChange(e) {
   
    var event = e.dataItem;
    if (event) {
        AvailableEvents(event.Dates)
    }
}

function onDropDownListTimesChange(e) {

    var item = e.dataItem;
 
    if (item) {
        SetFreePlaces(item.FreePlaces);
        SetEventId(item.EventId);
    }
    else {
        SetFreePlaces(0);
        SetEventId(0);
    }
}

function onDatePickerDateChange(e) {
    var date = this.value();
    var dropdownnames = $("#eventname").data("kendoDropDownList");
    var event = dropdownnames.dataItem();

    if (event && date) {

        event.Dates.forEach(function (obj, index) {
            var avdate = kendo.parseDate(obj.Date);
            if (avdate.valueOf() === date.valueOf()) {
                SetTimes(obj.Times);
            }
        });
    }
    else {
        SetTimes([]);
    }
}