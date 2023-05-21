function ShowJobs() {

    function DeleteJob(jobId) {
        var obj = new Object();
        obj.JobId = jobId;
        console.log(obj)
        debugger
        $.ajax({
            type: 'DELETE',
            url: '/Task/RemoveTask',
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

    }

    return {
        onInit: onInit,
        DeleteJob: DeleteJob,
        RedirectToList: DeleteJob
    }
}