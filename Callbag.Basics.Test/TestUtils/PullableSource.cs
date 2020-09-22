using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Callbag.Basics.Test.TestUtils
{
    class PullableSource<T> : ISource<T>
    {
        private readonly List<T> _data;
        private int _index;
        private ISink<T> _sink;

        public static PullableSource<T> Sends(List<T> data)
        {
            return new PullableSource<T>(data);
        }

        private PullableSource(List<T> data) 
        {
            _data = data;
        }

        public void Greet(in ISink<T> sink)
        {
            _sink = sink;
            _sink.Acknowledge(this);
        }

        public void Request()
        {
            Assert.IsNotNull(_sink);
            if (_index >= _data.Capacity)
            {
                _sink.Complete();
            }
            else
            {
                _sink.Deliver(_data[_index++]);
            }
        }

        public void Terminate()
        {
            _index = 0;
        }

        public void Terminate<TE>(in TE error)
        {
            throw new NotSupportedException();
        }
    }
}
