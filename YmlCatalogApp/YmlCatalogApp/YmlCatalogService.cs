using System;
using System.Net;
using YmlCatalogLib.Catalog;
using YmlCatalogLib.Exceptions;
using YmlCatalogModel;

namespace YmlCatalogApp
{
    /// <summary>
    /// Implementation of IYmlCatalogService
    /// </summary>
    public class YmlCatalogService : IYmlCatalogService
    {
        #region Fields

        private readonly IYmlCatalog _catalog;
        private YmlCatalog _ymlCatalog;
        private string _urlToYml;

        #endregion

        public YmlCatalogService(IYmlCatalog catalog, string urlToYml)
        {
            _catalog = catalog;
            _urlToYml = urlToYml;
        }

        #region Public methods

        public void DisplayYmlCatalog()
        {
            try
            {
                _ymlCatalog = _catalog.GetYmlCatalog(_urlToYml);
                Console.WriteLine("Yml Catalog:");
                Console.WriteLine("Date: {0}", _ymlCatalog.Date);
                Console.WriteLine("Name: {0}", _ymlCatalog.Shop.Name);
                Console.WriteLine("Company: {0}", _ymlCatalog.Shop.Company);
                Console.WriteLine("URL: {0}", _ymlCatalog.Shop.Url);
                Console.WriteLine("Offers count: {0}", _ymlCatalog.Shop.Offers.OfferList.Count);
            }
            catch (AggregateException e)
            {
                e.Handle((x) =>
                {
                    if (x is YmlCatalogException)
                    {
                        Console.WriteLine("Yml Catalog Exception");
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Please press any key to end the program");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    else
                    {
                        Console.WriteLine("Other Exception");
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Please press any key to end the program");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    return true;  
                });
            }
        }

        public void SendOffer(int offerId, string url)
        {
            try
            {
                _catalog.SendOffer(_ymlCatalog, offerId, url);
                Console.WriteLine("The offer sent successfully");
            }
            catch (AggregateException e)
            {
                e.Handle((x) =>
                {
                    if (x is YmlCatalogException)
                    {
                        Console.WriteLine("Yml Catalog Exception");
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Please press any key to end the program");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    else
                    {
                        Console.WriteLine("Other Exception");
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Please press any key to end the program");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    return true;
                });
            }
        }

        #endregion
    }
}
