using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FsCheck;
using System.Linq;
using Callbags;
using CallbagsTest.TestUtils;
using static Callbags.Source.Source;

namespace CallbagsTests.Source
{
    [TestClass]
    public class TestIter
    {

        [TestMethod("Should deliver data from iterables.")]
        public void TestPullables()
        {
            Prop.ForAll<string[]>(strings =>
            {
                var sent = new List<string>(strings);
                var received = new List<string>();
                var source = From(sent);
                var sink = AssertSink<string>.Receives(output =>
                {
                    received.Add(output);
                    return true;
                }, () => { }, () => Assert.Fail("Should not error out!"));
                source.Pipe(sink);
                return sent.SequenceEqual(received);
            }).QuickCheckThrowOnFailure();
        }
    }
}
