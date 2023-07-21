using System.Xml.Serialization;

namespace SharpEval.Webservices.Ecb;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://www.gesmes.org/xml/2002-08-01")]
public class EnvelopeSender
{
    public EnvelopeSender()
    {
        Name = string.Empty;
    }

    [XmlElement("name")]
    public string Name { get; set; }
}

