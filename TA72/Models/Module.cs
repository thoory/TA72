using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    abstract class Module
    {
        public string name { get; internal set; }
        public int posX { get; internal set; }
        public int posY { get; internal set; }
        public int imageId { get; internal set; }

        public Module(string name, int posX, int posY, int imageId)
        {
            this.name = name;
            this.posX = posX;
            this.posY = posY;
            this.imageId = imageId;
        }

        public Module(string name)
        {
            this.name = name;
        }
    }
}
