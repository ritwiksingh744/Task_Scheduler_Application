using Dapper;
using Task_Scheduler_App.Application.Repository.Interface;
using Task_Scheduler_App.Application.Services;
using Task_Scheduler_App.Infrastructure.QuartzService;
using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_App.Infrastructure.Services
{
    public class TaskSchedulerServices : ITaskSchedulerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly InitializeJob _initializeJob;

        public TaskSchedulerServices(IUnitOfWork unitOfWork, InitializeJob initializeJob)
        {
            _unitOfWork = unitOfWork;
            _initializeJob = initializeJob;
        }

        public async Task<int> AddJobDetails(TaskDetails taskDetails)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@JobName", taskDetails.JobName);
            parameters.Add("@Endpoint", taskDetails.Endpoint);
            parameters.Add("@InputJsonParameter", taskDetails.InputJsonParameter);
            parameters.Add("@JobOwnerName", taskDetails.JobOwnerName);
            parameters.Add("@JobOwnerEmail", taskDetails.JobOwnerEmail);
            parameters.Add("@JobFrequencyType", taskDetails.JobFrequencyType);
            parameters.Add("@FrequencyValue", taskDetails.FrequencyValue);
            parameters.Add("@StartDateTime", taskDetails.StartDateTime);

            var res = await _unitOfWork.Dapper.QueryAsync<int>("AddTaskDetails", parameters, System.Data.CommandType.StoredProcedure);
            if (res.First() > 0)
            {
                taskDetails.JobId = res.First();
                await _initializeJob.RunJobs(taskDetails);
            }
            return res.First();
        }

        public async Task<List<TaskDetails>> GetTaskDetailsList()
        {
            var taskList = await _unitOfWork.Dapper.QueryAsync<TaskDetails>("GetTaskDetailList", new DynamicParameters(), System.Data.CommandType.StoredProcedure);
            return taskList;
        }

        public async Task<bool> RemoveJob(int jobId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@JobId", jobId);
            var res = await _unitOfWork.Dapper.QueryAsync<TaskDetails>("RemoveJobById", parameters, System.Data.CommandType.StoredProcedure);
            if(res.First() != null)
            {
                await _initializeJob.RemoveJob(res.First().JobId.ToString(), res.First().JobName);
                return true;
            }
            return false;
        }
    }
}