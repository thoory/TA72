using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TA72.Models;

namespace TA72.Controllers
{
    class NetworkController : INotifyPropertyChanged
    {
        public NetworkController()
        {
            ScanNotRunning = true;
            Network = new Network();
            GetHostIp();
        }

        #region Properties
        public Network Network { get; set; }
        public List<int> _portScannedList;

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

        public List<int> PortScannedList
        {
            get { return _portScannedList; }
            set
            {
                _portScannedList = value;
                RaisePropertyChanged("PortScannedList");
            }
        }

        public List<int> PortList
        {
            get { return Network.PortList; }
            set
            {
                Network.PortList = value;
                RaisePropertyChanged("PortScan");
            }
        }

        public List<int> _portToScan = new List<int> { 20, 21, 22, 23, 80, 8080 };
        public List<int> PortToScan
        { 
            get { return _portToScan; }
            set
            {
                _portScannedList = value;
                RaisePropertyChanged("PortToScan");
            }
        }

        private bool _ScanNotRunning;
        public bool ScanNotRunning
        {
            get { return _ScanNotRunning; }
            set 
            {
                _ScanNotRunning = value;
                RaisePropertyChanged("ScanNotRunning");
            }
        }
        #endregion

        #region Public Funtions
        public IPAddress StringtoIp(string ip)
        {
            IPAddress address;
            if (IPAddress.TryParse(ip, out address))
            {
                switch (address.AddressFamily)
                {
                    case AddressFamily.InterNetwork:
                        return IPAddress.Parse(ip);
                    case AddressFamily.InterNetworkV6:
                        return null;
                    default:
                        return null;
                }
            }
            return null;
        }
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

                IpHostList = list;
                IpHost = list.First();
            }
        }

        public async Task<bool> NetworkScanAsync()
        {
            ScanNotRunning = false;
            CalculIpRange();
            var result = await PingNetworkRange();
            ScanNotRunning = true;
            return result;
        }

        public async Task PortScan(IPAddress ipAddr)
        {
            ScanNotRunning = false;

            List<Task<int>> portListTask = new List<Task<int>>();
            List<int> portList = new List<int>();

            await Task.Run(() => {
                foreach (int port in PortToScan)
                {
                    portListTask.Add(PortConnection(ipAddr, port));
                }

                Task.WaitAll(portListTask.ToArray());

                foreach (var port in portListTask)
                {
                    if (port.Result != 0)
                        portList.Add(port.Result);
                }
            });
            
            PortScannedList = portList;
            ScanNotRunning = true;
        }

        public async Task<int> PortConnection(IPAddress ipAddr, int port)
        {
            int res = 0;
            await Task.Run(() =>
            {
                using (TcpClient Scan = new TcpClient())
                {
                    try
                    {
                        Scan.Connect(ipAddr, port);
                        res = port;
                    }
                    catch
                    {
                        System.Diagnostics.Trace.WriteLine($"[{port}] | Close");
                    }
                }
            });
            return res;
        }

        public Task<PingReply> Ping(IPAddress ip)
        {
            var tcs = new TaskCompletionSource<PingReply>();
            Ping ping = new Ping();
            ping.PingCompleted += (obj, sender) =>
            {
                tcs.SetResult(sender.Reply);
            };
            ping.SendAsync(ip, new object());
            return tcs.Task;
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

        private async Task<bool> PingNetworkRange()
        {
            bool result = false;
            var current = IpStart.GetAddressBytes();
            var end = IpEnd.GetAddressBytes();
            List<Task<PingReply>> pingTasks = new List<Task<PingReply>>();
            List<IPAddress> list = new List<IPAddress>();

            await Task.Run(() => {
                foreach (byte secondPart in Enumerable.Range(current[1], end[1] - current[1] + 1))
                {
                    foreach (byte thirdPart in Enumerable.Range(current[2], end[2] - current[2] + 1))
                    {
                        foreach (byte fourthPart in Enumerable.Range(current[3], end[3] - current[3] + 1))
                        {
                            var ip = new IPAddress(new byte[] { current[0], secondPart, thirdPart, fourthPart });
                            pingTasks.Add(Ping(ip));
                        }
                    }
                }

                Task.WaitAll(pingTasks.ToArray());

                foreach (var pingTask in pingTasks)
                {
                    if(pingTask.Result.Status == IPStatus.Success)
                        list.Add(pingTask.Result.Address);
                }
            });
            IpFoundList = list;
            return result;
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