using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Controllers
{
    class AllDataContext
    {
        public ProjectController ProjCtrl{ get; set; }
        public NetworkController NetCtrl { get; set; }

        public AllDataContext()
        {
            ProjCtrl = new ProjectController();
            NetCtrl = new NetworkController();
        }
    }
}
