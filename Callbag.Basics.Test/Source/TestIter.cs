﻿using System.Collections.Generic;
using System.Linq;
using Callbag.Basics.Test.TestUtils;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Callbag.Basics.Source.Source;

namespace Callbag.Basics.Test.Source
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
                From(sent)
                    .AssertSink(output =>
                    {
                        received.Add(output);
                        return true;
                    }, () => { }, () => Assert.Fail("Should not error out!"));
                return sent.SequenceEqual(received);
            }).QuickCheckThrowOnFailure();
        }
    }
}
