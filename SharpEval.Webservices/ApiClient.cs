﻿using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

using SharpEval.Webservices.Ecb;

namespace SharpEval.Webservices
{
    public sealed class ApiClient : IApiClient
    {
        private readonly Dictionary<Uri, CacheEntry> _cache;
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

        private Dictionary<Uri, CacheEntry> LoadCache()
        {
            if (File.Exists(_cacheFile))
            {
                using (var file = File.OpenRead(_cacheFile))
                {
                    try
                    {
                        var deserialized = JsonSerializer.Deserialize<Dictionary<Uri, CacheEntry>>(file, _jsonOptions);
                        if (deserialized != null)
                        {
                            return deserialized;
                        }
                    }
                    catch (Exception) 
                    {
                        return new Dictionary<Uri, CacheEntry>();
                    }
                }
            }
            return new Dictionary<Uri, CacheEntry>();
        }

        private void UpdateCache(Uri uri, string result, DateTime endDate)
        {
            _cache[uri] = new CacheEntry(result, endDate);
            using (var file = File.Create(_cacheFile))
            {
                JsonSerializer.Serialize(file, _cache, _jsonOptions);
            }
        }

        private async ValueTask<string> CallEndpoint(Uri endpoint, TimeSpan validityRange)
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
            string xml = await CallEndpoint(new Uri(Endpoints.EcbEndpoint), TimeSpan.FromHours(24));
            XmlSerializer xs = new(typeof(Envelope));
            using (XmlReader xmlReader = new XmlTextReader(xml))
            {
                return xs.Deserialize(xmlReader) as Ecb.Envelope
                    ?? throw new InvalidOperationException("Can't deserialize response");
            }
        }
    }
}