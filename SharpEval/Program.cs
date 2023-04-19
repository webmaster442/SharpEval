using SharpEval;
using SharpEval.Core;
using SharpEval.Core.IO;

var docProvider = new FunctionDocumentationProvider();

using var reader = new ConsoleCommandReader(docProvider.Documentation);
var writer = new ConsoleResultWriter();

CommandParser parser = new CommandParser(reader, writer);

parser.Settings.EchoExpression = false;
reader.PromptFunction = () => $"{parser.Settings.CurrentAngleSystem} >";

await parser.RunAsync(reader.CancellationToken);