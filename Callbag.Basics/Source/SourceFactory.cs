using System;

namespace Callbag.Basics.Source
{
    internal sealed class SourceFactory<T>: ISource<T>
    {
        private readonly Func<ISink<T>, ISource<T>> _talkbackFactory;

        public SourceFactory(Func<ISink<T>, ISource<T>> talkbackFactory)
        {
            _talkbackFactory = talkbackFactory;
        }
        
        public void Greet(in ISink<T> sink)
        {
            sink.Acknowledge(_talkbackFactory(sink));
        }

        #region Unsupported Operations
        public void Request()
        {
            throw new NotSupportedException();
        }

        public void GoodBye()
        {
            throw new NotSupportedException();
        }

        public void ReceiveFailure<TError>(in TError error)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}