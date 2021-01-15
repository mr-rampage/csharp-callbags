using System;
using System.Timers;

namespace Callbag.Basics.Source
{
    internal sealed class Interval : ISource<int>
    {
        private readonly ISink<int> _sink;
        private int _counter;
        private readonly Timer _timer;

        public Interval(ISink<int> sink, double period)
        {
            _sink = sink;
            _timer = new Timer {Interval = period};
            _timer.Elapsed += Next;
            _timer.Enabled = true;
        }

        private void Next(object sender, ElapsedEventArgs e)
        {
            _sink.Deliver(_counter++);
        }


        public void Greet(in ISink<int> sink)
        {
            throw new NotSupportedException();
        }

        public void Request()
        {
        }

        public void GoodBye()
        {
            _timer.Stop();
        }

        public void ReceiveFailure<TError>(in TError error)
        {
            GoodBye();
        }
    }
}