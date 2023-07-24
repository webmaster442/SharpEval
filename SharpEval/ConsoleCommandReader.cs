using PrettyPrompt;

using SharpEval.Core;
using SharpEval.Core.IO;

namespace SharpEval;

internal sealed class ConsoleCommandReader : ICommandReader, IDisposable
{
    private readonly Prompt _prompt;
    private readonly PromptConfiguration _configuration;
    private readonly CancellationTokenSource _tokenSource;
    private bool _disposed;

    public CancellationToken CancellationToken => _tokenSource.Token;

    public Func<string>? PromptFunction { get; set; }

    public ConsoleCommandReader(IDocumentationProvider documentationProvider)
    {
        Console.CancelKeyPress += OnExitRequest;
        _tokenSource = new CancellationTokenSource();
        _configuration = new PromptConfiguration();
        _prompt = new Prompt(null, new PropmptCallbacks(documentationProvider), null, _configuration);
    }

    private void OnExitRequest(object? sender, ConsoleCancelEventArgs e)
    {
        _tokenSource.Cancel();
    }

    private async Task<(bool result, string line)> TryReadLine()
    {
        _configuration.Prompt = PromptFunction?.Invoke() ?? string.Empty;
        var result = await _prompt.ReadLineAsync();
        if (result.IsSuccess)
        {
            return (true, result.Text);
        }
        return (false, string.Empty);
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
                var (result, line) = TryReadLine().GetAwaiter().GetResult();
                if (result)
                {
                    yield return line;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
