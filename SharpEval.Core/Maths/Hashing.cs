using System.IO.Hashing;
using System.Security.Cryptography;
using System.Text;

namespace SharpEval.Core.Maths;

internal static class Hashing
{
    private static byte[] GetData(string @string)
    {
        return Encoding.UTF8.GetBytes(@string);
    }

    public static HashValue Md5(string @string)
    {
        return new HashValue(MD5.HashData(GetData(@string)));
    }

    public static HashValue Sha1(string @string)
    {
        return new HashValue(SHA1.HashData(GetData(@string)));
    }

    public static HashValue Sha256(string @string)
    {
        return new HashValue(SHA256.HashData(GetData(@string)));
    }

    public static HashValue Sha384(string @string)
    {
        return new HashValue(SHA384.HashData(GetData(@string)));
    }

    public static HashValue Sha512(string @string)
    {
        return new HashValue(SHA512.HashData(GetData(@string)));
    }

    public static HashValue Crc32(string @string)
    {
        var crc = new Crc32();
        crc.Append(GetData(@string));
        return new HashValue(crc.GetHashAndReset());
    }

    public static HashValue Crc64(string @string)
    {
        var crc = new Crc64();
        crc.Append(GetData(@string));
        return new HashValue(crc.GetHashAndReset());
    }
}
