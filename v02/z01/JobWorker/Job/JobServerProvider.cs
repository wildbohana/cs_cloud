using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class JobServerProvider : ISum
    {
        NetTcpBinding binding = new NetTcpBinding();
        private string internalEndpointName = "InternalRequest";

        public int DoCalculus(int to)
        {
            // Šta je ovo?
            binding.TransactionFlow = true;

            Trace.WriteLine(String.Format("DoCalculus method called - interval [1,{0}]", to.ToString()), "Information");

            // All internal endpoints of all worker role processes not including this worker role process
            List<EndpointAddress> internalEndpoints =
                RoleEnvironment.Roles[RoleEnvironment.CurrentRoleInstance.Role.Name].Instances.Where(instance =>
                instance.Id != RoleEnvironment.CurrentRoleInstance.Id).Select(process =>
                new EndpointAddress(String.Format("net.tcp://{0}/{1}", process.InstanceEndpoints[internalEndpointName].IPEndpoint.ToString(), internalEndpointName))).ToList();

            //// Ista stvar, samo malo više razumljivo
            //List<EndpointAddress> internalEndpoints = new List<EndpointAddress>();
            //foreach (RoleInstance instance in RoleEnvironment.Roles[RoleEnvironment.CurrentRoleInstance.Role.Name].Instances)
            //{
            //    if (instance.Id != RoleEnvironment.CurrentRoleInstance.Id)
            //    {
            //        internalEndpoints.Add(new EndpointAddress(String.Format("net.tcp://{0}/{1}", instance.InstanceEndpoints[internalEndpointName].IPEndpoint.ToString(), internalEndpointName)));
            //    }
            //}

            int numOfInstances = internalEndpoints.Count;
            Task<int>[] tasks = new Task<int>[numOfInstances];

            // Podinterval [a,b]
            int totalSum = 0;
            int a;
            int b = 0;
            int gap = to / numOfInstances;
            int remainder = to % numOfInstances;

            for (int i = 0; i < numOfInstances; i++)
            {
                int index = i;
                a = b + 1;
                b += gap;
                if (remainder > 0)
                {
                    b++;
                    remainder--;
                }

                int a2 = a;
                int b2 = b;

                Trace.WriteLine(String.Format("Calling node at: {0}", internalEndpoints[i].ToString()), "Information");

                Task<int> calculatePartialSum = new Task<int>(() =>
                {
                    IPartialJob proxy = new ChannelFactory<IPartialJob>(binding, internalEndpoints[index]).CreateChannel();
                    return proxy.DoSum(a2, b2);
                });

                calculatePartialSum.Start();
                tasks[index] = calculatePartialSum;
            }

            return totalSum;
        }
    }
}
