using System;

namespace YmlCatalogLib.Exceptions
{
    /// <summary>
    /// YmlCatalogException
    /// </summary>
    public class YmlCatalogException : Exception
    {
        public YmlCatalogException() { }

        public YmlCatalogException(string message) : base(message) { }

        public YmlCatalogException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
