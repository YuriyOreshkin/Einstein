using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public interface IPack
    {
        void Pack(string filename, string zipname);
        void UnPack(string filename);
    }
}
