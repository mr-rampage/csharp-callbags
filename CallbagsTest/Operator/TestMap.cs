using System.Collections.Generic;
using System.Linq;
using Callbags;
using CallbagsTest.TestUtils;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Callbags.Operator.Operator;

namespace CallbagsTest.Operator
{
    [TestClass]
    public class TestMap
    {
        [TestMethod("Should transform all source data to sinks")]
        public void TestPullables()
        {
            Prop.ForAll<int[]>(numbers =>
            {
                var sent = new List<int>(numbers);
                var received = new List<int>();
                var expected = new List<int>(numbers).Select(number => number * 2);
                PullableSource<int>
                    .Sends(sent)
                    .Pipe(Map<int, int>(number => number * 2))
                    .Pipe(AssertSink<int>.Receives(output =>
                    {
                        received.Add(output);
                        return true;
                    }, () => { }, () => Assert.Fail("Should not error out!")));
                return expected.SequenceEqual(received);
            }).QuickCheckThrowOnFailure();
        }
    }
}