using System.Collections.Generic;
using System.Linq;
using Callbag.Basics.Operator;
using Callbag.Basics.Test.TestUtils;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Callbag.Basics.Source.Source;

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

        [TestMethod("Should output the same if the seed is the identity")]
        public void TestIdentitySeed()
        {
            Prop.ForAll<string[]>(numbers =>
            {
                var sent = new List<string>(numbers);
                var source = From(sent);
                var withoutSeed = new List<string>();
                source.Scan((acc, number) => acc + number)
                    .AssertSink(output =>
                    {
                        withoutSeed.Add(output);
                        return true;
                    }, () => { }, () => Assert.Fail("Should not error out!"));
                    
                var withSeed = new List<string>();
                source
                    .Scan((acc, number) => acc + number, "")
                    .AssertSink(output =>
                    {
                        withSeed.Add(output);
                        return true;
                    }, () => { }, () => Assert.Fail("Should not error out!"));
                return sent.Count == 0  || withSeed.SequenceEqual(withoutSeed);
            }).QuickCheckThrowOnFailure();
        }
    }
}