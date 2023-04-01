using System.Collections;
using System.Numerics;

namespace SharpEval.Core.Maths
{
    public sealed class DataSet<T> : IEnumerable<T> where T : INumber<T>
    {
        private readonly T[] _data;

        public DataSet(params T[] data) 
        {
            _data = data;
            Array.Sort(data);
        }

        public int Count() => _data.Length;

        public T Sum()
        {
            T result = T.Zero;
            for (int i=0; i<_data.Length; i++) 
            {
                result += _data[i];
            }
            return result;
        }

        public double Average()
        {
            double sum = Convert.ToDouble(Sum());
            return sum / _data.Length;
        }

        public T Max() => _data[^1];

        public T Min() => _data[0];

        public T Range() => Max() - Min();

        public double Median()
        {
            int mid = _data.Length / 2;
            double result = _data.Length % 2 == 0
                ? Convert.ToDouble((_data[mid - 1] + _data[mid])) / 2.0
                : Convert.ToDouble(_data[mid]);
            return result;
        }

        public T Mode()
        {
            T mode = T.Zero;
            int maxCount = 0;
            int currentCount = 1;
            T currentValue = _data[0];
            int count = _data.Length;
            for (int i = 1; i < count; i++)
            {
                if (_data[i] == currentValue)
                {
                    currentCount++;
                }
                else
                {
                    if (currentCount > maxCount)
                    {
                        maxCount = currentCount;
                        mode = currentValue;
                    }
                    currentCount = 1;
                    currentValue = _data[i];
                }
            }
            if (currentCount > maxCount)
            {
                mode = currentValue;
            }
            return mode;
        }

        public T this[int index]
        {
            get
            {
                return _data[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> casted = _data;
            return casted.GetEnumerator();
        }
    }
}
