using System.Collections.Generic;

namespace Callbag.Basics.Source
{
    public static class Source
    {
        public static ISource<T> From<T>(in IEnumerable<T> enumerable)
        {
            return new Iter<T>(enumerable);
        }

        public static ISource<int> Interval(in int period)
        {
            return new Interval(period);
        }
    }
}