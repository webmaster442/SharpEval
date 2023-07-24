using System.Xml.Serialization;

namespace SharpEval.Webservices.Ecb;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
[XmlRoot(Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref", IsNullable = false)]
public class CubeRoot
{
    [XmlElement("Cube")]
    public CubeCollection Cubes { get; set; }

    public CubeRoot()
    {
        Cubes = new CubeCollection();
    }
}

