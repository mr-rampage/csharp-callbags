using System;

namespace Callbag.Basics.Operator
{
    internal sealed class Scan<T>: BaseOperator<T, T>
    {
        private readonly Func<T, T, T> _reducer;
        private bool _accumulatorInitialized;
        private T _accumulator;

        public Scan(ISink<T> sink, Func<T, T, T> reducer)
        {
            _reducer = reducer;
            Sink = sink;
            _accumulatorInitialized = false;
        }

        public Scan(ISink<T> sink, Func<T, T, T> reducer, T seed)
        {
            _reducer = reducer;
            _accumulator= seed;
            Sink = sink;
            _accumulatorInitialized = true;
        }

        public override void Acknowledge(in ISource<T> source)
        {
            base.Acknowledge(source);
            Sink.Acknowledge(source);
        }

        public override void Deliver(in T data)
        {
            if (_accumulatorInitialized)
            {
                _accumulator = _reducer(_accumulator, data);
            }
            else
            {
                _accumulatorInitialized = false;
                _accumulator = data;
            }
            Sink.Deliver(_accumulator);
        }
    }
}