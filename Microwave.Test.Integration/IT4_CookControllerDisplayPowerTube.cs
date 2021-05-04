using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT4_CookControllerDisplayPowerTube
    {
        private IOutput _output;
        private ITimer _timer;
        private IUserInterface _userInterface;
        private CookController _cooker;
        private Display _display;
        private PowerTube _powerTube;


        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<Timer>();
            _userInterface = Substitute.For<IUserInterface>();
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _cooker = new CookController(_timer, _display, _powerTube, _userInterface);
        }


        [Test]
        public void StartCooking_PowerTubeTest_CorrectPower()
        {
            _cooker.StartCooking(50, 5);


            //_output.Received().OutputLine("");

            _output.Received().OutputLine("PowerTube works with 50");  

        }

        //[Test]
        //public void StartCooking_PowerTubeTest_InvalidPower()
        //{
        //    _cookController.StartCooking(102, 5);


        //    //_output.Received().OutputLine("");

        //    _output.Received().OutputLine("PowerTube works with 102");

        //}

        [Test]
        public void StopCooking_PowerTubeTest_StartWithCorrectPower()
        {
            _cookController.StartCooking(50, 5);
            _cookController.Stop();


            //_output.Received().OutputLine("");

            _output.Received().OutputLine("PowerTube turned off");

        }


       
    }
}
