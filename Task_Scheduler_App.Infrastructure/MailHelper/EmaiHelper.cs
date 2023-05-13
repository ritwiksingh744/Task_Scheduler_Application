using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using Task_Scheduler_App.Models.Helper;


namespace Task_Scheduler_App.Infrastructure.MailHelper
{
    public class EmaiHelper
    {
        private readonly IHostingEnvironment _hostEnv;
        public EmaiHelper()
        {

        }
        public EmaiHelper(IHostingEnvironment hostEnv)
        {
            _hostEnv = hostEnv;
        }

        public async Task AddMailToBusQueue(List<string> toMails, string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(string.Join(",", toMails));
            mail.From = new MailAddress(MailHelperConstants.Support, MailHelperConstants.EmailHead, System.Text.Encoding.UTF8);
            mail.Subject = MailHelperConstants.BirthdayWish;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Credentials = new System.Net.NetworkCredential(MailHelperConstants.Sender, MailHelperConstants.AppPassword);
                    client.Port = MailHelperConstants.GmailPort;
                    client.Host = MailHelperConstants.GmailHost;
                    client.EnableSsl = true;
                    await client.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CreateBody(string name, string fileName)
        {
            string result = string.Empty;
            string path = "./wwwroot"+ fileName;

            using(StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }
            result = result.Replace("{{Name}}", name);
            return result;
        }
        
    }
}