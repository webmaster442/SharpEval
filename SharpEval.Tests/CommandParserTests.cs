using Moq;

using SharpEval.Webservices;

namespace SharpEval.Tests
{
    [TestFixture]
    internal class CommandParserTests
    {
        private Mock<ICommandReader> _commandReaderMock;
        private Mock<IResultWrtiter> _resultWriterMock;
        private Mock<IApiClient> _apiClientMock;
        private CommandParser _sut;
        private List<string> _commands;


        [SetUp]
        public void Setup()
        {
            _commandReaderMock = new Mock<ICommandReader>(MockBehavior.Strict);
            _resultWriterMock = new Mock<IResultWrtiter>(MockBehavior.Strict);
            _apiClientMock = new Mock<IApiClient>(MockBehavior.Strict);
            _commands = new List<string>();
            _commandReaderMock.SetupGet(x => x.InputLines).Returns(_commands);
            _resultWriterMock.Setup(x => x.Result(It.IsAny<string>()));
            _resultWriterMock.Setup(x => x.Error(It.IsAny<string>()));
            _resultWriterMock.Setup(x => x.Echo(It.IsAny<AngleSystem>(), It.IsAny<string>()));

            _sut = new CommandParser(_commandReaderMock.Object, _resultWriterMock.Object, _apiClientMock.Object);
        }

        [Test]
        public async Task RunEchoOff()
        {
            _commands.Clear();
            _commands.Add("$echo off");
            _commands.Add("$echo false");
            _commands.Add("3+2");

            await _sut.RunAsync();

            _resultWriterMock.Verify(x => x.Echo(It.IsAny<AngleSystem>(), It.IsAny<string>()), Times.Never);
            _resultWriterMock.Verify(x => x.Result(It.IsAny<string>()), Times.Once);
        }


        [Test]
        public async Task RunEchoOn()
        {
            _commands.Clear();
            _commands.Add("$echo on");
            _commands.Add("3+2");

            await _sut.RunAsync();

            _resultWriterMock.Verify(x => x.Echo(It.IsAny<AngleSystem>(), It.IsAny<string>()), Times.Once);
            _resultWriterMock.Verify(x => x.Result(It.IsAny<string>()), Times.Once);
        }


        [Test]
        public async Task RunModeChange()
        {
            _commands.Clear();
            _commands.Add("$mode rad");

            await _sut.RunAsync();

            Assert.That(_sut.Settings.CurrentAngleSystem, Is.EqualTo(AngleSystem.Rad));
        }

        [Test]
        public async Task Vars()
        {
            _commands.Clear();
            _commands.Add("var x = 11");
            _commands.Add("$vars");

            await _sut.RunAsync();

            _resultWriterMock.Verify(x => x.Result(It.Is<string>(x => x == "x = 11 //Int32")), Times.Once);
        }
    }
}
