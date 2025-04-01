using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace projekt
{
    public class DataManager
    {
        private string filePath;

        public DataManager(string path)
        {
            filePath = path;
        }

        public void SaveData(List<Measurement> measurements)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(measurements, Formatting.Indented));
        }

        public List<Measurement> LoadData()
        {
            if (!File.Exists(filePath)) return new List<Measurement>();
            return JsonConvert.DeserializeObject<List<Measurement>>(File.ReadAllText(filePath)) ?? new List<Measurement>();
        }
    }
}