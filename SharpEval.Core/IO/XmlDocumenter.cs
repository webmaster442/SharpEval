using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace SharpEval.Core.IO;

internal static class XmlDocumenter
{
    private static readonly Dictionary<string, string> LoadedXmlDocumentation = new();

    public static void LoadXmlDocumentation(string xmlDocumentation)
    {
        using (XmlReader xmlReader = XmlReader.Create(new StringReader(xmlDocumentation)))
        {
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
                {
                    string raw_name = xmlReader["name"] ?? throw new InvalidOperationException("name empty");
                    LoadedXmlDocumentation[raw_name] = xmlReader.ReadInnerXml();
                }
            }
        }
    }

    /// <summary>Gets the XML documentation on a type.</summary>
    /// <param name="type">The type to get the XML documentation of.</param>
    /// <returns>The XML documentation on the type.</returns>
    /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
    public static string GetDocumentation(this Type type)
    {
        string key = "T:" + Regex.Replace(type.FullName ?? string.Empty, @"\[.*\]", string.Empty).Replace('+', '.');
        if (LoadedXmlDocumentation.TryGetValue(key, out string? documentation))
            return documentation;

        return string.Empty;
    }

    /// <summary>Gets the XML documentation on a method.</summary>
    /// <param name="methodInfo">The method to get the XML documentation of.</param>
    /// <returns>The XML documentation on the method.</returns>
    /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
    public static string GetDocumentation(this MethodInfo methodInfo)
    {
        var genericTypes = methodInfo.GetGenericArguments();

        int genericParameterCounts = genericTypes.Length;
        ParameterInfo[] parameterInfos = methodInfo.GetParameters();

        var pars = parameterInfos.Select(x =>
        {
            int genericIndex = Array.IndexOf(genericTypes,
                                             x.ParameterType.IsArray
                                             ? x.ParameterType.GetElementType()
                                             : x.ParameterType);
            if (genericIndex != -1)
            {
                return x.ParameterType.IsArray
                    ? $"``{genericIndex}[]"
                    : $"``{genericIndex}";
            }
            return x.ParameterType.ToString();
        });

        string key = "M:" +
            Regex.Replace(methodInfo.DeclaringType?.FullName ?? string.Empty, @"\[.*\]", string.Empty).Replace('+', '.') + "." + methodInfo.Name +
            (genericParameterCounts > 0 ? "``" + genericParameterCounts : string.Empty) +
            (parameterInfos.Length > 0 ? "(" + string.Join(",", pars) + ")" : string.Empty);

        if (LoadedXmlDocumentation.TryGetValue(key, out string? documentation))
            return documentation;

        return string.Empty;
    }

    /// <summary>Gets the XML documentation on a constructor.</summary>
    /// <param name="constructorInfo">The constructor to get the XML documentation of.</param>
    /// <returns>The XML documentation on the constructor.</returns>
    /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
    public static string GetDocumentation(this ConstructorInfo constructorInfo)
    {
        ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
        string key = "M:" +
            Regex.Replace(constructorInfo.DeclaringType?.FullName ?? string.Empty, @"\[.*\]", string.Empty).Replace('+', '.') + ".#ctor" +
            (parameterInfos.Length > 0 ? "(" + string.Join(",", parameterInfos.Select(x => x.ParameterType.ToString())) + ")" : string.Empty);

        if (LoadedXmlDocumentation.TryGetValue(key, out string? documentation))
            return documentation;

        return string.Empty;
    }

    /// <summary>Gets the XML documentation on a property.</summary>
    /// <param name="propertyInfo">The property to get the XML documentation of.</param>
    /// <returns>The XML documentation on the property.</returns>
    /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
    public static string GetDocumentation(this PropertyInfo propertyInfo)
    {
        string key = "P:" + Regex.Replace(propertyInfo.DeclaringType?.FullName ?? string.Empty, @"\[.*\]", string.Empty).Replace('+', '.') + "." + propertyInfo.Name;
        if (LoadedXmlDocumentation.TryGetValue(key, out string? documentation))
            return documentation;
        return string.Empty;
    }

    /// <summary>Gets the XML documentation on a field.</summary>
    /// <param name="fieldInfo">The field to get the XML documentation of.</param>
    /// <returns>The XML documentation on the field.</returns>
    /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
    public static string GetDocumentation(this FieldInfo fieldInfo)
    {
        string key = "F:" + Regex.Replace(fieldInfo.DeclaringType?.FullName ?? string.Empty, @"\[.*\]", string.Empty).Replace('+', '.') + "." + fieldInfo.Name;
        if (LoadedXmlDocumentation.TryGetValue(key, out string? documentation))
            return documentation;

        return string.Empty;
    }

    /// <summary>Gets the XML documentation on an event.</summary>
    /// <param name="eventInfo">The event to get the XML documentation of.</param>
    /// <returns>The XML documentation on the event.</returns>
    /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
    public static string GetDocumentation(this EventInfo eventInfo)
    {
        string key = "E:" + Regex.Replace(eventInfo.DeclaringType?.FullName ?? string.Empty, @"\[.*\]", string.Empty).Replace('+', '.') + "." + eventInfo.Name;
        if (LoadedXmlDocumentation.TryGetValue(key, out string? documentation))
            return documentation;

        return string.Empty;
    }

    /// <summary>Gets the XML documentation on a member.</summary>
    /// <param name="memberInfo">The member to get the XML documentation of.</param>
    /// <returns>The XML documentation on the member.</returns>
    /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
    public static string GetDocumentation(this MemberInfo memberInfo)
    {
        if (memberInfo.MemberType.HasFlag(MemberTypes.Field))
        {
            return ((FieldInfo)memberInfo).GetDocumentation();
        }
        else if (memberInfo.MemberType.HasFlag(MemberTypes.Property))
        {
            return ((PropertyInfo)memberInfo).GetDocumentation();
        }
        else if (memberInfo.MemberType.HasFlag(MemberTypes.Event))
        {
            return ((EventInfo)memberInfo).GetDocumentation();
        }
        else if (memberInfo.MemberType.HasFlag(MemberTypes.Constructor))
        {
            return ((ConstructorInfo)memberInfo).GetDocumentation();
        }
        else if (memberInfo.MemberType.HasFlag(MemberTypes.Method))
        {
            return ((MethodInfo)memberInfo).GetDocumentation();
        }
        else if (memberInfo.MemberType.HasFlag(MemberTypes.TypeInfo) || memberInfo.MemberType.HasFlag(MemberTypes.NestedType))
        {
            return ((TypeInfo)memberInfo).GetDocumentation();
        }
        else
        {
            return string.Empty;
        }
    }
}
