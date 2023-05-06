using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Task_Scheduler_App.Application.Services;
using Task_Scheduler_App.Models.Helper;
using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_Application.Controllers
{
    public class BirthDayPeople : Controller
    {
        private readonly IPeopleBirthDayService _peopleBirthDayService;

        public BirthDayPeople(IPeopleBirthDayService peopleBirthDayService)
        {
            _peopleBirthDayService = peopleBirthDayService;
        }

        [HttpGet]
        public async Task<IActionResult> AddBirthDayPeople()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPeople(BirthDayPeopleModel model)
        {
            dynamic obj = new ExpandoObject();
            if (model != null)
                model.TaskType = (int)TaskHelper.WishBirthday;
            var result = await _peopleBirthDayService.AddPeopleBirthdayDetails(model);
            if (result > 0)
            {
                obj.Success = true;
                obj.Id = result;
                return Json(obj);
            }
            else
            {
                obj.Success = false;
                obj.Message = "failed to add";
                return Json(obj);
            }
        }
    }
}