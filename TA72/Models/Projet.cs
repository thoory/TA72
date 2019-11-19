using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    class Projet
    {
        public String name { get; set; }
        public String desc { get; set; }
        public DateTime lastupdate { get; set; }

        public Projet(string name, string desc, DateTime lastupdate)
        {
            this.name = name;
            this.desc = desc;
            this.lastupdate = lastupdate;
        }

        public override bool Equals(object obj)
        {
            return obj is Projet projet &&
                   name == projet.name &&
                   desc == projet.desc &&
                   lastupdate == projet.lastupdate;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(name, desc, lastupdate);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
