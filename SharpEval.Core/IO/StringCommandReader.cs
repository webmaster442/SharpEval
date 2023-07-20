namespace SharpEval.Core.IO
{
    internal class StringCommandReader : ICommandReader
    {
        private readonly string[] _lines;

        public static StringCommandReader FromFile(string fileName)
            => new StringCommandReader(File.ReadLines(fileName).ToArray());

        public static StringCommandReader FromString(string text)
        {
            var lines = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return new StringCommandReader(lines);
        }

        private StringCommandReader(string[] lines)
        {
            _lines = lines;
        }

        public IEnumerable<string> InputLines
            => _lines;
    }
}
