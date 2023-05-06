using Dapper;
using Task_Scheduler_App.Application.Repository.Interface;
using Task_Scheduler_App.Application.Services;
using Task_Scheduler_App.Infrastructure.QuartzService;
using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_App.Infrastructure.Services
{
    public class PeopleBirthDayService : IPeopleBirthDayService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly InitializeJob _initializeJob;

        public PeopleBirthDayService(IUnitOfWork unitOfWork, InitializeJob initializeJob)
        {
            _unitOfWork = unitOfWork;
            _initializeJob = initializeJob;
        }

        public async Task<int> AddPeopleBirthdayDetails(BirthDayPeopleModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", model.FirstName);
            parameters.Add("@LastName", model.LastName);
            parameters.Add("@TaskType", model.TaskType);
            parameters.Add("@BirthMonth", model.BirthMonth);
            parameters.Add("@BirthDay", model.BirthDay);
            parameters.Add("@Email", model.Email);

            var result =await _unitOfWork.Dapper.QueryAsync<int>("AddBirthdayPeopleDetails", parameters, System.Data.CommandType.StoredProcedure);
            //if(result.First() > 0)
            //{
            //    var taskDetail = new TaskDetails()
            //    {
            //        JobId = result.First(),
            //        JobName = $"wishBirthDay_{result.First()}",
            //        TaskType = model.TaskType,
            //        JobOwnerEmail = model.Email,
            //        JobOwnerName = $"{model.FirstName} {model.LastName}",
            //        StartDateTime = DateTime.Parse($"{model.BirthDay}/{model.BirthMonth}/{DateTime.Now.Year}")
            //    };
            //    await _initializeJob.RunJobs(taskDetail);
            //}
            return result.First();
        }
    }
}
