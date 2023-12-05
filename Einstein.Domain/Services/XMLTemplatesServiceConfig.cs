using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLTemplatesServiceConfig : ITemplatesServiceConfig
    {
        private string filename;
        private IXmlService xml;
        public XMLTemplatesServiceConfig(string _filename, IXmlService _xml)
        {
            filename = _filename;
            xml = _xml;
        }

       

        public void SaveSettings(TEMPLATES templates)
        {
           
            xml.SaveSettings(templates, typeof(TEMPLATES), filename);

        }

        public TEMPLATES ReadSettings()
        {
            if (xml.FileExist(filename))
            {

                TEMPLATES templates = (TEMPLATES)xml.ReadSettings(typeof(TEMPLATES), filename);
                
                return templates;
            }
            else
            {
                TEMPLATES templates = new TEMPLATES
                {
                    TEMPLATEs = new List<TEMPLATE>
                    {
                        new TEMPLATE
                        {
                        ID = "NewOrder",
                        FILENAME = "NewOrderTemplate.html",
                        ANCHOR="order",
                        TITLE="Заказ"
                        },
                         new TEMPLATE
                        {
                        ID = "Landing",
                        FILENAME = "MailingTemplate.html",
                        ANCHOR="mailing",
                        TITLE="Выгрузка"
                        },
                          new TEMPLATE
                        {
                        ID = "Terms",
                        FILENAME = "Terms.html",
                        ANCHOR=null,
                        TITLE="Условия"
                        }
                    }
                };

                SaveSettings(templates);

                return templates;
            }

        }
       
    }
}
