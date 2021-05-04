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
        private UserInterface _userInterface;
        private IButton _timeButton;
        private IButton _powerButton;
        private IButton _startCancelButton;


        [SetUp]
        public void Setup()
        {
            //_userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton,);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}