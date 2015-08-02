using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using YmlCatalogLib.Exceptions;
using YmlCatalogModel;
using YmlCatalogModel.Offers;

namespace YmlCatalogLib
{
    public class YmlCatalogWorker : ICatalog
    {
        public YmlCatalog GetYmlCatalog(string path)
        {
            if(String.IsNullOrEmpty(path))
                throw new YmlCatalogException("Path to XML is null.");
            
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            return GetXmlYmlCatalog(path, token).Result;
        }

        public bool SendYmlCatalog(YmlCatalog ymlCatalog, int offerId, string url)
        {
            if(ymlCatalog == null)
                throw new YmlCatalogException("Yml Catalog is null.");

            Offer offer = GetOfferById(ymlCatalog, offerId);

            if (ymlCatalog == null)
                throw new YmlCatalogException(String.Format("No offer with id = {0}.", offerId));

            string offerInJson = new JavaScriptSerializer().Serialize(offer);
            return SendJson(offerInJson, url).Result;
        }

        async private Task<YmlCatalog> GetXmlYmlCatalog(string path,CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                string xmlContent = GetXmlContent(path);

                if (String.IsNullOrEmpty(xmlContent))
                    throw new YmlCatalogException("XML is null");

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(YmlCatalog));
                return (YmlCatalog)xmlSerializer.Deserialize(new StringReader(xmlContent));
            }, cancellationToken);
            
        }

        async private Task<bool> SendJson(string json, string url)
        {
            return await Task.Run(() =>
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                //no request to url
                //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                return true;
            }); 
        }

        private Offer GetOfferById(YmlCatalog ymlCatalog,int offerId)
        {
            return ymlCatalog.Shop.Offers.OfferList.FirstOrDefault(item => item.Id == offerId);
        }

        private string GetXmlContent(string path)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(path);
            }
        }
    }
}
