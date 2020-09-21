using System;

namespace Callbags.Sink
{
    public static class Sink
    {
        public static ISink<T> ForEach<T>(in Action<T> operation)
        {
            return new ForEach<T>(operation);
        }
    }
}