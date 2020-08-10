using Callbags;
using Callbags.Source;
using System.Collections.Generic;
using System.Timers;
using System;
using System.Threading.Tasks;

namespace CallbagsTests.TestUtils
{
    class PushableSource<T>: Source<T>
    {
        private List<T> data;
        private int index;
        private static Timer timer;
        private TaskCompletionSource<bool> promise;
        private Callbag<T> sink;

        public static PushableSource<T> Sends(List<T> data, TaskCompletionSource<bool> promise)
        {
            return new PushableSource<T>(data, promise);
        }

        private PushableSource(List<T> data, TaskCompletionSource<bool> promise) 
        {
            this.data = data;
            this.promise = promise;
        }

        public override void Deliver()
        {
            if (timer == null)
            {

                timer = new Timer();
                timer.Interval = 5;

                timer.Elapsed += next;
                timer.Enabled = true;
            }
        }

        public override void Greet(Callbag<T> sink)
        {
            this.sink = sink;
            this.sink.Greet(this);
        }

        private void next(Object source, ElapsedEventArgs e)
        {
            if (index >= data.Capacity)
            {
                sink.Terminate();
                timer.Stop();
                promise.TrySetResult(true);
            }
            else
            {
                sink.Deliver(data[index++]);
            }
        }
    }
}
