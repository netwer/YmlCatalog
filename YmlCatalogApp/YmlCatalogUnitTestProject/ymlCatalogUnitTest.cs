using Microsoft.VisualStudio.TestTools.UnitTesting;
using YmlCatalogLib.Catalog;
using YmlCatalogLib.Exceptions;
using YmlCatalogModel.Offers;

namespace YmlCatalogUnitTestProject
{
    [TestClass]
    public class YmlCatalogUnitTest
    {
        [TestMethod]
        public void YmlCatalogWorker_GetYmlCatalog_NotNull()
        {
            //arrange
            IYmlCatalog ymlCatalog = new YmlCatalogImp();
            //act
            var ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            //assert
            Assert.IsNotNull(ymlCatalogResult);
        }

        [TestMethod]
        [ExpectedException(typeof(YmlCatalogException))]
        public void YmlCatalogWorker_GetYmlCatalog_GetYmlCatalogException()
        {
            //arrange
            IYmlCatalog ymlCatalog = new YmlCatalogImp();
            //act
            ymlCatalog.GetYmlCatalog(@"");
            //assert
        }
        
        [TestMethod]
        public void YmlCatalogWorker_SendOffer()
        {
            //arrange
            IYmlCatalog ymlCatalog = new YmlCatalogImp();
            //act
            var ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            bool result = ymlCatalog.SendOffer(ymlCatalogResult,12344,@"http://partner.market.yandex.ru/pages/help/YML.xml");
            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(YmlCatalogException))]
        public void YmlCatalogWorker_SendOffer_SendOfferException()
        {
            //arrange
            IYmlCatalog ymlCatalog = new YmlCatalogImp();
            const int offerId = 0;
            //act
            var ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            ymlCatalog.SendOffer(ymlCatalogResult, offerId, @"http://partner.market.yandex.ru/pages/help/YML.xml");           
        }

        [TestMethod]
        public void YmlCatalogWorker_GetOfferById_NotNull()
        {
            //arrange
            YmlCatalogImp ymlCatalog = new YmlCatalogImp();   
            const int offerId = 12344;
            
            //act
            var ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            PrivateObject obj = new PrivateObject(ymlCatalog);
            var offer = (Offer)obj.Invoke("GetOfferById", new object[] { ymlCatalogResult, offerId });
            
            //assert
            Assert.IsNotNull(offer);
        }

        [TestMethod]
        public void YmlCatalogWorker_GetOfferById_Null()
        {
            //arrange
            YmlCatalogImp ymlCatalog = new YmlCatalogImp();
            const int offerId = 123;

            //act
            var ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            PrivateObject obj = new PrivateObject(ymlCatalog);
            var offer = (Offer)obj.Invoke("GetOfferById", new object[] { ymlCatalogResult, offerId });

            //assert
            Assert.IsNull(offer);
        }
    }
}
