using SharpEval.Core.IO;

namespace SharpEval.Tests
{
    [TestFixture]
    internal class TestFunctionDocumentationProvider
    {
        private FunctionDocumentationProvider _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new FunctionDocumentationProvider();
        }

        [Test]
        public void TestDocumentationLoad()
        {
            Assert.That(_sut.GetDocumentations().Count > 0, Is.True);
        }

        [Test]
        public void TestIgnoredNames()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_sut.GetDocumentations().ContainsKey("ToString"), Is.False);
                Assert.That(_sut.GetDocumentations().ContainsKey("GetHashCode"), Is.False);
                Assert.That(_sut.GetDocumentations().ContainsKey("Equals"), Is.False);
                Assert.That(_sut.GetDocumentations().ContainsKey("GetType"), Is.False);
            });
        }

        private List<string>? GetDocumentation(string itemName)
        {
            return _sut.GetDocumentations()
                .Where(x => x.Key == itemName)
                .Select(x => x.Value)
                .FirstOrDefault();
        }

        [TestCase("Pi")]
        [TestCase("E")]
        [TestCase("Date")]
        [TestCase("Gcd")]
        [TestCase("Count")]
        [TestCase("Average")]
        [TestCase("Randomize")]
        public void TestDocumentation(string itemName)
        {
            var keys = _sut.GetDocumentations().Select(x => x.Key);
            var document = GetDocumentation(itemName);
            Assert.Multiple(() =>
            {
                Assert.That(document, Is.Not.Null);
                Assert.That(document, Is.Not.Empty);
            });
        }
    }
}