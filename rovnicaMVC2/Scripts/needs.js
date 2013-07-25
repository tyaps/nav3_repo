$(document).ready(



    function() {

        //Переключение вида Пользователь штаба или простой пользователь
        $("#user_is_admin").bind("click", function() {

            if ($(this).prop("checked")) {
                $("#hq_need .btn_info, #hq_need .btn_update, #hq_need .details>table").show();

            }
            else {
                $("#hq_need .btn_info, #hq_need .btn_update, #hq_need .details>table").hide();
            }

        });

        $('#participate_window_date').datepicker();

        $('#participate_window_time').timepicker();

        //        //Подключение timepicker к Я-пойду
        //        $('#participate_window_time').timepicker({
        //            // Options
        //            timeSeparator: ':',           // The character to use to separate hours and minutes. (default: ':')
        //            showLeadingZero: true,        // Define whether or not to show a leading zero for hours < 10.
        //            //(default: true)
        //            showMinutesLeadingZero: true, // Define whether or not to show a leading zero for minutes < 10.
        //            // (default: true)
        //            showPeriod: false,            // Define whether or not to show AM/PM with selected time. (default: false)
        //            showPeriodLabels: true,       // Define if the AM/PM labels on the left are displayed. (default: true)
        //            periodSeparator: ' ',         // The character to use to separate the time from the time period.
        //            altField: '#alternate_input', // Define an alternate input to parse selected time to
        //            defaultTime: '12:34',         // Used as default time when input field is empty or for inline timePicker
        //            // (set to 'now' for the current time, '' for no highlighted time,
        //            // default value: now)

        //            // trigger options
        //            showOn: 'focus',              // Define when the timepicker is shown.
        //            // 'focus': when the input gets focus, 'button' when the button trigger element is clicked,
        //            // 'both': when the input gets focus and when the button is clicked.
        //            button: null,                 // jQuery selector that acts as button trigger. ex: '#trigger_button'

        //            // Localization
        //            hourText: 'Hour',             // Define the locale text for "Hours"
        //            minuteText: 'Minute',         // Define the locale text for "Minute"
        //            amPmText: ['AM', 'PM'],       // Define the locale text for periods

        //            // Position
        //            myPosition: 'left top',       // Corner of the dialog to position, used with the jQuery UI Position utility if present.
        //            atPosition: 'left bottom',    // Corner of the input to position

        //            // Events
        //            //beforeShow: beforeShowCallback, // Callback function executed before the timepicker is rendered and displayed.
        //            //onSelect: onSelectCallback,   // Define a callback function when an hour / minutes is selected.
        //            //onClose: onCloseCallback,     // Define a callback function when the timepicker is closed.
        //            //onHourShow: onHourShow,       // Define a callback to enable / disable certain hours. ex: function onHourShow(hour)
        //            //onMinuteShow: onMinuteShow,   // Define a callback to enable / disable certain minutes. ex: function onMinuteShow(hour, minute)

        //            // custom hours and minutes
        //            hours: {
        //                starts: 0,                // First displayed hour
        //                ends: 23                  // Last displayed hour
        //            },
        //            minutes: {
        //                starts: 0,                // First displayed minute
        //                ends: 55,                 // Last displayed minute
        //                interval: 5               // Interval of displayed minutes
        //            },
        //            rows: 4,                      // Number of rows for the input tables, minimum 2, makes more sense if you use multiple of 2
        //            showHours: true,              // Define if the hours section is displayed or not. Set to false to get a minute only dialog
        //            showMinutes: true,            // Define if the minutes section is displayed or not. Set to false to get an hour only dialog

        //            // buttons
        //            showCloseButton: false,       // shows an OK button to confirm the edit
        //            closeButtonText: 'Done',      // Text for the confirmation button (ok button)
        //            showNowButton: false,         // Shows the 'now' button
        //            nowButtonText: 'Now',         // Text for the now button
        //            showDeselectButton: false,    // Shows the deselect time button
        //            deselectButtonText: 'Deselect' // Text for the deselect button

        //        });


        //Кнопка "Я пойду"
        $("#hq_need .btn_participate").bind("click", function() {

            var id = $(this).closest(".item").attr("id").slice(1); //получить id элемента


            $("#participate_window #participate_window_amount").val("");
            $("#participate_window #participate_window_date").val("");
            $("#participate_window #participate_window_time").val("");
            $("#participate_window #participate_window_description").val("");


            $("#participate_window .id").val(id);

            var buttons_container = $(this).closest(".buttons");

            $("#participate_window").prependTo(buttons_container).show();



        });
        //Я пойду - отмена
        $("#participate_window #participate_window_btn_cancel").bind("click", function() { $("#participate_window").appendTo($("body")).hide(); });

        //Я пойду - сохранить
        $("#participate_window #participate_window_btn_save").bind("click", function() {

            //Сохранить
            customAlert("Спасибо за участие!", 200, 100);
            $("#participate_window").appendTo($("body")).hide();
        });


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Кнопка "Инфо" (информация по участникам, которые готовы помочь
        $("#hq_need .btn_info").bind("click", function() {

            var id = $(this).closest(".item").attr("id").slice(1); //получить id элемента

            //запрос к бд



            //            $("#participate_window .id").val(id);

            var buttons_container = $(this).closest(".buttons");
            $("#expected_people_window").prependTo(buttons_container).show();



        });
        //"Инфо" - закрыть окошко
        $("#expected_people_window #expected_people_window_btn_close").bind("click", function() { $("#expected_people_window").appendTo($("body")).hide(); });


        //Подтвердить, что необходимую вещь принесли
        $("#expected_people_window .btn_approve").bind("click", function() {


            var id = $(this).closest("tr").attr("id").slice(1); //получить id элемента

            $(this).closest("tr").find(".btn_approve, .btn_discard").hide();
            $(this).closest("tr").find(".btn_back").removeClass("invisible");
            $(this).closest("tr").addClass("complete");


            if ($(this).closest("tr").next().hasClass("description"))
                $(this).closest("tr").next().addClass("complete");


        });

        //Отменить, что необходимую вещь принесли (отметить, что не принесут, но не вычеркивать)
        $("#expected_people_window .btn_discard").bind("click", function() {


            var id = $(this).closest("tr").attr("id").slice(1); //получить id элемента

            $(this).closest("tr").find(".btn_approve, .btn_discard").hide();
            $(this).closest("tr").find(".btn_back").removeClass("invisible");
            $(this).closest("tr").addClass("incomplete");

            if ($(this).closest("tr").next().hasClass("description"))
                $(this).closest("tr").next().addClass("incomplete");


        });

        //Отменить подтверждение, что необходимую вещь принесли
        $("#expected_people_window .btn_back").bind("click", function() {
            var id = $(this).closest("tr").attr("id").slice(1); //получить id элемента

            $(this).closest("tr").find(".btn_approve, .btn_discard").show();
            $(this).closest("tr").find(".btn_back").addClass("invisible");
            $(this).closest("tr").removeClass("complete").removeClass("incomplete");

            if ($(this).closest("tr").next().hasClass("description"))
                $(this).closest("tr").next().removeClass("complete").removeClass("incomplete");


        });


        /**************************************** Изменить данные по запросу вещей в штаб ********************/

        //Кнопка "Изменить" (информация по запросу вещей в штаб)
        $("#hq_need .btn_update").bind("click", function() {

            var id = $(this).closest(".item").attr("id").slice(1); //получить id элемента

            //запрос к бд



            //            $("#participate_window .id").val(id);

            var buttons_container = $(this).closest(".buttons");
            $("#hq_need_item_edit_form").prependTo(buttons_container).show();



        });
        //"Инфо" - закрыть окошко
        $("#hq_need_item_edit_form #hq_need_item_edit_form_btn_cancel").bind("click", function() { $("#hq_need_item_edit_form").appendTo($("body")).hide(); });
        $("#hq_need_item_edit_form #hq_need_item_edit_form_btn_save").bind("click", function() { $("#hq_need_item_edit_form").appendTo($("body")).hide(); });



    }
)



//Рисовать красивое всплывающее окошко вместо alert()
function customAlert(message, width, height) {
    $("body").append("<div class='customAlert' style='width:" + width + "px; height:" + height + "px; left:-" + width * 0.5 + "px;'><p>" + message + "</p><a class='button' href='javascript:void(0);' onclick='javascript:$(this).closest(\".customAlert\").remove();'>Закрыть</a></div>");
}
    