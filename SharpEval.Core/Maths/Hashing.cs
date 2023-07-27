using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEval.Core.Maths;
internal static class Hashing
{
    private static byte[] GetData(string @string)
    {
        return Encoding.UTF8.GetBytes(@string);
    }

}
