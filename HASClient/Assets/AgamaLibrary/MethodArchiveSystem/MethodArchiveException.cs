using System;

namespace MehodArchive
{
    public class MethodArchiveException : Exception
    {
        public Exception? exception;
        public string message;

        public MethodArchiveException(Exception? exception, string message)
        {
            this.exception = exception;
            this.message = message;
        }
    }
}
