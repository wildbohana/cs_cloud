using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class JobServiceProvider : ISum
    {
        public int DoCalculus(int to)
        {
            if (to < 0) 
                return 0;
            else
                return (to * (to + 1) / 2);
        }
    }
}
