using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_Scheduler_App.Application.Services;
using Task_Scheduler_App.Models.Model;
using Task_Scheduler_Application.Models;

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
        public async Task<IActionResult> Index()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.TaskList =await _taskSchedulerServices.GetTaskDetailsList();
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}