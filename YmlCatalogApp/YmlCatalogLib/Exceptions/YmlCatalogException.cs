using System;

namespace YmlCatalogLib.Exceptions
{
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
