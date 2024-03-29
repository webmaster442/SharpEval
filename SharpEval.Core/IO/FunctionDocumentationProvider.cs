﻿using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using SharpEval.Core.Internals;

namespace SharpEval.Core.IO;

/// <summary>
/// Provides documentation for globaly registered functions
/// </summary>
public sealed partial class FunctionDocumentationProvider : IDocumentationProvider
{
    /// <summary>
    /// Documentations
    /// </summary>
    private Dictionary<string, List<string>> _documentation;

    private readonly HashSet<string> _ignoreNames;

    /// <summary>
    /// Creates a new instance of FunctionDocumentationProvider
    /// </summary>
    public FunctionDocumentationProvider()
    {
        var fileName = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, ".xml");
        var xmlContent = File.ReadAllText(fileName);
        XmlDocumenter.LoadXmlDocumentation(xmlContent);

        _ignoreNames = new HashSet<string>
        {
            "ToString", "GetHashCode", "Equals", "GetType"
        };
        _documentation = new Dictionary<string, List<string>>();
        var members = typeof(Globals).GetMembers();
        Fill(members);
    }

    /// <inheritdoc/>
    public IReadOnlyDictionary<string, List<string>> GetDocumentations()
    {
        return _documentation;
    }

    private static string FixName(string name)
    {
        if (name.StartsWith("get_")
            || name.StartsWith("set_"))
        {
            return name[4..];
        }

        return name;
    }

    /// <summary>
    /// Add command documentation
    /// </summary>
    /// <param name="docs">command documentation</param>
    public void AddCommandDocumentation(IReadOnlyDictionary<string, string> docs)
    {
        foreach (var doc in docs)
        {
            if (_documentation.TryGetValue(doc.Key, out List<string>? value))
            {
                value.Add(doc.Value);
            }
            else
            {
                _documentation.Add(doc.Key, new List<string> { doc.Value });
            }
        }
    }


    private void Fill(MemberInfo[] members)
    {
        foreach (var member in members)
        {
            if (_ignoreNames.Contains(member.Name))
                continue;

            var key = FixName(member.Name);

            var doc = member.GetDocumentation();

            if (string.IsNullOrEmpty(doc)) continue;

            string cleanDoc = Clean(doc);

            if (_documentation.TryGetValue(key, out List<string>? value))
            {
                value.Add(cleanDoc);
            }
            else
            {
                _documentation.Add(key, new List<string> { cleanDoc });
            }
        }
    }

    [GeneratedRegex("<param name=\"(.+)\">(.+)<\\/param>")]
    private static partial Regex ParamRegex();

    private static string Clean(string doc)
    {
        const string elementSummary = "<summary>";
        const string elementSummaryEnd = "</summary>";
        const string elementReturns = "<returns>";

        StringBuilder final = new();
        int paramCounter = 0;
        using (var reader = new StringReader(doc))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var trimmed = line.Trim();
                if (trimmed == elementSummary)
                {
                    final.AppendLine("Description:");
                }
                else if (trimmed == elementSummaryEnd)
                {
                    final.AppendLine();
                }
                else if (trimmed.StartsWith(elementReturns))
                {
                    continue;
                }
                else if (ParamRegex().IsMatch(line))
                {
                    if (paramCounter == 0)
                    {
                        final.AppendLine("Parameters:");
                    }
                    final.AppendLine(ParamRegex().Replace(line, "$1: $2"));
                    ++paramCounter;
                }
                else
                {
                    final.AppendLine(line);
                }
            }
        }
        return final.ToString();
    }
}
