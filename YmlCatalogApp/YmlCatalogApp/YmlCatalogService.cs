using System;
using YmlCatalogLib;
using YmlCatalogLib.Catalog;
using YmlCatalogLib.Exceptions;
using YmlCatalogModel;

namespace YmlCatalogApp
{
    public class YmlCatalogService : IYmlCatalogService
    {
        private readonly IYmlCatalog _catalog;
        private YmlCatalog _ymlCatalog;

        public YmlCatalogService(IYmlCatalog catalog)
        {
            _catalog = catalog;
        }

        public void DisplayYmlCatalog(string url)
        {
            try
            {
                _ymlCatalog = _catalog.GetYmlCatalog(url);
                Console.WriteLine("Yml Catalog:");
                Console.WriteLine("Date: {0}", _ymlCatalog.Date);
                Console.WriteLine("Name: {0}", _ymlCatalog.Shop.Name);
                Console.WriteLine("Company: {0}", _ymlCatalog.Shop.Company);
                Console.WriteLine("URL: {0}", _ymlCatalog.Shop.Url);
                Console.WriteLine("Offers count: {0}", _ymlCatalog.Shop.Offers.OfferList.Count);
            }
            catch (YmlCatalogException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SendOffer(int offerId, string url)
        {
            try
            {
                _catalog.SendOffer(_ymlCatalog, offerId, url);
                Console.WriteLine("The offer sent successfully");
            }
            catch (YmlCatalogException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
