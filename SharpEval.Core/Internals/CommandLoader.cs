namespace SharpEval.Core.Internals
{
    internal static class CommandLoader
    {
        internal static Dictionary<string, ICommand> LoadCommands()
        {
            Dictionary<string, ICommand> results = new();

            Type cmdInterface = typeof(ICommand);

            IEnumerable<Type> types = cmdInterface.Assembly
                .GetTypes()
                .Where(type => type.IsAssignableTo(cmdInterface) 
                       && !type.IsInterface 
                       && !type.IsAbstract);

            foreach (Type type in types)
            {
                if (Activator.CreateInstance(type) is ICommand command)
                {
                    results.Add(command.Name, command);
                }
            }

            return results;
        }
    }
}
