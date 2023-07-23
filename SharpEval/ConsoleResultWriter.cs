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

        public void Error(string message, string trace = "")
        {
            AnsiConsole.MarkupLine($"[bold red]{message.EscapeMarkup()}[/]");
            AnsiConsole.MarkupLine($"[bold red]{trace.EscapeMarkup()}[/]");
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

        public void Result(ISvgImage image)
        {
            var name = $"plot_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.svg";
            var fullName = Path.Combine(Environment.CurrentDirectory, name);

            File.WriteAllText(fullName, image.Data);


            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = fullName;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
        }
    }
}