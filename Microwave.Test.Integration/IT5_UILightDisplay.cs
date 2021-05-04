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
        private ICookController _cooker;
        private Light _light;
        private Display _display;
        private PowerTube _powerTube;
        private UserInterface _ui;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _cooker = Substitute.For<ICookController>();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _ui = new UserInterface(_powerButton,_timeButton,_startCancelButton,_door,_display,_light,_cooker);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}