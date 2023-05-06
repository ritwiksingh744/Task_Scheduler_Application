function Birthday() {
    function SaveBirthdayDetails() {
        var obj = new Object();
        obj.FirstName = $("#firstName").val();
        obj.LastName = $("#lastName").val();
        var selectedDate = $("#birthDayMonth").val();
        var date = new Date(selectedDate);
        obj.BirthMonth = date.getMonth()+1;
        obj.BirthDay = date.getDate();
        obj.Email = $("#email").val();

        //Post details
        $.ajax({
            type: 'POST',
            url: '/BirthDayPeople/AddPeople',
            data: obj,
            success: function (response) {
                //RedirectToList();
                console.log("response: ", response);
            },
            error: function (err) {
                console.log("Error: ", err.Message);
            }
        });
    }

    function onInit() {
    }

    return {
        onInit: onInit,
        SaveBirthdayDetails: SaveBirthdayDetails
    }
}