using Microsoft.VisualStudio.TestTools.UnitTesting;
using Callbags.Sink;
using System.Collections.Generic;
using CallbagsTests.TestUtils;
using System.Threading.Tasks;
using FsCheck;
using System.Linq;

namespace CallbagsTests.Sink
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
                List<string> received = new List<string>();
                var source = PullableSource<string>.Sends(sent);
                var fixture = Sinks.forEach<string>((output) => received.Add(output));
                source.Pipe(fixture);
                return Enumerable.SequenceEqual(sent, received);
            }).QuickCheckThrowOnFailure();
        }

        // FSCheck 2 doesn't support async, wait for FSCheck 3
        [TestMethod]
        public async Task Should_process_all_in_order_from_pusables()
        {
            var sent = new List<string>(new string[] { "Fred", "Barney", "Wilma" });
            List<string> received = new List<string>();

            var promise = new TaskCompletionSource<bool>();
            var source = PushableSource<string>.Sends(sent, promise);
            var fixture = Sinks.forEach<string>((output) => received.Add(output));

            source.Pipe(fixture);

            await promise.Task;

            Assert.IsTrue(Enumerable.SequenceEqual(sent, received));
        }
    }
}
