using System.Text;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

using SharpEval.Core.Maths;
using SharpEval.Webservices;

namespace SharpEval.Core.Internals;

internal sealed class Evaluator
{
    private readonly ScriptOptions _options;
    private readonly Globals _globals;
    private ScriptState<object>? _state;

    public event EventHandler? OnReset;

    internal void SetRandomSeed(int seed)
    {
        _globals.RandomGenerator = new Random(seed);
    }

    public Evaluator(ISettingsProvider settingsProvider, IApiClient apiClient)
    {
        _options = ScriptOptions.Default
            .WithFileEncoding(Encoding.UTF8)
            .WithLanguageVersion(LanguageVersion.Latest)
            .WithEmitDebugInformation(false)
            .WithAllowUnsafe(false)
            .WithCheckOverflow(true)
            .AddReferences(typeof(Si).Assembly)
            .WithImports("System.Collections.Generic",
                         "System",
                         "System.Numerics",
                         "System.Collections",
                         "SharpEval.Core.Maths");
        _state = null;
        _globals = new Globals(settingsProvider, new ApiAdapter(apiClient));
    }

    public IReadOnlyDictionary<string, object> Variables
    {
        get
        {
            return
                _state == null
                ? new Dictionary<string, object>()
                : _state.Variables.VariablesToDictionary();
        }
    }

    public void Reset()
    {
        if (_state != null)
        {
            foreach (var item in _state.Variables)
            {
                if (item.Value is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _state = null;
            OnReset?.Invoke(this, EventArgs.Empty);
        }
    }

    public async Task<EvaluatorResult> EvaluateAsync(string expression)
    {
        string expressionToRun = PreprocessExpression(expression);
        try
        {
            if (_state == null)
                _state = await CSharpScript.RunAsync(expressionToRun, _options, _globals);
            else
                _state = await _state.ContinueWithAsync(expressionToRun, _options);

            return new EvaluatorResult
            {
                ResultData = _state?.ReturnValue,
                Error = string.Empty,
                Trace = string.Empty,
            };

        }
        catch (Exception ex)
        {
            return new EvaluatorResult
            {
                Error =ex.Message,
                ResultData = null,
                Trace = ex.StackTrace ?? string.Empty
            };
        }
    }

    private static string PreprocessExpression(string expression)
    {
        if (expression.StartsWith("var ") && !expression.EndsWith(';'))
            return $"{expression};";

        return expression;
    }
}
