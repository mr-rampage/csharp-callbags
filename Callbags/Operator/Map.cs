using System;

namespace Callbags.Operator
{
    internal sealed class Map<TInput, TOutput>: BaseOperator<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> _transformation;

        public Map(in Func<TInput, TOutput> transformation)
        {
            _transformation = transformation;
        }
        
        public override void Deliver(in TInput data)
        {
            Sink.Deliver(_transformation(data));
        }
    }
}