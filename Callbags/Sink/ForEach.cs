using System;

namespace Callbags.Sink
{
    internal class ForEach<T> : ISink<T>
    {
        private readonly Action<T> _operation;
        private readonly Action _onComplete;
        private ISource<T> _talkback;

        public ForEach(Action<T> operation, Action onComplete = null)
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

        public void Error<TE>(in TE error)
        {
            throw new NotSupportedException();
        }
    }
}