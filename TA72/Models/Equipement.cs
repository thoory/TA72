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
        public string MacAddress { get; internal set; }
        public string Notes { get; internal set; }
        public bool IsActive { get; internal set; }
        public DateTime LastUpdate { get; internal set; }

        public Equipement(string name) : base(name)
        {
            this.Name = name;
            this.LastUpdate = DateTime.Now;
        }
    }
}
