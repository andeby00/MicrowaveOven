using System.Threading;
using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT4_CookControllerDisplayPowerTubeTimer
    {
        private IOutput _output;
        private IUserInterface _userInterface;
        private Timer _timer;
        private Display _display;
        private PowerTube _powerTube;
        private CookController _cooker;


        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _userInterface = Substitute.For<IUserInterface>();
            _timer = new Timer();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _cooker = new CookController(_timer, _display, _powerTube, _userInterface);
        }


        #region PowerTube

        [Test]
        public void StartCooking_PowerTubeTest_CorrectPower()
        {
            _cooker.StartCooking(50, 5);

            _output.Received().OutputLine("PowerTube works with 50");
        }

        [Test]
        public void StopCooking_PowerTubeTest_StartWithCorrectPower()
        {
            _cooker.StartCooking(50, 5);

            _cooker.Stop();

            _output.Received().OutputLine("PowerTube turned off");
        }

        #endregion

        #region Timer

        [Test]
        public void StartCooking_TimerTest_StartTimerWithCorrectTime()
        {
            _cooker.StartCooking(50, 5);

            Assert.That(_timer.TimeRemaining, Is.EqualTo(5));
        }

        [Test]
        public void StartCooking_TimerTest_2SecondsPasses()
        {
            _cooker.StartCooking(50, 2);

            ManualResetEvent pause = new ManualResetEvent(false);
            pause.WaitOne(2 * 1000 + 100);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(0));
        }

        [Test]
        public void StartCooking_TimerPowerTubeTest_PowerTubeTurnsOffOnExpire()
        {
            _cooker.StartCooking(50, 2);

            ManualResetEvent pause = new ManualResetEvent(false);
            pause.WaitOne(2100);
            _output.Received().OutputLine("PowerTube turned off");
        }

        #endregion

        #region Display

        [Test]
        public void StartCooking_DisplayTest_DisplaysCorrectCountDown()
        {
            _cooker.StartCooking(50, 2);

            ManualResetEvent pause = new ManualResetEvent(false);
            pause.WaitOne(2100);
            _output.Received().OutputLine("Display shows: 00:01"); 
            _output.Received().OutputLine("Display shows: 00:00"); 
        }

        #endregion
    }
}
