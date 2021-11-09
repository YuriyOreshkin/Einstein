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
        string GetTemplateText();
        void SaveTemplate(string text);
        string FullTemplate(ORDER order);
    }
}
