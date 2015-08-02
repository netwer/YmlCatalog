using YmlCatalogModel;

namespace YmlCatalogLib
{
    public interface ICatalog
    {
        YmlCatalog GetYmlCatalog(string path);
        bool SendYmlCatalog(YmlCatalog ymlCatalog, int offerId, string url);
    }
}
