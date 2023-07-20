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

        public void Result(IEnumerable<ITableRow> tableRows)
        {
            var table = new Table();
            foreach (var row in tableRows)
            {
                var columnData = row.Columns.ToArray();
                table.AddRow(columnData);
            }
            AnsiConsole.Write(table);
        }
    }
}