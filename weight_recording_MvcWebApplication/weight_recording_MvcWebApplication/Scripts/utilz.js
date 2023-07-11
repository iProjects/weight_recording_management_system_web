
$(document).ready(function () {

    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var year = now.getFullYear();

    var today = (year) + "-" + (month) + "-" + (day);

    $('#dtpdate').val(today);
    $('#txtweight').val(generate_random_integer());

    //$.fn.datepicker.defaults.format = "dd-MM-yyyy";

    //var datepicker = $.fn.datepicker.noConflict();
    //$.fn.bootstrapDP = datepicker;//give $().bootstrapDP the bootstrap-datepicker functionality

    // $("#dtpdate").bootstrapDP({
    //     dateFormat: 'dd-MM-yyyy'
    // });

    //     $("#dtpdate").datepicker({
    //         format: 'dd-MM-yyyy'
    //     });

    // $("#dtpdate").datepicker("hide", {
    // dateFormat: 'dd-MM-yyyy',
    //     duration: "fast",
    //     showOn: "button"
    // });

    $('input#btn_create_weight').on('click', function (e) {
        e.preventDefault();
        create_weight_ajax();
    });

    $('input#btn_update_weight').on('click', function (e) {
        e.preventDefault();
        update_weight_ajax();
    });

    populate_dates_months_years();

    setInterval(function () {
        showcurrenttime();
    }, 1000);

    $("#progress_bar").hide();

    fetch_weights_ajax();

    log_info_messages("finished load...");
});

function showcurrenttime() {
    try {
        var d = new Date();

        g_arrMonth = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");

        g_arrDay = new Array("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");

        var strmonthname = g_arrMonth[d.getMonth()];
        var strdayname = g_arrDay[d.getDay()];

        var datestring = ("0" + d.getDate()).slice(-2) + "-" + strdayname + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + strmonthname + "-" + d.getFullYear();

        var ampm = d.getHours() >= 12 ? 'pm' : 'am';

        var timestring = ("0" + d.getHours()).slice(-2) + ":" + ("0" + d.getMinutes()).slice(-2) + ":" + ("0" + d.getSeconds()).slice(-2) + " " + ampm;

        $("#lblfooterdate").html(datestring);
        $("#lblfooterdate").attr("title", datestring);
        $("#lblfootertime").html(timestring);
        $("#lblfootertime").attr("title", timestring);

        var strcopyright = "Copyright (c) Daksen. " + d.getFullYear() + " All rights reserved"
        $("#lblcopyright").html(strcopyright);
        $("#lblcopyright").attr("title", strcopyright);

        //$('#lblfooterelapsedtime').text(today);

    } catch (err) {
        log_error_messages(err);
        console.log(err);
    }
}

function populate_dates_months_years() {
    //Reference the DropDownList.
    var cboyears = $("#cboyears");
    var cbomonths = $("#cbomonths");
    var cbodates = $("#cbodates");

    //Determine the Current Year.
    var currentYear = new Date().getFullYear();
    var currentMonth = new Date().getMonth();

    //Loop and add the Year values to DropDownList.
    for (var i = 1950; i <= currentYear; i++) {
        var option = $("<option />");
        option.html(i);
        option.val(i);
        cboyears.append(option);
    }

    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    //Emptying the Month Dropdown to clear our last values
    cbomonths.empty();
    //Appending Current Valid Months
    for (var month = 0; month < monthNames.length; month++) {
        cbomonths.append('<option value="' + (month + 1) + '">' + monthNames[month] + '</option>');
    }

    var daysInSelectedMonth = daysInMonth(cbomonths.val(), cboyears.val());

    for (var i = 1; i <= daysInSelectedMonth; i++) {
        cbodates.append($("<option></option>").attr("value", i).text(("0" + (i)).slice(-2)));
    }

}

function daysInMonth(month, year) {
    return new Date(year, month, 0).getDate();
}

