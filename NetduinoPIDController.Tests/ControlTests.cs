using NetduinoPIDController.FeedbackControls;
using NUnit.Framework;

namespace NetduinoPIDController.Tests
{
    [TestFixture]
    public class ControlTests
    {
        [TestCase(0f)]
        [TestCase(0.00000000001f)]
        [TestCase(0.001f)]
        [TestCase(1f)]
        [TestCase(1000f)]
        [TestCase(1000000f)]
        [TestCase(99999999999999f)]
        public void ProportionalControl_ScalesWithGain(float testGain)
        {
            var propControl = new ProportionalControl(testGain);

            var result = propControl.GetValue(1);

            Assert.That(result, Is.EqualTo(testGain).Within(0.00001f));
        }

        [TestCase(0f)]
        [TestCase(0.00000000001f)]
        [TestCase(0.001f)]
        [TestCase(1f)]
        [TestCase(1000f)]
        [TestCase(1000000f)]
        [TestCase(99999999999999f)]
        public void DiscreteSummationControl_IncrementScalesWithGain(float testGain)
        {
            var propControl = new DiscreteSummationControl(testGain);

            var result = propControl.GetValue(1);
            
            Assert.That(result, Is.EqualTo(testGain).Within(0.001f));

            var result2 = propControl.GetValue(1);

            Assert.That(result2, Is.EqualTo(testGain*2).Within(0.001f));
        }

        [TestCase(0f)]
        [TestCase(0.00000000001f)]
        [TestCase(0.001f)]
        [TestCase(1f)]
        [TestCase(1000f)]
        [TestCase(1000000f)]
        [TestCase(99999999999999f)]
        public void DifferenceControl_IncrementScalesWithGain(float testGain)
        {
            var propControl = new DifferenceControl(testGain);

            propControl.GetValue(0);
            var result = propControl.GetValue(1);

            Assert.That(result, Is.EqualTo(testGain).Within(0.001f));
        }
    }
}