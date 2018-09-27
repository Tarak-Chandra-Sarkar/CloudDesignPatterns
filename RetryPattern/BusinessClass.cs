using System;
using System.Threading.Tasks;

namespace RetryPattern
{
    public class BusinessClass
    {
        private int count = 0;
        public void TransientOperation(string msg)
        {
            Console.WriteLine("In TransientOperation with " + msg);
            count++;
            if (count <= 2)
            {
                throw new OperationTransientException("OperationTransientException");
            }
        }
    }
}