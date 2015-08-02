using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YmlCatalogLib.Catalog;
using YmlCatalogLib.Exceptions;
using YmlCatalogModel;
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
            IYmlCatalog ymlCatalog = new YmlCatalogWorker();
            var ymlCatalogResult = new YmlCatalog();
            const string ymlCatalogDate = "2010-04-01 17:00";
            const string ymlCatalogName = "Magazin";
            const string ymlCatalogCompany = "Magazin";
            const string ymlCatalogUrl = "http://www.magazin.ru/";
            //act
            ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            //assert
            Assert.IsNotNull(ymlCatalogResult);
            Assert.AreEqual(ymlCatalogDate,ymlCatalogResult.Date);
            Assert.AreEqual(ymlCatalogName, ymlCatalogResult.Shop.Name);
            Assert.AreEqual(ymlCatalogCompany, ymlCatalogResult.Shop.Company);
            Assert.AreEqual(ymlCatalogUrl, ymlCatalogResult.Shop.Url);
        }

        [TestMethod]
        [ExpectedException(typeof(YmlCatalogException))]
        public void YmlCatalogWorker_GetYmlCatalog_GetYmlCatalogException()
        {
            //arrange
            IYmlCatalog ymlCatalog = new YmlCatalogWorker();
            //act
            ymlCatalog.GetYmlCatalog(@"");
            //assert
        }

        [TestMethod]
        public void YmlCatalogWorker_GetYmlCatalog_GetException()
        {
            //arrange
            IYmlCatalog ymlCatalog = new YmlCatalogWorker();
            var ymlCatalogResult = new YmlCatalog();
            //act
            try
            {
                ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"");
            }
            //assert
            catch (YmlCatalogException e)
            {
                Assert.AreEqual("Path to XML is null.",e.Message);
            }
        }

        [TestMethod]
        public void YmlCatalogWorker_SendOffer()
        {
            //arrange
            IYmlCatalog ymlCatalog = new YmlCatalogWorker();
            var ymlCatalogResult = new YmlCatalog();
            bool result = false;
            //act
            ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            result = ymlCatalog.SendOffer(ymlCatalogResult,12344,@"http://partner.market.yandex.ru/pages/help/YML.xml");
            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void YmlCatalogWorker_SendOffer_NoId()
        {
            //arrange
            IYmlCatalog ymlCatalog = new YmlCatalogWorker();
            var ymlCatalogResult = new YmlCatalog();
            const int offerId = 0;
            //act
            ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            try
            {
                ymlCatalog.SendOffer(ymlCatalogResult, offerId, @"http://partner.market.yandex.ru/pages/help/YML.xml");
            }
            //assert
            catch (YmlCatalogException e)
            {
                Assert.AreEqual(String.Format("No offer with id = {0}.", offerId),e.Message);
            }            
        }

        [TestMethod]
        public void YmlCatalogWorker_GetOfferById()
        {
            //arrange
            YmlCatalogWorker ymlCatalog = new YmlCatalogWorker();            
            Offer offer = new Offer();
            YmlCatalog ymlCatalogResult = new YmlCatalog();
            int offerId = 12344;
            const string offerUrl = "http://magazin.ru/product_page.asp?pid=14347";
            const int offerPrice = 450;
            
            //act
            ymlCatalogResult = ymlCatalog.GetYmlCatalog(@"http://partner.market.yandex.ru/pages/help/YML.xml");
            PrivateObject obj = new PrivateObject(ymlCatalog);
            offer = (Offer)obj.Invoke("GetOfferById", new object[] { ymlCatalogResult, offerId });
            
            //assert
            Assert.IsNotNull(offer);
            Assert.AreEqual(offerId,offer.Id);
            Assert.AreEqual(offerUrl, offer.Url);
            Assert.AreEqual(offerPrice, offer.Price);

            offerId = 10;
            offer = (Offer)obj.Invoke("GetOfferById", new object[] { ymlCatalogResult, offerId });

            //assert
            Assert.IsNull(offer);

        }
    }
}
