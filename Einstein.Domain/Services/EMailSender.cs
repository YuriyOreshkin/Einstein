using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private IMailingServiceConfig mailing;
        public EMailSender(IMailServiceConfig _config, ITemplateService _template,IMailingServiceConfig _mailing)
        {
            config = _config;
            template = _template;
            mailing = _mailing;
        }


        private void SendEMail(MAILSERVICESETTINGS settings,string to, string subject, string body,string attachment=null)
        {
         
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient(settings.HOST, settings.PORT);
                // логин и пароль
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(settings.USER, settings.PASSWORD);
                //smtp.EnableSsl = true;

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

                //прикрепляем вложения
                if (!String.IsNullOrEmpty(attachment) & File.Exists(attachment))
                {
                    mail.Attachments.Add(new Attachment(attachment));
                }
           
            smtp.Send(mail);
           
            mail.Dispose();
        }
      

        public void SendOrder(object order)
        {

            MAILSERVICESETTINGS settings = config.ReadSettings();
            if (settings.ENABLE)
            {
                var property = order.GetType().GetProperties().FirstOrDefault(p => p.Name == "email");
                var to = property != null ? property.GetValue(order).ToString() : "";
                var body = template.GetTemplateBody(order);
                var subject = template.GetTemplateSubject(order);
                SendEMail(settings, to, subject, body);
            }

            else
            {
                throw new Exception("Сервис выключен!");
            }
        }

        public void MailingOrders(MAILINGSERVICESETTINGS settings, string filename)
        {
            MAILSERVICESETTINGS mail = config.ReadSettings();
            try
            {
                SendEMail(mail, settings.RECIPIENTS, "Академия Теслы", "Рассылка списка заказов по мероприятиям, проводимым за период !", filename);
            }
            finally
            {
                File.Delete(filename);
            }
        }

        public MAILINGSERVICESETTINGS GetMailingSettings()
        {
            return mailing.ReadSettings();
        }
    }
}
