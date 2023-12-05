using System.Xml.Serialization;

namespace BusinessLogicLayer
{
    public class XmlSerializer<T> where T : class
    {
        private readonly string _fileName;
        private readonly XmlSerializer _serializer;

        public XmlSerializer(string fileName)
        {
            _fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using StreamWriter w = File.AppendText(_fileName);
            _serializer = new XmlSerializer(typeof(List<T>));
        }

        public List<T> Load()
        {
            using (FileStream fileStream = new(_fileName, FileMode.Open))
            {
                return (_serializer.Deserialize(fileStream) as List<T>) ?? new List<T>();
            }
        }

        public void Save(List<T> listToSave)
        {
            using (FileStream fileStream = new(_fileName, FileMode.OpenOrCreate))
            {
                _serializer.Serialize(fileStream, listToSave);
            }
        }
    }
}