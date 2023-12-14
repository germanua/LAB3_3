using System.Runtime.Serialization.Formatters.Binary;

namespace BusinessLogicLayer
{
#pragma warning disable SYSLIB0011
    public class BinarySerializer<T> where T : class
    {
        private string _fileName;
        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        public BinarySerializer(string fileName)
        {
            _fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using (StreamWriter w = File.AppendText(_fileName))
            {
                // treba
            }
        }

        public void Save(List<T> obj)
        {
            using (FileStream fileStream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fileStream, obj);
            }
        }

        public List<T> Load()
        {
            List<T> deserialized;
            using (FileStream fileStream = new FileStream(_fileName, FileMode.Open))
            {
                deserialized = (List<T>)_formatter.Deserialize(fileStream);
            }
            return deserialized;
        }
    }
}
