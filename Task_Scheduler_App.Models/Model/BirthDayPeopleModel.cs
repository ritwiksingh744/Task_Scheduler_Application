using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler_App.Models.Model
{
    public class BirthDayPeopleModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int TaskType { get; set; }
        public int BirthMonth { get; set; }
        public int BirthDay { get; set; }
        public string? Email { get; set; }
    }
}
