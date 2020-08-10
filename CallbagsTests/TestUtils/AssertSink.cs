using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Callbags;
using System.Collections.Generic;

namespace CallbagsTests.TestUtils
{
    class AssertSink<T> : Callbag<T>
    {
        private List<T> expected;
        private int index;
        private Callbag<T> talkback;

        public static AssertSink<T> Receives(List<T> expected)
        {
            return new AssertSink<T>(expected);
        }

        private AssertSink(List<T> expected)
        {
            this.expected = expected;
            index = 0;
        }

        void Callbag<T>.Greet(Callbag<T> talkback)
        {
            this.talkback = talkback;
            this.talkback.Deliver();
        }

        void Callbag<T>.Deliver(T payload)
        {
            Assert.AreEqual(expected[index++], payload);
            talkback.Deliver();
        }

        void Callbag<T>.Deliver()
        {
            throw new NotImplementedException();
        }

        void Callbag<T>.Terminate(Exception payload)
        {
        }

        void Callbag<T>.Terminate()
        {
        }
    }
}
