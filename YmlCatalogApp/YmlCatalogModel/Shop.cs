using System;
using System.Xml.Serialization;

namespace YmlCatalogModel
{
    [Serializable]
    public class Shop
    {
        [XmlElement("name")] 
        public string Name { get; set; }

        [XmlElement("company")] 
        public string Company { get; set; }

        [XmlElement("url")] 
        public string Url { get; set; }

        [XmlElement("currencies")]
        public Currencies.Currencies Currencies { get; set; }

        [XmlElement("local_delivery_cost")] 
        public int LocalDeliveryCost { get; set; }

        [XmlElement("categories")]
        public Categories.Categories Categories { get; set; }

        [XmlElement("offers")]
        public Offers.Offers Offers { get; set; }

    }
}
