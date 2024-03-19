using Contracts;
using StudentService_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoker
{
    public class Program
    {
        private static IStudent proxy;

        public static void Connect()
        {
            var binding = new NetTcpBinding();
            ChannelFactory<IStudent> factory = new ChannelFactory<IStudent>(binding, new EndpointAddress("net.tcp://localhost:10100/InputRequest"));
            proxy = factory.CreateChannel();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Connect();

                    // Novi student
                    Console.WriteLine("Unesi novog studenta: ");
                    Console.Write("Ime: ");
                    string ime = Console.ReadLine();
                    Console.Write("Prezime: ");
                    string prezime = Console.ReadLine();
                    Console.Write("Indeks: ");
                    string indeks = Console.ReadLine();

                    proxy.AddStudent(indeks, ime, prezime);

                    Console.WriteLine("\nSvi studenti:");
                    List<Student> svi = proxy.RetrieveAllStudents();

                    foreach (Student s in svi)
                    {
                        Console.WriteLine(String.Format("STUDENT: {0} {1}", s.Name, s.LastName));
                    }

                    Console.WriteLine("\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error starting WCF service!" + e.Message);
                }
            }
        }
    }
}
