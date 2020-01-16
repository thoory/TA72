using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class NetworkController : INotifyPropertyChanged
    {
        public NetworkController()
        {
            Network = new Network();
            GetHostIp();
        }

        #region Properties
        public Network Network { get; set; }

        public IPAddress IpHost
        {
            get { return Network.IpHost; }
            set
            {
                Network.IpHost = value;
                RaisePropertyChanged("IpHost");
            }
        }

        public IPAddress IpStart
        {
            get { return Network.IpStart; }
            set
            {
                Network.IpStart = value;
                RaisePropertyChanged("IpStart");
            }
        }

        public IPAddress IpEnd
        {
            get { return Network.IpEnd; }
            set
            {
                Network.IpEnd = value;
                RaisePropertyChanged("IpEnd");
            }
        }

        public List<IPAddress> IpHostList
        {
            get { return Network.IpHostList; }
            set
            {
                Network.IpHostList = value;
                RaisePropertyChanged("IpHostList");
            }
        }

        public List<IPAddress> IpFoundList
        {
            get { return Network.IpFoundList; }
            set
            {
                Network.IpFoundList = value;
                RaisePropertyChanged("IpFoundList");
            }
        }
        #endregion

        #region Public Funtions
        public void GetHostIp()
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                IpHostList.Clear();
                List<IPAddress> list = new List<IPAddress>();
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        list.Add(ip);
                    }
                }
                IpHost = list.First();
                IpHostList = list;
            }
        }

        public void NetworkScan()
        {
            CalculIpRange();
            PingNetworkRange();
        }
        #endregion
        #region Private Functions

        private void CalculIpRange()
        {
            int bits = 25;
            uint mask = ~(uint.MaxValue >> bits);
            byte[] ipBytes = IpHost.GetAddressBytes();

            //Ne va pas jusqu'au broadcast
            byte[] maskBytes = BitConverter.GetBytes(mask).Reverse().ToArray();

            byte[] startIPBytes = new byte[ipBytes.Length];
            byte[] endIPBytes = new byte[ipBytes.Length];

            for (int i = 0; i < ipBytes.Length; i++)
            {
                startIPBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
                endIPBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]);
            }

            IpStart = new IPAddress(startIPBytes);
            IpEnd = new IPAddress(endIPBytes);
        }

        private void PortScan(IPAddress ipAddr)
        {
            //fonctionne mais prend du temps
            int[] ports = new int[] { 80, 8080, 21, 22 };
            foreach (int s in ports)
            {
                using (TcpClient Scan = new TcpClient())
                {
                    try
                    {
                        Scan.Connect(ipAddr, s);
                        System.Diagnostics.Trace.WriteLine($"[{s}] | OPEN");
                    }
                    catch
                    {
                        System.Diagnostics.Trace.WriteLine($"[{s}] | OPEN");
                    }
                }
            }
        }

        private void PingNetworkRange()
        {
            var current = IpStart.GetAddressBytes();
            var end = IpEnd.GetAddressBytes();
            List<IPAddress> list = new List<IPAddress>();

            foreach (byte secondPart in Enumerable.Range(current[1], end[1] - current[1] + 1))
            {
                foreach (byte thirdPart in Enumerable.Range(current[2], end[2] - current[2] + 1))
                {
                    foreach (byte fourthPart in Enumerable.Range(current[3], end[3] - current[3] + 1))
                    {
                        var ip = new IPAddress(new byte[] { current[0], secondPart, thirdPart, fourthPart });
                        System.Diagnostics.Trace.WriteLine(ip.ToString());
                        if (Ping(ip))
                        {
                            list.Add(ip);
                        }
                    }
                }
            }
            IpFoundList = list;
        }

        public static bool Ping(IPAddress ip)
        {
            Ping ping = new Ping();
            PingReply pingReply;

            try
            {
                pingReply = ping.Send(ip.ToString(), 3);
                if (pingReply != null &&
                    pingReply.Status == IPStatus.Success)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
    #endregion

        #region INotifyOrioertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}