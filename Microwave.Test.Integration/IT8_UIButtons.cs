using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT8_UIButtons
    {
        private IDoor _door;
        private IOutput _output;
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private ITimer _timer;
        private ILight _light;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ICookController _cooker;
        private UserInterface _ui;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _output = Substitute.For<IOutput>();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _timer = new Timer();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _cooker = new CookController(_timer, _display, _powerTube);
            _ui = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cooker);
        }

        [Test]
        public void OnPowerPressed_PowerButtonTest_StateWasREADY()
        {
            _powerButton.Press();

            _output.Received().OutputLine("Display shows: 50 W");
        }

        [Test]
        public void OnPowerPressed_PowerButtonTest_StateWasSETPOWER()
        {
            _powerButton.Press();

            _powerButton.Press();

            _output.Received().OutputLine("Display shows: 100 W");
        }

        [Test]
        public void OnTimePressed_TimeButtonTest_StateWasSETPOWER()
        {
            _powerButton.Press();

            _timeButton.Press();

            _output.Received().OutputLine("Display shows: 01:00");
        }

        [Test]
        public void OnTimePressed_TimeButtonTest_StateWasSETTIME()
        {
            _powerButton.Press();
            _timeButton.Press();

            _timeButton.Press();

            _output.Received().OutputLine("Display shows: 02:00");
        }


        [Test]
        public void OnStartCancelPressed_StartCancelButtonTest_StateWasSETPOWER()
        {
            _powerButton.Press();

            _startCancelButton.Press();

            _output.Received().OutputLine("Display cleared");
        }

        [Test]
        public void OnStartCancelPressed_StartCancelButtonTest_StateWasSETTIME()
        {
            _powerButton.Press();
            _timeButton.Press();

            _startCancelButton.Press();

            _output.Received().OutputLine("Light is turned on");
        }


        [Test]
        public void OnStartCancelPressed_StartCancelButtonTest_StateWasCOOKING()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            
            _startCancelButton.Press();

            _output.Received().OutputLine("Light is turned off");
        }
    }
}