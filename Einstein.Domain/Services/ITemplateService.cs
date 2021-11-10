using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public interface ITemplateService
    {
        string GetTemplateBody();
        string GetTemplateSubject();
        void SaveTemplate(string subject, string text);
        string GetTemplateBody(object order);
        string GetTemplateSubject(object order);
        Dictionary<string, string> AvailableParameters(Type type);

    }
}