$('#cboyears, #cbomonths').change(function () {

    if ($('#cboyears').val().length > 0 && $('#cbomonths').val().length > 0) {

        $('#cbodates').find('option').remove();

        var daysInSelectedMonth = daysInMonth($('#cbomonths').val(), $('#cboyears').val());

        for (var i = 1; i <= daysInSelectedMonth; i++) {
            $('#cbodates').append($("<option></option>").attr("value", i).text(("0" + (i)).slice(-2)));
        }

    }

});

function create_weight_ajax() {
    try {
        $("#progress_bar").show();
        $("#btn_create_weight").prop("value", "creating");
        log_info_messages("posting data to server...");

        var weight_weight = $('#txtweight').val();
        var weight_date = $('#dtpdate').val();

        var weight_date_arr = weight_date.split("-");
        var year = weight_date_arr[0];
        var month = weight_date_arr[1];
        var day = weight_date_arr[2];

        var formated_date = day + "-" + month + "-" + year;

        var obj_data = {
            "weight_weight": weight_weight,
            "weight_date": formated_date
        };

        var obj_data_str = JSON.stringify(obj_data);
        console.log(obj_data_str);

        $.ajax({
            type: "POST",
            url: "/weight_recording_web_app/create_weight",
            data: obj_data_str,
            dataType: 'json',
            contentType: "application/json",
            success: function (response) {
                $("#btn_create_weight").prop("value", "create");
                log_info_messages(response);

                //                $.each(response, function (e, f) {
                //                    log_info_messages(f);
                //                    console.log("response: " + f);
                //                });
                $('#txtweight').val(generate_random_integer());
                $("#progress_bar").hide();
            },
            error: function (response) {
                $("#btn_create_weight").prop("value", "create");
                console.log(response);
                log_error_messages(response.responseText);
                $('#txtweight').val(generate_random_integer());
                $("#progress_bar").hide();
            }

        });

    } catch (err) {
        log_error_messages(err);
        console.log(err);
    }
}

function update_weight_ajax() {
    try {
        log_info_messages("posting data to server...");

        var weight_weight = $('#txtweight').val();
        var weight_date = $('#dtpdate').val();

        var weight_date_arr = weight_date.split("-");
        var year = weight_date_arr[0];
        var month = weight_date_arr[1];
        var day = weight_date_arr[2];

        var formated_date = day + "-" + month + "-" + year;

        var obj_data = {
            "weight_weight": weight_weight,
            "weight_date": formated_date
        };

        $.ajax({
            type: "POST",
            url: "/tutorials/update_weight_on_post/",
            data: obj_data,
            dataType: 'json',
            headers: {
                "X-Requested-With": "XMLHttpRequest"
            },
            credentials: "same-origin",
            success: function (response) {

                $.each(response, function (e, f) {
                    log_info_messages(f);
                    console.log("response: " + f);
                });
                //$('#txtweight').val(generate_random_integer());
            },
            error: function (response) {
                console.log(response);
                log_error_messages(response.responseText);
                //$('#txtweight').val(generate_random_integer());
            }
        });
    } catch (err) {
        log_error_messages(err);
        console.log(err);
    }
}

function fetch_weights_ajax() {
    try {

        $("#progress_bar").show();
        log_info_messages("fetching data from server...");

        $.ajax({
            type: "GET",
            url: "weights/get_all_active_weights_from_service",
            dataType: 'json',
            contentType: "application/json",
            success: function (response) {

                var trHTML = '';
                var count = 0;

                $.each(response, function (index) {

                    trHTML += "<tr class='table-primary'>";
                    trHTML += "<td>" + response[index].weight_id + "</td>";
                    trHTML += "<td>" + response[index].weight_weight + "</td>";
                    trHTML += "<td>" + response[index].weight_date + "</td>";
                    trHTML += "<td>" + response[index].weight_status + "</td>";
                    trHTML += "<td>" + response[index].created_date + "</td>";
                    trHTML += "<td>" + response[index].weight_app + "</td>";

                    trHTML += "<td> <a type='button' class='btn btn-warning btnedit' data-original-title='edit' onclick='edit( " + response[index].weight_id + ")' >edit<i class='ui-tooltip fa fa-pencil'></i></a>" + "</td>";

                    trHTML += "<td> <a type='button' class='btn btn-danger btndelete' data-original-title='delete' onclick='delete_weight( " + response[index].weight_id + ")' >delete<i class='ui-tooltip fa fa-trash'></i></a>" + "</td>";

                    trHTML += "</tr>";

                    count++;
                });

                log_info_messages("fetched [ " + count + " ] records.");

                $('#table_body').html(trHTML);

                $('#header').html("weights list [ " + count + "  ]");

                log_info_messages("retrieved data from server successfully.");

                $("#progress_bar").hide();

            },
            error: function (response) {

                console.log(response);
                log_error_messages(response.responseText);

                $("#progress_bar").hide();
            }

        });

    } catch (err) {
        log_error_messages(err);
        console.log(err);
    }
}

