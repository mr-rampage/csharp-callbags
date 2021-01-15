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

        public virtual void SendFailure<TError>(in TError error)
        {
            Sink.SendFailure(error);
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

        public virtual void GoodBye()
        {
            Source.GoodBye();
        }

        public virtual void ReceiveFailure<TError>(in TError error)
        {
            Source.ReceiveFailure(error);
        }
    }
}