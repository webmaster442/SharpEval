﻿namespace SharpEval.Core.Internals.ResultFormatters;

internal sealed class ObjectSingleLineResultFormatter : SingleLineResultFormatter
{
    public override bool IsTypeMatch(object? o)
    {
        return o is not null &&
            o.GetType().GetMethod(nameof(ToString))?.DeclaringType != typeof(object);
    }

    public override string GetString(object? o, AngleSystem angleSystem)
    {
        return o?.ToString() ?? string.Empty;
    }
}
