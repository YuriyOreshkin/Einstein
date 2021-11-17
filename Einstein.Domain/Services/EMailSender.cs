using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public class EMailSender : IMailSender
    {
        private IMailServiceConfig config;
        private ITemplateService template;
        public EMailSender(IMailServiceConfig _config, ITemplateService _template)
        {
            config = _config;
            template = _template;
        }


        private void SendEMail(string to, string subject, string body)
        {
            MAILSERVICESETTINGS settings = config.ReadSettings();
            if (settings.ENABLE)
            {
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient(settings.HOST, settings.PORT);
                // логин и пароль
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(settings.USER, settings.PASSWORD);
                smtp.EnableSsl = true;

                // отправитель - устанавливаем адрес и отображаемое в письме имя
                //MailAddress from = new MailAddress(settings.USER);
                string from = settings.USER;
                // кому отправляем
                // MailAddress toadd = new MailAddress(to);
                // создаем объект сообщения
                MailMessage mail = new MailMessage(from, to);
                // тема письма
                mail.Subject = subject;
                // текст письма
                mail.Body = body;
                // письмо представляет код html
                mail.IsBodyHtml = true;


                smtp.Send(mail);
            }

        }

        public void SendOrder(object order)
        {
            var property= order.GetType().GetProperties().FirstOrDefault(p => p.Name == "email");
            var to = property != null ? property.GetValue(order).ToString() : "";
            var body = template.GetTemplateBody(order);
            var subject = template.GetTemplateSubject(order);
            SendEMail(to, subject, body);

        }
    }
}
