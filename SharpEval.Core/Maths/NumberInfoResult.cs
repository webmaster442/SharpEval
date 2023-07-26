using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEval.Core.Maths;
internal sealed class NumberInfoResult
{
    public required string Binary { get; init; }
    public required string Hexadecimal { get; init; }
    public required string Decimal { get; init; }
    public required string Type { get; init; }
    public required int Bits { get; init; }
}
