using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace YmlCatalogModel.Currencies
{
    [Serializable]
    public class Currencies
    {
        [XmlElement("currency")] 
        public List<Currency> CurrencyList { get; set; } 
    }
}