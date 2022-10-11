using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handymand.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Authenticate_WhenThereIsNoUser_ThrowException()
        {
            Assert.Pass();

        }
    }
}
