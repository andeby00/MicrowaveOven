using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT1_2_3_LightDisplayPowerTubeOutput
    {
        private IOutput _output;
        private Light _light;
        private Display _display;
        private PowerTube _powerTube;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
        }

        #region IT1_LightOutput

        [Test]
        public void TurnOn_LightIsOff()
        {
            _light.TurnOn();
            
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void TurnOn_LightIsOn()
        {
            _light.TurnOn();
            _light.TurnOn();
            
            _output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void TurnOff_LightIsOff()
        {
            _light.TurnOff();
            
            _output.DidNotReceive().OutputLine("Light is turned off");
        }

        [Test]
        public void TurnOff_LightIsOn()
        {
            _light.TurnOn();
            _light.TurnOff();
            
            _output.Received().OutputLine("Light is turned off");
        }

        #endregion

        #region IT2_DisplayOutput

        [Test]
        public void ShowTime_1Min1Sec()
        {
            _display.ShowTime(1,1);

            _output.Received().OutputLine("Display shows: 01:01");
        }

        [Test]
        public void ShowPower_50W()
        {
            _display.ShowPower(50);

            _output.Received().OutputLine("Display shows: 50 W");
        }

        [Test]
        public void Clear_DisplayIsCleared()
        {
            _display.Clear();

            _output.Received().OutputLine("Display cleared");
        }

        #endregion

        #region IT3_PowerTubeOutput

        [Test]
        public void TurnOn_CorrectPower()
        {
            _powerTube.TurnOn(50);

            _output.Received().OutputLine("PowerTube works with 50");
        }

        [Test]
        public void TurnOff_PowerTubeIsOff()
        {
            _powerTube.TurnOff();

            _output.DidNotReceive().OutputLine("PowerTube turned off");
        }

        [Test]
        public void TurnOff_PowerTubeIsOn()
        {
            _powerTube.TurnOn(50);
            _powerTube.TurnOff();

            _output.Received().OutputLine("PowerTube turned off");
        }

        #endregion
    }
}