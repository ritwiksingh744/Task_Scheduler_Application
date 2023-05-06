namespace Task_Scheduler_App.Application.Repository.Interface
{
    public interface IUnitOfWork
    {
        public IDapper Dapper { get; set; }
    }
}