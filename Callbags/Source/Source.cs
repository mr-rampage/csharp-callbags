using System.Collections.Generic;

namespace Callbags.Source
{
    public static class Source
    {
        public static ISource<T> From<T>(in IEnumerable<T> enumerable)
        {
            return new Iter<T>(enumerable);
        } 
    }
}