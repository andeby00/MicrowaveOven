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
        private IOutput _output;
        private IUserInterface _ui;
        private Light _light;
        private Display _display;
        private PowerTube _powerTube;
        private ICookController _cooker;
        private Button _button;
       
        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _ui = Substitute.For<IUserInterface>();
            _cooker = Substitute.For<ICookController>();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _button = new Button();
        }

        [Test]
        public void Test1()
        {
        }
    }
}