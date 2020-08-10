using Microsoft.VisualStudio.TestTools.UnitTesting;
using Callbags;
using Callbags.Source;
using System.Collections.Generic;

namespace CallbagsTests.TestUtils
{
    class PullableSource<T> : Source<T>
    {
        private List<T> data;
        private int index;
        private Callbag<T> sink;

        public static PullableSource<T> Sends(List<T> data)
        {
            return new PullableSource<T>(data);
        }

        private PullableSource(List<T> data) 
        {
            this.data = data;
        }

        public override void Deliver()
        {
            Assert.IsNotNull(sink);
            if (index >= data.Capacity)
            {
                sink.Terminate();
            }
            else
            {
                sink.Deliver(data[index++]);
            }
        }

        public override void Greet(Callbag<T> sink)
        {
            this.sink = sink;
            this.sink.Greet(this);
        }

        public override void Terminate()
        {
            index = 0;
        }
    }
}
