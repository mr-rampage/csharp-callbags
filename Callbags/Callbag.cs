namespace Callbags 
{
    
    public interface ISource<T>
    {
        public void Greet(in ISink<T> sink);
        public void Request();
        public void Terminate();
        public void Terminate<TError>(in TError error);
    }

    public interface ISink<T>
    {
        public void Acknowledge(in ISource<T> talkback);
        public void Deliver(in T data);
        public void Complete();
        public void Error<TError>(in TError error);
    }
    
    public interface IOperator<TInput, TOutput>: ISink<TInput>, ISource<TOutput> {}
    
    public static class CallbagExtension
    {
        public static void Pipe<T>(this ISource<T> source, ISink<T> sink)
        {
            source.Greet(sink);
        }

        public static ISource<TOutput> Pipe<TInput, TOutput>(this ISource<TInput> source, IOperator<TInput, TOutput> operation)
        {
            source.Greet(operation);
            return operation;
        }
    }
}