
using SharpEval.Tests.Internals;
using SharpEval.Tests.Properties;

namespace SharpEval.Tests;

[TestFixture]
public class TestApiClient
{
    private ApiClient _sut;
    private LocalTestServer _localTestServer;

    [SetUp]
    public void Setup()
    {
        _localTestServer = new LocalTestServer(9871);
        _sut = new ApiClient(new EndpointConfiguration
        {
            EuropeCentralBank = "http://localhost:9871/ecb.xml"
        }, cacheEnabled: false);
        _localTestServer.AddHandler("/ecb.xml", "text/xml", () => Resources.EcbResponse);
        _localTestServer.Start();
    }

    [TearDown]
    public void TearDown()
    {
        _localTestServer.Dispose();
    }

    [Test]
    [Timeout(3000)]
    public async Task TestEcb()
    {
        var result = await _sut.GetCurrencyRates();
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Cube.Cubes.Cube.Length, Is.EqualTo(30));
        });
    }
}
