using SharpEval;
using SharpEval.Core;

using var reader = new ConsoleCommandReader();
var writer = new ConsoleResultWriter();

CommandParser parser = new CommandParser(reader, writer);

parser.Settings.EchoExpression = false;
reader.PromptFunction = () => $"{parser.Settings.CurrentAngleSystem} >";

await parser.RunAsync(reader.CancellationToken);