using System.Net;
using System.Net.Mail;

namespace AvaTradeApp.Domain.Statics
{
    public static class EmailHelper
    {
        public static async Task SendEmailAsync(string fromEmail, string fromPassword, string toEmail, string subject, string body)
        {
            using (var smtpClient = new SmtpClient(MailInfo.Host, MailInfo.Port))
            {
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
