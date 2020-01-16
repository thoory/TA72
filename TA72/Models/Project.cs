﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Lastupdate { get; set; }
        public string Path { get; set; }
        public List<Equipement> Equipements { get; set; }

        public Project(string name, string desc)
        {
            this.Name = name;
            this.Description = desc;
            this.CreationDate = DateTime.Now;
            this.Lastupdate = DateTime.Now;
            this.Equipements = new List<Equipement>();
        }

        public override bool Equals(object obj)
        {
            return obj is Project project &&
                   Name == project.Name &&
                   Description == project.Description &&
                   CreationDate == project.CreationDate &&
                   Lastupdate == project.Lastupdate;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, CreationDate, Lastupdate);
        }

        public override string ToString()
        {
            return "Projet " + this.Name + ", creation: " + this.CreationDate + " dernière update: " + this.Lastupdate + ", " + this.Description;
        }
    }
}
