using System.Text;

using SharpEval.Core;

using Spectre.Console;

namespace SharpEval
{
    internal sealed class ConsoleCommandReader : ICommandReader, IDisposable
    {
        private readonly StringBuilder _lineBuffer;
        private bool _exitRequest;
        private readonly CancellationTokenSource _tokenSource;
        private bool _disposed = false;

        public CancellationToken CancellationToken => _tokenSource.Token;

        public Func<string>? PromptFunction { get; set; }

        public ConsoleCommandReader()
        {
            _lineBuffer = new StringBuilder(200);
            Console.CancelKeyPress += OnExitRequest;
            _tokenSource = new CancellationTokenSource();
        }

        private void OnExitRequest(object? sender, ConsoleCancelEventArgs e)
        {
            _exitRequest = true;
            _tokenSource.Cancel();
        }

        private bool TryReadLine(out string line)
        {
            string? read = Console.ReadLine();
            if (read != null)
            {
                line = read;
                return true;
            }
            line = string.Empty;
            return false;
        }

        public void Dispose()
        {
            if (!_disposed && _tokenSource != null)
            {
                _tokenSource.Dispose();
                _disposed = true;
            }
        }

        public IEnumerable<string> InputLines
        {
            get
            {
                while (true)
                {
                    string prompt = PromptFunction?.Invoke() ?? string.Empty;
                    AnsiConsole.Write(prompt);
                    if (TryReadLine(out string line))
                        yield return line;
                    else
                        yield break;
                }
            }
        }
    }
}
