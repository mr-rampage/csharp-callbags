using System;

namespace Callbag.Basics.Operator
{
    internal class Skip<T> : TalkbackOperator<T, T>
    {
        private readonly int _max;

        public Skip(int max)
        {
            _max = max;
        }

        public override void Acknowledge(in ISource<T> source)
        {
            Source = source;
        }

        public override void Greet(in ISink<T> sink)
        {
            Sink = sink;
            Source.Greet(new Talkback(_max, Sink));
        }

        private class Talkback : BaseOperator<T, T>
        {
            private readonly int _max;
            private int _skipped;
            
            public Talkback(int max, ISink<T> sink)
            {
                _max = max;
                Sink = sink;
            }

            public override void Acknowledge(in ISource<T> source)
            {
                base.Acknowledge(in source);
                // Optimization to skip sending the request through the talkback
                Sink.Acknowledge(source);
            }

            public override void Deliver(in T data)
            {
                if (_skipped++ < _max)
                {
                    Source.Request();
                }
                else
                {
                    Sink.Deliver(data);
                }
            }
        }
    }
}