using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallbagsTest.TestUtils;
using Callbags;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Callbags.Sink.Sink;

namespace CallbagsTest.Sink
{
    [TestClass]
    public class TestForEach
    {
        [TestMethod("Should process all pullables in order")]
        public void TestPullables()
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
        [TestMethod("Should process all pushables in order")]
        public async Task TestPushables()
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
