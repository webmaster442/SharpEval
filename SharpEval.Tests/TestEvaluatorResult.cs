using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using SharpEval.Core.Internals;

namespace SharpEval.Tests
{
    [TestFixture]
    public class TestEvaluatorResult
    {
        [Test]
        public void EnsureThat_EvaluatorResult_ToTable_WorksEnumerable()
        {
            var testObject = new EvaluatorResult
            {
                Error = string.Empty,
                ResultData = new string[] { "foo", "bar" }
            };

            var expected = new TableRow[]
            {
                new TableRow("foo"),
                new TableRow("bar")
            };

            var result = testObject.ToTable();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void EnsureThat_EvaluatorResult_ToTable_WorksFormattableEnumerable()
        {
            var testObject = new EvaluatorResult
            {
                Error = string.Empty,
                ResultData = new double[] { 11.22d, 0.25d }
            };

            var expected = new TableRow[]
            {
                new TableRow("11.22"),
                new TableRow("0.25")
            };

            var result = testObject.ToTable();

            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void EnsureThat_EvaluatorResult_ToTable_EqualsWorks()
        {
            var testObject = new EvaluatorResult
            {
                Error = string.Empty,
                ResultData = new string[] { "foo", "bar" }
            };

            var expected = new TableRow[]
            {
                new TableRow("foo"),
                new TableRow("Bar")
            };

            var result = testObject.ToTable();

            Assert.That(result, Is.Not.EqualTo(expected));
        }
    }
}
