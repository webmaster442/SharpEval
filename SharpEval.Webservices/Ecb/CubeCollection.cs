using System.Xml.Serialization;

namespace SharpEval.Webservices.Ecb;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
public class CubeCollection
{
    public CubeCollection()
    {
        Cube = Array.Empty<CubeItem>();
    }

    [XmlElement("Cube")]
    public CubeItem[] Cube { get; set; }

    [XmlAttribute("time", DataType = "date")]
    public DateTime Date { get; set; }
}
