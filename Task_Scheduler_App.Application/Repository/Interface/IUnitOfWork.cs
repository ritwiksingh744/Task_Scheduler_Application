using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler_App.Application.Repository.Interface
{
    public interface IUnitOfWork
    {
        public IDapper Dapper { get; set; }
    }
}
