using System.Collections.Generic;

namespace Callbags.Source
{
    public class Sources
    {
        public static Source<T> from<T>(IEnumerable<T> enumerable)
        {
            return new Iter<T>(enumerable);
        }
    }
}
