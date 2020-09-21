using System.Collections.Generic;
using System.Linq;
using Callbags.Operator;
using CallbagsTest.TestUtils;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CallbagsTest.Operator
{
    [TestClass]
    public class TestFilter
    {
        [TestMethod("Should transform all source data to sinks")]
        public void TestPullables()
        {
            Prop.ForAll<int[]>(numbers =>
            {
                var sent = new List<int>(numbers);
                var received = new List<int>();
                var expected = new List<int>(numbers).Where(number => number % 2 == 0);
                PullableSource<int>
                    .Sends(sent)
                    .Filter(number => number % 2 == 0)
                    .AssertSink(output =>
                    {
                        received.Add(output);
                        return true;
                    }, () => { }, () => Assert.Fail("Should not error out!"));
                return expected.SequenceEqual(received);
            }).QuickCheckThrowOnFailure();
        }
    }
}