using System.IO;
using System.Text.Json;

namespace BusinessLogicLayer
{
    public class JsonSerializer<T> where T : class
    {
        private readonly string _fileName;

        public JsonSerializer(string fileName)
        {
            _fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using StreamWriter w = File.AppendText(_fileName);
        }

        public List<T> Load()
        {
            // Check if the file exists before attempting to load
            if (!File.Exists(_fileName))
            {
                return new List<T>();
            }

            using (FileStream fileStream = new(_fileName, FileMode.Open))
            {
                // Check if the file is empty before attempting to deserialize
                if (fileStream.Length == 0)
                {
                    return new List<T>();
                }

                return JsonSerializer.Deserialize<List<T>>(fileStream, new JsonSerializerOptions(JsonSerializerDefaults.Web))
                       ?? new List<T>();
            }
        }

        public void Save(List<T> listToSave)
        {
            using (FileStream fileStream = new(_fileName, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fileStream, listToSave);
            }
        }
    }
}