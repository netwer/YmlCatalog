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

namespace YmlCatalogLib.Catalog
{
    /// <summary>
    /// Implementation of IYmlCatalog
    /// </summary>
    public class YmlCatalogWorker : IYmlCatalog
    {
        /// <summary>
        /// Gets the yml catalog.
        /// </summary>
        /// <param name="path">The path to catalog.</param>
        /// <returns></returns>
        /// <exception cref="YmlCatalogException">
        /// Path to XML is null
        /// or
        /// Other exception.
        /// </exception>
        public YmlCatalog GetYmlCatalog(string path)
        {
            if(String.IsNullOrEmpty(path))
                throw new YmlCatalogException("Path to XML is null.");
            
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            YmlCatalog catalog = new YmlCatalog();

            try
            {
                catalog = GetXmlYmlCatalog(path, token).Result;
            }
            catch (Exception e)
            {
                throw new YmlCatalogException(e.Message);
            }

            return catalog;
        }

        /// <summary>
        /// Send the offer.
        /// </summary>
        /// <param name="ymlCatalog">The yml catalog.</param>
        /// <param name="offerId">The offer id.</param>
        /// <param name="url">The URL to send.</param>
        /// <returns>true</returns>
        /// <exception cref="YmlCatalogException">
        /// Yml Catalog is null
        /// or
        /// Other exception.
        /// </exception>
        public bool SendOffer(YmlCatalog ymlCatalog, int offerId, string url)
        {
            if(ymlCatalog == null)
                throw new YmlCatalogException("Yml Catalog is null.");

            Offer offer = GetOfferById(ymlCatalog, offerId);

            if (offer == null)
                throw new YmlCatalogException(String.Format("No offer with id = {0}.", offerId));

            string offerInJson = new JavaScriptSerializer().Serialize(offer);
            bool sendResult = true;

            try
            {
                sendResult = SendJson(offerInJson, url).Result;
            }
            catch (Exception e)
            {
                throw new YmlCatalogException(e.Message);
            }

            return sendResult;
        }

        /// <summary>
        /// Get the XML yml catalog.
        /// </summary>
        /// <param name="path">The path to catalog.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>YmlCatalog.</returns>
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

        /// <summary>
        /// Send the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="url">The URL to send.</param>
        /// <returns>true.</returns>
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

        /// <summary>
        /// Gets the offer by id.
        /// </summary>
        /// <param name="ymlCatalog">The yml catalog.</param>
        /// <param name="offerId">The offer id.</param>
        /// <returns>Offer by id.</returns>
        private Offer GetOfferById(YmlCatalog ymlCatalog,int offerId)
        {
            return ymlCatalog.Shop.Offers.OfferList.FirstOrDefault(item => item.Id == offerId);
        }

        /// <summary>
        /// Get the content of the XML.
        /// </summary>
        /// <param name="path">The path to content.</param>
        /// <returns>Content.</returns>
        private string GetXmlContent(string path)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(path);
            }
        }
    }
}
