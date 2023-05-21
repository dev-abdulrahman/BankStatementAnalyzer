using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace BankStatementAnalyzer.BusinessLogic.Common
{
    public class EmailHelper
    {
        private SmtpClient smtpClient;

        private string smtpHostName;

        private string smtpUserName;

        private string smtpPassword;

        private int smtpPort;

        MailModel _mailModel;

        public EmailHelper(MailModel mailModel)
        {
            _mailModel = mailModel;
            smtpHostName = ConfigurationManager.AppSettings["smtp:host"];
            smtpUserName = ConfigurationManager.AppSettings["smtp:username"];
            smtpPassword = ConfigurationManager.AppSettings["smtp:password"];
            smtpPort = int.Parse(ConfigurationManager.AppSettings["smtp:port"]);
        }

        public void Send()
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_mailModel.FromId);

            foreach (var email in _mailModel.ToList)
            {
                message.To.Add(new MailAddress(email));
            }

            if (_mailModel.CCList != null)
            {
                foreach (var email in _mailModel.CCList)
                {
                    message.CC.Add(new MailAddress(email));
                }
            }

            message.Subject = _mailModel.Subject;
            message.IsBodyHtml = true;
            message.Body = _mailModel.MessageBody;

            SmtpClient smtp = new SmtpClient(smtpHostName)
            {
                Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword),
                Port = smtpPort,
                EnableSsl = true
            };

            ServicePointManager.ServerCertificateValidationCallback =
           delegate (
               object s,
               X509Certificate certificate,
               X509Chain chain,
               SslPolicyErrors sslPolicyErrors
           )
           {
               return true;
           };

            smtp.Send(message);
        }
    }


}
