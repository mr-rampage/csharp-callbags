using System;

namespace Callbags.Sink
{
    class ForEach<T> : Sink<T>
    {
        private readonly Action<T> operation;
        private Callbag<T> talkback;

        public ForEach(Action<T> operation)
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
