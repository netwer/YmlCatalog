using System;
using System.Xml.Serialization;

namespace YmlCatalogModel.Offers
{
    [Serializable]
    public class OfferCategoryId
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText()]
        public byte Value { get; set; }
    
    }
}
