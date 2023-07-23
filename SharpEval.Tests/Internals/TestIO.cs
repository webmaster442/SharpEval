using System.Text;

namespace SharpEval.Tests.Internals
{
    internal sealed class TestIO : ICommandReader, IResultWrtiter
    {
        internal enum EventType
        {
            Echo,
            Error,
            Result,
            Image,
            Table,
            Trace,
        }

        public record class Event(EventType EventType, string Result);

        public Stack<Event> Events { get; }

        public AngleSystem CurrentAngleSystem { get; private set; }

        private string[] _inputBuffer;

        public bool HasEventType(EventType eventType)
        {
            return Events.Where(e => e.EventType == eventType).Any();
        }

        public void SetInput(params string[] lines)
            => _inputBuffer = lines;

        public TestIO()
        {
            Events = new Stack<Event>();
            CurrentAngleSystem = AngleSystem.Deg;
            _inputBuffer = Array.Empty<string>();
        }

        IEnumerable<string> ICommandReader.InputLines
            => _inputBuffer;

        void IResultWrtiter.Echo(AngleSystem currentAngleSystem, string command)
        {
            CurrentAngleSystem = currentAngleSystem;
            Events.Push(new Event(EventType.Echo, command));
        }

        void IResultWrtiter.Error(string message, string trace)
        {
            Events.Push(new Event(EventType.Error, message));
            Events.Push(new Event(EventType.Trace, trace));
        }


        void IResultWrtiter.Error(string message)
        {
            Events.Push(new Event(EventType.Error, message));
        }

        void IResultWrtiter.Result(string result)
        {
            Events.Push(new Event(EventType.Result, result));
        }

        void IResultWrtiter.Result(IEnumerable<ITableRow> tableRows)
        {
            StringBuilder buffer = new();
            foreach (var row in tableRows)
            {
                foreach (var column in row.Columns)
                {
                    buffer.Append(column);
                    buffer.Append(';');
                }
                buffer.AppendLine();
            }
            Events.Push(new Event(EventType.Table, buffer.ToString()));
        }

        void IResultWrtiter.Result(ISvgImage image)
        {
            Events.Push(new Event(EventType.Image, image.Data));
        }
    }
}
