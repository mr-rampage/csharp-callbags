using System;

namespace Callbags.Operator
{
    public class Map<I, O>: IOperator<I, O>
    {
        private ISource<I> _source;
        private ISink<O> _sink;
        private readonly Func<I, O> _transformation;

        public Map(Func<I, O> transformation)
        {
            _transformation = transformation;
        }
        
        public void Acknowledge(in ISource<I> source)
        {
            _source = source;
        }

        public void Deliver(in I data)
        {
            _sink.Deliver(_transformation(data));
        }

        public void Complete()
        {
            _sink.Complete();
        }

        public void Error<TE>(in TE error)
        {
            _sink.Error(error);
        }

        public void Greet(in ISink<O> sink)
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

        public void Terminate<TE>(in TE error)
        {
            _source.Terminate(error);
        }
    }
}