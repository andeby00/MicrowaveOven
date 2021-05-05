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
        private IUserInterface _ui;

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
        public void Test1()
        {
        }
    }
}