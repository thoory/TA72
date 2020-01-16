using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TA72.Models
{
    class Equipement : Module
    {
        public string Type { get; internal set; }
        public string Os { get; internal set; }
        public string Version { get; internal set; }
        public IPAddress Ip { get; internal set; }
        public string Mac { get; internal set; }
        public bool IsActive { get; internal set; }
        public DateTime LastUpdate { get; internal set; }

        public Equipement(string name) : base(name)
        {
            this.Name = name;
            this.LastUpdate = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            return obj is Equipement equipement &&
                   base.Equals(obj) &&
                   Type == equipement.Type &&
                   Os == equipement.Os &&
                   Version == equipement.Version &&
                   Ip == equipement.Ip &&
                   Mac == equipement.Mac;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Type, Os, Version, Ip, Mac);
        }
    }
}
