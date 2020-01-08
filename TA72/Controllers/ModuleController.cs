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
            module.name = name;
        }
        public String GetName(Module module)
        {
            return module.name;
        }

        public void SetPosX(Module module, int posX)
        {
            module.posX = posX;
        }
        public int GetPosX(Module module)
        {
            return module.posX;
        }

        public void SetPosY(Module module, int posY)
        {
            module.posY = posY;
        }
        public int GetPosY(Module module)
        {
            return module.posY;
        }

        public void SetImageId(Module module, int imageId)
        {
            module.posY = imageId;
        }
        public int GetImageId(Module module)
        {
            return module.imageId;
        }

    }
}
