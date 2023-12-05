using System.Reflection;
using System.Text;

namespace BusinessLogicLayer
{
    public class CustomSerializer<T> where T : class, new()
    {
        private readonly string _fileName;

        public CustomSerializer(string fileName)
        {
            _fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using StreamWriter w = File.AppendText(_fileName);
        }

        public List<T> Load()
        {
            List<T> reading = new List<T>();
            using (var fileStream = File.OpenRead(_fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256))
            {
                string line;
                var entity = new StringBuilder();
                while ((line = streamReader.ReadLine()) != null)
                {
                    entity.Append(line);
                    if (line.EndsWith("?>"))
                    {
                        var serializedData = entity.ToString();
                        // Change the Load method to use Deserialize instead of DeSerialize
                        reading.Add(Deserialize(serializedData));
                        entity.Clear();
                    }
                }
            }
            return reading;
        }

        public void Save(List<T> listToSave)
        {
            using StreamWriter writetext = new(_fileName);
            foreach (var obj in listToSave)
            {
                // Change the Save method to use Serialize instead of your custom serialization
                writetext.WriteLine(Serialize(obj));
            }
        }

        public static string Serialize(object obj)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.Append("<?");
            var myType = obj.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (var prop in props)
            {
                var propValue = prop.GetValue(obj, null);
                sb.AppendLine();
                sb.Append(@"    [" + prop.Name + "=" + propValue + "]");
            }
            sb.AppendLine();
            sb.Append("?>");
            return sb.ToString();
        }
        public static T Deserialize(string serializeData)
        {
            var deserializedObject = new T();

            var properties = ExtractValuesFromData(serializeData);
            foreach (var property in properties)
            {
                var propInfo = deserializedObject.GetType().GetProperty(property.PropertyName);
                propInfo?.SetValue(deserializedObject, Convert.ChangeType(property.Value, propInfo.PropertyType), null);
            }

            return deserializedObject;
        }

        public static List<string> ExtractData(
            string text, string startString = "<?", string endString = "?>", bool raw = false)
        {
            var matched = new List<string>();
            var exit = false;
            while (!exit)
            {
                var indexStart = text.IndexOf(startString, StringComparison.Ordinal);
                var indexEnd = text.IndexOf(endString, StringComparison.Ordinal);
                if (indexStart != -1 && indexEnd != -1)
                {
                    if (raw)
                        matched.Add("<?" + text.Substring(indexStart + startString.Length,
                                        indexEnd - indexStart - startString.Length) + "?>");
                    else
                        matched.Add(text.Substring(indexStart + startString.Length,
                            indexEnd - indexStart - startString.Length));
                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                {
                    exit = true;
                }
            }
            return matched;
        }

        public static List<Data> ExtractValuesFromData(string text)
        {
            var listOfData = new List<Data>();
            var allData = ExtractData(text, "[", "]");
            foreach (var data in allData)
            {
                var pName = data.Substring(0, data.IndexOf("=", StringComparison.Ordinal));
                var pValue = data.Substring(data.IndexOf("=", StringComparison.Ordinal) + 1);
                listOfData.Add(new Data { PropertyName = pName, Value = pValue });
            }
            return listOfData;
        }
        

        public struct Data
        {
            public string PropertyName;
            public string Value;
        }
    }
}
