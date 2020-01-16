using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    abstract class Module
    {
        public string Name { get; internal set; }
        public int PosX { get; internal set; }
        public int PosY { get; internal set; }
        public int ImageId { get; internal set; }

        public Module(string name, int posX, int posY, int imageId)
        {
            this.Name = name;
            this.PosX = posX;
            this.PosY = posY;
            this.ImageId = imageId;
        }

        public Module(string name)
        {
            this.Name = name;
        }
    }
}
