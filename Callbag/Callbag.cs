namespace Callbag 
{
    
    public interface ISource<T>
    {
        public void Greet(in ISink<T> sink);
        public void Request();
        public void GoodBye();
        public void ReceiveFailure<TError>(in TError error);
    }

    public interface ISink<T>
    {
        public void Acknowledge(in ISource<T> talkback);
        public void Deliver(in T data);
        public void Complete();
        public void SendFailure<TError>(in TError error);
    }
    
    public interface IOperator<TInput, TOutput>: ISink<TInput>, ISource<TOutput> {}
}