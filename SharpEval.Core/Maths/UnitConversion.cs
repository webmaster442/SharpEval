using System.Globalization;

using UnitsNet;

namespace SharpEval.Core.Maths;

internal sealed class UnitConversion
{
    private readonly Dictionary<string, HashSet<string>> _conversions;
    private readonly HashSet<string> _units;

    private void AddToUnits(IEnumerable<string> units)
    {
        foreach (var unit in units)
        {
            _units.Add(unit.ToLower());
        }
    }

    public UnitConversion(CultureInfo cultureInfo)
    {
        _units = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        _conversions = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var info in Quantity.Infos)
        {
            var names = info.UnitInfos.Select(x => x.Name);

            var abbreviations =
                UnitsNetSetup.Default.UnitAbbreviations
                .GetAllUnitAbbreviationsForQuantity(info.UnitType, cultureInfo);

            _conversions.Add(info.Name, names.Concat(abbreviations).ToHashSet(StringComparer.OrdinalIgnoreCase));

            AddToUnits(names);
            AddToUnits(abbreviations);
        }
    }

    public IEnumerable<string> Units => _units;

    public double Convert(double value, string from, string to)
    {
        if (!_units.Contains(from))
            throw new InvalidOperationException($"Unknown unit: {from}");

        if (!_units.Contains(to))
            throw new InvalidOperationException($"Unknown unit: {to}");

        var conversion = _conversions
            .Where(x => x.Value.Contains(from)
                     && x.Value.Contains(to))
            .Select(x => x.Key)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(conversion))
            throw new InvalidOperationException($"Can't convert from {from} to {to}");

        if (UnitConverter.TryConvertByName(value, conversion, from, to, out double result))
        {
            return result;
        }
        else if (UnitConverter.TryConvertByAbbreviation(value, conversion, from, to, out result))
        {
            return result;
        }
        else
        {
            throw new InvalidOperationException($"Can't convert from {from} to {to}");
        }
    }
}
