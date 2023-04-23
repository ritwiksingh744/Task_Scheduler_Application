using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
