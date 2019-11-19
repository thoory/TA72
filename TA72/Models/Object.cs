using System;
using System.Collections.Generic;
using System.Text;

namespace TA72.Models
{
    class Object
    {
        public int id { get; set; }
        public String name { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public int imageId { get; set; }

        public Object(int id, string name, int posX, int posY, int imageId)
        {
            this.id = id;
            this.name = name;
            this.posX = posX;
            this.posY = posY;
            this.imageId = imageId;
        }

        public Object(int id, string name, int posX, int imageId)
        {
            this.id = id;
            this.name = name;
            this.posX = posX;
            this.imageId = imageId;
        }

        public override bool Equals(object obj)
        {
            return obj is Object @object &&
                   id == @object.id &&
                   name == @object.name &&
                   posX == @object.posX &&
                   posY == @object.posY &&
                   imageId == @object.imageId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name, posX, posY, imageId);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
