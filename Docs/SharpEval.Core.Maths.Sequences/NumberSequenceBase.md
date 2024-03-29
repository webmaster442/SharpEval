# NumberSequenceBase class

Represents a base class for number series

```csharp
public abstract class NumberSequenceBase : 
    IAdditionOperators<NumberSequenceBase, NumberSequenceBase, IEnumerable<long>>, 
    IDivisionOperators<NumberSequenceBase, NumberSequenceBase, IEnumerable<double>>, 
    IEnumerable<long>, 
    IMultiplyOperators<NumberSequenceBase, NumberSequenceBase, IEnumerable<long>>, 
    ISubtractionOperators<NumberSequenceBase, NumberSequenceBase, IEnumerable<long>>
```

## Public Members

| name | description |
| --- | --- |
| [Maximum](NumberSequenceBase/Maximum.md) { get; set; } | Series maximum value. Series items are generated between minimum and maximum. |
| abstract [Minimum](NumberSequenceBase/Minimum.md) { get; set; } | Series minimimum value. Series items are generated between minimum and maximum. |
| abstract [GetEnumerator](NumberSequenceBase/GetEnumerator.md)() |  |
| [operator +](NumberSequenceBase/op_Addition.md) |  |
| [operator /](NumberSequenceBase/op_Division.md) |  |
| [operator *](NumberSequenceBase/op_Multiply.md) |  |
| [operator -](NumberSequenceBase/op_Subtraction.md) |  |

## Protected Members

| name | description |
| --- | --- |
| [NumberSequenceBase](NumberSequenceBase/NumberSequenceBase.md)() | The default constructor. |
| [CheckRange](NumberSequenceBase/CheckRange.md)() | Check if Range is valid |

## See Also

* namespace [SharpEval.Core.Maths.Sequences](../SharpEval.Core.md)

<!-- DO NOT EDIT: generated by xmldocmd for SharpEval.Core.dll -->
