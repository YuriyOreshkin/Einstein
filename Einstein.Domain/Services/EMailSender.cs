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



        public void SendMail(ORDER order)
        {
            MAILSERVICESETTINGS settings = config.ReadSettings();
            if (settings.ENABLE)
            {
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient(settings.HOST, settings.PORT);
                // логин и пароль
                smtp.Credentials = new NetworkCredential(settings.USER, settings.PASSWORD);
                smtp.EnableSsl = true;


                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress(settings.USER);
                // кому отправляем
                MailAddress to = new MailAddress(order.EMAIL);
                // создаем объект сообщения
                MailMessage mail = new MailMessage(from, to);
                // тема письма
                mail.Subject = "Тест";
                // текст письма
                mail.Body = template.FullTemplate(order);
                // письмо представляет код html
                mail.IsBodyHtml = true;


                smtp.Send(mail);
            }

        }
    }
}
