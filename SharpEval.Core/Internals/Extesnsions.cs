using System.Collections.Generic;
using System.Collections.Immutable;

using Microsoft.CodeAnalysis.Scripting;

namespace SharpEval.Core.Internals
{
    internal static class Extesnsions
    {
        private static readonly HashSet<Type> ValTupleTypes = new()
        {
            typeof(ValueTuple<>),
            typeof(ValueTuple<,>),
            typeof(ValueTuple<,,>),
            typeof(ValueTuple<,,,>),
            typeof(ValueTuple<,,,,>),
            typeof(ValueTuple<,,,,,>),
            typeof(ValueTuple<,,,,,,>),
            typeof(ValueTuple<,,,,,,,>)
        };

        public static IReadOnlyDictionary<string, object> VariablesToDictionary(this ImmutableArray<ScriptVariable> variables)
        {
            Dictionary<string, object> result = new();
            foreach (var variable in variables) 
            {
                if (result.ContainsKey(variable.Name))
                    result[variable.Name] = variable.Value;
                else
                    result.Add(variable.Name, variable.Value);
            }
            return result;
        }

        public static bool IsValueTuple(this object obj)
        {
            var type = obj.GetType();
            return type.IsGenericType && ValTupleTypes.Contains(type.GetGenericTypeDefinition());
        }
    }
}
