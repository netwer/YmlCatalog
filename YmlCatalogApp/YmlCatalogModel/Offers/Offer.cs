using System;
using System.Xml.Serialization;

namespace YmlCatalogModel.Offers
{
    [Serializable]
    public class Offer
    {
        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("price")]
        public ushort Price { get; set; }

        [XmlElement("currencyId")]
        public string CurrencyId { get; set; }

        [XmlElement("categoryId")]
        public OfferCategoryId CategoryId { get; set; }

        [XmlElement("picture")]
        public string Picture { get; set; }

        [XmlElement("delivery")]
        public bool Delivery { get; set; }

        [XmlElement("local_delivery_cost")]
        public int LocalDeliveryCost { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("publisher")]
        public string Publisher { get; set; }

        [XmlElement("series")]
        public string Series { get; set; }

        [XmlElement("year")]
        public int Year { get; set; }

        [XmlIgnore()]
        public bool YearSpecified { get; set; }

        [XmlElement("ISBN")]
        public string Isbn { get; set; }

        [XmlElement("volume")]
        public int Volume { get; set; }

        [XmlIgnore()]
        public bool VolumeSpecified { get; set; }

        [XmlElement("part")]
        public int Part { get; set; }

        [XmlIgnore()]
        public bool PartSpecified { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("binding")]
        public string Binding { get; set; }

        [XmlElement("page_extent")]
        public ushort PageExtent { get; set; }

        [XmlIgnore()]
        public bool PageExtentSpecified { get; set; }
        
        [XmlElement("vendor")]
        public string Vendor { get; set; }

        [XmlElement("vendorCode")]
        public string VendorCode { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("downloadable")]
        public bool Downloadable { get; set; }

        [XmlIgnore()]
        public bool DownloadableSpecified { get; set; }

        [XmlElement("manufacturer_warranty")]
        public bool ManufacturerWarranty { get; set; }

        [XmlIgnore()]
        public bool ManufacturerWarrantySpecified { get; set; }

        [XmlElement("country_of_origin")]
        public string CountryOfOrigin { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("bid")]
        public int Bid { get; set; }

        [XmlAttribute("cbid")]
        public int Cbid { get; set; }

        [XmlIgnore()]
        public bool CbidSpecified { get; set; }

        [XmlAttribute("available")]
        public bool Available { get; set; }
    }
}