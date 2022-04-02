using System;

namespace FTPLayer
{
    public sealed class FTPLayerException : Exception
    {
        public FTPLayerException(string message) : base(message) { }
    }
}