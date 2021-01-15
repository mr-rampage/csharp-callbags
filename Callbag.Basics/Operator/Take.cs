using System;

namespace Callbag.Basics.Operator
{
    internal sealed class Take<T> : BaseOperator<T, T>
    {
        private readonly ISink<T> _sink;
        private readonly int _max;
        private int _taken;
        private bool _complete;

        public Take(ISink<T> sink, int max)
        {
            _sink = sink;
            _max = max;
        }

        public override void Acknowledge(in ISource<T> source)
        {
            base.Acknowledge(in source);
            _sink.Acknowledge(new Talkback(this));
        }

        public override void Deliver(in T data)
        {
            if (_taken++ < _max)
            {
                _sink.Deliver(data);

                if (_taken == _max && !_complete)
                {
                    _complete = true;
                    Source.GoodBye();
                    _sink.Complete();
                }
            }
        }

        private sealed class Talkback : ISource<T>
        {
            private readonly Take<T> _take;

            public Talkback(Take<T> take)
            {
                _take = take;
            }
            
            public void Greet(in ISink<T> sink)
            {
                throw new NotSupportedException();
            }

            public void Request()
            {
                if (_take._taken < _take._max)
                {
                    _take.Source.Request();
                }
            }

            public void GoodBye()
            {
                _take._complete = true;
                _take.Source.GoodBye();
            }

            public void ReceiveFailure<TError>(in TError error)
            {
                _take._complete = true;
                _take.Source.ReceiveFailure(error);
            }
        }

    }
}