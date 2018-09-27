using System;

namespace RetryPattern
{
    public class OperationTransientException : Exception
    {
        public OperationTransientException()
            :base()
        {

        }

        public OperationTransientException(string message)
            :base(message)
        {

        }

        public OperationTransientException(string message, Exception innerException)
            :base(message, innerException)
        {

        }

        public int CurrentRetryAttemptCount { get; set; }

    }
}