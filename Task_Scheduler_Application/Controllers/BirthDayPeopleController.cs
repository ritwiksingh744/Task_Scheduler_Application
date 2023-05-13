using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Task_Scheduler_App.Application.Services;
using Task_Scheduler_App.Models.Common;
using Task_Scheduler_App.Models.Helper;
using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_Application.Controllers
{
    public class BirthDayPeopleController : Controller
    {
        private readonly IPeopleBirthDayService _peopleBirthDayService;

        public BirthDayPeopleController(IPeopleBirthDayService peopleBirthDayService)
        {
            _peopleBirthDayService = peopleBirthDayService;
        }

        [HttpGet]
        public async Task<IActionResult> ShowBirthdayList()
        {
            ViewModel vm = new ViewModel();
            vm.BirthDayList = await _peopleBirthDayService.GetBirthdayList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddBirthDayPeople()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPeople(BirthDayPeopleModel model)
        {
            Response res = new Response();
            if (model != null)
                model.TaskType = (int)TaskHelper.WishBirthday;
            var result = await _peopleBirthDayService.AddPeopleBirthdayDetails(model);
            if (result > 0)
            {
                res.Success = true;
                res.Id = result;
                return Json(res);
            }
            else
            {
                res.Success = false;
                res.Message = "failed to add";
                return Json(res);
            }
        }
    }
}