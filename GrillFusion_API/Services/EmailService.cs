using System.Net;
using System.Net.Mail;

namespace GrillFusion_API.Services
{
    public class EmailService
    {
        public readonly IConfiguration _conf;

        public EmailService(IConfiguration conf)
        {
            _conf = conf;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_conf["EmailSettings:Host"], int.Parse(_conf["EmailSettings:Port"]))
            {
                Credentials = new NetworkCredential(_conf["EmailSettings:UserName"], _conf["EmailSettings:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(
                _conf["EmailSettings:FromEmail"], // 👆 verified email
                _conf["EmailSettings:FromName"]
            ),
                Subject = subject,
                Body = body,
                IsBodyHtml = true // 👆 allows clickable link
            };

            mailMessage.To.Add(toEmail); // 👆 user email

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}