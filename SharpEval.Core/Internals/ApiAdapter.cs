using SharpEval.Webservices;

namespace SharpEval.Core.Internals;

internal class ApiAdapter
{
    private readonly IApiClient _apiClient;

    public ApiAdapter(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public double GetExchange(double ammount, string from, string to)
    {
        var rates = _apiClient.GetCurrencyRates().GetAwaiter().GetResult();
        if (rates == null)
            throw new InvalidOperationException("Api returned no data");

        var exchangeTable = rates.Cube.Cubes.Cube.ToDictionary(x => x.Currency, x => (double)x.Rate, StringComparer.OrdinalIgnoreCase);
        exchangeTable.Add("EUR", 1d);

        if (!exchangeTable.ContainsKey(from))
            throw new InvalidOperationException($"Unknown currency: {from} Valid currencies: {string.Join(',', exchangeTable.Keys)}");

        if (!exchangeTable.ContainsKey(to))
            throw new InvalidOperationException($"Unknown currency: {from} Valid currencies: {string.Join(',', exchangeTable.Keys)}");

        return ammount / exchangeTable[from] * exchangeTable[to];
    }
}
