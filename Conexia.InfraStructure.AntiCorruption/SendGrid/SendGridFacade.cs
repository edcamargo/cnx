using Conexia.Domain.Shared.Facades;
using System.Net;
using System.Net.Mail;

namespace Conexia.InfraStructure.AntiCorruption.SendGrid
{
    public class SendGridFacade : IEmailFacade
    {
        public void Send(string to, string from, string subject, string body)
        {
            MailMessage message = new MailMessage();
            var pass = "48981B4C2EB179CE0C37DEE6953E6420702B";

            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = "From : " + "<br>Message: " + body;
            message.Subject = subject;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.elasticemail.com");
            smtp.Credentials = new NetworkCredential("edwin.desenv@gmail.com", "48981B4C2EB179CE0C37DEE6953E6420702B");
            smtp.EnableSsl = true;
            smtp.Port = 2525;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            smtp.Send(message);
        }
    }
}
