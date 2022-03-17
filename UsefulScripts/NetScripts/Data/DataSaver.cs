using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace UsefulScripts.NetScripts.Data
{
    public static class DataSaver
    {
        private static JsonSerializerSettings Settings => new JsonSerializerSettings(){ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
        
        private static void SaveData(string fname, string data)
        {
            using (var fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                fs.SetLength(0);//Очистка файла
                using (var writer = new StreamWriter(fs))
                    writer.Write(data);
            }
        }
        private static string LoadData(string fname)
        {
            using (var fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var writer = new StreamReader(fs))
                {
                    var text = writer.ReadToEnd();
                    return text != "" ? text : "";
                }
            }
        }
        
        public static void Save(string fname, object data, bool brettyPrint = false)
        {
            SaveF(Path.Combine(Application.persistentDataPath, fname), data, brettyPrint);
        }

        public static T Load<T>(string fname)
        {
            return LoadF<T>(Path.Combine(Application.persistentDataPath, fname));
        }
        public static object Load(string fname, Type type)
        {
            return LoadF(Path.Combine(Application.persistentDataPath, fname), type);
        }
        
        public static void SaveF(string fname, object data, bool brettyPrint = false)
        {
            SaveData(fname, Serialize(data, brettyPrint));
        }

        public static T LoadF<T>(string fname)
        {
            var text = LoadData(fname);
            return text != "" ? Deserialize<T>(text) : default;
        }
        public static object LoadF(string fname, Type type)
        {
            var text = LoadData(fname);
            return text != "" ? Deserialize(text, type) : default;
        }

        public static string Serialize(object data, bool brettyPrint = false)
        {
            return JsonConvert.SerializeObject(data, brettyPrint ? Formatting.Indented : Formatting.None, Settings);
        }

        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, Settings);
        }
        public static object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type, Settings);
        }
    }
}
