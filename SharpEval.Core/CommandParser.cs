using SharpEval.Core.Internals;

namespace SharpEval.Core
{
    public sealed class CommandParser : ISettingsProvider
    {
        private readonly ICommandReader _commandReader;
        private readonly IResultWrtiter _resultWrtiter;
        private readonly Evaluator _evaluator;
        private readonly Dictionary<string, Action<Arguments>> _commandTable;

        private bool _exitFlag;

        public Settings Settings { get; private set; }

        Settings ISettingsProvider.GetSettings() => Settings;

        public CommandParser(ICommandReader commandReader, IResultWrtiter resultWrtiter)
        {
            Settings = new Settings();
            _evaluator = new Evaluator(this);
            _commandReader = commandReader;
            _resultWrtiter = resultWrtiter;
            _commandTable = new Dictionary<string, Action<Arguments>>
            {
                { "$echo", (args) => Settings.EchoExpression = args.Get<bool>(0) },
                { "$mode", (args) => Settings.CurrentAngleSystem = args.GetEnum<AngleSystem>(0) },
                { "$exit", (args) => _exitFlag = true },
                { "$reset", (args) => _evaluator.Reset() },
                { "$vars", PrintVariables }
            };
            _evaluator.OnReset += EvaluatorOnReset;
        }

        private void EvaluatorOnReset(object? sender, EventArgs e)
        {
            Settings = new();
        }

        private void PrintVariables(Arguments arguments)
        {
            foreach (var variable in _evaluator.Variables)
            {
                _resultWrtiter.Result($"{variable.Key} = {variable.Value} //{variable.Value.GetType().Name}");
            }
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            foreach (var input in _commandReader.InputLines)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                if (input.StartsWith('$'))
                {
                    try
                    {
                        string[] splitted = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        string command = splitted[0];
                        var arguments = new Arguments(splitted.Skip(1));
                        if (_commandTable.ContainsKey(command))
                            _commandTable[command].Invoke(arguments);
                        else
                            _resultWrtiter.Error($"Unknown command: {command}");
                    }
                    catch (Exception ex)
                    {
                        _resultWrtiter.Error(ex.Message);
                    }
                }
                else
                {
                    //expression
                    var result = await _evaluator.EvaluateAsync(input);
                    if (Settings.EchoExpression)
                        _resultWrtiter.Echo(Settings.CurrentAngleSystem, input);

                    if (string.IsNullOrEmpty(result.error))
                        _resultWrtiter.Result(result.result);
                    else
                        _resultWrtiter.Error(result.error);
                }

                if (_exitFlag)
                    return;
            }
        }
    }
}
