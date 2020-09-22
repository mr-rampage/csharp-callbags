using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Callbag.Basics.Sink;
using Callbag.Basics.Test.TestUtils;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Callbag.Basics.Test.Sink
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
                PullableSource<string>
                    .Sends(sent)
                    .ForEach(output => received.Add(output));
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
            PushableSource<string>
                .Sends(sent, promise)
                .ForEach(output => received.Add(output));

            await promise.Task;

            Assert.IsTrue(sent.SequenceEqual(received));
        }
    }
}
