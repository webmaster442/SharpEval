using SharpEval.Webservices.Ecb;

namespace SharpEval.Webservices;

public interface IApiClient
{
    Task<Envelope> GetCurrencyRates();
}