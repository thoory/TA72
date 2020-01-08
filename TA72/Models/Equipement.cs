using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    class Equipement : Module
    {
        public string type { get; internal set; }
        public string os { get; internal set; }
        public string version { get; internal set; }
        public string ip { get; internal set; }
        public string mac { get; internal set; }
        public bool isActive { get; internal set; }
        public DateTime lastUpdate { get; internal set; }

        public Equipement(string name) : base(name)
        {
            this.name = name;
            this.lastUpdate = DateTime.Now;
        }
        public Equipement(string name, int posX, int posY, int imageId, string type, string os, string version, string ip, string mac) : base(name, posX, posY, imageId)
        {
            this.name = name;
            this.posX = posX;
            this.posY = posY;
            this.imageId = imageId;
            this.type = type;
            this.os = os;
            this.version = version;
            this.ip = ip;
            this.mac = mac;
            this.lastUpdate = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            return obj is Equipement equipement &&
                   base.Equals(obj) &&
                   type == equipement.type &&
                   os == equipement.os &&
                   version == equipement.version &&
                   mac == equipement.mac;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), type, os, version, ip, mac, isActive, lastUpdate);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
