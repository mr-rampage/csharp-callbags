using System;

namespace Callbags.Sink
{
    public abstract class Sink<T> : Callbag<T>
    {
        public void Deliver()
        {
            throw new InvalidOperationException("Sinks do not deliver data");
        }

        public virtual void Deliver(T payload)
        {

        }

        public abstract void Greet(Callbag<T> talkback);

        public virtual void Terminate()
        {

        }

        public virtual void Terminate(Exception payload)
        {
            Terminate();
        }
    }
}
