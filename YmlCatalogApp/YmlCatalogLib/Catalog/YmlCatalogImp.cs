using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using JetBrains.Annotations;
using YmlCatalogLib.Exceptions;
using YmlCatalogModel;
using YmlCatalogModel.Offers;

namespace YmlCatalogLib.Catalog
{
    /// <summary>
    /// Implementation of IYmlCatalog
    /// </summary>
    public class YmlCatalogImp : IYmlCatalog
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
        [NotNull]
        public YmlCatalog GetYmlCatalog(string path)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            YmlCatalog catalog =new YmlCatalog();

            try
            {
                var catalogTask = GetXmlYmlCatalog(path, token);
                catalog = catalogTask.Result;
            }
            catch
            {
                throw;
            }

            return catalog;
        }

        /// <summary>
        /// Send the offer.
        /// </summary>
        /// <param name="ymlCatalog">The yml catalog.</param>
        /// <param name="offerId">The offer id.</param>
        /// <param name="urlToSend">The URL to send.</param>
        /// <returns>true</returns>
        /// <exception cref="YmlCatalogException">
        /// Yml Catalog is null
        /// or
        /// Other exception.
        /// </exception>
        public bool SendOffer(YmlCatalog ymlCatalog, int offerId, string urlToSend)
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
                var sendResultTask = SendJson(offerInJson, urlToSend);
                sendResult = sendResultTask.Result;
            }
            catch
            {
                throw;
            }

            return sendResult;
        }

        /// <summary>
        /// Sends the json.
        /// </summary>
        /// <param name="offerInJson">The offer in json.</param>
        /// <param name="urlToSend">The URL to send.</param>
        /// <returns>Task bool</returns>
        async private Task<bool> SendJson(string offerInJson, string urlToSend)
        {
            var asyncResult = SendJsonAsync(offerInJson, urlToSend);
            bool sendResult = await asyncResult;
            return sendResult;
        }

        /// <summary>
        /// Gets the XML yml catalog.
        /// </summary>
        /// <param name="path">The path to catalog.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task YmlCatalog.</returns>
        async private Task<YmlCatalog> GetXmlYmlCatalog(string path, CancellationToken cancellationToken)
        {
            
            var asyncResult = GetXmlYmlCatalogAsync(path, cancellationToken);
            YmlCatalog catalog = await asyncResult;
            return catalog;
        }

        /// <summary>
        /// Get the XML yml catalog.
        /// </summary>
        /// <param name="path">The path to catalog.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>YmlCatalog.</returns>
        async private Task<YmlCatalog> GetXmlYmlCatalogAsync(string path,CancellationToken cancellationToken)
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
        /// <param name="urlToSend">The URL to send.</param>
        /// <returns>true.</returns>
        async private Task<bool> SendJsonAsync(string json, string urlToSend)
        {
            return await Task.Run(() =>
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlToSend);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

               var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
               using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
               {
                   var result = streamReader.ReadToEnd();
               }
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
