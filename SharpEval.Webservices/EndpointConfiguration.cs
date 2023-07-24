namespace SharpEval.Webservices;
public sealed class EndpointConfiguration
{
    public string EuropeCentralBank { get; init; }

    public EndpointConfiguration()
    {
        EuropeCentralBank = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
    }
}
