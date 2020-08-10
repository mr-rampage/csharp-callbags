using Microsoft.VisualStudio.TestTools.UnitTesting;
using Callbags.Source;
using Callbags.Sink;
using System.Collections.Generic;
using FsCheck;
using System.Linq;

namespace CallbagsTests.Source
{
    [TestClass]
    public class TestIter
    {

        [TestMethod]
        public void Should_send_and_process_in_order()
        {
            Prop.ForAll<string[]>(strings =>
            {
                var sent = new List<string>(strings);
                List<string> received = new List<string>();
                var source = Iter<string>.from(sent);
                var sink = ForEach<string>.forEach((output) => received.Add(output));
                source.Pipe(sink);
                return Enumerable.SequenceEqual(sent, received);
            }).QuickCheckThrowOnFailure();
        }
    }
}
