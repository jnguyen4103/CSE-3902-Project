/*using System.Xml;
namespace BlankMonoGameProject.LevelContentXML
{
    public class EagleXMLHandler
    {
        XmlReader reader;
        public void LoadLevel(string filename)
        {
            reader = XmlReader.Create(filename);
            while (reader.Read())
            {
                if(reader.NodeType.Equals(XmlNodeType.Element))
                {
                    switch (reader.Name)
                    {
                        case "Item":
                            break;
                        case "NPC":
                            break;
                        case "BlockCollider":
                            break;
                        case "Door":
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
*/