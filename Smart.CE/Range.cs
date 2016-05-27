namespace Smart
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Range<T> : IComparable<Range<T>>, IComparable<T> where T : IComparable<T>
    {
        private readonly T min;

        private readonly T max;

        public T Min
        {
            get { return min; }
        }

        public T Max
        {
            get { return max; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Range(T min, T max)
        {
            if (Comparer<T>.Default.Compare(min, max) > 0)
            {
                this.min = max;
                this.max = min;
            }
            else
            {
                this.min = min;
                this.max = max;
            }
        }

        public static bool operator ==(Range<T> left, Range<T> right)
        {
            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(Range<T> left, Range<T> right)
        {
            return !(left == right);
        }

        public static bool operator <(Range<T> left, Range<T> right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(Range<T> left, T right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Range<T> left, Range<T> right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(Range<T> left, T right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Range<T> left, Range<T> right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(Range<T> left, T right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Range<T> left, Range<T> right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(Range<T> left, T right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            var comparer = Comparer<T>.Default;
            return comparer.Compare(min, value) <= 0 && comparer.Compare(max, value) >= 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool Contains(Range<T> range)
        {
            var comparer = Comparer<T>.Default;
            return comparer.Compare(min, range.min) <= 0 && comparer.Compare(max, range.max) >= 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool Overlaps(Range<T> range)
        {
            return Contains(range.min) || Contains(range.max) || range.Contains(min) || range.Contains(max);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool Touches(Range<T> range)
        {
            var comparer = Comparer<T>.Default;
            return comparer.Compare(max, range.min) == 0 || comparer.Compare(min, range.max) == 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Range<T> other)
        {
            return Comparer<T>.Default.Compare(min, other.min);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(T other)
        {
            return Comparer<T>.Default.Compare(min, other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (min.GetHashCode() * 7) + max.GetHashCode();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Range<T>)
            {
                var other = (Range<T>)obj;
                return (min.CompareTo(other.min) == 0) && (max.CompareTo(other.max) == 0);
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{" + min + "->" + max + "}";
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ComparerRange<T> : IComparable<ComparerRange<T>>, IComparable<T> where T : IComparable<T>
    {
        private readonly Comparer<T> comparer;

        private readonly T min;

        private readonly T max;

        public T Min
        {
            get { return min; }
        }

        public T Max
        {
            get { return max; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="comparer"></param>
        public ComparerRange(T min, T max, Comparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            this.comparer = comparer;
            if (comparer.Compare(min, max) > 0)
            {
                this.min = max;
                this.max = min;
            }
            else
            {
                this.min = min;
                this.max = max;
            }
        }

        public static bool operator ==(ComparerRange<T> left, ComparerRange<T> right)
        {
            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(ComparerRange<T> left, ComparerRange<T> right)
        {
            return !(left == right);
        }

        public static bool operator <(ComparerRange<T> left, ComparerRange<T> right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(ComparerRange<T> left, T right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ComparerRange<T> left, ComparerRange<T> right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(ComparerRange<T> left, T right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ComparerRange<T> left, ComparerRange<T> right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(ComparerRange<T> left, T right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ComparerRange<T> left, ComparerRange<T> right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(ComparerRange<T> left, T right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            return comparer.Compare(min, value) <= 0 && comparer.Compare(max, value) >= 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool Contains(ComparerRange<T> range)
        {
            return comparer.Compare(min, range.min) <= 0 && comparer.Compare(max, range.max) >= 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool Overlaps(ComparerRange<T> range)
        {
            return Contains(range.min) || Contains(range.max) || range.Contains(min) || range.Contains(max);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool Touches(ComparerRange<T> range)
        {
            return comparer.Compare(max, range.min) == 0 || comparer.Compare(min, range.max) == 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ComparerRange<T> other)
        {
            return comparer.Compare(min, other.min);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(T other)
        {
            return comparer.Compare(min, other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (min.GetHashCode() * 7) + max.GetHashCode();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is ComparerRange<T>)
            {
                var other = (ComparerRange<T>)obj;
                return (min.CompareTo(other.min) == 0) && (max.CompareTo(other.max) == 0);
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{" + min + "->" + max + "}";
        }
    }
}
