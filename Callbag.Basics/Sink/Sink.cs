using System;

namespace Callbag.Basics.Sink
{
    public static class SinkExtension
    {
        public static void ForEach<T>(this ISource<T> source, in Action<T> operation)
        {
            source.Greet(new ForEach<T>(operation));
        }
    }
}