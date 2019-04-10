using System;

namespace SimpleMiner.Exceptions
{
    public class UnsupportedContentException : Exception
    {
        public UnsupportedContentException(string message)
            :base(message)
        {
        }
    }
}
