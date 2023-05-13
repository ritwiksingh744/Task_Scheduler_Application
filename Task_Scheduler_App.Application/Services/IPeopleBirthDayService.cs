using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_App.Application.Services
{
    public interface IPeopleBirthDayService
    {
        Task<int> AddPeopleBirthdayDetails(BirthDayPeopleModel model);
        Task<List<BirthDayPeopleModel>> GetBirthdayList();
    }
}