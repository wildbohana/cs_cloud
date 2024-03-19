using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class HealthMonitoring : IHealthMonitoring
    {
        public void IAmAlive()
        {
            Console.WriteLine("Worker role has invoked service");
        }
    }
}
