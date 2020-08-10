using System;

namespace Callbags
{
    public interface Callbag<T>
    {
        void Greet(Callbag<T> payload);
        void Deliver(T payload);
        void Deliver();
        void Terminate(Exception payload);
        void Terminate();
    }
}
