using System;
using System.Timers;

namespace Callbag.Basics.Source
{
    internal sealed class Interval: ISource<int>
    {
        private readonly int _period;

        public Interval(in int period)
        {
            _period = period;
        }
        
        public void Greet(in ISink<int> sink)
        {
            sink.Acknowledge(new Talkback(_period, sink));
        }

        public void Request()
        {
            throw new NotSupportedException();
        }

        public void Terminate()
        {
            throw new NotSupportedException();
        }

        public void Terminate<TError>(in TError error)
        {
            throw new NotSupportedException();
        }

        private class Talkback : ISource<int>
        {
            private readonly ISink<int> _sink;
            private int _counter;
            private readonly Timer _timer;

            public Talkback(double period, ISink<int> sink)
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

            public void Request() { }

            public void Terminate()
            {
                _timer.Stop();
            }

            public void Terminate<TError>(in TError error)
            {
                Terminate();
            }
        }
    }
}