using SharpEval;
using SharpEval.Core;
using SharpEval.Core.IO;

var docProvider = new FunctionDocumentationProvider();

using var reader = new ConsoleCommandReader(docProvider);
var writer = new ConsoleResultWriter();

CommandParser parser = new CommandParser(reader, writer);

parser.Settings.EchoExpression = false;
reader.PromptFunction = () => $"{parser.Settings.CurrentAngleSystem} >";

docProvider.AddCommandDocumentation(parser.GetCommandDocumentation());


await parser.RunAsync(reader.CancellationToken);