namespace SharpEval.Webservices
{
    internal sealed class CacheEntry : IEquatable<CacheEntry?>
    {
        public string Value { get; }
        public DateTime EndDate { get; }

        public CacheEntry(string value, DateTime endDate)
        {
            Value = value;
            EndDate = endDate;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CacheEntry);
        }

        public bool Equals(CacheEntry? other)
        {
            return other is not null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(CacheEntry? left, CacheEntry? right)
        {
            return EqualityComparer<CacheEntry>.Default.Equals(left, right);
        }

        public static bool operator !=(CacheEntry? left, CacheEntry? right)
        {
            return !(left == right);
        }
    }
}
