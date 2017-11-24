using System.Xml.Serialization;
using System.IO;
using UnityEngine;
//Class used to create Xml File
public static class XMLFilHelper {

    // Create an Xml file of object type
    public static void CreateXmlFile(string path, object file)
    {
        using (var stream = new FileStream(path, FileMode.Create))
        {
            var serializer = new XmlSerializer(file.GetType());
            serializer.Serialize(stream, file);
            stream.Close();
        }
    }

    public static void DeleteFile(string path)
    {
        File.Delete(path);
    }

    // Load Xml file of the desired type
    public static object LoadFile(string path, System.Type fileType)
    {
        using (var stream = new FileStream(path, FileMode.Open))
        {
            var serializer = new XmlSerializer(fileType);
            var file = serializer.Deserialize(stream);
            stream.Close();
            return file;
        }
    }
}
