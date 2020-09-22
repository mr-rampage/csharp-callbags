namespace Callbag.Basics.Operator
{
    internal class Skip<T> : BaseOperator<T, T>
    {
        private readonly int _max;
        private int _skipped;

        public Skip(ISink<T> sink, int max)
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