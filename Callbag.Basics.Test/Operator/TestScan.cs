using System.Collections.Generic;
using System.Linq;
using Callbag.Basics.Operator;
using Callbag.Basics.Test.TestUtils;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Callbag.Basics.Test.Operator
{
    [TestClass]
    public class TestScan
    {
        [TestMethod("Should scan source data to sinks")]
        public void TestPullables()
        {
            Prop.ForAll<int[]>(numbers =>
            {
                var sent = new List<int>(numbers);
                var received = new List<int>();
                var expected = new List<int>(numbers).Sum();
                PullableSource<int>
                    .Sends(sent)
                    .Scan((acc, number) => acc + number, 0)
                    .AssertSink(output =>
                    {
                        received.Add(output);
                        return true;
                    }, () => { }, () => Assert.Fail("Should not error out!"));
                return sent.Count == 0 || expected == received.Last();
            }).QuickCheckThrowOnFailure();
        }
    }
}