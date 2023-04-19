using System.Reflection;

using SharpEval.Core.Internals;

namespace SharpEval.Core.IO
{
    /// <summary>
    /// Provides documentation for globaly registered functions
    /// </summary>
    public sealed class FunctionDocumentationProvider
    {
        /// <summary>
        /// Documentations
        /// </summary>
        public Dictionary<string, List<string>> Documentation { get; }
        
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
            Documentation = new Dictionary<string, List<string>>();
            var members = typeof(Globals).GetMembers();
            Fill(members);
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

        private void Fill(MemberInfo[] members)
        {
            foreach (var member in members) 
            {
                if (_ignoreNames.Contains(member.Name))
                    continue;

                var key = FixName(member.Name);

                var doc = member.GetDocumentation();

                if (Documentation.TryGetValue(key, out List<string>? value))
                {
                    value.Add(doc);
                }
                else
                {
                    Documentation.Add(key, new List<string> { doc });
                }
            }
        }
    }
}
