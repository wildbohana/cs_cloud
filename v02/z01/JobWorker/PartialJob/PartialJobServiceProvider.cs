using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class PartialJobServiceProvider : IPartialJob
    {
        public int DoSum(int from, int to)
        {
            // Koristi se Trace za ispis, ne Console!
            Trace.WriteLine(String.Format("DoSum method called - interval [{0},{1}]", from, to), "Information");

            int sum = 0;
            for (int i = from; i <= to; i++)
            {
                sum += i;
            }

            return sum;
        }
    }
}
