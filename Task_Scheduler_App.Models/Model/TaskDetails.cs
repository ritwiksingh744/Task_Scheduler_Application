using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler_App.Models.Model
{
    public class TaskDetails
    {
        public int JobId { get; set; }
        public string? JobName { get; set; }
        public string? Endpoint { get; set; }
        public string? InputJsonParameter { get; set; }
        public string? JobOwnerName { get; set; }
        public string? JobOwnerEmail { get; set; }
        public string? JobFrequencyType { get; set; }
        public string? FrequencyValue { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public DateTime StartDateTime { get; set; }
        public int TaskType { get; set; }
    }
}
