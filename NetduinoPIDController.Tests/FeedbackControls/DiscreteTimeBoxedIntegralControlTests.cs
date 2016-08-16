using NetduinoPIDController.FeedbackControls;
using NUnit.Framework;

namespace NetduinoPIDController.Tests.FeedbackControls
{
    public class DiscreteTimeBoxedIntegralControlTests
    {
        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void DiscreteTimeBoxedIntegralControl_ScalesWithGain(float testGain)
        {
            var timeboxedIntegralControl = new DiscreteTimeBoxedIntegralControl(testGain, 1);

            var result = timeboxedIntegralControl.GetValue(1);

            Assert.That(result, Is.EqualTo(testGain).Within(TestHelpers.DesignPrecision));
        }

        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void DiscreteTimeBoxedIntegralControl_DoesNotAccumualteWhenHistoryLengthIsOne(float gain)
        {
            var timeboxedIntegralControl = new DiscreteTimeBoxedIntegralControl(gain, 1);

            var result1 = timeboxedIntegralControl.GetValue(1);
            var result2 = timeboxedIntegralControl.GetValue(1);
            var result3 = timeboxedIntegralControl.GetValue(1);

            Assert.AreEqual(gain, result1);
            Assert.AreEqual(gain, result2);
            Assert.AreEqual(gain, result3);
        }
        
        [Test]
        public void DiscreteTimeBoxedIntegralControl_AccumulatesOnlyCurrentAndCurrentMinusOneIfLengthIsTwo()
        {
            var timeboxedIntegralControl = new DiscreteTimeBoxedIntegralControl(1, 2);

            var result1 = timeboxedIntegralControl.GetValue(1);
            var result2 = timeboxedIntegralControl.GetValue(1);
            var result3 = timeboxedIntegralControl.GetValue(1);

            Assert.AreEqual(1, result1);
            Assert.AreEqual(2, result2);
            Assert.AreEqual(2, result3);
        }

        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void DiscreteTimeBoxedIntegralControl_AccumulatesOnlyToEndOfLength(float gain)
        {
            var timeboxedIntegralControl = new DiscreteTimeBoxedIntegralControl(gain, 5);

            var result1 = timeboxedIntegralControl.GetValue(1);
            var result2 = timeboxedIntegralControl.GetValue(1);
            var result3 = timeboxedIntegralControl.GetValue(1);
            var result4 = timeboxedIntegralControl.GetValue(1);
            var result5 = timeboxedIntegralControl.GetValue(1);
            var result6 = timeboxedIntegralControl.GetValue(1);

            Assert.AreEqual(gain, result1);
            Assert.AreEqual(gain + result1, result2);
            Assert.AreEqual(gain + result2, result3);
            Assert.AreEqual(gain + result3, result4);
            Assert.AreEqual(gain + result4, result5);
            Assert.AreEqual(result5, result6);
        }

        [TestCase(float.MaxValue)]
        [TestCase(float.MinValue)]
        public void DiscreteTimeBoxedIntegralControl_ClampsAccumulation(float gain)
        {
            var timeboxedIntegralControl = new DiscreteTimeBoxedIntegralControl(gain, 2);

            var result1 = timeboxedIntegralControl.GetValue(1);
            var result2 = timeboxedIntegralControl.GetValue(1);

            Assert.AreEqual(gain, result1);
            Assert.AreEqual(gain, result2);
        }
    }
}