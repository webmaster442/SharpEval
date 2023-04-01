using SharpEval.Core;

using Spectre.Console;

namespace SharpEval
{
    internal sealed class ConsoleResultWriter : IResultWrtiter
    {
        public void Echo(AngleSystem currentAngleSystem, string command)
        {
            AnsiConsole.MarkupLine($"[bold]({currentAngleSystem}) {command}:[/]");
        }

        public void Error(string message)
        {
            AnsiConsole.MarkupLine($"[bold red]{message}[/]");
        }

        public void Result(string result)
        {
            AnsiConsole.MarkupLine($"[green italic]{result}[/]");
        }
    }
}