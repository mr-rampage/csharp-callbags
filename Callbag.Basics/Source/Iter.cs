using System;
using System.Collections.Generic;

namespace Callbag.Basics.Source
{
    internal sealed class Iter<T> : ISource<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly ISink<T> _sink;
        private bool _sending;

        private bool Terminated { get; set; }

        public Iter(in ISink<T> sink, in IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
            _sink = sink;
        }

        public void Greet(in ISink<T> sink)
        {
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

        public void GoodBye()
        {
            Terminated = true;
        }

        public void ReceiveFailure<TError>(in TError error)
        {
            throw new NotSupportedException();
        }
    }
}