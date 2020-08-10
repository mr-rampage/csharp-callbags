using Microsoft.VisualStudio.TestTools.UnitTesting;
using Callbags.Source;
using System.Collections.Generic;
using CallbagsTests.TestUtils;

namespace CallbagsTests.Source
{
    [TestClass]
    public class TestIter
    {

        [TestMethod]
        public void Should_Iterate_As_A_Pullable()
        {
            List<string> names = new List<string>(new string[] { "Fred", "Barney", "Wilma"});
            var fixture = Iter<string>.from(names);
            fixture.Pipe(AssertSink<string>.Receives(names));
        }
    }
}
