using System;
using System.Xml.Serialization;

namespace YmlCatalogModel
{
    [Serializable]
    [XmlRoot("yml_catalog")]
    
    public class YmlCatalog
    {
        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlElement("shop")] 
        public Shop Shop { get; set; }
    }
}
