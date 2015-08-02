﻿using System;
using System.Xml.Serialization;

namespace YmlCatalogModel.Currencies
{
    [Serializable]
    public class Currency
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("rate")]
        public int Rate { get; set; }

        [XmlAttribute("plus")]
        public int Plus { get; set; }
    }
}
