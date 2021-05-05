using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT7_UIButtons
    {
        private Door _door;
        private IOutput _output;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private ITimer _timer;
        private ILight _light;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ICookController _cooker;
        private IUserInterface _ui;

        [SetUp]
        public void Setup()
        {
            _door = new Door();
            _output = Substitute.For<IOutput>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _timer = new Timer();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _cooker = new CookController(_timer, _display, _powerTube);
            _ui = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cooker);
        }

        [Test]
        public void OnDoorOpened_DoorTest_StateWasREADY()
        {
            _door.Open();

            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void OnDoorOpened_DoorTest_StateWasSETPOWER()
        {
            _powerButton.Pressed += Raise.Event();
            _door.Open();

            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void OnDoorOpened_DoorTest_StateWasSETTIME()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _door.Open();

            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void OnDoorOpened_DoorTest_StateWasCOOKING()
        {
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _startCancelButton.Pressed += Raise.Event();
            _door.Open();

            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void OnDoorOpened_DoorTest_StateWasDOOROPEN()
        {
            _door.Open();
            _door.Close();

            _output.Received().OutputLine("Light is turned off");
        }
    }
}