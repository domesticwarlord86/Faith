using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Faith.Tests
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
