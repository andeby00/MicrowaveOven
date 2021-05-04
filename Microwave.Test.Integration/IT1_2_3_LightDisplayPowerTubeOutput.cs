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
        private IOutput output;
        private ILight light;
        private IDisplay display;
        private IPowerTube powerTube;


        [SetUp]
        public void Setup()
        {
            output = new Output();
            light = new Light(output);
            display = new Display(output);
            powerTube = new PowerTube(output);
        }

        #region IT1_LightOutput

        [Test]
        public void TurnOn_LightIsOff()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            light.TurnOn();

            string expectedResult = "Light is turned on\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
            
        }

        [Test]
        public void TurnOn_LightIsOn()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            light.TurnOn();
            light.TurnOn();
            //?

            string expectedResult = "Light is turned on\r\n";
            Assert.That(output.ToString(), Is.LessThanOrEqualTo(expectedResult));
            //output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void TurnOff_LightIsOff()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            light.TurnOff();

            string expectedResult = "";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TurnOff_LightIsOn()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            light.TurnOn();

            light.TurnOff();

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

            display.ShowTime(1,1);

            string expectedResult = "Display shows: 01:01\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
            
        }

        [Test]
        public void ShowPower_50W()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            display.ShowPower(50);

            string expectedResult = "Display shows: 50 W\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Clear_DisplayIsCleared()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            display.Clear();

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

            powerTube.TurnOn(50);

            string expectedResult = "PowerTube works with 50\r\n";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TurnOff_PowerTubeIsOff()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            powerTube.TurnOff();

            string expectedResult = "";
            Assert.That(output.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TurnOff_PowerTubeIsOn()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            powerTube.TurnOn(50);
            
            powerTube.TurnOff();

            string expectedResult = "PowerTube turned off\r\n";
            Assert.That(output.ToString(), Contains.Substring(expectedResult));
            
        }

        #endregion
    }
}