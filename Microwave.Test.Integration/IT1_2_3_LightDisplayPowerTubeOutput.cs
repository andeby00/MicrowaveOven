using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using NSubstitute;
using System;
using System.IO;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT1_2_3_LightDisplayPowerTubeOutput
    {
        private Output _output;
        private Light _light;
        private Display _display;
        private PowerTube _powerTube;


        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
        }

        #region IT1_LightOutput

        [Test]
        public void TurnOn_LightIsOff()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _light.TurnOn();

            string expectedResult = "Light is turned on\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
            
        }

        [Test]
        public void TurnOn_LightIsOn()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _light.TurnOn();

            _light.TurnOn();

            string expectedResult = "Light is turned on\r\n";
            Assert.That(output.ToString(), Is.LessThanOrEqualTo(expectedResult));
            //output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void TurnOff_LightIsOff()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _light.TurnOff();

            string expectedResult = "";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TurnOff_LightIsOn()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _light.TurnOn();

            _light.TurnOff();

            string expectedResult = "Light is turned off\r\n";
            Assert.That(output.ToString(), Contains.Substring(expectedResult));
        }

        #endregion

        #region IT2_DisplayOutput

        [Test]
        public void ShowTime_1Min1Sec()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _display.ShowTime(1,1);

            string expectedResult = "Display shows: 01:01\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
            
        }

        [Test]
        public void ShowPower_50W()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _display.ShowPower(50);

            string expectedResult = "Display shows: 50 W\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Clear_DisplayIsCleared()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _display.Clear();

            string expectedResult = "Display cleared\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        #endregion

        #region IT3_PowerTubeOutput

        [Test]
        public void TurnOn_CorrectPower()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _powerTube.TurnOn(50);

            string expectedResult = "PowerTube works with 50\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TurnOff_PowerTubeIsOff()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _powerTube.TurnOff();

            string expectedResult = "";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TurnOff_PowerTubeIsOn()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _powerTube.TurnOn(50);
            
            _powerTube.TurnOff();

            string expectedResult = "PowerTube turned off\r\n";
            Assert.That(output.ToString(), Contains.Substring(expectedResult));
            
        }

        #endregion
    }
}