using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace YmlCatalogModel.Offers
{
    [Serializable]
    public class Offers
    {
        [XmlElement("offer")]
        public List<Offer> OfferList { get; set; }
    }
}