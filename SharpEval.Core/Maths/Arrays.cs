namespace SharpEval.Core.Maths
{
    internal static class Arrays
    {
        public static T[] Distinct<T>(params T[] items) where T : IComparable<T>
        {
            return Enumerable.Distinct(items).ToArray();
        }

        public static T[] Randomize<T>(params T[] items)
        {
            return items
                .OrderBy(x => Random.Shared.Next())
                .ToArray();
        }

        public static T RandomPick<T>(params T[] items)
        {
            int max = items.Length;
            int index = Random.Shared.Next(0, max);
            return items[index];
        }
    }
}
