using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Save File")]
public class SaveFileTemplate {

    public int ButtonId;

    public string Name;

    public int Difficulty;

    public SaveFileTemplate()
    {

    }

    public SaveFileTemplate(string name, int difficulty, int buttonId)
    {
        Name = name;
        Difficulty = difficulty;
        ButtonId = buttonId;
    }

}
