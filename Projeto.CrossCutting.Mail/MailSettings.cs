using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.CrossCutting.Mail
{
    public class MailSettings
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
