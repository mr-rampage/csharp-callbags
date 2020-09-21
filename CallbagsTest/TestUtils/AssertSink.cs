using System;
using Callbags;

namespace CallbagsTest.TestUtils
{
    class AssertSink<T> : ISink<T>
    {
        private ISource<T> _talkback;
        private readonly Func<T, bool> _onContinue;
        private readonly Action _onComplete;
        private readonly Action _onError;

        internal AssertSink(Func<T, bool> onEach, Action onComplete, Action onError)
        {
            _onContinue = onEach;
            _onComplete = onComplete;
            _onError = onError;
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

    public static class TestSink
    {
        public static void AssertSink<T>(this ISource<T> source, Func<T, bool> onEach, Action onComplete = null, Action onError = null)
        {
            source.Greet(new AssertSink<T>(onEach, onComplete, onError));
        }
    }
}
