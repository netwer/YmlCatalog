using System;
using YmlCatalogLib;
using YmlCatalogLib.Catalog;

namespace YmlCatalogApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IYmlCatalogService service = new YmlCatalogService(CatalogWorkerFactory.CreateInstance<YmlCatalogWorker>());
            service.DisplayYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            service.SendOffer(12344, @"https://www.google.ru/?gws_rd=ssl");
            Console.ReadKey();
        }
    }
}
