function ContentLoader() {
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
    function RedirectToHome() {
        $.ajax({
            type: 'GET',
            url: '/Home/Index',
            success: function (response) {
                $(document.body).html(response);
            },
            error: function (err) {
                data = `<p>Error:${err.Message}</p>`;
            }
        })
    }
    function RedirectToAddJob() {
        $.ajax({
            type: 'GET',
            url: '/Task/AddJobs',
            success: function (response) {
                $(document.body).html(response);
            },
            error: function (err) {
                data = `<p>Error:${err.Message}</p>`;
            }
        })
    }

    function RedirectToAddBirthDayPeople() {
        $.ajax({
            type: 'GET',
            url: '/BirthDayPeople/AddBirthDayPeople',
            success: function (response) {
                $(document.body).html(response);
            },
            error: function (err) {
                data = `<p>Error:${err.Message}</p>`;
            }
        })
    }

    function onInit() {
    }

    return {
        onInit: onInit,
        RedirectToList: RedirectToList,
        RedirectToHome: RedirectToHome,
        RedirectToAddJob: RedirectToAddJob,
        RedirectToAddBirthDayPeople: RedirectToAddBirthDayPeople
    }
}