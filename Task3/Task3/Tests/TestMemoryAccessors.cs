using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Task3
{
    [TestFixture]    
    class TestMemoryAccessors
    {
        [Test]
        public void GetByName()
        {
            MemoryAccessors testMem = new MemoryAccessors();

            string testName = "Joe";
            Assert.AreEqual(testName, testMem.GetByName(testName));
            testName = "Peter";
            Assert.AreEqual(null, testMem.GetByName(testName));
        }
    }
}
