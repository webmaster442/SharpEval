namespace SharpEval.Core;

/// <summary>
/// Represents a Hash value, that is the result of a hash calculation
/// </summary>
public sealed class HashValue : IEquatable<HashValue?>
{
    private readonly byte[] _data;

    internal HashValue(byte[] data)
    {
        _data = data;
    }

    /// <summary>
    /// Create an instance from a base64 encoded string
    /// </summary>
    /// <param name="base64">base64 encoded form</param>
    /// <returns>A Hash value</returns>
    public static HashValue FromBase64(string base64)
    {
        return new HashValue(Convert.FromBase64String(base64));
    }

    /// <summary>
    /// Create an instance from a hex encoded string
    /// </summary>
    /// <param name="hexString">hex encoded form</param>
    /// <returns>A Hash value</returns>
    public static HashValue FromHexString(string hexString)
    {
        return new HashValue(Convert.FromHexString(hexString));
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as HashValue);
    }

    /// <inheritdoc/>
    public bool Equals(HashValue? other)
    {
        if (other == null
            || other?._data.Length != _data.Length)
        {
            return false;
        }

        for (int i = 0; i < _data.Length; i++)
        {
            if (_data[i] != other._data[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        HashCode hash = new();
        for (int i = 0; i < _data.Length; i++)
            hash.Add(_data[i]);
        return hash.ToHashCode();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Hex: {Convert.ToHexString(_data)}\r\nBase64: {Convert.ToBase64String(_data)}";
    }

    /// <inheritdoc/>
    public static bool operator ==(HashValue? left, HashValue? right)
    {
        return EqualityComparer<HashValue>.Default.Equals(left, right);
    }

    /// <inheritdoc/>
    public static bool operator !=(HashValue? left, HashValue? right)
    {
        return !(left == right);
    }
}
