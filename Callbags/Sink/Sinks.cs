using System;

namespace Callbags.Sink
{
    public class Sinks
    {
        public static Sink<T> forEach<T>(Action<T> operation)
        {
            return new ForEach<T>(operation);
        }
    }
}
