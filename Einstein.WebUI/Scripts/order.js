//Валидация данных
(function ($, kendo) {
    $.extend(true, kendo.ui.validator, {
        rules: { // custom rules
            validmask: function (input) {
                if (input.is("[name='phonenumber']") && input.val() != "") {
                    var maskedtextbox = input.data("kendoMaskedTextBox");
                    var mask = maskedtextbox.options.mask;
                    var val = maskedtextbox.value();
                    var re = /^\+7\(\d{3}\)\s\d{3}-\d{4}$/
                    input.attr("data-validmask-msg", "Введите телефона в формате " + mask);
                    return re.test(val);
                }

                return true;
            },
            maximumvalidation: function (input, params) {

                if ((input.is("[name='persons']") || input.is("[name='persons14']")) && input.val() != "") {

                    var numerictextbox = input.data("kendoNumericTextBox");
                    var val = numerictextbox.value();
                    var max = $(input).data("max");
                    if (max) {
                        var min = numerictextbox.min();
                        var msg = "Количество всего посетителей";
                        if (input.is("[name='persons14']")) {
                            msg = "Количество детей до 14 лет";
                        }
                        input.attr("data-maximumvalidation-msg", msg + " должно быть в диапазоне от " + min + " до " + max);

                        return (val >= min && val <= max);
                    }
                    return true;
                }

                return true
            },
            maxpersonsvalidation: function (input, params) {

                if (input.is("[name='persons14']") && input.val() != "") {

                    var numerictextbox = $("#persons14").data("kendoNumericTextBox");
                    var val = numerictextbox.value();
                    var max = $("#persons").data("kendoNumericTextBox").value();
                    
                    return ( val < max);
                }

                return true
            }
            

        },
        messages: {
            maxpersonsvalidation: function (input) {

                var max = $("#persons").data("kendoNumericTextBox").value();
                return kendo.format("Количетво детей должно быть меньше общего количества ({0}) !", max);

            }
        }
        
    });
})(jQuery, kendo);


function onPersonsChange(e) {

    var numerictextbox = $("#persons14").data("kendoNumericTextBox");
    numerictextbox.trigger("change");
}

function getParameters(data)
{
    //var grid = $("#gridOrders").data("kendoGrid");
    //var uid = $('#eventid').closest(".k-popup-edit-form").data("uid");
    //if (grid && uid) {
    //    var item = grid.dataSource.getByUid(uid);
    //    return { eventid: item.eventid };
    //}
    //return { eventid: 0 }
    var orderid = $("#id").val();
    
    if (orderid) {

        return { orderid: orderid }
    }
    else {

        return { orderid: orderid }
    }

}

function SetMinNumerictextbox(numerictextbox)
{
    var min = numerictextbox.min();
    numerictextbox.value(min);
    
    numerictextbox.trigger("change");
}

function SetMaxNumerictextbox(element, num)
{
    
    var numerictextbox = element.data("kendoNumericTextBox");
    if (num > 0) {
       
        var val = numerictextbox.value();
        
        if (val > num) {
            SetMinNumerictextbox(numerictextbox);
        }
        element.attr("data-max", num);
        numerictextbox.enable(true);

       //numerictextbox.max(num);

    }
    else {

        SetMinNumerictextbox(numerictextbox);
        numerictextbox.enable(false);
    }

  
}

function SetFreePlaces14(num, num14)
{

    if (num14 >= num) {

        if (num > 0) {

            return num - 1;
        }
        else {

            return num;
        }
    }
    return num14;
}

function SetFreePlaces(num, num14) {

    
    num14= SetFreePlaces14(num, num14);

    var element = $("#persons");
    SetMaxNumerictextbox(element, num);
    element = $("#persons14");
    SetMaxNumerictextbox(element, num14);


    //for view
    $("#freeplaces-view").text(num);
    //for view
    $("#freeplaces14-view").text(num14);

    SetElementValue($("#freeplaces"), num);
    SetElementValue($("#freeplaces14"), num14);


}
function SetElementValue(element,val)
{
    element.val(val);
    element.change();
}

function SetEventId(id)
{
    SetElementValue($("#eventid"), id);

}

function SetTimes(times) {
    var dropdowntimes = $("#timeevent").data("kendoDropDownList");
    dropdowntimes.select(-1);
    dropdowntimes.trigger("select");

    if (times.length > 0) {
        var dataSource = new kendo.data.DataSource({
            data: times.sort((a, b) =>  parseInt(a.Value) - parseInt(b.Value) )
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

        datepicker.enable(false);
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
    else {
        
        AvailableEvents([]);
    }
}

function onDropDownListTimesChange(e) {

    var item = e.dataItem;
 
    if (item) {
        SetFreePlaces(item.FreePlaces, item.FreePlaces14);
        SetEventId(item.EventId);
    }
    else {
        SetFreePlaces(0,0);
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