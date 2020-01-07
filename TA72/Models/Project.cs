using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    class Project
    {
        public string name { get; set; }
        public string desc { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime lastupdate { get; set; }
        public String path { get; set; }

        public Project(string name, string desc)
        {
            this.name = name;
            this.desc = desc;
            this.creationDate = DateTime.Now;
            this.lastupdate = DateTime.Now;
        }

        public void save()
        {
            Serializer serializer = new Serializer();
            serializer.save(this);
        }

        public override bool Equals(object obj)
        {
            return obj is Project project &&
                   name == project.name &&
                   desc == project.desc &&
                   creationDate == project.creationDate &&
                   lastupdate == project.lastupdate;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(name, desc, creationDate, lastupdate);
        }

        public override string ToString()
        {
            return "Projet " + this.name + ", creation: " + this.creationDate + " dernière update: " + this.lastupdate + ", " + this.desc;
        }
    }
}
