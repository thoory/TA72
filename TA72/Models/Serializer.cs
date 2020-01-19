using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using TA72.Controllers;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TA72.Models
{
    class Serializer
    {
        public Serializer() { }

        public string Save(Project p)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = p.Name;
            dlg.Filter = "json (*.json)|*.json"; ;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                return Save(p, dlg.FileName);
            }
            return null;
        }

        public string Save(Project p, string path)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPAddressConverter());
            settings.Converters.Add(new IPEndPointConverter());
            settings.Formatting = Formatting.Indented;

            string json = JsonConvert.SerializeObject(p, settings);
            File.WriteAllText(path, json);
            return path;
        }

        public void Load(ProjectController projectController)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new IPAddressConverter());
                settings.Converters.Add(new IPEndPointConverter());
                settings.Formatting = Formatting.Indented;

                
                String sourceFile = File.ReadAllText(openFileDialog.FileName);
                projectController.Project = JsonConvert.DeserializeObject<Project>(sourceFile, settings);
                projectController.Path = openFileDialog.FileName;
            }
        }
    }

    //Added to serialize IP
    class IPAddressConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPAddress));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;
            return IPAddress.Parse((string)reader.Value);
        }
    }

    class IPEndPointConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPEndPoint));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IPEndPoint ep = (IPEndPoint)value;
            JObject jo = new JObject();
            jo.Add("Address", JToken.FromObject(ep.Address, serializer));
            jo.Add("Port", ep.Port);
            jo.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            IPAddress address = jo["Address"].ToObject<IPAddress>(serializer);
            int port = (int)jo["Port"];
            return new IPEndPoint(address, port);
        }
    }
}
