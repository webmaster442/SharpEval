using System.Xml.Serialization;

namespace SharpEval.Webservices.Ecb;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
public class CubeItem
{
    public CubeItem()
    {
        Currency = string.Empty;
    }

    [XmlAttribute("currency")]
    public string Currency { get; set; }

    [XmlAttribute("rate")]
    public decimal Rate { get; set; }
}

