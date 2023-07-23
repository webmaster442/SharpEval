using Moq;

using SharpEval.Tests.Internals;
using SharpEval.Webservices;

namespace SharpEval.Tests
{
    [TestFixture]
    public class CommandParserSystemTests
    {
        private TestIO _testIO;
        private CommandParser _sut;
        private Mock<IApiClient> _apiClientMock;

        [SetUp]
        public void Setup()
        {
            _testIO = new TestIO();
            _apiClientMock = new Mock<IApiClient>(MockBehavior.Strict);
            _sut = new CommandParser(_testIO, _testIO, _apiClientMock.Object);
        }

        [TestCase("", "")]
        [TestCase("Vector(1, 3)", "x: 1 y: 3")]
        [TestCase("Vector(1, 3, 2)", "x: 1 y: 3 z: 2")]
        [TestCase("Complex(0, 1)", "0 + 1i\r\nr = 1 φ = 90")]
        [TestCase("Date(2000, 1, 1) - Date(1995, 1, 1)", "1826 days 0 hours 0 minutes 0 seconds\r\nYears (Aproximated): 4\r\nMonths (Aproximated): 59")]
        public async Task TestSingleLineResult(string input, string expected)
        {
            _testIO.SetInput(input);
            await _sut.RunAsync();
            var lastEvent = _testIO.Events.Pop();
            Assert.Multiple(() =>
            {
                Assert.That(lastEvent.EventType, Is.EqualTo(TestIO.EventType.Result));
                Assert.That(lastEvent.Result, Is.EqualTo(expected));
            });
        }

        [Test]
        public async Task TestEchoCommandOff()
        {
            _testIO.SetInput("$echo off", "3+2");
            await _sut.RunAsync();
            var lastEvent = _testIO.Events.Pop();
            Assert.Multiple(() =>
            {
                Assert.That(_testIO.HasEventType(TestIO.EventType.Echo), Is.False);
                Assert.That(lastEvent.Result, Is.EqualTo("5"));
            });
        }

        [Test]
        public async Task TestEchoCommandOn()
        {
            _testIO.SetInput("$echo on", "3+2");
            await _sut.RunAsync();
            var resultEvent = _testIO.Events.Pop();
            var echoEvent = _testIO.Events.Pop();
            Assert.Multiple(() =>
            {
                Assert.That(resultEvent.EventType, Is.EqualTo(TestIO.EventType.Result));    
                Assert.That(resultEvent.Result, Is.EqualTo("5"));
                Assert.That(echoEvent.EventType, Is.EqualTo(TestIO.EventType.Echo));
                Assert.That(echoEvent.Result, Is.EqualTo("3+2"));
            });
        }

        [Test]
        public async Task TestModeCommand()
        {
            _testIO.SetInput("$mode rad");
            await _sut.RunAsync();
            Assert.That(_sut.Settings.CurrentAngleSystem, Is.EqualTo(AngleSystem.Rad));
        }

        [Test]
        public async Task TestVarsCommand()
        {
            _testIO.SetInput("var x = 11", "$vars");
            await _sut.RunAsync();
            var result = _testIO.Events.Pop();
            Assert.Multiple(() =>
            {
                Assert.That(result.EventType, Is.EqualTo(TestIO.EventType.Result));
                Assert.That(result.Result, Is.EqualTo("x = 11 //Int32"));
            });
        }

    }
}
