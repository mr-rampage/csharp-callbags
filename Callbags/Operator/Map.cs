using System;

namespace Callbags.Operator
{
    internal sealed class Map<TInput, TOutput>: IOperator<TInput, TOutput>
    {
        private ISource<TInput> _source;
        private ISink<TOutput> _sink;
        private readonly Func<TInput, TOutput> _transformation;

        public Map(in Func<TInput, TOutput> transformation)
        {
            _transformation = transformation;
        }
        
        public void Acknowledge(in ISource<TInput> source)
        {
            _source = source;
        }

        public void Deliver(in TInput data)
        {
            _sink.Deliver(_transformation(data));
        }

        public void Complete()
        {
            _sink.Complete();
        }

        public void Error<TError>(in TError error)
        {
            _sink.Error(error);
        }

        public void Greet(in ISink<TOutput> sink)
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