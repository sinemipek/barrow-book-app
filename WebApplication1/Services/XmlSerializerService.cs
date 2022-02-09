using System;
using System.IO;
using System.Xml.Serialization;


namespace BarrowBookApp.Services
{
    public class XmlSerializerService : IXmlSerializerService
    {
        public T Deserialize<T>(string path) where T : class
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (FileStream stream = File.OpenRead(path))
                {
                    var dezerializedList = (T)serializer.Deserialize(stream);
                    return dezerializedList;
                }
            }
            catch (Exception e)
            {
                return null;
                //TODO Log exception
            }
        }
    }
}
