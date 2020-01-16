using System;
using System.Collections.Generic;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    abstract class ModuleController
    {
        public ModuleController() { }

        public void Create() { }
        public void Delete() { }

        public void SetName(Module module, String name)
        {
            module.Name = name;
        }
        public String GetName(Module module)
        {
            return module.Name;
        }

        public void SetPosX(Module module, int posX)
        {
            module.PosX = posX;
        }
        public int GetPosX(Module module)
        {
            return module.PosX;
        }

        public void SetPosY(Module module, int posY)
        {
            module.PosY = posY;
        }
        public int GetPosY(Module module)
        {
            return module.PosY;
        }

        public void SetImageId(Module module, int imageId)
        {
            module.PosY = imageId;
        }
        public int GetImageId(Module module)
        {
            return module.ImageId;
        }

    }
}
