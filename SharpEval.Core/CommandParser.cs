using System.Diagnostics;
using System.Globalization;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;

using SharpEval.Core.Internals;
using SharpEval.Core.Internals.ResultFormatters;

namespace SharpEval.Core
{
    /// <summary>
    /// The main expression command parser
    /// </summary>
    public sealed class CommandParser : ISettingsProvider, ICommandHost
    {
        private readonly ICommandReader _commandReader;
        private readonly IResultWrtiter _resultWrtiter;
        private readonly Evaluator _evaluator;
        private readonly Dictionary<string, ICommand> _commandTable;
        private readonly ResultFormatter[] _resultFormatters;

        private bool _exitFlag;

        /// <summary>
        /// Current settings
        /// </summary>
        public Settings Settings { get; private set; }

        Settings ICommandHost.Settings => Settings;

        Evaluator ICommandHost.Evaluator => _evaluator;

        bool ICommandHost.ExitFlag
        {
            get => _exitFlag;
            set => _exitFlag = value;
        }

        IResultWrtiter ICommandHost.ResultWrtiter => _resultWrtiter;

        Settings ISettingsProvider.GetSettings() => Settings;

        /// <summary>
        /// Creates a new instance of Command parser
        /// </summary>
        /// <param name="commandReader">Command reader abstraction</param>
        /// <param name="resultWrtiter">Result writer abstraction</param>
        /// <seealso cref="ICommandReader"/>
        /// <seealso cref="IResultWrtiter"/>
        public CommandParser(ICommandReader commandReader, IResultWrtiter resultWrtiter)
        {
            Settings = new Settings();
            _evaluator = new Evaluator(this);
            _commandReader = commandReader;
            _resultWrtiter = resultWrtiter;
            _commandTable = CommandLoader.LoadCommands();
            _evaluator.OnReset += EvaluatorOnReset;
            _resultFormatters = new ResultFormatter[]
            {
                new NullSingleLineResultFormatter(),
                new EnumerableTableResultFormatter(),
                new CompexSingleLineResultFormatter(),
                new TimeSpanSingleLineResultFormatter(),
                new FormattableSingleLineResultFormatter(),
                new ObjectSingleLineResultFormatter(),
                new ObjectTableResultFormatter(),
            };
        }

        /// <summary>
        /// Get registered command documentation
        /// </summary>
        /// <returns>Documentation as a Dictionary</returns>
        public IReadOnlyDictionary<string, string> GetCommandDocumentation()
        {
            return _commandTable.ToDictionary(x => x.Key, x => x.Value.HelpMessage);
        }

        private void EvaluatorOnReset(object? sender, EventArgs e)
        {
            Settings = new();
        }

        /// <summary>
        /// Main entry point for command parsing
        /// </summary>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>An asyncronous task that can be awaited</returns>
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
                    RunCommand(input);
                }
                else
                {
                    //expression
                    var result = await _evaluator.EvaluateAsync(input);
                    if (Settings.EchoExpression)
                        _resultWrtiter.Echo(Settings.CurrentAngleSystem, input);

                    if (!string.IsNullOrEmpty(result.Error))
                    {
                        _resultWrtiter.Error(result.Error);
                    }
                    else if (result.ResultData is ISvgImage svgImage)
                    {
                        _resultWrtiter.Result(svgImage);
                    }
                    else
                    {
                        PrintResult(result);
                    }
                }

                if (_exitFlag)
                    return;
            }
        }

        private void PrintResult(EvaluatorResult result)
        {
            var formatter = _resultFormatters
                .FirstOrDefault(f => f.IsTypeMatch(result.ResultData));

            if (formatter == null)
                throw new InvalidOperationException($"Unknown result type: {result.GetType().Name}");

            if (formatter is SingleLineResultFormatter singleLineFormatter)
            {
                string resultString = singleLineFormatter.GetString(result.ResultData, Settings.CurrentAngleSystem);
                _resultWrtiter.Result(resultString);
            }
            else if (formatter is TableResultFormatter tableResultFormatter)
            {
                var rows = tableResultFormatter.ToTable(result.ResultData, Settings.CurrentAngleSystem);
                _resultWrtiter.Result(rows);
            }
            else
            {
                throw new UnreachableException($"Unknown formatter type: {formatter.GetType().Name}");
            }
        }

        private void RunCommand(string input)
        {
            try
            {
                string[] splitted = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string command = splitted[0];
                var arguments = new Arguments(splitted.Skip(1));
                if (_commandTable.ContainsKey(command))
                    _commandTable[command].Execute(this, arguments);
                else
                    _resultWrtiter.Error($"Unknown command: {command}");
            }
            catch (Exception ex)
            {
                _resultWrtiter.Error(ex.Message);
            }
        }
    }
}
