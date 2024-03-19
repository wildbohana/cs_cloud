using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        private static ISum proxy;

        public static void Connect()
        {
            var binding = new NetTcpBinding();
            ChannelFactory<ISum> factory = new
            ChannelFactory<ISum>(binding, new
            EndpointAddress("net.tcp://localhost:10100/InputRequest"));

            proxy = factory.CreateChannel();
        }

        public static void Main(string[] args)
        {
            while(true)
            {
                try
                {
                    Connect();
                    int to = 0;

                    Console.Write("Enter number: ");
                    bool success = int.TryParse(Console.ReadLine(), out to);

                    if (!success)
                        continue;

                    int sum = proxy.DoCalculus(to);
                    Console.WriteLine($"Sum from 1 to {to} is: {sum}\n");

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error starting WCF service!" + e.Message);
                }
            }            
        }
    }
}
