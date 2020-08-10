using System;
using Callbags;

namespace CallbagsTests.TestUtils
{
    class AssertSink<T> : Callbag<T>
    {
        private int index;
        private Callbag<T> talkback;
        private Func<T, bool> onContinue;

        public static AssertSink<T> Receives(Func<T, bool> onEach)
        {
            return new AssertSink<T>(onEach);
        }

        private AssertSink(Func<T, bool> onEach)
        {
            this.onContinue = onEach;
            index = 0;
        }

        void Callbag<T>.Greet(Callbag<T> talkback)
        {
            this.talkback = talkback;
            this.talkback.Deliver();
        }

        void Callbag<T>.Deliver(T payload)
        {
            if (onContinue(payload))
            {
                talkback.Deliver();
            } else
            {
                talkback.Terminate();
            }
        }

        void Callbag<T>.Deliver()
        {
            throw new NotImplementedException();
        }

        void Callbag<T>.Terminate(Exception payload)
        {
        }

        void Callbag<T>.Terminate()
        {
        }
    }
}
