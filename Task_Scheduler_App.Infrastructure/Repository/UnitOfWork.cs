using Task_Scheduler_App.Application.Repository.Interface;

namespace Task_Scheduler_App.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDapper dapper)
        {
            Dapper = dapper;
        }

        public IDapper Dapper { get; set; }
    }
}