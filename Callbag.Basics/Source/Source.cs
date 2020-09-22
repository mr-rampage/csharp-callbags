using System.Collections.Generic;

namespace Callbag.Basics.Source
{
    public static class Source
    {
        public static ISource<T> From<T>(IEnumerable<T> enumerable)
        {
            return new SourceFactory<T>(sink => new Iter<T>(sink, enumerable.GetEnumerator()));
        }

        public static ISource<int> Interval(int period)
        {
            return new SourceFactory<int>(sink => new Interval(sink, period));
        }
    }
}