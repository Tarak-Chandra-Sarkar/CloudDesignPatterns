using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetryPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DemoRetryPattern();


            Console.WriteLine();
            Console.WriteLine("Please press any key to exit...");
            Console.ReadKey();
        }

        private static void DemoRetryPattern()
        {
            BusinessClass business = new BusinessClass();

            string msg = "HelloRetry";

            try
            {
                Action action = () =>
                {
                    business.TransientOperation(msg);
                };

                Retry.OperationWithBasicRetry(action, 3, TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException: " + ex.Message + " after Retrying");
            }

        }
    }
}
