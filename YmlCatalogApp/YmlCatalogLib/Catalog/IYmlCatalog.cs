using YmlCatalogModel;

namespace YmlCatalogLib.Catalog
{
    /// <summary>
    /// IYmlCatalog interface
    /// </summary>
    public interface IYmlCatalog
    {
        /// <summary>
        /// Gets the yml catalog.
        /// </summary>
        /// <param name="path">The path to catalog.</param>
        /// <returns></returns>
        YmlCatalog GetYmlCatalog(string path);

        /// <summary>
        /// Send the offer.
        /// </summary>
        /// <param name="ymlCatalog">The yml catalog.</param>
        /// <param name="offerId">The offer id.</param>
        /// <param name="urlToSend">The URL to send.</param>
        /// <returns></returns>
        bool SendOffer(YmlCatalog ymlCatalog, int offerId, string urlToSend);
    }
}
