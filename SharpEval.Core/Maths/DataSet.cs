using System.Collections;
using System.Numerics;

namespace SharpEval.Core.Maths
{
    /// <summary>
    /// Represents a Data set that can be used for statistics
    /// </summary>
    /// <typeparam name="T">Number type</typeparam>
    public sealed class DataSet<T> : IEnumerable<T> where T : INumber<T>
    {
        private readonly T[] _data;

        /// <summary>
        /// Creates a new instance of DataSet
        /// </summary>
        /// <param name="data">Number data</param>
        public DataSet(params T[] data) 
        {
            _data = data;
            Array.Sort(data);
        }

        /// <summary>
        /// Gets the total number of elements
        /// </summary>
        /// <returns>The total number of elements</returns>
        public int Count() => _data.Length;

        /// <summary>
        /// Computes the sum of elements in the DataSet
        /// </summary>
        /// <returns>The sum of the elements</returns>
        public T Sum()
        {
            T result = T.Zero;
            for (int i=0; i<_data.Length; i++) 
            {
                result += _data[i];
            }
            return result;
        }

        /// <summary>
        /// Computes the average of the elements in the DataSet
        /// </summary>
        /// <returns>The average of the elements</returns>
        public double Average()
        {
            double sum = Convert.ToDouble(Sum());
            return sum / _data.Length;
        }

        /// <summary>
        /// Returns the maximum value of the elements
        /// </summary>
        /// <returns>The maximum value of the elements</returns>
        public T Max() => _data[^1];

        /// <summary>
        /// Returns the minimum value of the elements
        /// </summary>
        /// <returns>The minimum value of the elements</returns>
        public T Min() => _data[0];

        /// <summary>
        /// Computes the range of the elements in the DataSet. 
        /// The range is the spread of the elements from the lowest to the highest value in the DataSet.
        /// </summary>
        /// <returns>The range of the elements</returns>
        public T Range() => Max() - Min();

        /// <summary>
        /// Computes the median of the elements in the DataSet.
        /// The median is the value in the middle of a data set
        /// </summary>
        /// <returns>The median of the elements</returns>
        public double Median()
        {
            int mid = _data.Length / 2;
            double result = _data.Length % 2 == 0
                ? Convert.ToDouble((_data[mid - 1] + _data[mid])) / 2.0
                : Convert.ToDouble(_data[mid]);
            return result;
        }

        /// <summary>
        /// Computes the mode of the elements in the DataSet.
        /// The mode is the value that appears most often in the DataSet.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Indexer operator
        /// </summary>
        /// <param name="index">the index to get from the DataSet</param>
        /// <returns>The value at the given index</returns>
        public T this[int index]
        {
            get
            {
                return _data[index];
            }
        }

        /// <summary>
        /// Returns distinct elements from this DataSet as a new DataSet
        /// </summary>
        /// <returns>A DataSet, containing only Distinct elements</returns>
        public DataSet<T> Distinct()
        {
            return new DataSet<T>(_data.Distinct().ToArray());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> casted = _data;
            return casted.GetEnumerator();
        }
    }
}
