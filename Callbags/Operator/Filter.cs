using System;

namespace Callbags.Operator
{
    public class Filter<T> : IOperator<T, T>
    {
        private ISource<T> _source;
        private ISink<T> _sink;
        private readonly Predicate<T> _predicate;

        public Filter(Predicate<T> predicate)
        {
            _predicate = predicate;
        }

        public void Acknowledge(in ISource<T> source)
        {
            _source = source;
        }

        public void Deliver(in T data)
        {
            if (_predicate(data))
            {
                _sink.Deliver(data);
            }
            else
            {
                _source.Request();
            }
        }

        public void Complete()
        {
            _sink.Complete();
        }

        public void Error<TError>(in TError error)
        {
            _sink.Error(error);
        }

        public void Greet(in ISink<T> sink)
        {
            _sink = sink;
            _sink.Acknowledge(this);
        }

        public void Request()
        {
            _source.Request();
        }

        public void Terminate()
        {
            _source.Terminate();
        }

        public void Terminate<TError>(in TError error)
        {
            _source.Terminate(error);
        }
    }
}