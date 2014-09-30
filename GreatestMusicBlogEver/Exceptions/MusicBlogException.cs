using System;

namespace Service.Exceptions
{
    public class MusicBlogException : Exception
    {
        public MusicBlogException()
        {
        }

        public MusicBlogException(string message)
            : base(message)
        {
        }

        public MusicBlogException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}