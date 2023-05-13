namespace Task_Scheduler_App.Models.Model
{
    public class ViewModel
    {
        public IEnumerable<TaskDetails>? TaskList { get; set; }
        public IEnumerable<BirthDayPeopleModel>? BirthDayList { get; set; }
    }
}