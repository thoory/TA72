using System;
using System.Collections.Generic;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class ProjectController
    {
        public ProjectController(){}

        public Project create(string name, string desc) {
            Project p;

            p = new Project(name, desc);

            return p;
        }

        public String getName(Project p)
        {
            return p.name;
        }
        public String getDesc(Project p)
        {
            return p.desc;
        }

        public void setName(Project p, String name)
        {
            p.name = name;
            p.lastupdate = DateTime.Now;
        }
        public void setDesc(Project p, String desc)
        {
            p.desc = desc;
            p.lastupdate = DateTime.Now;
        }
        public void save(Project p)
        {
            p.save();
        }

        public Project load(String path)
        {
            Serializer serializer = new Serializer();
            Project p = serializer.load(path);

            return p;
        }
    }
}
