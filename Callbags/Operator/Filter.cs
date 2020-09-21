using System;

namespace Callbags.Operator
{
    internal sealed class Filter<T> : BaseOperator<T, T>
    {
        private readonly Predicate<T> _predicate;

        public Filter(Predicate<T> predicate)
        {
            _predicate = predicate;
        }

        public override void Deliver(in T data)
        {
            if (_predicate(data))
            {
                Sink.Deliver(data);
            }
            else
            {
                Source.Request();
            }
        }
    }
}