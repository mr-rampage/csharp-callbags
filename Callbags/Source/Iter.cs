using System;
using System.Collections.Generic;

namespace Callbags.Source
{
    internal class Iter<T> : ISource<T>
    {
        private readonly IEnumerable<T> _enumerable;

        public Iter(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        private class Talkback : ISource<T>
        {
            private readonly IEnumerator<T> _enumerator;
            private readonly ISink<T> _sink;
            private bool _sending;
            
            private bool Terminated { get; set; }

            public Talkback(IEnumerator<T> enumerator, ISink<T> sink)
            {
                _enumerator = enumerator;
                _sink = sink;
            }

            public void Greet(in ISink<T> sink)
            {
                throw new NotSupportedException();
            }

            public void Request()
            {
                if (Terminated || _sending) return;
                _sending = true;
                _enumerator.Reset();
                while (_enumerator.MoveNext() && !Terminated)
                {
                    _sink.Deliver(_enumerator.Current);
                }
                _sending = false;
                _sink.Complete();
            }

            public void Terminate()
            {
                Terminated = true;
            }

            public void Terminate<TE>(in TE error)
            {
                throw new NotSupportedException();
            }
        }

        public void Greet(in ISink<T> sink)
        {
            sink.Acknowledge(new Talkback(_enumerable.GetEnumerator(), sink));
        }

        public void Request()
        {
            throw new NotSupportedException();
        }

        public void Terminate()
        {
            throw new NotSupportedException();
        }

        public void Terminate<TE>(in TE error)
        {
            throw new NotSupportedException();
        }
    }
}