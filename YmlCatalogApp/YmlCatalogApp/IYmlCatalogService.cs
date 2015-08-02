using YmlCatalogModel;

namespace YmlCatalogApp
{
    /// <summary>
    /// IYmlCatalogService interface
    /// </summary>
    public interface IYmlCatalogService
    {
        /// <summary>
        /// Displays the yml catalog.
        /// </summary>
        /// <param name="url">The URL.</param>
        void DisplayYmlCatalog(string url);

        /// <summary>
        /// Send the offer.
        /// </summary>
        /// <param name="offerId">The offer id.</param>
        /// <param name="url">The URL to send.</param>
        void SendOffer(int offerId, string url);
    }
}
