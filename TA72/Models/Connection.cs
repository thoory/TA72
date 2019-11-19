using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    abstract class Connection
    {
        public String protocole { get; set; }
        public int port { get; set; }
    }
}
