using Microsoft.VisualStudio.TestTools.UnitTesting;
using Callbags.Sink;
using System.Collections.Generic;
using CallbagsTests.TestUtils;
using System.Threading.Tasks;

namespace CallbagsTests.Sink
{
    [TestClass]
    public class TestForEach
    {
        [TestMethod]
        public void Should_Process_Each_Item_From_A_Pullable()
        {
            List<string> names = new List<string>(new string[] { "Fred", "Barney", "Wilma" });
            int index = 0;

            var source = PullableSource<string>.Sends(names);
            var fixture = ForEach<string>.forEach((output) => Assert.AreEqual(names[index++], output));

            source.Pipe(fixture);
            Assert.AreEqual(names.Capacity, index);
        }

        [TestMethod]
        public async Task Should_Process_Each_Item_From_A_Pushable()
        {
            List<string> names = new List<string>(new string[] { "Fred", "Barney", "Wilma" });
            int index = 0;

            var promise = new TaskCompletionSource<bool>();
            var source = PushableSource<string>.Sends(names, promise);
            var fixture = ForEach<string>.forEach((output) => Assert.AreEqual(names[index++], output));

            source.Pipe(fixture);
            await promise.Task;

            Assert.AreEqual(names.Capacity, index);
        }
    }
}
