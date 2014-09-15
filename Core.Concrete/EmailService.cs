using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class EmailService: IEmailService
    {
        public async Task SendErrorEmailAsync(Exception ex, string message)
        {
            using (var smtp = new SmtpClient())
            {
                var emailTo = ConfigurationManager.AppSettings["ErrorEmailRecipient"];
                if (!string.IsNullOrWhiteSpace(emailTo))
                {
                    var emailFrom = ConfigurationManager.AppSettings["EmailFrom"];
                    if (string.IsNullOrWhiteSpace(emailFrom))
                        emailFrom = "noreply@test.com";

                    using (var mail = new MailMessage())
                    {
                        mail.To.Add(emailTo);
                        mail.From = new MailAddress(emailFrom);
                        mail.Body = FormatErrorMessage(ex, message);
                        var hostedSystem = ConfigurationManager.AppSettings["EmailSubject"];
                        if (!string.IsNullOrWhiteSpace(hostedSystem))
                            mail.Subject = hostedSystem + " - ";
                        mail.Subject += ex.Message;

                        var host = ConfigurationManager.AppSettings["EmailServer"];
                        if (!string.IsNullOrWhiteSpace(host))
                            smtp.Host = host;
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
        }

        private static string FormatErrorMessage(Exception ex, string message)
        {
            var errorMessage = new StringBuilder();
            //errorMessage.AppendLine("\r\nException Message: " + ex.Message);
            if (ex.InnerException != null)
            {
                errorMessage.AppendLine("Inner Exception: ");
                errorMessage.AppendLine(ex.InnerException.Message);

                errorMessage.AppendLine("\r\nInner Inner Exception: ");
                errorMessage.AppendLine(ex.InnerException.InnerException.Message);
            }
            errorMessage.AppendLine("Stack Trace: ");
            errorMessage.AppendLine(ex.StackTrace);
            return string.Format("{0}{1}{2}", message, Environment.NewLine, errorMessage);
        }
    }
}
