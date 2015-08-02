using YmlCatalogModel;

namespace YmlCatalogApp
{
    public interface IYmlCatalogService
    {
        void DisplayYmlCatalog(string url);

        void SendOffer(int offerId, string url);
    }
}
