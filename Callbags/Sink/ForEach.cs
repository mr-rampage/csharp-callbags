using System;

namespace Callbags.Sink
{
    public class ForEach<T> : Sink<T>
    {
        private readonly Action<T> operation;
        private Callbag<T> talkback;

        public static ForEach<T> forEach(Action<T> operation)
        {
            return new ForEach<T>(operation);
        }

        private ForEach(Action<T> operation)
        {
            this.operation = operation;
        }

        public override void Deliver(T payload)
        {
            if (talkback != null)
            {
                operation(payload);
                talkback.Deliver();
            }
        }

        public override void Greet(Callbag<T> talkback)
        {
            if (talkback != null)
            {
                this.talkback = talkback;
                this.talkback.Deliver();
            }
        }

        public override void Terminate()
        {
            talkback = null;
        }
    }
}
