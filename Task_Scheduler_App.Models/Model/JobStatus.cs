namespace Task_Scheduler_App.Models.Model
{
    public class JobStatus
    {
        public int JobId { get; set; }
        public string? JobName { get; set; }
        public Status Status { get; set; }
        public string? StatusDescription { get; set; }
        public string? InputJsonParameter { get; set; }
        public string? Endpoint { get; set; }
    }

    public enum Status
    {
        Completed = 1,
        Failed = 2,
        Waiting = 3
    }
}