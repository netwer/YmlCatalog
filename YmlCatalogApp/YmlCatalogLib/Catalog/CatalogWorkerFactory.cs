namespace YmlCatalogLib.Catalog
{
    public static class CatalogWorkerFactory
    {
        /// <summary>
        /// Create instance for catalog
        /// </summary>
        /// <typeparam name="T">The class implements the ICatalog</typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>() where T : IYmlCatalog, new()
        {
            return new T();
        }
    }
}
