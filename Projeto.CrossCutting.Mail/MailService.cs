using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Projeto.CrossCutting.Mail
{
    public class MailService
    {
        private readonly MailSettings mailSettings;
        public MailService(MailSettings mailSettings)
        {
            this.mailSettings = mailSettings;
        }

        public void SendMail(string email, string subject, string body)
        {
            var mail = new MailMessage(mailSettings.EmailAddress, email);
            mail.Subject = subject;
            mail.Body = body;

            var client = new SmtpClient(mailSettings.Smtp, mailSettings.Port);
            client.EnableSsl = mailSettings.EnableSsl;
            client.Credentials = new NetworkCredential
                                (mailSettings.EmailAddress, mailSettings.Password);
            client.Send(mail);
        }
    }
}
