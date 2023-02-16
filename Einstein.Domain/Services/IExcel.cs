using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public interface IExcel
    {
        void Export(string filename,string template, List<Sheet> sheets);
    }
}
