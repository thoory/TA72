using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using TA72.Controllers;

namespace TA72.Models
{
    class Serializer
    {
        public Serializer() { }

        public string Save(Project p)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = p.Name;
            dlg.Filter = "json (*.json)|*.json"; ;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                String jsonString = JsonConvert.SerializeObject(p);
                File.WriteAllText(dlg.FileName, jsonString);
            }
            return dlg.FileName;
        }

        public string Save(Project p, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(p));
            return path;
        }

        public void Load(ProjectController projectController)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                String sourceFile = System.IO.File.ReadAllText(openFileDialog.FileName);
                projectController.Project = JsonConvert.DeserializeObject<Project>(sourceFile);
                projectController.Path = openFileDialog.FileName;
            }
        }
    }
}
