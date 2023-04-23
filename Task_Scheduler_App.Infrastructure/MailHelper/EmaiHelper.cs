using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Task_Scheduler_App.Models.Helper;

namespace Task_Scheduler_App.Infrastructure.MailHelper
{
    public class EmaiHelper
    {

        public EmaiHelper()
        {

        }
        public async Task AddMailToBusQueue(List<string> toMails)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(string.Join(",",toMails));
            mail.From = new MailAddress(MailHelperConstants.Sender, MailHelperConstants.EmailHead, System.Text.Encoding.UTF8);
            mail.Subject = MailHelperConstants.BirthdayWish;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "This is Email Body Text";
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
    }
}
