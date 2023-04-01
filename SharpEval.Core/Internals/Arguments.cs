namespace SharpEval.Core.Internals
{
    internal class Arguments
    {
        private readonly string[] _arguments;

        public Arguments(IEnumerable<string> arguments)
        {
            _arguments = arguments.ToArray();
        }

        public T Get<T>(int index) where T : IConvertible
        {
            if (index < 0 || index > _arguments.Length - 1)
                throw new InvalidOperationException($"invalid argument index: {index}");

            if (typeof(T) == typeof(bool))
                return (T)GetBool(_arguments[index]);

            return (T)Convert.ChangeType(_arguments[index], typeof(T));
        }

        internal T GetEnum<T>(int index) where T: struct, Enum
        {
            return Enum.Parse<T>(_arguments[index], true);
        }

        private static IConvertible GetBool(string value)
        {
            switch (value.ToLower())
            {
                case "on":
                case "enabled":
                case "1":
                    return true;
                case "off":
                case "disabled":
                case "0":
                    return false;
                default:
                    return bool.Parse(value);
            }
        }
    }
}
