using System.Collections.Generic;

namespace Callbags.Source
{
    public class Iter<T> : Source<T>
    {
        private readonly IEnumerable<T> iterable;

        private class Talkback : Source<T>
        {
            private bool completed;
            private IEnumerator<T> iterable;
            private Callbag<T> sink;

            public Talkback(Callbag<T> sink, IEnumerator<T> iterable)
            {
                this.sink = sink;
                this.iterable = iterable;
            }

            override public void Terminate()
            {
                completed = true;
            }

            override public void Deliver()
            {
                if (!completed)
                {
                    if (iterable.MoveNext())
                    {
                        sink.Deliver(iterable.Current);
                    } else
                    {
                        sink.Terminate();
                    }
                }
            }
        }

        public static Iter<T> from(IEnumerable<T> items)
        {
            return new Iter<T>(items);
        }
        
        private Iter(IEnumerable<T> items)
        {
            iterable = items;
        }

        public override void Greet(Callbag<T> sink)
        {
            sink.Greet(new Talkback(sink, iterable.GetEnumerator()));
        }

    }
}
