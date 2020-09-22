using System;

namespace Callbag.Basics.Sink
{
    internal sealed class ForEach<T> : ISink<T>
    {
        private readonly Action<T> _operation;
        private readonly Action _onComplete;
        private ISource<T> _talkback;

        public ForEach(in Action<T> operation, in Action onComplete = null)
        {
            _operation = operation;
            _onComplete = onComplete;
        }

        public void Acknowledge(in ISource<T> talkback)
        {
            _talkback = talkback;
            _talkback.Request();
        }

        public void Deliver(in T data = default)
        {
            _operation(data);
            _talkback.Request();
        }

        public void Complete()
        {
            _onComplete?.Invoke();
        }

        public void Error<TError>(in TError error)
        {
            throw new NotSupportedException();
        }
    }
}