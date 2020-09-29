using NUnit.Framework;

namespace Faith.Tests
{
    [TestFixture]
    public class SampleTests
    {
        [Test]
        public void AlwaysSucceeds()
        {
            Assert.IsTrue(true, "Debug message displayed when tests fails.");
        }
    }
}
