using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;


namespace Callbag.Basics.Test.TestUtils
{
    class PushableSource<T>: ISource<T>
    {
        private readonly List<T> _data;
        private int _index;
        private Timer _timer;
        private readonly TaskCompletionSource<bool> _promise;
        private ISink<T> _sink;

        public static PushableSource<T> Sends(List<T> data, TaskCompletionSource<bool> promise)
        {
            return new PushableSource<T>(data, promise);
        }

        private PushableSource(List<T> data, TaskCompletionSource<bool> promise) 
        {
            _data = data;
            _promise = promise;
        }

        private void Next(object source, ElapsedEventArgs e)
        {
            if (_index >= _data.Capacity)
            {
                _sink.Complete();
                _timer.Stop();
                _promise.TrySetResult(true);
            }
            else
            {
                _sink.Deliver(_data[_index++]);
            }
        }

        public void Greet(in ISink<T> sink)
        {
            _sink = sink;
            _sink.Acknowledge(this);
        }

        public void Request()
        {
            if (_timer == null)
            {
                _timer = new Timer {Interval = 5};
                _timer.Elapsed += Next;
                _timer.Enabled = true;
            }
        }

        public void GoodBye()
        {
            throw new NotSupportedException();
        }

        public void ReceiveFailure<TE>(in TE error)
        {
            throw new NotSupportedException();
        }
    }
}
