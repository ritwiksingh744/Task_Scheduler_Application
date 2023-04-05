using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;
using Task_Scheduler_App.Application.Services;
using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_Application.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskSchedulerServices _taskSchedulerServices;

        public TaskController(ITaskSchedulerServices taskSchedulerServices)
        {
            _taskSchedulerServices = taskSchedulerServices;
        }

        [HttpGet]
        public async Task<IActionResult> ShowJobList()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.TaskList =await _taskSchedulerServices.GetTaskDetailsList();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddJobs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJobDetails(TaskDetails model)
        {
            var res = await _taskSchedulerServices.AddJobDetails(model);
            dynamic response = new ExpandoObject();
            if (res > 0)
                response.Id = res;
            else
                response.Message = "Failed to add";

            return Json(response);
        }

    }
}