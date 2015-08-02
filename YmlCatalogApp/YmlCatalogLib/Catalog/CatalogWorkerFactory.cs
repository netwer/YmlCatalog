namespace YmlCatalogLib.Catalog
{
    public static class CatalogWorkerFactory
    {
        /// <summary>
        /// Create instance for catalog
        /// </summary>
        /// <typeparam name="T">The class implements the IYmlCatalog</typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>() where T : IYmlCatalog, new()
        {
            return new T();
        }
    }
}
