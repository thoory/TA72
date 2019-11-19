using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    class Equipement : Object
    {
        public String type { get; private set; }
        public String os { get; private set; }
        public String version { get; private set; }
        public String ip { get; private set; }
        public String mac { get; private set; }
        public bool isActive { get; private set; }
        public DateTime lastUpdate { get; private set; }

        public Equipement(int id, string name, int posX, int posY, int imageId, string type, string os, string version, string ip, string mac, bool isActive, DateTime lastUpdate) : base(id, name, posX, posY, imageId)
        {
            this.id = id;
            this.name = name;
            this.posX = posX;
            this.posY = posY;
            this.imageId = imageId;
            this.type = type;
            this.os = os;
            this.version = version;
            this.ip = ip;
            this.mac = mac;
            this.isActive = isActive;
            this.lastUpdate = lastUpdate;
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
