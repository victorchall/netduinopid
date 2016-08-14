using NetduinoPIDController.FeedbackControls;
using NUnit.Framework;

namespace NetduinoPIDController.Tests
{
    [TestFixture]
    public class ControlTests
    {
        [TestCase(0f)]
        [TestCase(1f)]
        [TestCase(10000f)]
        public void ProportionalControl_ScalesWithGain(float testGain)
        {
            var propControl = new ProportionalControl(testGain);

            var result = propControl.GetValue(1);

            Assert.That(result, Is.EqualTo(-testGain).Within(0.001f));
        }
    }
}