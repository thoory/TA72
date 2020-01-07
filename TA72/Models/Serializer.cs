using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TA72.Models
{
    class Serializer
    {
        public Serializer() { }

        public void save(Project p)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = p.name;
            dlg.Filter = "json (*.json)|*.json"; ;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                String jsonString = JsonConvert.SerializeObject(p);
                File.WriteAllText(dlg.FileName, jsonString);
            }
        }

        public Project load(String path)
        {
            String sourceFile = System.IO.File.ReadAllText(path);
            Project p = JsonConvert.DeserializeObject<Project>(sourceFile);

            return p;
        }
    }
}
