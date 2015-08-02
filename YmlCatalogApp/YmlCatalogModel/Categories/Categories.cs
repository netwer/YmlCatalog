using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace YmlCatalogModel.Categories
{
    [Serializable]
    public class Categories
    {
        [XmlElement("category")]
        public List<Category> CategoryList { get; set; }
    }
}