using Einstein.Domain.Entities;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Einstein.Domain.Services
{
    public class EMailBackgroundJobs : IBackgroundJobs
    {
        private ISender sender;
        private ITemplateService template;
        private IExcel excel;
        private IMailingServiceConfig config;

        public EMailBackgroundJobs(IMailingServiceConfig _config, ISender _sender, ITemplateService _template,IExcel _excel)
        {
            sender = _sender;
            template = _template;
            excel = _excel;
            config = _config;
        }
        private void ExportExcel(List<Sheet> sheets, string filename)
        {
          
            excel.Export(filename, "Orders export template.xlsx", sheets);
        }

        public string ExportOrdersExcelList(List<Sheet> sheets,DateTime datebegin, DateTime dateend)
        {
          
            var path = HttpContext.Current.Server.MapPath("~/App_Data/Mailing");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filename = Path.Combine(path, String.Format("Список заказов по мероприятиям за период с {0} по {1}.xlsx", datebegin.ToShortDateString(), dateend.ToShortDateString()));

            ExportExcel( sheets, filename);

            return filename;
          
        }

        public void MailingOrders(List<Sheet>  sheets)
        {
            var settings = config.ReadSettings();
            if (settings.ENABLE)
            {
                var period = new { datebegin = GetDateBegin(), dateend = GetDateEnd() };
                var filename = ExportOrdersExcelList(sheets, period.datebegin, period.dateend);
                if (File.Exists(filename))
                {
                    try
                    {

                        var body = template.GetTemplateBody(period);
                        var subject = template.GetTemplateSubject(period);
                        sender.SendEMail(settings.RECIPIENTS, subject, body, filename);
                    }

                    finally
                    {
                        File.Delete(filename);
                    }
                }
            }
        }

       

        public DateTime GetDateBegin()
        {
            var settings = config.ReadSettings();
          
            return DateTime.Now.AddDays(settings.BEGINFROMTODAY).Date.AddHours(0).AddMinutes(0).AddSeconds(0);

            
        }

        public DateTime GetDateEnd()
        {
            var settings = config.ReadSettings();

            return DateTime.Now.AddDays(settings.ENDFROMTODAY).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}
