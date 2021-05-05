using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT7_UIButtons
    {
        private Door _door;


        [SetUp]
        public void Setup()
        {
            _door = new Door();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}