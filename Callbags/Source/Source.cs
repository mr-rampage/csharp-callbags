using System;

namespace Callbags.Source
{
    public abstract class Source<T> : Callbag<T>
    {
        public virtual void Deliver()
        {

        }

        public void Deliver(T payload)
        {
            throw new InvalidOperationException("Sources do not consume data");
        }

        public virtual void Greet(Callbag<T> payload)
        {
            throw new NotImplementedException("Greeting has not be implemented!");
        }

        public virtual void Terminate()
        {

        }

        public virtual void Terminate(Exception payload)
        {
            Terminate();
        }

        public virtual void Pipe(Callbag<T> sink)
        {
            Greet(sink);
        }
    }
}
