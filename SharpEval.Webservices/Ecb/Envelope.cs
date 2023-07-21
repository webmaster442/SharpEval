using System.Xml.Serialization;

namespace SharpEval.Webservices.Ecb;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://www.gesmes.org/xml/2002-08-01")]
[XmlRoot(Namespace = "http://www.gesmes.org/xml/2002-08-01", IsNullable = false)]
public class Envelope
{
    public Envelope()
    {
        Subject = string.Empty;
        Sender = new EnvelopeSender();
        Cube = new CubeRoot();
    }

    [XmlElement("subject")]
    public string Subject { get; set; }

    [XmlElement]
    public EnvelopeSender Sender { get; set; }

    [XmlElement(Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
    public CubeRoot Cube { get; set; }
}

