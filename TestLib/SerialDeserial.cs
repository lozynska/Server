using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestLib
{
   public class SerialDeserial
    {
        public T Deserialize<T>(String fileName) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (StreamReader sr = new StreamReader (fileName))
            {
                return (T)ser.Deserialize(sr);
            }
        }
        public void Serialize<T>(T ObjectToSerialize, String fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                xmlSerializer.Serialize(writer, ObjectToSerialize);
            }
        }
    }
}
