using System;

namespace Callbag.Basics.Operator
{
    internal abstract class BaseOperator<TInput, TOutput>: IOperator<TInput, TOutput>
    {
        protected ISource<TInput> Source { get; set; }
        protected ISink<TOutput> Sink { get; set; }
        
        public virtual void Acknowledge(in ISource<TInput> source)
        {
            Source = source;
        }

        public abstract void Deliver(in TInput data);

        public virtual void Complete()
        {
            Sink.Complete();
        }

        public virtual void Error<TError>(in TError error)
        {
            Sink.Error(error);
        }

        public virtual void Greet(in ISink<TOutput> sink)
        {
            Sink = sink;
            Sink.Acknowledge(this);
        }

        public virtual void Request()
        {
            Source.Request();
        }

        public virtual void Terminate()
        {
            Source.Terminate();
        }

        public virtual void Terminate<TError>(in TError error)
        {
            Source.Terminate(error);
        }
    }

    internal abstract class TalkbackOperator<TInput, TOutput> : IOperator<TInput, TOutput>
    {
        protected ISource<TInput> Source { get; set; }
        protected ISink<TOutput> Sink { get; set; }
        
        public virtual void Acknowledge(in ISource<TInput> talkback)
        {
            throw new NotSupportedException();
        }

        public virtual void Deliver(in TInput data)
        {
            throw new NotSupportedException();
        }

        public virtual void Complete()
        {
            throw new NotSupportedException();
        }

        public virtual void Error<TError>(in TError error)
        {
            throw new NotSupportedException();
        }

        public virtual void Greet(in ISink<TOutput> sink)
        {
            throw new NotSupportedException();
        }

        public virtual void Request()
        {
            throw new NotSupportedException();
        }

        public virtual void Terminate()
        {
            throw new NotSupportedException();
        }

        public virtual void Terminate<TError>(in TError error)
        {
            throw new NotSupportedException();
        }
    }
}