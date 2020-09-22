using System;
using System.Collections.Generic;
using System.Linq;
using Callbag.Basics.Operator;
using Callbag.Basics.Test.TestUtils;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Random = System.Random;

namespace Callbag.Basics.Test.Operator
{
    [TestClass]
    public class TestSkip
    {
        [TestMethod("Should transform all source data to sinks")]
        public void TestPullables()
        {
            var random = new Random();
            Prop.ForAll<int[]>(numbers =>
            {
                var skip = random.Next(numbers.Length);
                var sent = new List<int>(numbers);
                var received = new List<int>();
                var expected = new List<int>(numbers).Skip(skip);
                PullableSource<int>
                    .Sends(sent)
                    .Skip(skip)
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