using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallbagsTest.TestUtils;
using Callbags;
using CallbagsTests.TestUtils;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Callbags.Sink.Sink;

namespace CallbagsTest.Sink
{
    [TestClass]
    public class TestForEach
    {
        [TestMethod]
        public void Should_process_all_in_order_from_pullables()
        {
            Prop.ForAll<string[]>(strings =>
            {
                var sent = new List<string>(strings);
                var received = new List<string>();
                var source = PullableSource<string>.Sends(sent);
                var fixture = ForEach<string>(output => received.Add(output));
                source.Pipe(fixture);
                return sent.SequenceEqual(received);
            }).QuickCheckThrowOnFailure();
        }

        // FSCheck 2 doesn't support async, wait for FSCheck 3
        [TestMethod]
        public async Task Should_process_all_in_order_from_pusables()
        {
            var sent = new List<string>(new[] { "Fred", "Barney", "Wilma" });
            var received = new List<string>();

            var promise = new TaskCompletionSource<bool>();
            var source = PushableSource<string>.Sends(sent, promise);
            var fixture = ForEach<string>((output) => received.Add(output));

            source.Pipe(fixture);

            await promise.Task;

            Assert.IsTrue(Enumerable.SequenceEqual(sent, received));
        }
    }
}
