using System.Net.Http.Headers;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

using SharpEval.Webservices.Ecb;

namespace SharpEval.Webservices
{
    public sealed class ApiClient : IApiClient
    {
        private readonly Dictionary<string, CacheEntry> _cache;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _cacheFile;

        public ApiClient()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict,
                WriteIndented = false,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            _cacheFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SharpEval.Webcache.json");
            _cache = LoadCache();
        }

        private Dictionary<string, CacheEntry> LoadCache()
        {
            if (File.Exists(_cacheFile))
            {
                using (var file = File.OpenRead(_cacheFile))
                {
                    try
                    {
                        var deserialized = JsonSerializer.Deserialize<Dictionary<string, CacheEntry>>(file, _jsonOptions);
                        if (deserialized != null)
                        {
                            return deserialized;
                        }
                    }
                    catch (Exception) 
                    {
                        return new Dictionary<string, CacheEntry>();
                    }
                }
            }
            return new Dictionary<string, CacheEntry>();
        }

        private void UpdateCache(string uri, string result, DateTime endDate)
        {
            _cache[uri] = new CacheEntry(result, endDate);
            using (var file = File.Create(_cacheFile))
            {
                JsonSerializer.Serialize(file, _cache, _jsonOptions);
            }
        }

        private async ValueTask<string> CallEndpoint(string endpoint, TimeSpan validityRange)
        {
            using (var client = new HttpClient())
            {
                if (_cache.ContainsKey(endpoint)
                    && _cache[endpoint].EndDate >= DateTime.Now)
                {
                    return _cache[endpoint].Value;
                }
                var result = await client.GetStringAsync(endpoint);
                DateTime endDate = DateTime.Now + validityRange;
                UpdateCache(endpoint, result, endDate);
                return result;
            }
        }

        public async Task<Ecb.Envelope> GetCurrencyRates()
        {
            string xml = await CallEndpoint(Endpoints.EcbEndpoint, TimeSpan.FromHours(24));
            return DeserializeXml<Ecb.Envelope>(xml);
        }

        private static T DeserializeXml<T>(string xml) where T : class
        {
            XmlSerializer xs = new(typeof(T));

            using (StringReader reader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(reader))
                {
                    return xs.Deserialize(xmlReader) as T
                        ?? throw new InvalidOperationException("Can't deserialize response");
                }
            }
        }
    }
}
