using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task_Scheduler_App.Models.Model;
using static Quartz.Logging.OperationName;
using Task_Scheduler_App.Models.Helper;

namespace Task_Scheduler_App.Infrastructure.QuartzService.Jobs
{
    public class JobReminders : IJob
    {
        public JobReminders()
        {
        }
        public Task Execute(IJobExecutionContext context)
        {
            TaskDetails jobData = null;
            var jobDataMap = context.MergedJobDataMap;
            if (jobDataMap.GetString("JobData") != null)
            {
                jobData = JsonConvert.DeserializeObject<TaskDetails>(jobDataMap.GetString("JobData"));
            }
            if (jobData != null)
            {
                switch ((TaskHelper)jobData.TaskType)
                {
                    case TaskHelper.RunApi:
                        RunApi(jobData);

                        //testing schedule trigger
                        TestLog(jobData.JobId.ToString(), $"A job is Scheduled with JobId: {jobData.JobId}, JobName: {jobData.JobName}, Job Frequecy Typ: {jobData.JobFrequencyType} which starts on {(jobData.StartDateTime !=null ?jobData.StartDateTime:DateTime.Now)}.");
                        break;
                    case TaskHelper.WishBirthday:
                        WishBirthdayByMail(jobData);
                        break;
                }
                
            }
            return Task.CompletedTask;
        }

        private void WishBirthdayByMail(TaskDetails jobData)
        {
            throw new NotImplementedException();
        }

        private async Task RunApi(TaskDetails jobData)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(jobData.Endpoint);
                    //client.DefaultRequestHeaders.Add("Authorization", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(jobData.InputJsonParameter, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(client.BaseAddress, content);
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        //todo
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void TestLog(string fileName, string message)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TestLogScheduler");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, $"{fileName}.txt");
            using FileStream stream = new FileStream(path, FileMode.Create);
            using TextWriter textWriter = new StreamWriter(stream);
            textWriter.WriteLine($"{message} {DateTime.Now}");
        }
    }
}
