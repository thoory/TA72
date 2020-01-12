using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace TA72.Models
{
    class Network
    {
        public IPAddress ip { get; set; }
        public IPAddress ipStart { get; set; }
        public IPAddress ipEnd { get; set; }
        public List<Equipement> devices { get; set; }

        public Network()
        {
            this.devices = new List<Equipement>();
        }

        public IPAddress GetHostIp()
        {
            if(NetworkInterface.GetIsNetworkAvailable() == true)
            {
                List<IPAddress> ipList = new List<IPAddress>();
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipList.Add(ip);
                    }
                }

                IPAddress result = ipList.First();

                if (ipList.Count > 1)
                {
                    //TODO: ask to user
                }
                return result;
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
