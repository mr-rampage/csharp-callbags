namespace Callbags 
{
    
    public interface ISource<T>
    {
        public void Greet(in ISink<T> sink);
        public void Request();
        public void Terminate();
        public void Terminate<TE>(in TE error);
    }

    public interface ISink<T>
    {
        public void Acknowledge(in ISource<T> talkback);
        public void Deliver(in T data);
        public void Complete();
        public void Error<TE>(in TE error);
    }
    
    public interface IOperator<I, O>: ISink<I>, ISource<O> {}
    
    public static class CallbagExtension
    {
        public static void Pipe<T>(this ISource<T> source, ISink<T> sink)
        {
            source.Greet(sink);
        }

        public static ISource<O> Pipe<I, O>(this ISource<I> source, IOperator<I, O> operation)
        {
            source.Greet(operation);
            return operation;
        }
    }
}