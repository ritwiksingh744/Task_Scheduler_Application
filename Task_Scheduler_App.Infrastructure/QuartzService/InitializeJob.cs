using Newtonsoft.Json;
using Quartz;
using Task_Scheduler_App.Infrastructure.MailHelper;
using Task_Scheduler_App.Infrastructure.QuartzService.Jobs;
using Task_Scheduler_App.Models.Model;

namespace Task_Scheduler_App.Infrastructure.QuartzService
{
    public class InitializeJob
    {
        private readonly ISchedulerFactory _scheduleFactory;
        private IScheduler _scheduler;
        private readonly EmaiHelper _emailHelper;

        public InitializeJob(ISchedulerFactory factory, EmaiHelper emaiHelper)
        {
            _scheduleFactory = factory;
            _emailHelper = emaiHelper;
        }

        public async Task RunJobs(TaskDetails myjob)
        {
            var job = CreateJob(myjob);
            var trigger = CreateTrigger(myjob);
            _scheduler = await _scheduleFactory.GetScheduler();
            await _scheduler.ScheduleJob(job, trigger);
        }

        private IJobDetail CreateJob(TaskDetails myjob)
        {
            var jsonString = JsonConvert.SerializeObject(myjob);
            JobKey job1key = new JobKey(myjob.JobId.ToString(), myjob.JobName);
            return JobBuilder.Create<JobReminders>()
                .WithIdentity(job1key)
                .UsingJobData("JobData", jsonString)
                .WithDescription($"A job is Scheduled at:{DateTime.Now} with JobId: {myjob.JobId}, JobName: {myjob.JobName}, Job Frequecy Typ: {myjob.JobFrequencyType} which starts on {myjob.StartDateTime}.")
                .Build();
        }

        private ITrigger CreateTrigger(TaskDetails myjob)
        {
            var jsonString = JsonConvert.SerializeObject(myjob);
            TriggerKey triggerKey = new TriggerKey(myjob.JobId.ToString(), myjob.JobName);

            if (myjob.JobFrequencyType == "TriggerNow")
            {
                return TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .UsingJobData("JobData", jsonString)
                .WithSimpleSchedule()
                .WithDescription($"A job is Scheduled at:{DateTime.Now} with JobId: {myjob.JobId}, JobName: {myjob.JobName}, Job Frequecy Typ: {myjob.JobFrequencyType} which starts on {myjob.StartDateTime}.")
                .StartNow()
                .Build();
            }
            if (myjob.JobFrequencyType == "NoRepeat")
            {
                return TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .UsingJobData("JobData", jsonString)
                .WithSimpleSchedule()
                .WithDescription($"A job is Scheduled at:{DateTime.Now} with JobId: {myjob.JobId}, JobName: {myjob.JobName}, Job Frequecy Typ: {myjob.JobFrequencyType} which starts on {myjob.StartDateTime}.")
                .StartAt(myjob.StartDateTime)
                .Build();
            }
            return TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .UsingJobData("JobData", jsonString)
                .WithCronSchedule(CreateCronExpression(myjob))
                .WithDescription($"A job is Scheduled at:{DateTime.Now} with JobId: {myjob.JobId}, JobName: {myjob.JobName}, Job Frequecy Typ: {myjob.JobFrequencyType} which starts on {myjob.StartDateTime}.")
                .Build();
        }

        private string CreateCronExpression(TaskDetails data)
        {
            var year = data?.StartDateTime.Year ?? 0;
            var month = data?.StartDateTime.Month ?? 0;
            var day = data?.StartDateTime.Day ?? 0;
            var hour = data?.StartDateTime.Hour ?? 0;
            var minute = data?.StartDateTime.Minute ?? 0;

            switch (data?.JobFrequencyType)
            {
                case "Daily":
                    return $"0 {minute} {hour} 0 0 ? 0";

                case "Minutely":
                    return $"0 {minute}/{data.FrequencyValue} {hour} 0 0 ? 0";

                case "Houlry":
                    return $"0 {minute} {hour}/{data.FrequencyValue} {day} ? 0";

                case "Weekly":
                    return $"0 {minute} {hour} ? 0 {data.FrequencyValue} 0";

                default:
                    return $"0 {minute} {hour} {day} {month} ? {year}";
            }
        }
    }
}