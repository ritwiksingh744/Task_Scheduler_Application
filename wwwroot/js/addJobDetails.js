function Scheduer() {

    function SelectTriggerType() {
        var triggerSelection = $(".custom-frequency");
        var trigger1 = $("#trigger1");
        var trigger2 = $("#trigger2");
        if (trigger1.is(":checked")) {
            triggerSelection.addClass("d-none");
        }
        if (trigger2.is(":checked")) {
            triggerSelection.removeClass("d-none");
        }
    }

    function FrequencySelection() {
        var frequencySelect = $(".frequency-select");
        frequencySelect.addClass("d-none");
        var freqType = $("#jobFrequencyType").val();
        switch (freqType) {
            case "Minutely":
                $("#selectMinute").removeClass("d-none");
                break;
            case "Hourly":
                $("#selectHour").removeClass("d-none");
                break;
            case "Weekly":
                $("#selectDay").removeClass("d-none");
                break;
        }
    }

    function SaveJobDetails() {
        var obj = new Object();
        obj.JobName = $("#jobName").val();
        obj.Endpoint = $("#endpoint").val();
        obj.InputJsonParameter = $("#inputJsonParameter").val();
        obj.JobOwnerName = $("#jobOwnerName").val();
        obj.JobOwnerEmail = $("#jobOwnerEmail").val();

        if ($("#trigger1").is(":checked")) {
            obj.JobFrequencyType = "TriggerNow";
            var date = new Date();
            obj.StartDateTime = `${date.getMonth()}/${date.getDay()}/${date.getFullYear()}`;
        }
        if ($("#trigger2").is(":checked")) {
            let date = $("#selectDate").val();
            let time = $("#selectTime").val();
            obj.StartDateTime = `${date} ${time}`;

            obj.JobFrequencyType = $("#jobFrequencyType").val();
            switch (obj.JobFrequencyType) {
                case "Minutely":
                    obj.FrequencyValue = $("#frequencyValueMin").val();
                    break;
                case "Hourly":
                    obj.FrequencyValue = $("#frequencyValueHour").val();
                    break;
                case "Weekly":
                    let arrStr = [];
                    if ($("#Sunday").is(":checked")) {
                        arrStr.push("SUN");
                    }
                    if ($("#Monday").is(":checked")) {
                        arrStr.push("MON");
                    }
                    if ($("#Tuesday").is(":checked")) {
                        arrStr.push("TUE");
                    }
                    if ($("#Wednesday").is(":checked")) {
                        arrStr.push("WED");
                    }
                    if ($("#Thrusday").is(":checked")) {
                        arrStr.push("THU");
                    }
                    if ($("#Friday").is(":checked")) {
                        arrStr.push("FRI");
                    }
                    if ($("#Saturday").is(":checked")) {
                        arrStr.push("SAT");
                    }
                    let newStr = "";
                    newStr = arrStr.join(',');
                    obj.FrequencyValue = newStr;
                    break;
            }
        }

        //Post details
        $.ajax({
            type: 'POST',
            url: '/Task/AddJobDetails',
            data: obj,
            success: function (response) {
                RedirectToList();
                console.log("response: ", response);
            },
            error: function (err) {
                console.log("Error: ", err.Message);
            }
        });

    }

    function RedirectToList() {
        $.ajax({
            type: 'GET',
            url: '/Task/ShowJobList',
            success: function (response) {
                $(document.body).html(response);
            },
            error: function (err) {
                data = `<p>Error:${err.Message}</p>`;
            }
        })
    }

    function onInit() {
        SelectTriggerType();
    }
    return {
        onInit: onInit,
        SelectTriggerType: SelectTriggerType,
        FrequencySelection: FrequencySelection,
        SaveJobDetails: SaveJobDetails,
        RedirectToList: RedirectToList
    }
}