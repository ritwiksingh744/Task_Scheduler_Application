using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler_App.Models.Model
{
    public class ViewModel
    {
        public IEnumerable<TaskDetails>? TaskList { get; set; }
    }
}
