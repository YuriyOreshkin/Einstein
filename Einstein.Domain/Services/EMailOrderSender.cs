using Einstein.Domain.Entities;
using NPOI.XWPF.UserModel;
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
    public class EMailOrderSender : IOrderSender
    {
        private ISender sender;
        private ITemplateService template;
        
        public EMailOrderSender(ISender _sender, ITemplateService _template)
        {
            sender = _sender;
            template = _template;
            
        }
      

        public void SendOrder(object order)
        {
           
            if (sender.IsEnable())
            {
                var property = order.GetType().GetProperties().FirstOrDefault(p => p.Name == "email");
                var to = property != null ? property.GetValue(order).ToString() : "";
                var body = template.GetTemplateBody(order);
                var subject = template.GetTemplateSubject(order);
                sender.SendEMail(to, subject, body);
            }

            else
            {
                throw new Exception("Сервис выключен!");
            }
        }
       
    }
}
