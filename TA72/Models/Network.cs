using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TA72.Models
{
    class Network
    {
        public IPAddress IpHost { get; set; }
        public IPAddress IpStart { get; set; }
        public IPAddress IpEnd { get; set; }
        public List<IPAddress> IpHostList { get; set; }
        public List<IPAddress> IpFoundList { get; set; }

        public Network()
        {
            IpFoundList = new List<IPAddress>();
            IpHostList = new List<IPAddress>();
        }
    }
}