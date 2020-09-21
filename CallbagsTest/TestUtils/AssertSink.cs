using System;
using Callbags;

namespace CallbagsTest.TestUtils
{
    class AssertSink<T> : ISink<T>
    {
        private int index;
        private ISource<T> _talkback;
        private readonly Func<T, bool> _onContinue;
        private readonly Action _onComplete;
        private readonly Action _onError;

        public static AssertSink<T> Receives(Func<T, bool> onEach, Action onComplete = null, Action onError = null)
        {
            return new AssertSink<T>(onEach, onComplete, onError);
        }

        private AssertSink(Func<T, bool> onEach, Action onComplete, Action onError)
        {
            _onContinue = onEach;
            _onComplete = onComplete;
            _onError = onError;
            index = 0;
        }

        public void Acknowledge(in ISource<T> talkback)
        {
            _talkback = talkback;
            _talkback.Request();
        }

        public void Deliver(in T data)
        {
            if (_onContinue(data))
            {
                _talkback.Request();
            } else
            {
                _talkback.Terminate();
            }
        }

        public void Complete()
        {
            _onComplete?.Invoke();
        }

        public void Error<TE>(in TE error)
        {
            _onError?.Invoke();
        }
    }
}
