using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class NetworkController
    {
        public NetworkController() { }

        public String GetIp(Network network)
        {
            return network.ip.ToString();
        }
        public void SetIp(Network network, String ip)
        {
            if (this.CheckIpV4(ip) == true)
                network.ip = IPAddress.Parse(ip);
        }

        public bool CheckIpV4(string ip)
        {
            if (IPAddress.TryParse(ip, out IPAddress address))
                return true;
            return false;
        }

        public void IpRange(Network network, IPAddress ip)
        {
            int bits = 25;

            uint mask = ~(uint.MaxValue >> bits);

            byte[] ipBytes = ip.GetAddressBytes();

            //Ne va pas jusqu'au broadcast
            byte[] maskBytes = BitConverter.GetBytes(mask).Reverse().ToArray();

            byte[] startIPBytes = new byte[ipBytes.Length];
            byte[] endIPBytes = new byte[ipBytes.Length];

            for (int i = 0; i < ipBytes.Length; i++)
            {
                startIPBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
                endIPBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]);
            }

            network.ipStart = new IPAddress(startIPBytes);
            network.ipEnd = new IPAddress(endIPBytes);
        }

        public void PortScan(IPAddress ipAddr )
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

        public void PingNetworkRange(Network network, Project proj)
        {
            var current = network.ipStart.GetAddressBytes();
            var end = network.ipEnd.GetAddressBytes();

            EquipementController equipCtrl = new EquipementController();

            foreach (byte secondPart in Enumerable.Range(current[1], end[1] - current[1] + 1))
            {
                foreach (byte thirdPart in Enumerable.Range(current[2], end[2] - current[2] + 1))
                {
                    foreach (byte fourthPart in Enumerable.Range(current[3], end[3] - current[3] + 1))
                    {
                        var ip = new IPAddress(new byte[] { current[0], secondPart, thirdPart, fourthPart });
                        System.Diagnostics.Trace.WriteLine(ip.ToString());
                        if (this.Ping(ip))
                        {
                            Equipement equip = equipCtrl.Create(ip.ToString(), proj);
                            equipCtrl.SetIp(equip, ip.ToString());
                            network.devices.Add(equip);
                        }
                    }
                }
            }
        }

        public bool Ping(IPAddress ip)
        {
            Ping ping = new Ping();
            PingReply pingReply;

            try
            {
                pingReply = ping.Send(ip.ToString(), 5);
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
    }
}
