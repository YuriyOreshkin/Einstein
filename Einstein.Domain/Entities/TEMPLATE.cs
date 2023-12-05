using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Einstein.Domain.Entities
{
    public class TEMPLATES
    {
        [XmlArray("Items")]
        public List<TEMPLATE> TEMPLATEs { get; set; }
    }
  
    public class TEMPLATE
    {
        [XmlAttribute]
        public string ID { get; set; }
        [XmlAttribute]
        public string ANCHOR { get; set; }
        [XmlAttribute]
        public string TITLE { get; set; }
        [XmlAttribute]
        public string FILENAME { get; set; }
    }
}
