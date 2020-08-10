using Microsoft.VisualStudio.TestTools.UnitTesting;
using Callbags.Source;
using System.Collections.Generic;
using FsCheck;
using System.Linq;
using CallbagsTests.TestUtils;

namespace CallbagsTests.Source
{
    [TestClass]
    public class TestIter
    {

        [TestMethod]
        public void Should_pull_items_in_order()
        {
            Prop.ForAll<string[]>(strings =>
            {
                var sent = new List<string>(strings);
                List<string> received = new List<string>();
                var source = Sources.from(sent);
                var sink = AssertSink<string>.Receives((output) =>
                {
                    received.Add(output);
                    return true;
                });
                source.Pipe(sink);
                return Enumerable.SequenceEqual(sent, received);
            }).QuickCheckThrowOnFailure();
        }
    }
}
