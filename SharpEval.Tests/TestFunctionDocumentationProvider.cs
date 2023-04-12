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
            Assert.That(_sut.Documentation.Count > 0, Is.True);
        }

        [Test]
        public void TestIgnoredNames()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Documentation.ContainsKey("ToString"), Is.False);
                Assert.That(_sut.Documentation.ContainsKey("GetHashCode"), Is.False);
                Assert.That(_sut.Documentation.ContainsKey("Equals"), Is.False);
                Assert.That(_sut.Documentation.ContainsKey("GetType"), Is.False);
            });
        }
    }
}
