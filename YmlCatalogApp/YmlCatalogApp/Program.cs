using System;
using YmlCatalogLib.Catalog;

namespace YmlCatalogApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IYmlCatalogService service = new YmlCatalogService(new YmlCatalogImp(), @"http://partner.market.yandex.ru/pages/help/YML.xml");
            service.DisplayYmlCatalog();
            service.SendOffer(12344, @"https://www.google.ru/?gws_rd=ssl");
            Console.WriteLine("Enter key...");
            Console.ReadKey();
        }
    }
}
