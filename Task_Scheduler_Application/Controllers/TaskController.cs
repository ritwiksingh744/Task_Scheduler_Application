using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Task_Scheduler_App.Application.Services;
using Task_Scheduler_App.Models.Common;
using Task_Scheduler_App.Models.Helper;
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
            viewModel.TaskList = await _taskSchedulerServices.GetTaskDetailsList();
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
            if (model != null)
                model.TaskType = (int)TaskHelper.RunApi;
            if (model != null && model.JobFrequencyType == "TriggerNow")
                model.StartDateTime = DateTime.Now;
            var res = await _taskSchedulerServices.AddJobDetails(model);
            dynamic response = new ExpandoObject();
            if (res > 0)
                response.Id = res;
            else
                response.Message = "Failed to add";

            return Json(response);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTask(DeleteJobRequest request)
        {
            var response = new BaseResponse();
            var res = await _taskSchedulerServices.RemoveJob(request.JobId);
            if (res)
            {
                response.Success = true;
                response.Message = "Successfully removed task.";
                return Json(response);
            }
            else
            {
                response.Success = false;
                response.ErrorMessage = "Failed to delete task. Please try again.";
                return Json(response);
            }
        }
    }
}