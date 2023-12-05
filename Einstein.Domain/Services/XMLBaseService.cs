using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLService : IXmlService
    {
        
        private string path;
        public XMLService(string _path)
        {
            path = _path;
        }

        public void SaveSettings(object settings,Type type,string filename)
        {
            filename = Path.Combine(path, filename);
            XmlSerializer formatter = new XmlSerializer(type);
            TextWriter writer = new StreamWriter(filename, false, Encoding.GetEncoding(1251));

            formatter.Serialize(writer, settings);

            writer.Close();
        }
        public object ReadSettings(Type type, string filename) 
        {
            filename = Path.Combine(path, filename);
            XmlSerializer formatter = new XmlSerializer(type);

            using (StreamReader fs = new StreamReader(filename, Encoding.GetEncoding(1251), false))
            {
                var  settings = formatter.Deserialize(fs);
                fs.Close();
                return settings;
            }
        }

        public bool FileExist(string filename)
        {
            filename = Path.Combine(path, filename);
            return File.Exists(filename);
        }
    }
}
