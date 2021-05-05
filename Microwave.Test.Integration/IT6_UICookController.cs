using System.Threading;
using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using NSubstitute;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT6_UICookController
    {
        private IOutput _output;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private Timer _timer;
        private Light _light;
        private Display _display;
        private PowerTube _powerTube;
        private CookController _cooker;
        private UserInterface _ui;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _timer = new Timer();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _cooker = new CookController(_timer,_display,_powerTube);
            _ui = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cooker);
            _cooker.UI = _ui;
        }

        [Test]
        public void CookerStartCooking_CookerTest_StartCookingProcess()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();

            _output.Received().OutputLine("PowerTube works with 50");
        }

        [Test]
        public void CookerStartCooking_CookerTest_StopCookingWithButton()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();

            _output.Received().OutputLine("PowerTube turned off");
        }

        [Test]
        public void CookerStartCooking_CookerTest_StopCookingWithDoor()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            
            _output.Received().OutputLine("PowerTube turned off");
        }

        [Test]
        public void TimerExpired_UiTest_TellUiCookingIsDone()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            ManualResetEvent pause = new ManualResetEvent(false);
            pause.WaitOne(60100);

            _output.Received().OutputLine("Display cleared");
        }
    }
}