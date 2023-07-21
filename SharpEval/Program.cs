using SharpEval;
using SharpEval.Core;
using SharpEval.Core.IO;
using SharpEval.Webservices;

var docProvider = new FunctionDocumentationProvider();

using var reader = new ConsoleCommandReader(docProvider);
var writer = new ConsoleResultWriter();

CommandParser parser = new CommandParser(reader, writer, new ApiClient());

parser.Settings.EchoExpression = false;
reader.PromptFunction = () => $"{parser.Settings.CurrentAngleSystem} >";

docProvider.AddCommandDocumentation(parser.GetCommandDocumentation());


await parser.RunAsync(reader.CancellationToken);