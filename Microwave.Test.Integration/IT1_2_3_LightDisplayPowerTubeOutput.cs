using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;

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

        [Test]
        public void LightOn_ToOutputTest()
        {
            // Arrange
            _light.TurnOn();

            // Act

            // Assert
            _output.Received().OutputLine("Light is turned on");
        }
    }
}