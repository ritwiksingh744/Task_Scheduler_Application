using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_App.Application.Services
{
    public interface ITaskSchedulerServices
    {
        Task<List<TaskDetails>> GetTaskDetailsList();

        Task<int> AddJobDetails(TaskDetails taskDetails);
    }
}