function delete_weight(weight_id) {
    try {

        $("#progress_bar").show();
        log_info_messages("deleting data from server...");

        console.log(weight_id);

        var weight_obj = {
            weight_id: weight_id
        };

        var str_obj = JSON.stringify(weight_obj);

        $.ajax({
            type: "POST",
            url: "weights/delete_weight_from_service",
            data: str_obj,
            dataType: "json",
            contentType: "application/json",
            success: function (response) {

                log_info_messages(response.responsesuccessmessage);

                log_info_messages("record with id [ " + weight_id + " ] successfully purged.");

                log_info_messages("processed task successfully.");

                fetch_weights_ajax();

                $("#progress_bar").hide();
            },
            error: function (response) {

                console.log(response);
                log_error_messages(response.responseText);

                $("#progress_bar").hide();
            }
        });

    } catch (err) {
        log_error_messages(err);
        console.log(err);
    }
}

function getCookie(name) {
    var cookieValue = null;
    if (document.cookie && document.cookie !== "") {
        var cookies = document.cookie.split(";");
        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i].trim();
            // Does this cookie string begin with the name we want?
            if (cookie.substring(0, name.length + 1) === (name + "=")) {
                cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                break;
            }
        }
    }
    return cookieValue;
}

function log_info_messages(message) {
    $("#div_messages")
            .fadeIn(2000, function () {

                var d = new Date();
                var datestring = ("0" + d.getDate()).slice(-2) + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + d.getFullYear();
                var timestring = ("0" + d.getHours()).slice(-2) + ":" + ("0" + d.getMinutes()).slice(-2) + ":" + ("0" + d.getSeconds()).slice(-2);
                var today = datestring + " " + timestring;

                var msg = today + " : " + message;

                $("#div_messages")
                        .prepend('<div class="div_info_message_item">' + msg + '</div>')
            });
}

function log_error_messages(message) {
    $("#div_messages")
            .fadeIn(2000, function () {

                var d = new Date();
                var datestring = ("0" + d.getDate()).slice(-2) + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + d.getFullYear();
                var timestring = ("0" + d.getHours()).slice(-2) + ":" + ("0" + d.getMinutes()).slice(-2) + ":" + ("0" + d.getSeconds()).slice(-2);
                var today = datestring + " " + timestring;

                var msg = today + " : " + message;

                $("#div_messages")
                        .prepend('<div class="div_error_message_item">' + msg + '</div>')
            });
}

function generate_random_integer() {
    var d = new Date();
    var milliseconds = d.getMilliseconds().toString();
    var virtual_weight = milliseconds.substring(0, 2);
    return virtual_weight;
}

myInterval = setInterval(elapsed_time, 1000);

function myStopFunction() {
    clearInterval(myInterval);
}

var _initial_date = new Date();

function elapsed_time() {

    var _today = _initial_date;
    var _current = new Date();
    var _days = parseInt((_current - _today) / (1000 * 60 * 60 * 24));
    var _hours = parseInt(Math.abs(_current - _today) / (1000 * 60 * 60) % 24);
    var _minutes = parseInt(Math.abs(_current.getTime() - _today.getTime()) / (1000 * 60) % 60);
    var _seconds = parseInt(Math.abs(_current.getTime() - _today.getTime()) / (1000) % 60);

    var _elapsed_time = _days + ':' + _hours + ':' + _minutes + ':' + _seconds;

    $('#lblfooterelapsedtime').text(_elapsed_time);


}