using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEval.Tests
{
    internal class CommandParserFormattingTests : ICommandReader, IResultWrtiter
    {
        private string _intutBuffer;
        private string _output;
        private CommandParser _sut;

        public IEnumerable<string> InputLines
        {
            get
            {
                yield return _intutBuffer;
            }
        }


        public void Echo(AngleSystem currentAngleSystem, string command)
        {
            _output = command;
        }

        public void Error(string message)
        {
            _output = message;
        }

        public void Result(string result)
        {
            _output = result;
        }

        public void ResultTable(IEnumerable<ITableRow> tableRows)
        {
        }

        [SetUp]
        public void Setup()
        {
            _intutBuffer = string.Empty;
            _sut = new CommandParser(this, this);
        }

        [TestCase("", "")]
        [TestCase("Complex(0, 1)", "0 + 1i\r\nr = 1 φ = 90")]
        [TestCase("Date(2000, 1, 1) - Date(1995, 1, 1)", "1826 days 0 hours 0 minutes 0 seconds\r\nYears (Aproximated): 4\r\nMonths (Aproximated): 59")]
        public async Task TestWithFormatting(string input, string expected)
        {
            _intutBuffer = input;
            await _sut.RunAsync();
            Assert.That(_output, Is.EqualTo(expected));
        }

    }
}
