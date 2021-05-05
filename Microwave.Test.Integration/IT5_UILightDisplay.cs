using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT5_UILightDisplay
    {
        private IOutput _output;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private ICookController _cooker;
        private Light _light;
        private Display _display;
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
            _powerTube = new PowerTube(_output);
            _light = new Light(_output);
            _display = new Display(_output);
            _cooker = new CookController(_timer, _display, _powerTube);
            _ui = new UserInterface(_powerButton,_timeButton,_startCancelButton,_door,_display,_light,_cooker);
        }

        [Test]
        public void OnPowerPressed_DisplayTest_StateREADY_displayPower()
        {
            _powerButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 50 W")));
        }

        [Test]
        public void OnPowerPressed_DisplayTest_StateSETPOWER_displayPower()
        {
            _powerButton.Pressed += Raise.Event();

            _powerButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 100 W")));
        }

        [Test]
        public void OnTimePressed_DisplayTest_StateSETPOWER_displayTime()
        {
            _powerButton.Pressed += Raise.Event();

            _timeButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 01:00")));
        }

        [Test]
        public void OnTimePressed_DisplayTest_StateSETTIME_displayTime()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();

            _timeButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 02:00")));
        }


        [Test]
        public void OnStartCancelPressed_DisplayTest_StateSETPOWER_displayClear()
        {
            _powerButton.Pressed += Raise.Event();

            _startCancelButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }

        [Test]
        public void OnStartCancelPressed_LightTest_StateSETTIME_lightOn()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();

            _startCancelButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void OnStartCancelPressed_DisplayTest_StateCOOKING_displayClear()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();

            _startCancelButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
           
        }

        [Test]
        public void OnStartCancelPressed_LightTest_StateCOOKING_LightOff()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();

            _startCancelButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        [Test]
        public void OnDoorOpened_LightTest_StateREADY_lightOn()
        {
            _door.Opened += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void OnDoorOpened_LightTest_StateSETPOWER_lightOn()
        {
            _powerButton.Pressed += Raise.Event();

            _door.Opened += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void OnDoorOpened_DisplayTest_StateSETPOWER_displayClear()
        {
            _powerButton.Pressed += Raise.Event();

            _door.Opened += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }

        [Test]
        public void OnDoorOpened_LightTest_StateSETTIME_lightOn()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();

            _door.Opened += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void OnDoorOpened_DisplayTest_StateSETTIME_displayClear()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();

            _door.Opened += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }

        [Test]
        public void OnDoorOpened_DisplayTest_StateCOOKING_displayClear()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();

            _door.Opened += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }

        [Test]
        public void OnDoorClosed_LightTest_StateDOOROPEN_lightOff()
        {
            _door.Opened += Raise.Event();

            _door.Closed += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        [Test]
        public void CookingIsDone_LightTest_StateCOOKING_lightOff()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();

            _ui.CookingIsDone();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        [Test]
        public void CookingIsDone_DisplayTest_StateCOOKING_displayClear()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();

            _ui.CookingIsDone();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }
    }
}