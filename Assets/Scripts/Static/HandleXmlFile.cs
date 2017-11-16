using System.Xml.Serialization;
using System.IO;

//Class used to create Xml File
public static class HandleXmlFile {

    // Create an Xml file of object type
    public static void CreateXmlFile(string path, object file)
    {
        var serializer = new XmlSerializer(file.GetType());
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, file);
        stream.Close();
    }

    
    public static void DeleteFile(string path)
    {
        File.Delete(path);
    }

    // Load Xml file of the desired type
    public static object LoadFile(string path, System.Type type)
    {
        var serializer = new XmlSerializer(type);
        var stream = new FileStream(path, FileMode.Open);
        var file = serializer.Deserialize(stream);
        stream.Close();
        return file;
    }
}
