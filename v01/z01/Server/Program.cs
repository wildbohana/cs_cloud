using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                new JobServer();
                Console.WriteLine("WCF Service is running...");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error starting WCF service!" + e.Message);
            }

            Console.ReadLine();
        }
    }
}
