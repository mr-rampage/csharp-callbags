using System;

namespace Callbag.Basics.Operator
{
    internal sealed class OperatorFactory<TInput, TOutput>: IOperator<TInput, TOutput>
    {
        private readonly Func<ISink<TOutput>, IOperator<TInput, TOutput>> _talkbackFactory;
        private ISource<TInput> _source;

        public OperatorFactory(Func<ISink<TOutput>, IOperator<TInput, TOutput>> talkbackFactory)
        {
            _talkbackFactory = talkbackFactory;
        }
        
        public void Acknowledge(in ISource<TInput> source)
        {
            _source = source;
        }
        
        public void Greet(in ISink<TOutput> sink)
        {
            _source.Greet(_talkbackFactory(sink));
        }

        #region Unsupported operations
        public void Deliver(in TInput data)
        {
            throw new NotSupportedException();
        }

        public void Complete()
        {
            throw new NotSupportedException();
        }

        public void Error<TError>(in TError error)
        {
            throw new NotSupportedException();
        }


        public void Request()
        {
            throw new NotSupportedException();
        }

        public void Terminate()
        {
            throw new NotSupportedException();
        }

        public void Terminate<TError>(in TError error)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}