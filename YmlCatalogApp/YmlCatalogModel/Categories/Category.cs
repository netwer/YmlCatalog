using System;
using System.Xml.Serialization;

namespace YmlCatalogModel.Categories
{
    [Serializable]
    public class Category
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("parentId")]
        public int ParentId { get; set; }
    }
}