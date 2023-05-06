using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_App.Application.Services
{
    public interface IPeopleBirthDayService
    {
        Task<int> AddPeopleBirthdayDetails(BirthDayPeopleModel model);
    }
}
