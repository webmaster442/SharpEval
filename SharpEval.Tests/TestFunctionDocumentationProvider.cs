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
    }
}
