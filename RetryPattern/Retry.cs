using System;
using System.Linq;
using System.Diagnostics;
using System.Net;

namespace RetryPattern
{
    public class Retry
    {
        private static int retryCount = 3;
        private static TimeSpan delay = TimeSpan.FromSeconds(5);

        public static void OperationWithBasicRetry(Action action , int retryAttempt, TimeSpan delaySpan)
        {
            int currentRetryCount = 0;

            retryCount = retryAttempt;
            delay = delaySpan;

            while (true)
            {
                try
                {
                    // Call external service.
                    action();

                    // log for success
                    Console.WriteLine("Succeeded:  Operation");

                    // Return or break.
                    break;
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Operation Exception");
                    Console.WriteLine("Failed: Operation");

                    currentRetryCount++;

                    // Check if the exception thrown was a transient exception
                    // based on the logic in the error detection strategy.
                    // Determine whether to retry the operation, as well as how
                    // long to wait, based on the retry strategy.
                    if (currentRetryCount >= retryCount || !IsTransient(ex))
                    {
                        // If this isn't a transient error or we shouldn't retry, 
                        // rethrow the exception.
                        throw;
                    }

                }
            }
        }

        private static bool IsTransient(Exception ex)
        {
            // Determine if the exception is transient.
            // In some cases this is as simple as checking the exception type, in other
            // cases it might be necessary to inspect other properties of the exception.
            if (ex is OperationTransientException)
            {
                return true;
            }

            var webException = ex as WebException;
            if (webException != null)
            {
                // If the web exception contains one of the following status values
                // it might be transient.
                return new[]
                {
                    WebExceptionStatus.ConnectionClosed,
                    WebExceptionStatus.Timeout,
                    WebExceptionStatus.RequestCanceled
                }.Contains(webException.Status);
            }

            // Additional exception checking logic goes here.

            return false;
        }
    }
}