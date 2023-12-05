using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public interface IXmlService
    {
        bool FileExist(string filename);
        void SaveSettings(object settings, Type type, string filename);
        object ReadSettings(Type type, string filename);
    }
}
